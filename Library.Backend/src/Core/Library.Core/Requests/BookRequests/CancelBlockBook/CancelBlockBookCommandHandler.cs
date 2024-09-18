using Library.Core.Abstractions;
using Library.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Requests.BookRequests.CancelBlockBook
{
    /// <summary>
    /// Обработчик команды <see cref="CancelBlockBookCommand"/>
    /// </summary>
    public class CancelBlockBookCommandHandler : IRequestHandler<CancelBlockBookCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">Контекст EF</param>
        /// <param name="userContext">Контекст пользователя</param>
        public CancelBlockBookCommandHandler(
            IDbContext dbContext,
            IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(CancelBlockBookCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var book = await _dbContext.Books
                .FirstOrDefaultAsync(x => x.Id == request.Id,
                 cancellationToken: cancellationToken)
                 ?? throw new NotFoundException("Не найдена книга по Id");

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId,
                 cancellationToken: cancellationToken)
                 ?? throw new NotFoundException("Не найден пользователь по Id");

            book.CancelBlock(user);

            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            return default;
        }
    }
}
