namespace Library.Contracts.Book.TakeBook
{
    /// <summary>
    /// Запрос на получение книги
    /// </summary>
    public class TakeBookRequest
    {
        /// <summary>
		/// Идентификатор пользователя, от которого принимают книгу
		/// </summary>
		public Guid UserId { get; set; }
    }
}
