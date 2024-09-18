namespace Library.Contracts.Book.CreateBook
{
    /// <summary>
    /// Запрос на создание книги
    /// </summary>
    public class CreateBookRequest
    {
        /// <summary>
        /// Навзание книги
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
		/// Идентификатор жанра
		/// </summary>
		public Guid GenreId { get; set; }

        /// <summary>
		/// Идентификатор издателя
		/// </summary>
		public Guid PublisherId { get; set; }

        /// <summary>
		/// Идентификатор автора
		/// </summary>
		public Guid AuthorId { get; set; }
    }
}
