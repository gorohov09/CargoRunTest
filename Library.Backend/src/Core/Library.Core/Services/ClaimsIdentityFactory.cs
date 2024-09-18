using Library.Core.Abstractions;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using System.Security.Claims;

namespace Library.Core.Services
{
    /// <summary>
	/// Фабрика ClaimsPrincipal для пользователей.
	/// </summary>
	public class ClaimsIdentityFactory : IClaimsIdentityFactory
    {
        /// <inheritdoc/>
        public ClaimsIdentity CreateClaimsIdentity(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (user.Role == null)
                throw new NotIncludedException(nameof(user.Role));

            var claims = new List<Claim>
            {
                new(ClaimNames.UserId, user.Id.ToString(), ClaimValueTypes.String),
                new(ClaimNames.RoleId, user.Role.Id.ToString(), ClaimValueTypes.String),
            };

            return new ClaimsIdentity(claims);
        }
    }
}
