using Library.Domain.Exceptions;
using Library.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Library.WebAPI.Authentication
{
    /// <summary>
	/// Базовый обработчик для обработчиков требований с поддержкой атрибутов
	/// </summary>
	public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute>
        : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
        where TAttribute : Attribute
    {
        /// <summary>
        /// Обработать требование
        /// </summary>
        /// <param name="authContext">Контекст авторизации</param>
        /// <param name="requirement">Требование</param>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext authContext, TRequirement requirement)
        {
            var httpContext = authContext.Resource as HttpContext
                ?? throw new ApplicationExceptionBase("Контекст запроса отсутствует");

            var attributes = httpContext.GetAttributes<TAttribute>().ToList();

            return HandleRequirementAsync(
                authContext,
                httpContext,
                requirement,
                attributes,
                httpContext.RequestAborted);
        }

        /// <summary>
        /// Обработать требование
        /// </summary>
        /// <param name="authContext">Контекст авторизации</param>
        /// <param name="httpContext">Контекст запроса</param>
        /// <param name="requirement">Требование</param>
        /// <param name="attributes">Атрибуты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>-</returns>
        protected abstract Task HandleRequirementAsync(
            AuthorizationHandlerContext authContext,
            HttpContext httpContext,
            TRequirement requirement,
            IEnumerable<TAttribute> attributes,
            CancellationToken cancellationToken);
    }
}
