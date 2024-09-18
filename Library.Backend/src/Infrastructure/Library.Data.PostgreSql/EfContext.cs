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

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(DefaultSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfContext).Assembly);
        }
    }
}
