from decimal import Decimal
import requests

class CorePaymentIterator(object):
    def __init__(self, core_url: str):
        self.core_url = core_url

    def add_cash(self, userId: int, amount: Decimal):
        print(f"Adding {amount} RUB to user {userId}")

        response = requests.post(f"{self.core_url}/api/student/{userId}/add-student-balance", params={"amount": amount})
        if response.status_code != 200:
            raise Exception(f"Failed to add cash: {response.text}")
