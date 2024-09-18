using MediatR;

namespace Library.Core.Requests.BookRequests.BlockBook
{
    /// <summary>
    /// Команда на бронирование книги
    /// </summary>
    public class BlockBookCommand : IRequest
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid Id { get; set; }
    }
}
