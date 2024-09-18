using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.PostgreSql.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Genre"/>
    /// </summary>
    internal class GenreConfiguration : EntityBaseConfiguration<Genre>
    {
        public override void ConfigureChild(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("genre", "public")
               .HasComment("Жанр");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasComment("Название")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
