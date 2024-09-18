using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.PostgreSql.Configurations
{
    /// <summary>
	/// Базовая конфигурация для базовой сущности <see cref="EntityBase"/>
	/// </summary>
	/// <typeparam name="TEntity">Тип сущности</typeparam>
	internal abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
         where TEntity : EntityBase
    {
        protected const string ListEnumCommand = "'{}'::integer[]";
        protected const string ListGuidCommand = "'{}'::uuid[]";

        private const string GuidCommand = "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)";

        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureId(builder);
            ConfigureChild(builder);
        }

        /// <summary>
        /// Конфигурация сущности, не считая полей базового класса  <see cref="EntityBase"/>
        /// </summary>
        /// <param name="builder">Строитель конфигурации</param>
        public abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// Конфигурация идентификатора сущности
        /// </summary>
        /// <param name="builder">Строитель конфигурации</param>
        protected virtual void ConfigureId(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql(GuidCommand);
        }
    }
}
