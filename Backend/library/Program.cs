using System;
using System.Collections.Generic;
using System.Linq;

#region Logger Class

/// <summary>
/// Класс для логирования сообщений с форматированием "Класс:Метод:Текст".
/// </summary>
public static class Logger
{
    public static void Log(string className, string methodName, string message)
    {
        Console.WriteLine($"{className}:{methodName}:{message}\n");
    }
}

#endregion

#region Composite Pattern Classes

/// <summary>
/// Абстрактный класс Component для паттерна Композит.
/// </summary>
public abstract class Component
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual void Add(Component component)
    {
        Logger.Log(this.GetType().Name, nameof(Add), $"Добавление компонента {component.GetType().Name}");
    }

    public virtual void Remove(Component component)
    {
        Logger.Log(this.GetType().Name, nameof(Remove), $"Удаление компонента {component.GetType().Name}");
    }

    public virtual Component GetChild(int id)
    {
        Logger.Log(this.GetType().Name, nameof(GetChild), $"Получение дочернего компонента с ID {id}");
        return null;
    }

    public abstract void Operation();
}

/// <summary>
/// Класс Catalog, представляющий каталог книг в библиотеке.
/// </summary>
public class Catalog : Component
{
    public List<Component> Shelves { get; set; } = new List<Component>();

    public void SearchBooks(string query)
    {
        Logger.Log("Catalog", nameof(SearchBooks), $"Поиск книг по запросу '{query}'");
        foreach (var shelf in Shelves)
        {
            shelf.Operation();
        }
    }

    public void AddShelf(Shelf shelf)
    {
        Shelves.Add(shelf);
        Logger.Log("Catalog", nameof(AddShelf), $"Добавлен шкаф с ID {shelf.Id}");
    }

    public void RemoveShelf(int shelfId)
    {
        Shelves.RemoveAll(s => s.Id == shelfId);
        Logger.Log("Catalog", nameof(RemoveShelf), $"Удален шкаф с ID {shelfId}");
    }

    public override void Add(Component component)
    {
        if (component is Shelf shelf)
        {
            Shelves.Add(shelf);
            Logger.Log("Catalog", nameof(Add), $"Добавлен шкаф {shelf.Id}");
        }
        else
        {
            base.Add(component);
        }
    }

    public override void Remove(Component component)
    {
        if (component is Shelf shelf)
        {
            Shelves.Remove(shelf);
            Logger.Log("Catalog", nameof(Remove), $"Удален шкаф {shelf.Id}");
        }
        else
        {
            base.Remove(component);
        }
    }

    public override Component GetChild(int id)
    {
        foreach (var shelf in Shelves)
        {
            if (shelf.Id == id)
                return shelf;
        }
        return null;
    }

    public override void Operation()
    {
        Logger.Log("Catalog", nameof(Operation), "Выполнение операции.");
        foreach (var shelf in Shelves)
        {
            shelf.Operation();
        }
    }
}

/// <summary>
/// Класс Shelf, представляющий шкаф с ячейками в каталоге.
/// </summary>
public class Shelf : Component
{
    public List<Component> Cells { get; set; } = new List<Component>();

    public void AddCell(Cell cell)
    {
        Cells.Add(cell);
        Logger.Log("Shelf", nameof(AddCell), $"Добавлена ячейка с ID {cell.Id}");
    }

    public void RemoveCell(int cellId)
    {
        Cells.RemoveAll(c => c.Id == cellId);
        Logger.Log("Shelf", nameof(RemoveCell), $"Удалена ячейка с ID {cellId}");
    }

    public override void Add(Component component)
    {
        if (component is Cell cell)
        {
            Cells.Add(cell);
            Logger.Log("Shelf", nameof(Add), $"Добавлена ячейка {cell.Name}");
        }
        else
        {
            base.Add(component);
        }
    }

    public override void Remove(Component component)
    {
        if (component is Cell cell)
        {
            Cells.Remove(cell);
            Logger.Log("Shelf", nameof(Remove), $"Удалена ячейка {cell.Name}");
        }
        else
        {
            base.Remove(component);
        }
    }

    public override Component GetChild(int id)
    {
        foreach (var cell in Cells)
        {
            if (cell.Id == id)
                return cell;
        }
        return null;
    }

    public override void Operation()
    {
        Logger.Log("Shelf", nameof(Operation), "Выполнение операции.");
        foreach (var cell in Cells)
        {
            cell.Operation();
        }
    }
}

/// <summary>
/// Класс Cell, представляющий ячейку для хранения одной книги.
/// </summary>
public class Cell : Component
{
    public Book Book { get; set; }

    public void AddBook(Book book)
    {
        if (Book == null)
        {
            Book = book;
            Logger.Log("Cell", nameof(AddBook), $"Добавлена книга с ID {book.BookId}");
        }
        else
        {
            Logger.Log("Cell", nameof(AddBook), $"Ячейка уже содержит книгу с ID {Book.BookId}");
        }
    }

    public void RemoveBook()
    {
        if (Book != null)
        {
            Logger.Log("Cell", nameof(RemoveBook), $"Удалена книга с ID {Book.BookId}");
            Book = null;
        }
        else
        {
            Logger.Log("Cell", nameof(RemoveBook), "В ячейке нет книги для удаления");
        }
    }

    public override void Add(Component component)
    {
        if (component is Book book)
        {
            AddBook(book);
        }
        else
        {
            base.Add(component);
        }
    }

