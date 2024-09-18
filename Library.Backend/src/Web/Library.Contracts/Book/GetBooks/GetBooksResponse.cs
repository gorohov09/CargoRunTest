namespace Library.Contracts.Book.GetBooks
{
    /// <summary>
    /// Ответ на получение книг
    /// </summary>
    public class GetBooksResponse
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="entities"></param>
        public GetBooksResponse(List<GetBooksResponseItem> entities)
        {
            Entities = entities;
        }

        /// <summary>
        /// Элементы списка
        /// </summary>
        public List<GetBooksResponseItem> Entities { get; }
    }
}
