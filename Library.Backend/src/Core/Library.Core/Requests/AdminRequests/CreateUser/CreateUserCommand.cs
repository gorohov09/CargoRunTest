using Library.Contracts.Admin.CreateUser;
using MediatR;

namespace Library.Core.Requests.AdminRequests.CreateUser
{
    /// <summary>
    /// Команда на создание пользователя
    /// </summary>
    public class CreateUserCommand : CreateUserRequest, IRequest<CreateUserResponse>
    {
    }
}
