from decimal import Decimal
from PaymentProcessor import PaymentProcessor


class FakePaymentProcessor(PaymentProcessor):
    def __init__(self, payment_iterator):
        self.payment_iterator = payment_iterator

    def add_student_balance(self, userId: int, amount: Decimal):
        print(f"Start adding {amount} RUB to user {userId}")
        try:
            self.payment_iterator.add_cash(userId, amount)
            print(f"Successfully added {amount} RUB to user {userId}")
        except Exception as e:
            print(f"Failed to add {amount} RUB to user {userId}: {str(e)}")

    def withdrawn_student_balance(self, userId: int, amount: Decimal):
        print(f"Start withdrawning {amount} RUB to user {userId}")
        try:
            self.payment_iterator.add_cash(userId, -amount)
            print(f"Successfully withdrawn {amount} RUB to user {userId}")
        except Exception as e:
            print(f"Failed to withdrawn {amount} RUB to user {userId}: {str(e)}")

    def process_lesson(self, studentUserId: int, ): ...




