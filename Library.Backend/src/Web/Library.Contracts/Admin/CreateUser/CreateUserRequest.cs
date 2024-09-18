namespace Library.Contracts.Admin.CreateUser
{
    /// <summary>
    /// Запрос на создание пользователя
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = default!;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = default!;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = default!;

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
