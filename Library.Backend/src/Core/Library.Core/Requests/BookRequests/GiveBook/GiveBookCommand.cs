using Library.Contracts.Book.GiveBook;
using MediatR;

namespace Library.Core.Requests.BookRequests.GiveBook
{
    /// <summary>
    /// Команда на выдачу книги
    /// </summary>
    public class GiveBookCommand : GiveBookRequest, IRequest
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid Id { get; set; }
    }
}
