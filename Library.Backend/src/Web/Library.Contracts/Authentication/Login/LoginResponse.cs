namespace Library.Contracts.Authentication.Login
{
    /// <summary>
	/// Ответ на запрос <see cref="LoginRequest"/>
	/// </summary>
	public class LoginResponse
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="token">Токен авторизации</param>
        /// <param name="role">Роль пользователя</param>
        public LoginResponse(
            string token,
            string role)
        {
            Token = token;
            Role = role;
        }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role { get; } = default!;

        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; } = default!;
    }
}
