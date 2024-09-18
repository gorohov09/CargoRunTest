using Library.Contracts.Book.CreateBook;
using MediatR;

namespace Library.Core.Requests.BookRequests.CreateBook
{
    /// <summary>
    /// Команда на создание книги
    /// </summary>
    public class CreateBookCommand : CreateBookRequest, IRequest<CreateBookResponse>
    {
    }
}
