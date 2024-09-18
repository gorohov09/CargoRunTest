using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.PostgreSql.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Author"/>
    /// </summary>
    internal class AuthorConfiguration : EntityBaseConfiguration<Author>
    {
        public override void ConfigureChild(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("author", "public")
               .HasComment("Автор");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasComment("Название")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
