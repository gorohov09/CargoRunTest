using Library.Core.Abstractions;
using Library.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Requests.BookRequests.TakeBook
{
    /// <summary>
    /// Обработчик команды <see cref="TakeBookCommand"/>
    /// </summary>
    public class TakeBookCommandHandler : IRequestHandler<TakeBookCommand>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">Контекст EF</param>
        public TakeBookCommandHandler(
            IDbContext dbContext)
            => _dbContext = dbContext;

        /// <inheritdoc/>
        public async Task<Unit> Handle(TakeBookCommand request, CancellationToken cancellationToken)
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

            book.Take(user);

            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            return default;
        }
    }
}
