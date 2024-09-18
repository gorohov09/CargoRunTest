using Library.Contracts.Authentication.Login;
using Library.Core.Requests.AuthenticationRequests.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using LoginRequest = Library.Contracts.Authentication.Login.LoginRequest;

namespace Library.WebAPI.Controllers
{
    /// <summary>
	/// Контроллер для аутентификации
	/// </summary>
	[AllowAnonymous]
    public class AuthenticationController : ApiControllerBase
    {
        /// <summary>
		/// Логин
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект логина</returns>
		[HttpPost("Login")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(LoginResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<LoginResponse> LoginAsync(
            [FromServices] IMediator mediator,
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            return await mediator.Send(
                new LoginQuery
                {
                    Email = request.Email,
                    Password = request.Password,
                },
                cancellationToken);
        }
    }
}
