using Library.Contracts.Book.CreateBook;
using Library.Core.Abstractions;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Requests.BookRequests.CreateBook
{
    /// <summary>
	/// Обработчик команды <see cref="CreateBookCommand"/>
	/// </summary>
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookResponse>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">Контекст EF</param>
        public CreateBookCommandHandler(
            IDbContext dbContext)
            => _dbContext = dbContext;

        /// <inheritdoc/>
        public async Task<CreateBookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == request.GenreId, 
                cancellationToken: cancellationToken)
                ?? throw new NotFoundException("Не найден жанр по Id");

            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.AuthorId, 
                cancellationToken: cancellationToken)
               ?? throw new NotFoundException("Не найден автор по Id");

            var publisher = await _dbContext.Publishers.FirstOrDefaultAsync(x => x.Id == request.PublisherId, 
                cancellationToken: cancellationToken)
               ?? throw new NotFoundException("Не найден издатель по Id");

            var book = new Book(
                name: request.Name,
                genre: genre,
                publisher: publisher,
                author: author);

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return new CreateBookResponse(book.Id);
        }
    }
}
