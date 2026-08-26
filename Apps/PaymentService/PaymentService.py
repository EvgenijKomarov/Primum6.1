from decimal import Decimal
import os
import uvicorn
from PaymentProcessor.FakePaymentProcessor import FakePaymentProcessor
from get_url import load_url, load_host_and_port
from fastapi import FastAPI


is_prod_mode = os.getenv("MODE", "Development") == "Production"

app = FastAPI(title="PaymentService")

processor = FakePaymentProcessor()

@app.post("/register-teacher")
def request_topup(teacherUserId: int, fullName: str, inn: str, phone: str,
                           accountNumber: str, bankBik: str):
    try:
       resp = processor.register_teacher(fullName, inn, phone, accountNumber, bankBik)
    except Exception as e:
        return False
    return resp["success"]

@app.post("/enrole-teacher-registration/{teacherUserId}")
def enrole_teacher_registration(teacherUserId: int):
    return True

@app.post("/request-topup-student-balance")
def request_topup(userId: int, amount: Decimal):
    try:
        url = processor.request_topup(userId, amount)
    except Exception as e:
        return {"success": False, "error": str(e)}
    return {"success": True, "url": url}

@app.post("/withdrawn-student-balance")
def withdrawn_student_balance(userId: int, amount: Decimal):
    try:
        processor.withdrawn_student_balance(userId, amount)
    except Exception as e:
        return {"success": False, "error": str(e)}
    return {"success": True}

@app.get("/is-teacher-ready/{studentUserId}")
def is_teacher_ready(userId: int):
    try:
        value = processor.check_teacher("", "", "")
    except Exception as e:
        return False
    return value["success"] == True

@app.post("/process-lesson-payment")
def process_lesson_payment(lessonId: int, studentUserId: int, teacherUserId: int, teacherCash: Decimal, platformCash: Decimal):
    try:
        processor.process_lesson(teacherUserId, teacherCash, platformCash)
    except Exception as e:
        return {"success": False, "error": str(e)}
    return {"success": True}

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