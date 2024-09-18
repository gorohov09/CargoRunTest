using MediatR;

namespace Library.Core.Requests.BookRequests.CancelBlockBook
{
    /// <summary>
    /// Команда на снятие брони книги
    /// </summary>
    public class CancelBlockBookCommand : IRequest
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid Id { get; set; }
    }
}
