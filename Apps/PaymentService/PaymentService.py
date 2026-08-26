from decimal import Decimal
import os
import uvicorn
from pymongo import MongoClient, ASCENDING
from PaymentProcessor.FakePaymentProcessor import FakePaymentProcessor
from get_url import load_host_and_port, load_variable
from fastapi import FastAPI
 
MONGO_URI = load_variable("MongoDBUrl")
DB_NAME = "paymentservice"
 
is_prod_mode = os.getenv("MODE", "Development") == "Production"
 
app = FastAPI(title="PaymentService")
 
processor = FakePaymentProcessor()
 
# --- MongoDB init ---------------------------------------------------------
# Сервис - единственный владелец этой базы, поэтому инициализацию
# (подключение + индекс) делаем один раз при старте модуля.
mongo_client = MongoClient(MONGO_URI)
db = mongo_client[DB_NAME]
teachers_collection = db["teachers"]
# teacherUserId - логический идентификатор учителя, должен быть уникальным
teachers_collection.create_index([("teacherUserId", ASCENDING)], unique=True)
# ---------------------------------------------------------------------------
 
 
@app.post("/register-teacher")
def register_teacher(teacherUserId: int, fullName: str, inn: str, phone: str,
                      accountNumber: str, bankBik: str):
    try:
        teachers_collection.update_one(
                {"teacherUserId": teacherUserId},
                {"$set": {
                    "teacherUserId": teacherUserId,
                    "fullName": fullName,
                    "inn": inn,
                    "phone": phone,
                    "accountNumber": accountNumber,
                    "bankBik": bankBik,
                }},
                upsert=True,
            )
    except Exception as e:
        return False
    return True
 
 
@app.post("/enrole-teacher-registration/{teacherUserId}")
def enrole_teacher_registration(teacherUserId: int):
    try:
        teacher = teachers_collection.find_one({"teacherUserId": teacherUserId})
        if teacher is None:
            return False

        resp = processor.register_teacher(
            teacher["fullName"],
            teacher["inn"],
            teacher["phone"],
            teacher["accountNumber"],
            teacher["bankBik"],
        )
 
        if resp["success"]:
            teachers_collection.update_one(
                {"teacherUserId": teacherUserId},
                {"$set": {"recipientRef": resp["recipientRef"]}},
            )
    except Exception as e:
        return False
    return resp["success"]
 
 
@app.post("/request-topup-student-balance")
def request_topup(userId: int, amount: Decimal):
    try:
        url = processor.request_topup(userId, amount)
    except Exception as e:
        return ""
    return url
 

@app.post("/withdrawn-student-balance")
def withdrawn_student_balance(userId: int, amount: Decimal):
    try:
        resp = processor.withdrawn_student_balance(userId, amount)
    except Exception as e:
        return False
    return resp["success"]
 
 
@app.get("/is-teacher-ready/{teacherUserId}")
def is_teacher_ready(teacherUserId: int):
    try:
        teacher = teachers_collection.find_one({"teacherUserId": teacherUserId}, {"_id": 0})
        resp = processor.check_teacher(teacher["inn"], teacher["phone"], teacher["recipientRef"])
    except Exception as e:
        return False
    return resp["success"]
 
 
@app.post("/process-lesson-payment")
def process_lesson_payment(lessonId: int, studentUserId: int, teacherUserId: int, teacherCash: Decimal, platformCash: Decimal):
    try:
        processor.process_lesson(teacherUserId, teacherCash, platformCash)
    except Exception as e:
        return False
    return True
 
 
@app.get("/get-student-balance/{studentUserId}")
def get_student_balance(studentUserId: int):
    try:
        amount = processor.get_student_balance(studentUserId)
    except Exception as e:
        return None
    return amount
 
 
if __name__ == "__main__":
    print("Starting server initialization...")
 
    HOST, PORT = load_host_and_port("PaymentService/SelfUrl")
    print(f"Binding to http://{HOST}:{PORT}")
 
    uvicorn.run(
        f"PaymentService:app",
        host=HOST,
        port=PORT,
        log_level="info"
    )