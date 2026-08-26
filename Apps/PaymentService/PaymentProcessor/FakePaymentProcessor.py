from decimal import Decimal


class FakePaymentProcessor(object):

    def register_teacher(self, fullName: str, inn: str, phone: str,
                           accountNumber: str, bankBic: str) -> dict:
        print(f"Fakely registered teacher {fullName}")
        return {"success": True,"recipientRef": phone} #номер номинально выступает как внутренний айди эквайринга

    def check_teacher(self, inn: str, phone: str, recipientRef: str) -> dict:
        print(f"Fakely checked teacher")
        return {"success": True}

    #def parse_topup_webhook(self, rawBody: dict) -> dict:
    #    print("Fakely parsed topup webhook")
    #    return {"success": True}

    def withdrawn_student_balance(self, userId: int, amount: Decimal):
        print(f"Fakely withdrawned {amount} RUB to user {userId}")

    def process_lesson(self, teacherUserId: int, teacherCash: Decimal, platformCash: Decimal):
        print(f"Fakely sent {teacherCash} RUB to teacher {teacherUserId}")
        print(f"Fakely sent {platformCash} RUB to platform")

    def get_student_balance(self, refId: str):
        print(f"Fakely get balance of student {refId}")
        return 9999