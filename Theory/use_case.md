@startuml
left to right direction

actor "Читатель" as Reader
actor "Библиотекарь" as Librarian

rectangle "Библиотека" {
    usecase "Регистрация" as UC1
    usecase "Вход в систему" as UC2
    usecase "Поиск книг" as UC3
    usecase "Бронирование книги" as UC4
    usecase "Оформление выдачи книги" as UC5
    usecase "Возврат книги" as UC6
    usecase "Просмотр штрафов" as UC7
    usecase "Управление каталогом" as UC8
    usecase "Обработка возвратов" as UC9
    usecase "Отправка уведомлений" as UC10
}

' Взаимодействие Читателя с Use Cases
Reader --> UC1 : Зарегистрироваться
Reader --> UC2 : Войти в систему
Reader --> UC3 : Искать книги
Reader --> UC4 : Забронировать книгу
Reader --> UC5 : Запросить выдачу книги
Reader --> UC6 : Вернуть книгу
Reader --> UC7 : Просмотреть штрафы

' Взаимодействие Библиотекаря с Use Cases
Librarian --> UC2 : Войти в систему
Librarian --> UC3 : Помогать в поиске книг
Librarian --> UC4 : Обрабатывать бронирования
Librarian --> UC5 : Оформлять выдачу книг
Librarian --> UC6 : Принимать возврат книг
Librarian --> UC7 : Управлять штрафами
Librarian --> UC8 : Управлять каталогом
Librarian --> UC9 : Обрабатывать возвраты

@enduml
