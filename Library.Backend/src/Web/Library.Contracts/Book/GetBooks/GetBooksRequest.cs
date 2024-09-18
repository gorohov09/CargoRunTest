namespace Library.Contracts.Book.GetBooks
{
    /// <summary>
    /// Запрос на получение книг
    /// </summary>
    public class GetBooksRequest
    {
        /// <summary>
        /// Поиск по жанру
        /// </summary>
        public string? SearchGenre { get; set; }

        /// <summary>
        /// Поиск по автору
        /// </summary>
        public string? SearchAuthor { get; set; }

        /// <summary>
        /// Поиск по издателю
        /// </summary>
        public string? SearchPublisher { get; set; }
    }
}
