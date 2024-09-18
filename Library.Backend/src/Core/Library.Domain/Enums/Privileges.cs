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
    }
}
