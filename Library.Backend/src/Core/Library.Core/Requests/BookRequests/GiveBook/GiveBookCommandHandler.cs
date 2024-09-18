using Library.Core.Abstractions;
using Library.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Requests.BookRequests.GiveBook
{
    /// <summary>
    /// Обработчик команды <see cref="GiveBookCommand"/>
    /// </summary>
    public class GiveBookCommandHandler : IRequestHandler<GiveBookCommand>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">Контекст EF</param>
        /// <param name="userContext">Контекст пользователя</param>
        public GiveBookCommandHandler(
            IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(GiveBookCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var book = await _dbContext.Books
                .FirstOrDefaultAsync(x => x.Id == request.Id,
                 cancellationToken: cancellationToken)
                 ?? throw new NotFoundException("Не найдена книга по Id");

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId,
                 cancellationToken: cancellationToken)
                 ?? throw new NotFoundException("Не найден пользователь по Id");

            book.Give(user);

            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            return default;
        }
    }
}
