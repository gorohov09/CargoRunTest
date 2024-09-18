using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Abstractions
{
    /// <summary>
	/// Контекст EF Core для приложения
	/// </summary>
    public interface IDbContext
    {
        /// <summary>
		/// Пользователи
		/// </summary>
		DbSet<User> Users { get; }

        /// <summary>
        /// Роли
        /// </summary>
        DbSet<Role> Roles { get; }

        /// <summary>
        /// Жанры
        /// </summary>
        DbSet<Genre> Genres { get; }

        /// <summary>
        /// Авторы
        /// </summary>
        DbSet<Author> Authors { get; }

        /// <summary>
        /// Издатели
        /// </summary>
        DbSet<Publisher> Publishers { get; }
        
        /// <summary>
        /// Книги
        /// </summary>
        DbSet<Book> Books { get; }

        /// <summary>
		/// Сохранить изменения в БД
		/// </summary>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Количество обновленных записей</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
