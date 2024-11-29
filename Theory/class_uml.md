@startuml
class Пользователь {
    +user_id: int
    +contact_details: string
    +checkDebtStatus(): bool
    +receiveBookReminder(): void
    +receiveBookNotification(): void
    +reportOverdue(): void
    +reportDamage(): void
}

class Библиотекарь {
    +employee_id: int
    +checkBookCondition(): void
    +updateBookStatus(book_id: int, status: string): void
    +calculateOverdueFine(days_overdue: int): float
    +reportFine(fine_amount: float): void
    +reportDamageFine(fine_amount: float): void
    +createReservation(user_id: int, book_id: int): void
    +searchBook(params_dict: Map): List
    +checkUserDebts(user_id: int): bool
    +handleBookReturn(book_id: int): void
    +chooseOption(option_number: int): void
}

class Книга {
    +book_id: int
    +title: string
    +author: string
    +status: string  // "доступна", "выдана", "повреждена", "на ремонте"
    +isAvailable(): bool
    +getReturnDate(): Date
}

class Система {
    +books: List<Книга>
    +users: List<Пользователь>
    +reservations: List<Reservation>
    +findBooks(params_dict: Map): List<Книга>
    +sendReminder(user_id: int, message: string): void
    +checkBookAvailability(book_id: int): bool
    +addToReservationQueue(user_id: int, reservation_id: int): void
    +updateBookStatus(book_id: int, status: string): void
    +addFineToUser(user_id: int, fine_amount: float): void
    +calculateDamageFine(book_id: int): float
}

class Reservation {
    +reservation_id: int
    +user_id: int
    +book_id: int
    +status: string  // "ожидает", "выдана"
    +reservationDate: Date
}

Пользователь --> Система : searchBook(params_dict)
Библиотекарь --> Система : checkBookAvailability(book_id)
Библиотекарь --> Система : checkUserDebts(user_id)
Библиотекарь --> Система : findBooks(params_dict)
Система --> Библиотекарь : displayBooksList()
Система --> Библиотекарь : displayBookInfo(book_id)
Библиотекарь --> Система : updateBookStatus(book_id, status)
Система --> Библиотекарь : bookStatusUpdated
Система --> Библиотекарь : fineAmountCalculated
Система --> Библиотекарь : reservationDetails
Система --> Библиотекарь : sendReminder(user_id, message)
@enduml
