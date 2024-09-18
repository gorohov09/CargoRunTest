namespace Library.Contracts.Book.GiveBook
{
    /// <summary>
    /// Запрос на выдачу книги
    /// </summary>
    public class GiveBookRequest
    {
        /// <summary>
		/// Идентификатор пользователя, кому выдают книгу
		/// </summary>
		public Guid UserId { get; set; }
    }
}
