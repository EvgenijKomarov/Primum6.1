from decimal import Decimal
import os
from tkinter import E
import uvicorn
from PaymentProcessor import FakePaymentProcessor
from get_url import load_host_and_port
from fastapi import FastAPI
from CorePaymentIterator import CorePaymentIterator


is_prod_mode = os.getenv("MODE", "Development") == "Production"

app = FastAPI(title="PaymentService")

processor = FakePaymentProcessor()
core_payment_iterator = CorePaymentIterator(load_host_and_port("CoreService/SelfUrl"))

@app.post("/force/topup-student-balance")
def force_topup_student_balance(userId: int, amount: Decimal):
    try:
        core_payment_iterator.add_cash(userId, amount)
    except Exception as e:
        return {"success": False, "error": str(e)}
    return {"success": True}

@app.post("/request-topup-student-balance")
def add_student_balance(userId: int, amount: Decimal):
    try:
        url = processor.topup_student_balance(userId, amount)
    except Exception as e:
        return {"success": False, "error": str(e)}
    return {"success": True, "url": url}

@app.post("/withdrawn-student-balance")
def withdrawn_student_balance(userId: int, amount: Decimal):
    try:
        processor.withdrawn_student_balance(userId, amount)
        core_payment_iterator.add_cash(userId, amount)
    except Exception as e:
        return {"success": False, "error": str(e)}
    return {"success": True}

@app.post("/process-lesson-payment")
def process_lesson_payment(teacherUserId: int, teacherCash: Decimal, platformCash: Decimal):
    try:
        processor.process_lesson(teacherUserId, teacherCash, platformCash)
    except Exception as e:
        return {"success": False, "error": str(e)}
    return {"success": True}

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