    public override void Remove(Component component)
    {
        if (component is Book)
        {
            RemoveBook();
        }
        else
        {
            base.Remove(component);
        }
    }

    public override Component GetChild(int id)
    {
        if (Book != null && Book.BookId == id)
            return Book;
        return null;
    }

    public override void Operation()
    {
        Logger.Log("Cell", nameof(Operation), "Выполнение операции.");
        if (Book != null)
        {
            Book.Operation();
        }
        else
        {
            Logger.Log("Cell", nameof(Operation), "В ячейке нет книги.");
        }
    }
}

/// <summary>
/// Класс Book, представляющий книгу в библиотеке.
/// </summary>
public class Book : Component
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Status { get; set; }
    public Cell Location { get; set; }
    public List<Damage> Damages { get; set; } = new List<Damage>();

    public void UpdateStatus(string status)
    {
        Logger.Log("Book", nameof(UpdateStatus), $"Обновление статуса с '{Status}' на '{status}'.");
        Status = status;
    }

    public void AddDamage(Damage damage)
    {
        Damages.Add(damage);
        Logger.Log("Book", nameof(AddDamage), $"Добавлено повреждение '{damage.Description}' стоимостью {damage.Cost}.");
    }

    public void RemoveDamage(int damageId)
    {
        Damages.RemoveAll(d => d.DamageId == damageId);
        Logger.Log("Book", nameof(RemoveDamage), $"Удалено повреждение с ID {damageId}.");
    }

    public override void Operation()
    {
        Logger.Log("Book", nameof(Operation), $"Текущий статус - '{Status}'.");
    }

    public override void Add(Component component)
    {
        Logger.Log("Book", nameof(Add), "Невозможно добавить дочерний компонент.");
    }

    public override void Remove(Component component)
    {
        Logger.Log("Book", nameof(Remove), "Невозможно удалить дочерний компонент.");
    }

    public override Component GetChild(int id)
    {
        Logger.Log("Book", nameof(GetChild), "Невозможно получить дочерний компонент.");
        return null;
    }
}

#endregion

#region Additional Classes

/// <summary>
/// Класс Damage, представляющий повреждение книги.
/// </summary>
public class Damage
{
    public int DamageId { get; set; }
    public string Description { get; set; }
    public float Cost { get; set; }

    public void Operation()
    {
        Logger.Log("Damage", nameof(Operation), $"Описание: {Description}, Стоимость: {Cost}");
    }
}

/// <summary>
/// Класс Notification для управления уведомлениями.
/// </summary>
public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }

    public void SendNotification()
    {
        Logger.Log("Notification", nameof(SendNotification), $"Отправлено уведомление '{Message}' пользователю с ID {UserId} по книге ID {BookId}.");
    }

    public void ScheduleNotification(DateTime date)
    {
        Logger.Log("Notification", nameof(ScheduleNotification), $"Запланировано уведомление на {date}.");
    }
}

/// <summary>
/// Класс Reservation для управления бронированиями книг.
/// </summary>
public class Reservation
{
    public int ReservationId { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ExpectedReturnDate { get; set; }
    public string Status { get; set; }

    public void CreateReservation(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        ReservationDate = DateTime.Now;
        Status = "Active";
        Logger.Log("Reservation", nameof(CreateReservation), $"Создано бронирование для пользователя {UserId} на книгу {BookId}.");
    }

    public void AddToReservationQueue(int userId, int reservationId)
    {
        Logger.Log("Reservation", nameof(AddToReservationQueue), $"Пользователь {userId} добавлен в очередь бронирования с ID {reservationId}.");
    }

    public ReservationDetails GetReservationDetails(int reservationId)
    {
        Logger.Log("Reservation", nameof(GetReservationDetails), $"Получены детали бронирования с ID {reservationId}.");
        return new ReservationDetails
        {
            ReservationId = reservationId,
            ExpectedDate = DateTime.Now.AddDays(7),
            Status = "Active"
        };
    }

    public void UpdateStatus(string newStatus)
    {
        Logger.Log("Reservation", nameof(UpdateStatus), $"Обновлен статус бронирования с ID {ReservationId} на '{newStatus}'.");
        Status = newStatus;
    }
}

/// <summary>
/// Класс ReservationDetails для предоставления подробной информации о бронировании.
/// </summary>
public class ReservationDetails
{
    public int ReservationId { get; set; }
    public DateTime ExpectedDate { get; set; }
    public string Status { get; set; }

    public void UpdateStatus(string newStatus)
    {
        Logger.Log("ReservationDetails", nameof(UpdateStatus), $"Обновлен статус бронирования ID {ReservationId} на '{newStatus}'.");
        Status = newStatus;
    }
}

/// <summary>
/// Класс Debts для управления штрафами пользователей.
/// </summary>
public class Debts
{
    public int UserId { get; set; }
    public float FineAmount { get; set; }

    public void AddFine(int userId, float amount)
    {
        if (UserId != userId)
        {
            UserId = userId;
            FineAmount = 0;
        }
        FineAmount += amount;
        Logger.Log("Debts", nameof(AddFine), $"Добавлен штраф {amount} пользователю {UserId}. Текущая задолженность: {FineAmount}.");
    }

