import html
from dataclasses import dataclass
from enum import Enum
from functools import cache
from pathlib import Path
from string import Template
from urllib.parse import urlparse

TEMPLATES_DIR = Path(__file__).parent / "templates"


class EmailTemplate(str, Enum):
    INFO = "InfoEmail"
    CONFIRMATION = "ConfirmationEmail"
    LESSONREADY = "LessonReadyEmail"


class TemplateDataError(ValueError):
    """Некорректные данные для шаблона (нет ключа, плохая ссылка)."""


@dataclass(frozen=True)
class TemplateSpec:
    required: frozenset[str]                 # обязательные ключи в data
    text: Template                           # текстовый фолбэк
    urls: frozenset[str] = frozenset()       # ключи, которые должны быть http(s)-ссылками
    multiline: frozenset[str] = frozenset()  # для ключа k в шаблоне доступен $k_html (\n -> <br>)


_SPECS: dict[EmailTemplate, TemplateSpec] = {
    EmailTemplate.INFO: TemplateSpec(
        required=frozenset({"body"}),
        text=Template("$body"),
        multiline=frozenset({"body"}),
    ),
    EmailTemplate.CONFIRMATION: TemplateSpec(
        required=frozenset({"link", "token"}),
        text=Template("Подтверждение почты PrimumCode\n\nСсылка: $link\nКод: $token"),
        urls=frozenset({"link"}),
    ),
    EmailTemplate.LESSONREADY: TemplateSpec(
        required=frozenset({"body", "link"}),
        text=Template("$body\n\nПерейти: $link"),
        urls=frozenset({"link"}),
        multiline=frozenset({"body"}),
    ),
}


@dataclass(frozen=True)
class RenderedEmail:
    text: str
    html: str


@cache
def _load_template(template: EmailTemplate) -> Template:
    path = TEMPLATES_DIR / f"{template.value}.html"
    return Template(path.read_text(encoding="utf-8"))


def _validate(spec: TemplateSpec, data: dict[str, str]) -> None:
    missing = sorted(k for k in spec.required if not data.get(k))
    if missing:
        raise TemplateDataError(f"missing required keys: {', '.join(missing)}")

    for key in spec.urls:
        if urlparse(data[key]).scheme not in ("http", "https"):
            raise TemplateDataError(f"'{key}' must be an http(s) URL")


def render_email(template: EmailTemplate, subject: str, data: dict[str, str]) -> RenderedEmail:
    spec = _SPECS[template]
    _validate(spec, data)

    html_ctx = {k: html.escape(v) for k, v in data.items()}
    for key in spec.multiline:
        html_ctx[f"{key}_html"] = (
            html.escape(data[key]).replace("\r\n", "\n").replace("\n", "<br>")
        )
    html_ctx["subject"] = html.escape(subject)

    text_ctx = {**data, "subject": subject}

    return RenderedEmail(
        text=spec.text.safe_substitute(text_ctx),
        html=_load_template(template).safe_substitute(html_ctx),
    )