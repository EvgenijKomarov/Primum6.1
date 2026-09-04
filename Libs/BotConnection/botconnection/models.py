from dataclasses import dataclass, field
from typing import Dict, Any

@dataclass
class Sign:
    realization_tag: str
    chat_id: int
    username: str

    def to_dict(self) -> Dict[str, Any]:
        return {
            "realizationTag": self.realization_tag,
            "chatId": self.chat_id,
            "username": self.username,
        }

@dataclass
class Request:
    sign: Sign
    data: str

    def to_dict(self) -> Dict[str, Any]:
        return {
            "sign": self.sign.to_dict(),
            "data": self.data,
        }

@dataclass
class Response:
    message: str
    buttons: Dict[str, str] = field(default_factory=dict)

    @classmethod
    def from_dict(cls, data: Dict[str, Any]) -> "Response":
        return cls(
            message=data.get("message", ""),
            buttons=data.get("buttons", {}) or {},
        )