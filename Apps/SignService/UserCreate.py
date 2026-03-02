from pydantic import BaseModel

class UserCreate(BaseModel):
    userId: int
    realizationTag: str
    chatId: int
    username: str




