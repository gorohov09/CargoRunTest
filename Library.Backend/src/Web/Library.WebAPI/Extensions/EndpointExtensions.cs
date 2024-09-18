using Library.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Library.WebAPI.Extensions
{
    /// <summary>
	/// Методы расширения для <see cref="RouteEndpoint"/>
	/// </summary>
	public static class EndpointExtensions
    {
        /// <summary>
        /// Получить атрибуты у контроллера и метода.
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибуты</typeparam>
        /// <param name="context">Контекст запроса</param>
        /// <returns>Список атрибутов</returns>
        /// <exception cref="ApplicationExceptionBase"></exception>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this HttpContext? context)
        {
            if (context?.GetEndpoint() is RouteEndpoint routeEndpoint)
            {
                var actionDescriptor = routeEndpoint.Metadata
                    .OfType<ControllerActionDescriptor>()
                    .FirstOrDefault()
                    ?? throw new ApplicationExceptionBase("Дескриптор не найден");

                var attributes = GetAttributes<TAttribute>(actionDescriptor.ControllerTypeInfo);
                attributes = attributes.Concat(GetAttributes<TAttribute>(actionDescriptor.MethodInfo));
                return attributes;
            }

            return new List<TAttribute>();
        }

        private static IEnumerable<TAttribute> GetAttributes<TAttribute>(ICustomAttributeProvider memberInfo)
            => memberInfo.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>();
    }
}
