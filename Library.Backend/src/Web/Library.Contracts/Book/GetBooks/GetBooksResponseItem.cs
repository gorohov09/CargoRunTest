namespace Library.Contracts.Book.GetBooks
{
    /// <summary>
    /// Модель книги
    /// </summary>
    public class GetBooksResponseItem
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Жанр
        /// </summary>
        public string Genre { get; set; } = default!;

        /// <summary>
        /// Автор
        /// </summary>
        public string Author { get; set; } = default!;

        /// <summary>
        /// Издатель
        /// </summary>
        public string Publisher { get; set; } = default!;

        /// <summary>
        /// Забронирована
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Выдана
        /// </summary>
        public bool IsPicked { get; set; }
    }
}
