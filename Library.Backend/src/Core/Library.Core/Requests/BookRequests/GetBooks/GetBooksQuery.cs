using Library.Contracts.Book.GetBooks;
using MediatR;

namespace Library.Core.Requests.BookRequests.GetBooks
{
    /// <summary>
    /// Запрос на получение списка книг
    /// </summary>
    public class GetBooksQuery : GetBooksRequest, IRequest<GetBooksResponse>
    {
    }
}
