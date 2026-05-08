from decimal import Decimal


class FakePaymentProcessor(object):

    def topup_student_balance(self, userId: int, amount: Decimal):
        print(f"Fakely requested topup of {amount} RUB to user {userId}")
        return "http://fake-payment.com/form"

    def withdrawn_student_balance(self, userId: int, amount: Decimal):
        print(f"Fakely withdrawned {amount} RUB to user {userId}")

    def process_lesson(self, teacherUserId: int, teacherCash: Decimal, platformCash: Decimal):
        print(f"Fakely sent {teacherCash} RUB to teacher {teacherUserId}")
        print(f"Fakely sent {platformCash} RUB to platform")



