using Library.Contracts.Admin.CreateUser;
using Library.Contracts.Authentication.Login;
using Library.Core.Requests.AdminRequests.CreateUser;
using Library.Core.Requests.AuthenticationRequests.Login;
using Library.Domain.Enums;
using Library.WebAPI.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для администратора
    /// </summary>
    public class AdminController : ApiControllerBase
    {
        /// <summary>
		/// Логин
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект логина</returns>
		[HttpPost("CreateUser")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(CreateUserResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [Permission(Privileges.CreateUser)]
        public async Task<CreateUserResponse> CreateUserAsync(
            [FromServices] IMediator mediator,
            [FromServices] IAuthorizationService authorizationService,
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            return await mediator.Send(
                new CreateUserCommand
                {
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    Email = request.Email,  
                    Password = request.Password,
                    RoleId = request.RoleId,
                },
                cancellationToken);
        }
    }
}
