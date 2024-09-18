namespace Library.Contracts.Book.CreateBook
{
    /// <summary>
	/// Ответ на запрос <see cref="CreateBookRequest"/>
	/// </summary>
    public class CreateBookResponse
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userId">Идентификатор книги</param>
        public CreateBookResponse(Guid userId)
            => UserId = userId;

        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid UserId { get; }
    }
}
