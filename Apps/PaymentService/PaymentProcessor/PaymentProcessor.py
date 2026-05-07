from abc import ABC, abstractmethod
from decimal import Decimal

class PaymentProcessor(object):
    @abstractmethod
    def add_student_balance(self, userId: int, amount: Decimal): ...

    @abstractmethod
    def withdrawn_student_balance(self, userId: int, amount: Decimal): ...

    @abstractmethod
    def process_lesson(self, id: int): ...


