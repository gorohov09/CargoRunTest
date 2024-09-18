namespace Library.Contracts.Admin.CreateUser
{
    /// <summary>
	/// Ответ на запрос <see cref="CreateUserRequest"/>
	/// </summary>
    public class CreateUserResponse
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        public CreateUserResponse(Guid userId)
            => UserId = userId;

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; }
    }
}
