using Library.Contracts.Book.GetBooks;
using Library.Core.Abstractions;
using Library.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Requests.BookRequests.GetBooks
{
    /// <summary>
    /// Обработчик команды <see cref="GetBooksQuery"/>
    /// </summary>
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, GetBooksResponse>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">Контекст EF</param>
        public GetBooksQueryHandler(
            IDbContext dbContext)
            => _dbContext = dbContext;

        /// <inheritdoc/>
        public async Task<GetBooksResponse> Handle(
            GetBooksQuery request, 
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var entities = await _dbContext.Books
                .WhereIf(!string.IsNullOrWhiteSpace(request.SearchGenre),
                    x => x.Genre!.Name.ToLower().Contains(request.SearchGenre!.ToLower()))
                .WhereIf(!string.IsNullOrWhiteSpace(request.SearchPublisher),
                    x => x.Publisher!.Name.ToLower().Contains(request.SearchPublisher!.ToLower()))
                .WhereIf(!string.IsNullOrWhiteSpace(request.SearchAuthor),
                    x => x.Author!.Name.ToLower().Contains(request.SearchAuthor!.ToLower()))
                .Select(x => new GetBooksResponseItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Author = x.Author!.Name,
                    Genre = x.Genre!.Name,
                    Publisher = x.Publisher!.Name,
                    IsBlocked = x.BlockedUserId.HasValue,
                    IsPicked = x.PickedUserId.HasValue
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return new GetBooksResponse(entities);
        }
    }
}
