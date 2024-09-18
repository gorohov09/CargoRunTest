namespace Library.Core.Abstractions
{
    /// <summary>
	/// Контекст текущего пользователя
	/// </summary>
	public interface IUserContext
    {
        /// <summary>
        /// Идентификатор текущего пользователя
        /// </summary>
        Guid CurrentUserId { get; }

        /// <summary>
        /// Роль текущего пользователя
        /// </summary>
        Guid CurrentRoleId { get; }
    }
}
