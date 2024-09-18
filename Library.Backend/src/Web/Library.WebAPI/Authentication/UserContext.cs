using Library.Core.Abstractions;
using System.Security.Claims;

namespace Library.WebAPI.Authentication
{
    /// <summary>
	/// Контекст текущего пользователя
	/// </summary>
	public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="httpContextAccessor">Адаптер Http-context'а</param>
        public UserContext(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        /// <inheritdoc/>
        public Guid CurrentUserId => Guid.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)
            ? userId
            : Guid.Empty;

        /// <inheritdoc/>
		public Guid CurrentRoleId => Guid.TryParse(User?.FindFirst(ClaimTypes.Role)?.Value, out var roleId)
            ? roleId
            : Guid.Empty;

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    }
}
