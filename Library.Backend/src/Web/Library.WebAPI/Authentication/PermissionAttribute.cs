using Library.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Library.WebAPI.Authentication
{
    /// <summary>
	/// Атрибут установки привилегии
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Привилегия
        /// </summary>
        public Privileges Privilege { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="codes">Коды привилегии</param>
        public PermissionAttribute(Privileges privilege)
            : base(PolicyNames.Permission)
            => Privilege = privilege;   
    }
}
