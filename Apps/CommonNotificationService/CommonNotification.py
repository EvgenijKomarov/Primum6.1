from pydantic import BaseModel
from datetime import datetime

class CommonNotification(BaseModel):
    userId: int
    seen: bool
    text: str
    datetime: datetime




