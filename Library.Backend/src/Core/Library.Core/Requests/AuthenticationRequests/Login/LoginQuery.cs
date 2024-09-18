using Library.Contracts.Authentication.Login;
using MediatR;

namespace Library.Core.Requests.AuthenticationRequests.Login
{
    /// <summary>
	/// Запрос на логин
	/// </summary>
	public class LoginQuery : LoginRequest, IRequest<LoginResponse>
    {
    }
}