    public float GetDebt(int userId)
    {
        if (UserId == userId)
        {
            Logger.Log("Debts", nameof(GetDebt), $"Пользователь {UserId} имеет задолженность {FineAmount}.");
            return FineAmount;
        }
        Logger.Log("Debts", nameof(GetDebt), $"Пользователь {userId} не имеет задолженностей.");
        return 0;
    }
}

#endregion

#region User Classes

/// <summary>
/// Базовый класс User для всех типов пользователей.
/// </summary>
public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string ContactInfo { get; set; }

    public virtual void Register()
    {
        Logger.Log("User", nameof(Register), $"Регистрация.");
    }

    public virtual void Login()
    {
        Logger.Log("User", nameof(Login), $"Вход в систему.");
    }

    public virtual void Logout()
    {
        Logger.Log("User", nameof(Logout), $"Выход из системы.");
    }
}

/// <summary>
/// Класс Reader, представляющий читателя библиотеки.
/// </summary>
public class Reader : User
{
    public List<Book> BorrowedBooks { get; set; } = new List<Book>();
    public float Fines { get; set; }

    public void GiveBookToReturn(int bookId, Library library)
    {
        Logger.Log("Reader", nameof(GiveBookToReturn), $"Сдача книги с ID {bookId}.");
        Book book = library.GetBookById(bookId);
        if (book != null && BorrowedBooks.Contains(book))
        {
            // Проверка книги на наличие повреждений
            if (library.Librarians[0].CheckBookCondition(bookId, book, library))
            {
                Logger.Log("Reader", nameof(GiveBookToReturn), $"Книга '{book.Title}' возвращена без повреждений.");
                book.UpdateStatus("Available");
                BorrowedBooks.Remove(book);
                float debt = library.Debts.GetDebt(UserId); // Проверка задолженностей
                Logger.Log("Reader", nameof(GiveBookToReturn), $"Книга '{book.Title}' успешно возвращена.");
            }
            else
            {
                // Книга возвращена с повреждениями
                Logger.Log("Reader", nameof(GiveBookToReturn), $"Книга '{book.Title}' возвращена с повреждениями.");

                Damage damage = book.Damages[0];
                Logger.Log("Librarian", nameof(GiveBookToReturn), $"Повреждение: {damage.Description}, Стоимость: {damage.Cost}");

                // Рассчитываем штраф за повреждение
                library.Debts.AddFine(UserId, damage.Cost);

                // Обновляем статус книги
                book.UpdateStatus("Damaged");

                // Отправка уведомления
                Notification notification = new Notification
                {
                    NotificationId = library.GenerateNotificationId(),
                    UserId = UserId,
                    BookId = bookId,
                    Type = "Damage Notification",
                    Message = $"Ваша книга '{book.Title}' была возвращена с повреждениями. Штраф: {damage.Cost} рублей.",
                    Date = DateTime.Now,
                    Status = "Sent"
                };
                library.SendNotification(notification);
            }
        }
    }

    public void RequestBookIssue(int bookId, Library library)
    {
        Logger.Log("Reader", nameof(RequestBookIssue), $"Запрос на выдачу книги с ID {bookId}.");
        Book book = library.GetBookById(bookId);
        if (book != null && book.Status == "Available")
        {
            // Проверка на наличие задолженностей
            float debt = library.Debts.GetDebt(UserId);
            if (debt == 0)
            {
                Logger.Log("Reader", nameof(RequestBookIssue), $"Запрос на выдачу книги '{book.Title}' успешно сделан.");

            }
            else
            {
                // Пользователь имеет задолженность
                Logger.Log("Reader", nameof(RequestBookIssue), $"У пользователя {UserId} имеется задолженность {debt} рублей. Выдача книги заблокирована.");      
            }
        }
        else
        {
            // Книга недоступна
            Logger.Log("Reader", nameof(RequestBookIssue), $"Книга с ID {bookId} недоступна для выдачи.");

            // Предложение альтернатив
            Logger.Log("Librarian", nameof(RequestBookIssue), $"Предложение альтернатив пользователю.");

            // Заказ книги (бронирование)
            ReserveBook(bookId, library);

            // Выбор другой книги
            foreach (var availableBook in library.SearchBooks(""))
            {
                if (availableBook.Status == "Available" && availableBook.BookId != bookId)
                {
                    Logger.Log("Librarian", nameof(RequestBookIssue), $"Предлагается другая доступная книга: '{availableBook.Title}'.");
                    if (library.Librarians.Count > 0)
                    {
                        library.Librarians[0].GiveBook(availableBook.BookId, this, library);
                    }
                    break;
                }
            }
        }
    }

    public void ReserveBook(int bookId, Library library)
    {
        Logger.Log("Reader", nameof(ReserveBook), $"Бронирование книги с ID {bookId}.");
        Book book = library.GetBookById(bookId);

        if (book == null)
        {
            Logger.Log("Reader", nameof(ReserveBook), $"Книга с ID {bookId} не найдена. Бронирование невозможно.");
        }
        else if (book.Status != "Available")
        {
         
            library.Librarians[0].CreateReservation(UserId, bookId, library);
            // Отправка уведомления о бронировании
            Notification notification1 = new Notification
            {
                NotificationId = library.GenerateNotificationId(),
                UserId = UserId,
                BookId = bookId,
                Type = "Reservation Confirmation",
                Message = $"Вы успешно забронировали книгу с ID {bookId}. Ожидайте доступности.",
                Date = DateTime.Now,
                Status = "Sent"
            };
            library.SendNotification(notification1);
            Logger.Log("Reader", nameof(ReserveBook), $"Книга '{book.Title}' зарезервирована.");
           
        }
        else
        {
            library.Librarians[0].CreateReservation(UserId, bookId, library);
            Logger.Log("Reader", nameof(ReserveBook), $"Книга '{book.Title}' доступна и может быть выдана.");
            Notification notification = new Notification
            {
                NotificationId = library.GenerateNotificationId(),
                UserId = UserId,
                BookId = bookId,
                Type = "Reservation Confirmation",
                Message = $"Книга '{book.Title}' ожидает вас.",
                Date = DateTime.Now,
                Status = "Sent"
            };
            library.SendNotification(notification);
        }
    }

