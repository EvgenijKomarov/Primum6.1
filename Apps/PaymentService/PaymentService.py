from decimal import Decimal
import os
import uvicorn
from pymongo import MongoClient, ASCENDING
from PaymentProcessor.FakePaymentProcessor import FakePaymentProcessor
from fastapi import FastAPI
import logging
import sys
 
MONGO_URI = os.getenv("MONGO_URI")
DB_NAME = "paymentservice"
 
is_prod_mode = os.getenv("MODE", "Development") == "Production"
 
app = FastAPI(title="PaymentService")
 
processor = FakePaymentProcessor()

logging.basicConfig(
    level=logging.INFO if is_prod_mode else logging.DEBUG,
    format="%(asctime)s | %(levelname)-8s | %(name)s | %(message)s",
    handlers=[logging.StreamHandler(sys.stdout)],
)
logger = logging.getLogger("PaymentService")
 
# --- MongoDB init ---------------------------------------------------------
# Сервис - единственный владелец этой базы, поэтому инициализацию
# (подключение + индекс) делаем один раз при старте модуля.
mongo_client = MongoClient(MONGO_URI)
db = mongo_client[DB_NAME]
teachers_collection = db["teachers"]
# teacherUserId - логический идентификатор учителя, должен быть уникальным
teachers_collection.create_index([("teacherUserId", ASCENDING)], unique=True)
logger.info("Connected to MongoDB, db=%s", DB_NAME)
# ---------------------------------------------------------------------------
 
 
@app.post("/register-teacher")
def register_teacher(teacherUserId: int, fullName: str, inn: str, phone: str,
                      accountNumber: str, bankBik: str):
    logger.info("register_teacher called: teacherUserId=%s", teacherUserId)
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
    except Exception:
        logger.exception("register_teacher failed: teacherUserId=%s", teacherUserId)
        return False
    logger.info("register_teacher success: teacherUserId=%s", teacherUserId)
    return True
 
 
@app.post("/enrole-teacher-registration/{teacherUserId}")
def enrole_teacher_registration(teacherUserId: int):
    logger.info("enrole_teacher_registration called: teacherUserId=%s", teacherUserId)
    try:
        teacher = teachers_collection.find_one({"teacherUserId": teacherUserId})
        if teacher is None:
            logger.warning(
                "enrole_teacher_registration: teacher not found, teacherUserId=%s",
                teacherUserId,
            )
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
    except Exception:
        logger.exception(
            "enrole_teacher_registration failed: teacherUserId=%s", teacherUserId
        )
        return False
    logger.info(
        "enrole_teacher_registration finished: teacherUserId=%s success=%s",
        teacherUserId, resp["success"],
    )
    return resp["success"]
 
 
@app.post("/request-topup-student-balance")
def request_topup(userId: int, amount: Decimal):
    logger.info("request_topup called: userId=%s amount=%s", userId, amount)
    try:
        url = processor.request_topup(userId, amount)
    except Exception:
        logger.exception(
            "request_topup failed: userId=%s amount=%s", userId, amount
        )
        return ""
    logger.info("request_topup success: userId=%s amount=%s", userId, amount)
    return url
 
 
@app.post("/withdrawn-student-balance")
def withdrawn_student_balance(userId: int, amount: Decimal):
    logger.info(
        "withdrawn_student_balance called: userId=%s amount=%s", userId, amount
    )
    try:
        resp = processor.withdrawn_student_balance(userId, amount)
    except Exception:
        logger.exception(
            "withdrawn_student_balance failed: userId=%s amount=%s", userId, amount
        )
        return False
    logger.info(
        "withdrawn_student_balance finished: userId=%s amount=%s success=%s",
        userId, amount, resp["success"],
    )
    return resp["success"]
 
 
@app.get("/is-teacher-ready/{teacherUserId}")
def is_teacher_ready(teacherUserId: int):
    logger.info("is_teacher_ready called: teacherUserId=%s", teacherUserId)
    try:
        teacher = teachers_collection.find_one({"teacherUserId": teacherUserId}, {"_id": 0})
        resp = processor.check_teacher(teacher["inn"], teacher["phone"], teacher["recipientRef"])
    except Exception:
        logger.exception("is_teacher_ready failed: teacherUserId=%s", teacherUserId)
        return False
    logger.info(
        "is_teacher_ready finished: teacherUserId=%s success=%s",
        teacherUserId, resp["success"],
    )
    return resp["success"]
 
 
@app.post("/process-lesson-payment")
def process_lesson_payment(lessonId: int, studentUserId: int, teacherUserId: int, teacherCash: Decimal, platformCash: Decimal):
    logger.info(
        "process_lesson_payment called: lessonId=%s studentUserId=%s teacherUserId=%s "
        "teacherCash=%s platformCash=%s",
        lessonId, studentUserId, teacherUserId, teacherCash, platformCash,
    )
    try:
        processor.process_lesson(teacherUserId, teacherCash, platformCash)
    except Exception:
        logger.exception(
            "process_lesson_payment failed: lessonId=%s studentUserId=%s teacherUserId=%s",
            lessonId, studentUserId, teacherUserId,
        )
        return False
    logger.info("process_lesson_payment success: lessonId=%s", lessonId)
    return True
 
 
@app.get("/get-student-balance/{studentUserId}")
def get_student_balance(studentUserId: int):
    logger.info("get_student_balance called: studentUserId=%s", studentUserId)
    try:
        amount = processor.get_student_balance(studentUserId)
    except Exception:
        logger.exception("get_student_balance failed: studentUserId=%s", studentUserId)
        return None
    logger.info(
        "get_student_balance finished: studentUserId=%s amount=%s",
        studentUserId, amount,
    )
    return amount
 
 
if __name__ == "__main__":
    print("Starting server initialization...")
 
    uvicorn.run(
        f"PaymentService:app",
        host="0.0.0.0",
        port=5000,
        log_level="info"
    )