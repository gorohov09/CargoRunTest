using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.PostgreSql.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Publisher"/>
    /// </summary>
    internal class PublishHouseConfiguration : EntityBaseConfiguration<Publisher>
    {
        public override void ConfigureChild(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("publisher", "public")
               .HasComment("Издатель");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasComment("Название")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