    public override void Register()
    {
        Logger.Log("Reader", nameof(Register), $"Регистрация.");
    }
}

/// <summary>
/// Класс Librarian, представляющий библиотекаря.
/// </summary>
public class Librarian : User
{
    public int EmployeeId { get; set; }
    public DateTime WorkStart { get; set; }
    public DateTime WorkEnd { get; set; }
    public Library Library { get; set; }

    /// <summary>
    /// Проверяет состояние книги.
    /// </summary>
    public bool CheckBookCondition(int bookId,Book book, Library library)
    {
        Logger.Log("Librarian", nameof(CheckBookCondition), $"Проверка состояния книги с ID {bookId}.");
        if (book != null)
        {
            if (book.Damages.Count == 0)
            {
                Logger.Log("Librarian", nameof(CheckBookCondition), $"Книга '{book.Title}' не повреждена.");
                return true;
            }
            else
            {
                Logger.Log("Librarian", nameof(CheckBookCondition), $"Книга '{book.Title}' повреждена.");
                return false;
            }
        }
        Logger.Log("Librarian", nameof(CheckBookCondition), $"Книга с ID {bookId} не найдена.");
        return false;
    }

    public void ReportOverdue(int userId, Library library)
    {
        Logger.Log("Librarian", nameof(ReportOverdue), $"Сообщение о просрочке для пользователя {userId}.");
    }

    public float CalculateOverdueFine(int daysOverdue)
    {
        float fine = daysOverdue * 10;
        Logger.Log("Librarian", nameof(CalculateOverdueFine), $"Рассчитан штраф за {daysOverdue} дней просрочки: {fine}.");
        return fine;
    }

    public void ReportFine(int userId, float fineAmount, Library library)
    {
        Logger.Log("Librarian", nameof(ReportFine), $"Сообщение о штрафе {fineAmount} пользователю {userId}.");
        library.Debts.AddFine(userId, fineAmount);
    }

    public void AddFineToUser(int userId, float fineAmount, Library library)
    {
        Logger.Log("Librarian", nameof(AddFineToUser), $"Добавление штрафа {fineAmount} пользователю {userId}.");
        library.Debts.AddFine(userId, fineAmount);
    }

    public void ReportDamage(int userId, Library library)
    {
        Logger.Log("Librarian", nameof(ReportDamage), $"Сообщение о повреждении книги пользователю {userId}.");
    }

    public float CalculateDamageFine(int bookId, Library library)
    {
        float damageFine = 50; 
        Logger.Log("Librarian", nameof(CalculateDamageFine), $"Рассчитан штраф за повреждение книги {bookId}: {damageFine}.");
        return damageFine;
    }

    public void ReportDamageFine(int userId, float fineAmount, Library library)
    {
        Logger.Log("Librarian", nameof(ReportDamageFine), $"Сообщение о штрафе за повреждение {fineAmount} пользователю {userId}.");
        library.Debts.AddFine(userId, fineAmount);
    }

    public void FixIssue(int userId, DateTime dateIssue, int returnPeriod, Library library)
    {
        Logger.Log("Librarian", nameof(FixIssue), $"Исправление проблемы для пользователя {userId}, дата выдачи {dateIssue}, срок возврата {returnPeriod} дней.");
    }

    /// <summary>
    /// Выдаёт книгу читателю.
    /// </summary>
    public void GiveBook(int bookId, Reader reader, Library library)
    {
        Logger.Log("Librarian", nameof(GiveBook), $"Выдача книги с ID {bookId} читателю {reader.Name}.");
        Book book = library.GetBookById(bookId);
        if (book != null && book.Status == "Available")
        {
            // Проверка на наличие задолженностей у пользователя
            float debt = library.Debts.GetDebt(reader.UserId);
            if (debt == 0)
            {
                book.UpdateStatus("Issued");
                reader.BorrowedBooks.Add(book);
                Logger.Log("Librarian", nameof(GiveBook), $"Книга '{book.Title}' выдана читателю {reader.Name}.");
            }
            else
            {
                // Пользователь имеет задолженность
                Logger.Log("Librarian", nameof(GiveBook), $"Пользователь {reader.UserId} имеет задолженность {debt} рублей. Выдача книги заблокирована.");
            }
        }
        else
        {
            // Книга недоступна
            Logger.Log("Librarian", nameof(GiveBook), $"Книга с ID {bookId} недоступна для выдачи.");

            // Предложение альтернатив
            Logger.Log("Librarian", nameof(GiveBook), $"Предложение альтернатив пользователю.");

            // Предлагаем заказ книги (бронирование)
            reader.ReserveBook(bookId, library);

            // Предлагаем выбрать другую доступную книгу
            List<Book> availableBooks = library.SearchBooks(new Dictionary<string, string> { { "query", "" } });
            foreach (var availableBook in availableBooks)
            {
                if (availableBook.Status == "Available" && availableBook.BookId != bookId)
                {
                    Logger.Log("Librarian", nameof(GiveBook), $"Предлагается другая доступная книга: '{availableBook.Title}'.");
                    GiveBook(availableBook.BookId, reader, library);
                    break;
                }
            }
        }
    }

