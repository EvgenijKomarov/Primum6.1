import os
import uvicorn
from get_url import load_host_and_port
from fastapi import FastAPI


is_prod_mode = os.getenv("MODE", "Development") == "Production"

app = FastAPI(title="PaymentService")

@app.post("/add-student-balance")
def add_student_balance(userId: int, amount: int):
    return;

@app.post("/withdrawn-student-balance")
def withdrawn_student_balance(userId, amount: int):
    return;

@app.post("/process-lesson-payment")
def process_lesson_payment(userId):
    return;

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