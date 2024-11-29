@startuml
class Пользователь {
    +user_id : int
    +contact_details : string
    +checkDebts() : bool
    +receiveNotification() : void
    +chooseAlternative() : void
    +reserveBook(book_id) : void
    +returnBook(book_id) : void
    +payFine(fine_amount) : void
}

class Библиотекарь {
    +librarian_id : int
    +checkBookCondition(book_id) : void
    +checkReturnDate(book_id, user_id) : void
    +calculateOverdueFine(days_overdue) : float
    +calculateDamageFine(book_id) : float
    +updateBookStatus(book_id, status) : void
    +reportOverdue(user_id, fine_amount) : void
    +reportDamage(user_id, fine_amount) : void
    +reportFine(user_id, fine_amount) : void
    +reportDebt(user_id) : void
    +offerDebtRepay(user_id) : void
    +createReservation(user_id, book_id) : void
    +checkBookAvailability(book_id) : bool
    +offerAlternatives() : void
    +giveBook(book_id) : void
    +confirmDeletion(book_id) : void
    +addBook(params_dict) : void
    +removeBook(book_id) : void
    +sendBookForRepair(book_id) : void
}

class Книга {
    +book_id : int
    +title : string
    +author : string
    +status : string
    +isAvailable() : bool
    +reserve(user_id) : void
    +markDamaged() : void
    +updateStatus(new_status) : void
}

class Задолженность {
    +user_id : int
    +fine_amount : float
    +isOverdue() : bool
    +calculateFine() : float
    +addFine() : void
}

class Система {
    +findBook(book_id) : Книга
    +updateBookStatus(book_id, status) : void
    +createReservation(user_id, book_id) : void
    +addToReservationQueue(user_id, reservation_id) : void
    +notifyUser(user_id, message) : void
    +addFineToUser(user_id, fine_amount) : void
    +searchBooks(params_dict) : List<Книга>
    +checkReturnDate(book_id, user_id) : void
    +sendReminder(user_id) : void
    +checkDebts(user_id) : bool
    +blockIssue(user_id) : void
    +getBookInfo(book_id) : string
}

Пользователь --> Система : Использует
Библиотекарь --> Система : Использует
Система --> Книга : Управляет
Система --> Задолженность : Управляет
Система --> Пользователь : Уведомляет
Система --> Библиотекарь : Уведомляет

@enduml