    public void BlockIssue(int userId, Library library)
    {
        Logger.Log("Librarian", nameof(BlockIssue), $"Блокировка выдачи книг для пользователя {userId}.");
    }

    public void ReportDebt(int userId, Library library)
    {
        Logger.Log("Librarian", nameof(ReportDebt), $"Проверка задолженности пользователя {userId}.");
        float debt = library.Debts.GetDebt(userId);
        if (debt > 0)
        {
            Logger.Log("Librarian", nameof(ReportDebt), $"Пользователь {userId} имеет задолженность {debt}.");
        }
        else
        {
            Logger.Log("Librarian", nameof(ReportDebt), $"Пользователь {userId} не имеет задолженностей.");
        }
    }

    public void OfferDebtRepay(int userId, Library library)
    {
        Logger.Log("Librarian", nameof(OfferDebtRepay), $"Предложение погашения задолженностей пользователю {userId}.");
    }

    public void ReportBookUnavailable(int bookId)
    {
        Logger.Log("Librarian", nameof(ReportBookUnavailable), $"Сообщение о недоступности книги с ID {bookId}.");
    }

    public void OfferAlternatives(int bookId)
    {
        Logger.Log("Librarian", nameof(OfferAlternatives), $"Предложение альтернативных книг для книги с ID {bookId}.");
    }

    /// <summary>
    /// Создаёт бронирование для пользователя на книгу.
    /// </summary>
    public void CreateReservation(int userId, int bookId, Library library)
    {
        Logger.Log("Librarian", nameof(CreateReservation), $"Создание бронирования для пользователя {userId} на книгу {bookId}.");
        Reservation reservation = new Reservation
        {
            ReservationId = library.GenerateReservationId(),
            UserId = userId,
            BookId = bookId,
            Status = "Active"
        };
        reservation.CreateReservation(userId, bookId);
        library.Reservations.Add(reservation);
        Logger.Log("Librarian", nameof(CreateReservation), $"Бронирование создано с ID {reservation.ReservationId}.");

    }

    public void ReportReservationDetails(ReservationDetails reservationDetails)
    {
        Logger.Log("Librarian", nameof(ReportReservationDetails), $"Сообщение деталей бронирования ID {reservationDetails.ReservationId}.");
    }

    public void DisplaySearchBox()
    {
        Logger.Log("Librarian", nameof(DisplaySearchBox), $"Отображение окна поиска.");
    }

    /// <summary>
    /// Находит книги по заданным параметрам.
    /// </summary>
    public List<Book> FindBooks(Dictionary<string, string> paramsDict, Library library)
    {
        Logger.Log("Librarian", nameof(FindBooks), $"Поиск книг с параметрами.");
        List<Book> foundBooks = library.SearchBooks(paramsDict);
        return foundBooks;
    }

    /// <summary>
    /// Отображает список найденных книг.
    /// </summary>
    public void DisplayBooksList(List<Book> books)
    {
        Logger.Log("Librarian", nameof(DisplayBooksList), $"Отображение списка книг.");
        foreach (var book in books)
        {
            Logger.Log("Librarian", nameof(DisplayBooksList), $"- {book.Title} by {book.Author} (ID: {book.BookId}, Status: {book.Status})");
        }
    }

    public void SelectBook(int bookId, Library library)
    {
        Logger.Log("Librarian", nameof(SelectBook), $"Выбор книги с ID {bookId}.");
        Book book = library.GetBookById(bookId);
        if (book != null)
        {
            Logger.Log("Librarian", nameof(SelectBook), $"Книга '{book.Title}' выбрана.");
        }
        else
        {
            Logger.Log("Librarian", nameof(SelectBook), $"Книга с ID {bookId} не найдена.");
        }
    }

    /// <summary>
    /// Начинает процесс инвентаризации с заданными опциями.
    /// </summary>
    /// <param name="options">Список опций инвентаризации.</param>
    public void StartInventory(List<InventoryOption> options)
    {
        Logger.Log("Librarian", nameof(StartInventory), $"Начало инвентаризации с {options.Count} опциями.");

        foreach (var option in options)
        {
            switch (option)
            {
                case InventoryOption.AddNewBook:
                    // Вариант A: Добавить новую книгу
                    Logger.Log("Librarian", nameof(StartInventory), "Выбран вариант A: Добавить новую книгу.");
                    Dictionary<string, string> newBookParams = new Dictionary<string, string>
                    {
                        { "title", "Анна Каренина" },
                        { "author", "Лев Толстой" },
                        { "year", "1877" },
                        { "genre", "Роман" }
                    };
                    AddBook(newBookParams, Library);
                    Dictionary<string, string> bookToDeleteParams = new Dictionary<string, string>
                    {
                        { "title", "Старик и море" },
                        { "author", "Эрнест Хемингуэй" },
                        { "year", "1952" },
                        { "genre", "Роман" }
                    };
                    AddBook(bookToDeleteParams, Library);
                    break;

                case InventoryOption.DeleteBook:
                    // Вариант B: Списать книгу
                    Logger.Log("Librarian", nameof(StartInventory), "Выбран вариант B: Списать книгу.");
                    // Поиск книги для списания
                    List<Book> booksToDelete = Library.SearchBooks(new Dictionary<string, string> { { "query", "Старик и море" } });
                    if (booksToDelete.Count > 0)
                    {
                        Book bookToDelete = booksToDelete[0];
                        DeleteBook(bookToDelete.BookId, Library);
                    }
                    break;

                case InventoryOption.SendBookForRepair:
                    Logger.Log("Librarian", nameof(StartInventory), "Выбран вариант C: Отправить поврежденную книгу на ремонт.");
                    int damagedBookId = 302;
                    Book damagedBook = Library.GetBookById(damagedBookId);
                    if (damagedBook != null)
                    {
                        // Фиксируем повреждение
                        Damage damage = new Damage
                        {
                            DamageId = 1,
                            Description = "Разорванные страницы",
                            Cost = 30
                        };
                        damagedBook.AddDamage(damage);

                        // Отправляем на ремонт
                        SendBookForRepair(damagedBookId, Library);
                    }
                    else
                    {
                        Logger.Log("Librarian", nameof(StartInventory), $"Книга с ID {damagedBookId} не найдена для отправки на ремонт.");
                    }
                    break;

                default:
                    Logger.Log("Librarian", nameof(StartInventory), $"Неизвестная опция инвентаризации: {option}.");
                    break;
            }
        }

        // Завершение инвентаризации
        FinishInventory();
    }

