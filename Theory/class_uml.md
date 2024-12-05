@startuml

' Абстрактный класс Component для паттерна Композит
abstract class Component {
    +id: int
    +name: String
    +add(component: Component): void
    +remove(component: Component): void
    +getChild(id: int): Component
    +operation(): void
}

' Классы Composite, наследующиеся от Component
class Каталог {
    +shelves: List<Component>
    +searchBooks(query: String): List<Книга>
    +addShelf(shelf: Шкаф): void
    +removeShelf(shelf_id: int): void
    +operation(): void
}

class Шкаф {
    +cells: List<Component>
    +addCell(cell: Ячейка): void
    +removeCell(cell_id: int): void
    +operation(): void
}

class Ячейка {
    +books: List<Component>
    +addBook(book: Книга): void
    +removeBook(book_id: int): void
    +operation(): void
}

' Класс Leaf, наследующийся от Component
class Книга {
    +book_id: int
    +title: String
    +author: String
    +status: String
    +location: Ячейка
    +damages: List<Повреждение>
    
    +updateStatus(status: String): void
    +addDamage(damage: Повреждение): void
    +removeDamage(damage_id: int): void
    +operation(): void
}

' Класс Leaf для Повреждения без наследования от Component
class Повреждение {
    +damage_id: int
    +description: String
    +cost: float
    
    +operation(): void
}

' Новый класс Уведомление
class Уведомление {
    +notification_id: int
    +user_id: int
    +book_id: int
    +type: String
    +message: String
    +date: Date
    +status: String
    
    +sendNotification(): void
    +scheduleNotification(date: Date): void
}

' Новый класс Бронирование
class Бронирование {
    +reservation_id: int
    +user_id: int
    +book_id: int
    +reservation_date: Date
    +expected_return_date: Date
    +status: String
    
    +createReservation(user_id: int, book_id: int): void
    +addToReservationQueue(user_id: int, reservation_id: int): void
    +getReservationDetails(reservation_id: int): ReservationDetails
    +updateStatus(new_status: String): void
}

' Новый класс ReservationDetails для деталей бронирования
class ReservationDetails {
    +reservation_id: int
    +expected_date: Date
    +status: String
    
    +updateStatus(new_status: String): void
}

' Класс Задолженности
class Задолженности {
    +user_id: int
    +fine_amount: float
    
    +addFine(user_id: int, amount: float): void
    +getDebt(user_id: int): float
}

' Базовый класс Пользователь
class Пользователь {
    +user_id: int
    +name: String
    +email: String
    +contact_info: String
    +register(): void
    +login(): void
    +logout(): void
}

' Класс Читатель наследуется от Пользователь
class Читатель {
    +borrowedBooks: List<Книга>
    +fines: float
    
    +giveBookToReturn(book_id: int): void
    +requestBookIssue(): void
    +chooseAlternative(alternative_number: int): void
    +reserveBook(book_id: int): void
}

' Класс Библиотекарь наследуется от Пользователь
class Библиотекарь {
    +employeeId: int
    +workStart: Date
    +workEnd: Date
    +library: Библиотека
    
    +checkBookCondition(book_id: int): boolean
    +reportOverdue(user_id: int): void
    +calculateOverdueFine(days_overdue: int): float
    +reportFine(user_id: int, fine_amount: float): void
    +addFineToUser(user_id: int, fine_amount: float): void
    +reportDamage(user_id: int): void
    +calculateDamageFine(book_id: int): float
    +reportDamageFine(user_id: int, fine_amount: float): void
    +fixIssue(userID: int, date_issue: Date, return_period: int): void
    +giveBook(book_id: int): void
    +blockIssue(userID: int): void
    +reportDebt(): void
    +offerDebtRepay(): void
    +reportBookUnavailable(): void
    +offerAlternatives(): void
    
    ' Добавленные методы для бронирования
    +createReservation(user_id: int, book_id: int): void
    +reportReservationDetails(reservation_details: ReservationDetails): void
    
    ' Добавленные методы для поиска и выбора книг
    +displaySearchBox(): void
    +findBooks(params_dict: Map<String, String>): List<Книга>
    +displayBooksList(books: List<Книга>): void
    +selectBook(book_id: int): void
    
    ' Добавленные методы для инвентаризации
    +startInventory(): void
    +displayOptions(): void
    +chooseOption(option_number: int): void
    +displayAddWindow(): void
    +addBook(params_dict: Map<String, String>): void
    +confirmAddBook(params_dict: Map<String, String>): void
    +deleteBook(book_id: int): void
    +requestDeleteConfirmation(): void
    +approveDeleteConfirmation(): void
    +confirmDeleteBook(): void
    +sendBookForRepair(book_id: int): void
    +confirmSendBookForRepair(): void
    +finishInventory(): void
}

Пользователь <|-- Читатель
Пользователь <|-- Библиотекарь

' Класс Библиотека
class Библиотека {
    +libraryName: String
    +address: String
    +phone: String
    +catalog: Каталог
    +debts: Задолженности
    +reservations: List<Бронирование>
    +notifications: List<Уведомление>
    +librarians: List<Библиотекарь>
    +readers: List<Читатель>
    
    +updateBookStatus(book_id: int, status: String): void
    +searchBooks(query: String): List<Книга>
    +sendDamageList(damage_dict: Map<String, String>): void
    +checkReturnDate(book_id: int, user_id: int): void
    +checkBookAvailability(book_id: int, user_id: int): void
    +checkUserDebts(userID: int): boolean
    +sendNotification(notification: Уведомление): void
    +handleNoContactDetails(user_id: int): void
    
    ' Добавленные методы для бронирования
    +createReservation(user_id: int, book_id: int): void
    +addToReservationQueue(user_id: int, reservation_id: int): void
    +getReservationDetails(reservation_id: int): ReservationDetails
    
    ' Добавленные методы для инвентаризации
    +addBook(params_dict: Map<String, String>): void
    +deleteBook(book_id: int): void
    +updateBookStatus(book_id: int, status: String): void
    
    ' Добавленный метод для получения информации о книге
    +getBookInfo(book_id: int): Книга
}

' Класс Каталог наследуется от Component
Каталог -|> Component
Шкаф -|> Component
Ячейка -|> Component
Книга -|> Component

' Ассоциации между классами Composite
Каталог "1" o-- "1..*" Шкаф
Шкаф "1" o-- "1..*" Ячейка
Ячейка "1" o-- "0..*" Книга

' Ассоциации между Книгой и Повреждением
Книга "1" *-- "0..*" Повреждение

' Ассоциации между Библиотекой и Бронированием
Библиотека "1" o-- "0..*" Бронирование

' Ассоциации между Бронированием и ReservationDetails
Бронирование "1" --> "1" ReservationDetails

' Ассоциации между Библиотекой и Уведомлениями
Библиотека "1" o-- "0..*" Уведомление

' Ассоциации других классов
Библиотека "1" o-- "1" Каталог
Библиотека "1" o-- "1..*" Задолженности
Библиотека "1" o-- "1..*" Библиотекарь
Библиотека "1" o-- "1..*" Читатель

' Взаимодействия между классами
Читатель ..> Библиотекарь : взаимодействует с
Библиотекарь --> Библиотека : использует
Библиотекарь ..> Читатель : взаимодействует с
Библиотекарь --> Бронирование : создает заявку
Библиотекарь --> Библиотека : добавляет в очередь бронирования
Бронирование --> ReservationDetails : содержит детали

@enduml
