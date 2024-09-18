using Library.Contracts.Book.TakeBook;
using MediatR;

namespace Library.Core.Requests.BookRequests.TakeBook
{
    /// <summary>
    /// Команда на выдачу книги
    /// </summary>
    public class TakeBookCommand : TakeBookRequest, IRequest
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid Id { get; set; }
    }
}
