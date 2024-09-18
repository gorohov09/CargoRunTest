using Library.Contracts.Admin.CreateUser;
using Library.Core.Abstractions;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Requests.AdminRequests.CreateUser
{
    /// <summary>
	/// Обработчик команды <see cref="CreateUserCommand"/>
	/// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">Контекст EF</param>
        /// <param name="passwordEncryptionService">Сервис хэширования паролей</param>
        public CreateUserCommandHandler(
            IDbContext dbContext, 
            IPasswordEncryptionService passwordEncryptionService)
        {
            _dbContext = dbContext;
            _passwordEncryptionService = passwordEncryptionService;
        }

        /// <inheritdoc/>
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var userExist = await _dbContext.Users.AnyAsync(
                x => x.Email == request.Email, 
                cancellationToken: cancellationToken);

            if (userExist)
                throw new ApplicationExceptionBase("Пользователь с данным email уже существует");


            var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken: cancellationToken)
                ?? throw new NotFoundException("Не найдена роль по Id");

            var passwordHash = _passwordEncryptionService.EncodePassword(request.Password);

            var user = new User(
                lastName: request.LastName,
                firstName: request.FirstName,
                passwordHash: passwordHash,
                email: request.Email,
                role: role);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return new CreateUserResponse(user.Id);
        }
    }
}