    public void DisplayOptions()
    {
        Logger.Log("Librarian", nameof(DisplayOptions), $"Отображение опций инвентаризации.");
    }

    public void ChooseOption(int optionNumber)
    {
        Logger.Log("Librarian", nameof(ChooseOption), $"Выбор опции инвентаризации {optionNumber}.");
    }

    public void DisplayAddWindow()
    {
        Logger.Log("Librarian", nameof(DisplayAddWindow), $"Отображение окна добавления книги.");
    }

    /// <summary>
    /// Добавляет новую книгу в каталог.
    /// </summary>
    public void AddBook(Dictionary<string, string> paramsDict, Library library)
    {
        Logger.Log("Librarian", nameof(AddBook), $"Добавление книги '{paramsDict["title"]}' автора '{paramsDict["author"]}'.");
        Book newBook = new Book
        {
            BookId = library.GenerateBookId(),
            Title = paramsDict["title"],
            Author = paramsDict["author"],
            Status = "Available"
        };
        library.AddBookToCatalog(newBook);
        Logger.Log("Librarian", nameof(AddBook), $"Книга '{newBook.Title}' добавлена в каталог с ID {newBook.BookId}.");
    }

    public void ConfirmAddBook(Dictionary<string, string> paramsDict, Library library)
    {
        Logger.Log("Librarian", nameof(ConfirmAddBook), $"Подтверждение добавления книги '{paramsDict["title"]}'.");
    }

    public void DeleteBook(int bookId, Library library)
    {
        Logger.Log("Librarian", nameof(DeleteBook), $"Удаление книги с ID {bookId}.");
        library.DeleteBookFromCatalog(bookId);
    }

    public void RequestDeleteConfirmation()
    {
        Logger.Log("Librarian", nameof(RequestDeleteConfirmation), $"Запрос подтверждения удаления книги.");
    }

    public void ApproveDeleteConfirmation(int bookId, Library library)
    {
        Logger.Log("Librarian", nameof(ApproveDeleteConfirmation), $"Одобрение удаления книги с ID {bookId}.");
        library.DeleteBookFromCatalog(bookId);
    }

    public void ConfirmDeleteBook(int bookId, Library library)
    {
        Logger.Log("Librarian", nameof(ConfirmDeleteBook), $"Подтверждение удаления книги с ID {bookId}.");
        library.DeleteBookFromCatalog(bookId);
    }

    /// <summary>
    /// Отправляет книгу на ремонт.
    /// </summary>
    public void SendBookForRepair(int bookId, Library library)
    {
        Logger.Log("Librarian", nameof(SendBookForRepair), $"Отправка книги с ID {bookId} на ремонт.");
        Book book = library.GetBookById(bookId);
        if (book != null)
        {
            book.UpdateStatus("Under Repair");
            Logger.Log("Librarian", nameof(SendBookForRepair), $"Книга '{book.Title}' отправлена на ремонт.");
        }
        else
        {
            Logger.Log("Librarian", nameof(SendBookForRepair), $"Книга с ID {bookId} не найдена.");
        }
    }

    public void ConfirmSendBookForRepair(int bookId, Library library)
    {
        Logger.Log("Librarian", nameof(ConfirmSendBookForRepair), $"Подтверждение отправки книги с ID {bookId} на ремонт.");
    }

    /// <summary>
    /// Завершает процесс инвентаризации.
    /// </summary>
    public void FinishInventory()
    {
        Logger.Log("Librarian", nameof(FinishInventory), $"Завершение инвентаризации.");
    }

    public override void Register()
    {
        Logger.Log("Librarian", nameof(Register), $"Регистрация.");
    }
}

#endregion

#region Library Class

/// <summary>
/// Класс Library, представляющий библиотеку и её компоненты.
/// </summary>
public class Library
{
    public string LibraryName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public Catalog Catalog { get; set; } = new Catalog();
    public Debts Debts { get; set; } = new Debts();
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    public List<Notification> Notifications { get; set; } = new List<Notification>();
    public List<Librarian> Librarians { get; set; } = new List<Librarian>();
    public List<Reader> Readers { get; set; } = new List<Reader>();

