using System.Security.Claims;

namespace Library.Domain.Enums
{
    /// <summary>
	/// Названия клеймов
	/// </summary>
	public static class ClaimNames
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public const string UserId = ClaimTypes.NameIdentifier;

        /// <summary>
        /// Роль
        /// </summary>
        public const string RoleId = ClaimTypes.Role;
    }
}
