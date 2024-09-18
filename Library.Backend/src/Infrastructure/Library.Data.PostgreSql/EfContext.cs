using Library.Core.Abstractions;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.PostgreSql
{
    /// <summary>
    /// Контекст EF Core для приложения
    /// </summary>
    public class EfContext : DbContext, IDbContext
    {
        private const string DefaultSchema = "public";

        /// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="options">Параметры подключения к БД</param>
		public EfContext(
            DbContextOptions<EfContext> options)
            : base(options)
        {
        }

        /// <summary>
		/// Пользователи
		/// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
		/// Роли
		/// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Жанры
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Авторы
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Издатели
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Книги
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(DefaultSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfContext).Assembly);
        }
    }
}