    private int nextBookId = 1000;
    private int nextReservationId = 2000;
    private int nextNotificationId = 3000;

    public int GenerateBookId()
    {
        return nextBookId++;
    }

    public int GenerateReservationId()
    {
        return nextReservationId++;
    }

    public int GenerateNotificationId()
    {
        return nextNotificationId++;
    }

    /// <summary>
    /// Добавляет книгу в каталог, размещая её в первой доступной ячейке.
    /// </summary>
    public void AddBookToCatalog(Book book)
    {
        Logger.Log("Library", nameof(AddBookToCatalog), $"Добавление книги '{book.Title}' в каталог.");
        foreach (var shelf in Catalog.Shelves)
        {
            Shelf currentShelf = shelf as Shelf;
            foreach (var cell in currentShelf.Cells)
            {
                Cell currentCell = cell as Cell;
                if (currentCell.Book == null) // Ячейка пуста
                {
                    currentCell.AddBook(book);
                    book.Location = currentCell;
                    Logger.Log("Library", nameof(AddBookToCatalog), $"Книга '{book.Title}' добавлена в ячейку '{currentCell.Name}' шкафа '{currentShelf.Id}'.");
                    return;
                }
            }
        }
        Logger.Log("Library", nameof(AddBookToCatalog), "Нет доступных ячеек для добавления книги.");
    }

    /// <summary>
    /// Удаляет книгу из каталога по её ID.
    /// </summary>
    public void DeleteBookFromCatalog(int bookId)
    {
        Logger.Log("Library", nameof(DeleteBookFromCatalog), $"Удаление книги с ID {bookId} из каталога.");
        foreach (var shelf in Catalog.Shelves)
        {
            Shelf currentShelf = shelf as Shelf;
            foreach (var cell in currentShelf.Cells)
            {
                Cell currentCell = cell as Cell;
                if (currentCell.Book != null && currentCell.Book.BookId == bookId)
                {
                    currentCell.RemoveBook();
                    Logger.Log("Library", nameof(DeleteBookFromCatalog), $"Книга с ID {bookId} удалена из ячейки '{currentCell.Name}' шкафа '{currentShelf.Name}'.");
                    return;
                }
            }
        }
        Logger.Log("Library", nameof(DeleteBookFromCatalog), $"Книга с ID {bookId} не найдена в каталоге.");
    }

    /// <summary>
    /// Получает книгу по её ID.
    /// </summary>
    public Book GetBookById(int bookId)
    {
        foreach (var shelf in Catalog.Shelves)
        {
            Shelf currentShelf = shelf as Shelf;
            foreach (var cell in currentShelf.Cells)
            {
                Cell currentCell = cell as Cell;
                if (currentCell.Book != null && currentCell.Book.BookId == bookId)
                {
                    Logger.Log("Library", nameof(GetBookById), $"Найдена книга '{currentCell.Book.Title}' с ID {bookId}.");
                    return currentCell.Book;
                }
            }
        }
        Logger.Log("Library", nameof(GetBookById), $"Книга с ID {bookId} не найдена.");
        return null;
    }

    /// <summary>
    /// Ищет книги по заданному запросу.
    /// </summary>
    public List<Book> SearchBooks(string query)
    {
        Logger.Log("Library", nameof(SearchBooks), $"Поиск книг по запросу '{query}'.");
        List<Book> foundBooks = new List<Book>();
        foreach (var shelf in Catalog.Shelves)
        {
            Shelf currentShelf = shelf as Shelf;
            foreach (var cell in currentShelf.Cells)
            {
                Cell currentCell = cell as Cell;
                if (currentCell.Book != null)
                {
                    Book currentBook = currentCell.Book;
                    if (currentBook.Title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        currentBook.Author.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        foundBooks.Add(currentBook);
                        Logger.Log("Library", nameof(SearchBooks), $"Найдена книга '{currentBook.Title}' авторства '{currentBook.Author}' с ID {currentBook.BookId}.");
                    }
                }
            }
        }
        if (foundBooks.Count == 0)
        {
            Logger.Log("Library", nameof(SearchBooks), "Книги по запросу не найдены.");
        }
        return foundBooks;
    }

    /// <summary>
    /// Ищет книги по заданным параметрам.
    /// </summary>
    public List<Book> SearchBooks(Dictionary<string, string> paramsDict)
    {
        string query = paramsDict.ContainsKey("query") ? paramsDict["query"] : "";
        return SearchBooks(query);
    }

    /// <summary>
    /// Отправляет уведомление пользователю.
    /// </summary>
    public void SendNotification(Notification notification)
    {
        notification.NotificationId = GenerateNotificationId();
        Notifications.Add(notification);
        notification.SendNotification();
    }
}

#endregion

#region Program

/// <summary>
/// Перечисление доступных опций инвентаризации.
/// </summary>
public enum InventoryOption
{
    AddNewBook,        // Вариант A: Добавить новую книгу
    DeleteBook,        // Вариант B: Списать книгу
    SendBookForRepair  // Вариант C: Отправить поврежденную книгу на ремонт
}


public class LibraryService
{
    private readonly Library _library;
    private readonly Librarian _librarian;
    private readonly Reader _reader;

    public LibraryService()
    {
      
        _library = CreateLibrary();
        _librarian = CreateLibrarian(_library);
        _reader = CreateReader(401, "Иван Иванов", "ivan@example.com", "555-1234");
    }

