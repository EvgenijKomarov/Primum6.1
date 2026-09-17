import re
from enum import Enum
from pathlib import Path
from string import Template
 
TEMPLATES_DIR = Path(__file__).parent / "templates"
 
_TOKEN_RE = re.compile(r"token=([^\s&]+)")
_LINK_RE = re.compile(r"(https?://\S+)")
 
 
class EmailTemplate(str, Enum):
    INFO = "InfoEmail"
    CONFIRMATION = "ConfirmationEmail"
 
 
def _load_template(template: EmailTemplate) -> Template:
    path = TEMPLATES_DIR / f"{template.value}.html"
    return Template(path.read_text(encoding="utf-8"))
 
 
def _parse_confirmation_message(message: str) -> tuple[str, str]:
    link_match = _LINK_RE.search(message)
    token_match = _TOKEN_RE.search(message)
 
    if token_match:
        token = token_match.group(1)
    else:
        # запасной вариант: последняя непустая строка сообщения
        lines = [line.strip() for line in message.splitlines() if line.strip()]
        token = lines[-1] if lines else ""
 
    link = link_match.group(1) if link_match else "#"
    return link, token
 
 
def render_info_email(subject: str, body: str) -> str:
    template = _load_template(EmailTemplate.INFO)
    body_html = body.replace("\n", "<br>")
    return template.safe_substitute(subject=subject, body_html=body_html)
 
 
def render_confirmation_email(message: str) -> str:
    template = _load_template(EmailTemplate.CONFIRMATION)
    link, token = _parse_confirmation_message(message)
    return template.safe_substitute(link=link, token=token)
 
 
_RENDERERS = {
    EmailTemplate.INFO: lambda subject, body: render_info_email(subject, body),
    EmailTemplate.CONFIRMATION: lambda subject, body: render_confirmation_email(body),
}
 
 
def render_email(template: EmailTemplate, subject: str, body: str) -> str:
    """Рендерит письмо по явно указанному шаблону."""
    return _RENDERERS[template](subject, body)