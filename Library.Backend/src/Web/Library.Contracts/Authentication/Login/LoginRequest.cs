namespace Library.Contracts.Authentication.Login
{
    /// <summary>
	/// Запрос на логин
	/// </summary>
	public class LoginRequest
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
