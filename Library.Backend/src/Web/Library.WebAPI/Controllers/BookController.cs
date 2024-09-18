using Library.Contracts.Book.CreateBook;
using Library.Contracts.Book.GetBooks;
using Library.Contracts.Book.GiveBook;
using Library.Contracts.Book.TakeBook;
using Library.Core.Requests.BookRequests.BlockBook;
using Library.Core.Requests.BookRequests.CancelBlockBook;
using Library.Core.Requests.BookRequests.CreateBook;
using Library.Core.Requests.BookRequests.GetBooks;
using Library.Core.Requests.BookRequests.GiveBook;
using Library.Core.Requests.BookRequests.TakeBook;
using Library.Domain.Enums;
using Library.WebAPI.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для книги
    /// </summary>
    public class BookController : ApiControllerBase
    {
        /// <summary>
		/// Получить книги
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Список книг</returns>
		[HttpGet("GetBooks")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetBooksResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<GetBooksResponse> GetBooksAsync(
            [FromServices] IMediator mediator,
            [FromQuery] GetBooksRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            return await mediator.Send(
                new GetBooksQuery
                {
                    SearchAuthor = request.SearchAuthor,
                    SearchGenre = request.SearchGenre,
                    SearchPublisher = request.SearchPublisher
                },
                cancellationToken);
        }

        /// <summary>
		/// Создать книгу
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект логина</returns>
		[HttpPost("CreateBook")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(CreateBookResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [Permission(Privileges.CreateBook)]
        public async Task<CreateBookResponse> CreateBookAsync(
            [FromServices] IMediator mediator,
            [FromBody] CreateBookRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            return await mediator.Send(
                new CreateBookCommand
                {
                    Name = request.Name,
                    AuthorId = request.AuthorId,
                    GenreId = request.GenreId,
                    PublisherId = request.PublisherId,
                },
                cancellationToken);
        }

        /// <summary>
		/// Забронировать книгу
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
        /// <param name="id">Идентификатор книги</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Объект логина</returns>
		[HttpPut("BlockedBook/{id}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [Permission(Privileges.BlockBook)]
        public async Task BlockedBookAsync(
            [FromServices] IMediator mediator,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            await mediator.Send(
                new BlockBookCommand
                {
                    Id = id,
                },
                cancellationToken);
        }

        /// <summary>
		/// Выдать книгу
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
        /// <param name="id">Идентификатор книги</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>-</returns>
		[HttpPut("GiveBook/{id}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [Permission(Privileges.GiveBook)]
        public async Task GiveBookAsync(
            [FromServices] IMediator mediator,
            [FromRoute] Guid id,
            [FromBody] GiveBookRequest request,
            CancellationToken cancellationToken)
        {
            await mediator.Send(
                new GiveBookCommand
                {
                    Id = id,
                    UserId = request.UserId,
                },
                cancellationToken);
        }

        /// <summary>
		/// Получить книгу
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
        /// <param name="id">Идентификатор книги</param>
		/// <param name="request">Запрос</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>-</returns>
		[HttpPut("TakeBook/{id}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [Permission(Privileges.TakeBook)]
        public async Task TakeBookAsync(
            [FromServices] IMediator mediator,
            [FromRoute] Guid id,
            [FromBody] TakeBookRequest request,
            CancellationToken cancellationToken)
        {
            await mediator.Send(
                new TakeBookCommand
                {
                    Id = id,
                    UserId = request.UserId,
                },
                cancellationToken);
        }

        /// <summary>
		/// Снять бронь с книги
		/// </summary>
		/// <param name="mediator">Медиатор CQRS</param>
        /// <param name="id">Идентификатор книги</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>-</returns>
		[HttpPut("CancelBlockBook/{id}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [Permission(Privileges.CancelBook)]
        public async Task CancelBlockBookAsync(
            [FromServices] IMediator mediator,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            await mediator.Send(
                new CancelBlockBookCommand
                {
                    Id = id,
                },
                cancellationToken);
        }
    }
}