    private Library CreateLibrary()
    {
        var library = new Library
        {
            LibraryName = "Городская библиотека",
            Address = "ул. Примерная, д. 1",
            Phone = "123-456-7890"
        };


        var catalog = new Catalog
        {
            Id = 1,
            Name = "Основной каталог"
        };
        library.Catalog = catalog;


        var shelf1 = new Shelf { Id = 101, Name = "Шкаф A" };
        var shelf2 = new Shelf { Id = 102, Name = "Шкаф B" };
        catalog.AddShelf(shelf1);
        catalog.AddShelf(shelf2);

        var cell1 = new Cell { Id = 201, Name = "Ячейка 1" };
        var cell2 = new Cell { Id = 202, Name = "Ячейка 2" };
        var cell3 = new Cell { Id = 203, Name = "Ячейка 3" };
        var cell4 = new Cell { Id = 204, Name = "Ячейка 4" };
        var cell5 = new Cell { Id = 205, Name = "Ячейка 5" };
        shelf1.AddCell(cell1);
        shelf1.AddCell(cell2);
        shelf2.AddCell(cell3);
        shelf2.AddCell(cell4);
        shelf2.AddCell(cell5);


        var book1 = new Book { BookId = 301, Title = "Война и мир", Author = "Лев Толстой", Status = "Available" };
        var book2 = new Book { BookId = 302, Title = "Преступление и наказание", Author = "Федор Достоевский", Status = "Available" };
        library.AddBookToCatalog(book1);
        library.AddBookToCatalog(book2);
        

        return library;
    }


    private Librarian CreateLibrarian(Library library)
    {
        var librarian = new Librarian
        {
            UserId = 501,
            Name = "Мария Петрова",
            Email = "maria@example.com",
            ContactInfo = "555-5678",
            EmployeeId = 601,
            WorkStart = DateTime.Now.AddHours(-8),
            WorkEnd = DateTime.Now.AddHours(8),
            Library = library
        };
        library.Librarians.Add(librarian);
        librarian.Register();
        librarian.Login();

        return librarian;
    }


    public Reader CreateReader(int userId, string name, string email, string contactInfo)
    {
        var reader = new Reader
        {
            UserId = userId,
            Name = name,
            Email = email,
            ContactInfo = contactInfo
        };
        _library.Readers.Add(reader);
        reader.Register();
        reader.Login();

        return reader;
    }

    public Reader GetReader(int userId)
    {
        var reader = _library.Readers.FirstOrDefault(r => r.UserId == userId);
        if (reader == null)
        {
            throw new Exception($"Читатель с ID {userId} не найден.");
        }
        return reader;
    }


    public void IssueBooks(int readerId)
    {
        var reader = GetReader(readerId);
        Logger.Log("LibraryService", nameof(IssueBooks), "------ Выдача книги  ------");
        reader.RequestBookIssue(301, _library);
        reader.RequestBookIssue(302, _library);
        _librarian.GiveBook(301, reader, _library);
        _librarian.GiveBook(302, reader, _library);
    }


    public void ReturnBooks(int readerId)
    {
        var reader = GetReader(readerId);
        Logger.Log("LibraryService", nameof(ReturnBooks), "------ Возврат книги ------");
        reader.GiveBookToReturn(301, _library);
    }


    public void ReserveBooks(int readerId)
    {
        var reader = GetReader(readerId);
        Logger.Log("LibraryService", nameof(ReserveBooks), "------ заказ книги  ------");
        Book book3 = new Book { BookId = 303, Title = "Анна Каренина", Author = "Лев Толстой", Status = "Available" };
        _library.AddBookToCatalog(book3);
        reader.ReserveBook(301, _library);
        reader.ReserveBook(302, _library);
    }


    public void SearchBooks(Dictionary<string, string> searchParams)
    {
        Logger.Log("LibraryService", nameof(SearchBooks), "------ Поиск книги ------");
        List<Book> foundBooks = _librarian.FindBooks(searchParams, _library);
        _librarian.DisplayBooksList(foundBooks);
    }


    public void SendNotification(int readerId)
    {
        var reader = GetReader(readerId);
        Logger.Log("LibraryService", nameof(SendNotification), "------ Уведомление ------");
        Notification notification1 = new Notification
        {
            NotificationId = _library.GenerateNotificationId(),
            UserId = reader.UserId,
            BookId = 302,
            Type = "Due Soon",
            Message = $"Срок возврата книги 'Преступление и наказание' истекает завтра.",
            Date = DateTime.Now,
            Status = "Sent"
        };
        _library.SendNotification(notification1);
    }

    public void StartInventory()
    {
        Logger.Log("LibraryService", nameof(StartInventory), "------ Инвентаризация  ------");
        List<InventoryOption> inventoryOptions = new List<InventoryOption>
        {
            InventoryOption.AddNewBook,
            InventoryOption.DeleteBook,
            InventoryOption.SendBookForRepair
        };
        _librarian.StartInventory(inventoryOptions);
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        var service = new LibraryService();


        service.IssueBooks(401);  

        service.ReturnBooks(401);

     
        service.ReserveBooks(401);

        var searchParams = new Dictionary<string, string> { { "query", "Война" } };
        service.SearchBooks(searchParams);


        service.SendNotification(401);

        service.StartInventory();

        Logger.Log("Program", nameof(Main), "--- Завершение операций ---");
    }
}

#endregion
