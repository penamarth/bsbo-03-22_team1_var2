@startuml

actor Пользователь
participant Система

Система -> Система : Проверка сроков владения книгой checkReturnDate(book_id, user_id)
alt Срок подходит к концу
    alt У пользователя нет контактных данных
      Система -> Система : Не может отправить уведомление noContactDetails()
    end
    else
      Система -> Пользователь : Уведомление о сроке возврата bookReturnReminder(user_dict)
    
end


Система -> Система : Проверка доступности зарезервированной книги книги checkBookAvailability(book_id)
alt Книга доступна
    alt У пользователя нет контактных данных
      Система -> Система : Не может отправить уведомление noContactDetails()
    end
    else
      Система -> Пользователь : Уведомление о доступности книги bookAvailableNotification(user_dict)

else Книга забронирована другим пользователем
     alt У пользователя нет контактных данных
      Система -> Система : Не может отправить уведомление noContactDetails()
    end
    else
    Система -> Пользователь : Уведомление о недоступности книги bookNotAvailableForUser(user_dict)

end


@enduml
