namespace Library.Domain.Enums
{
    /// <summary>
	/// Права доступа
	/// </summary>
	public enum Privileges
    {
        /// <summary>
        /// Создание пользователя
        /// </summary>
        CreateUser = 1,

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        DeleteUser = 2,

        /// <summary>
        /// Установление пароля
        /// </summary>
        SetPassword = 3,

        /// <summary>
        /// Создание книги
        /// </summary>
        CreateBook = 10,

        /// <summary>
        /// Забронировать книгу
        /// </summary>
        BlockBook = 11,

        /// <summary>
        /// Выдать книгу
        /// </summary>
        GiveBook = 12,

        /// <summary>
        /// Получить книгу
        /// </summary>
        TakeBook = 13,

        /// <summary>
        /// Снять бронь с книги
        /// </summary>
        CancelBook = 14,
    }
}
