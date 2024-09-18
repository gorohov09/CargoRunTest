using Library.Data.PostgreSql.Extensions;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.PostgreSql.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Book"/>
    /// </summary>
    internal class BookConfiguration : EntityBaseConfiguration<Book>
    {
        /// <inheritdoc/>
        public override void ConfigureChild(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("book", "public")
                .HasComment("Книга");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasComment("Название")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(p => p.AuthorId)
                .HasComment("Идентификатор автора");

            builder.Property(p => p.GenreId)
                .HasComment("Идентификатор жанра");

            builder.Property(p => p.PublisherId)
                .HasComment("Идентификатор издателя");

            builder.Property(p => p.BlockedUserId)
                .HasComment("Идентификатор пользователя, забронировавшего книгу");

            builder.Property(p => p.PickedUserId)
                .HasComment("Идентификатор пользователя, читающего книгу в библиотеке");

            builder.HasOne(x => x.Genre)
                .WithMany()
                .HasForeignKey(x => x.GenreId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.Publisher)
                .WithMany()
                .HasForeignKey(x => x.PublisherId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.BlockedUser)
                .WithMany(x => x.BlockedBooks)
                .HasForeignKey(x => x.BlockedUserId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.PickedUser)
                .WithMany(x => x.GivenBooks)
                .HasForeignKey(x => x.PickedUserId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.SetPropertyAccessModeField(x => x.Genre, Book.GenreField);
            builder.SetPropertyAccessModeField(x => x.Author, Book.AuthorField);
            builder.SetPropertyAccessModeField(x => x.Publisher, Book.PublisherField);
            builder.SetPropertyAccessModeField(x => x.BlockedUser, Book.BlockedUserField);
            builder.SetPropertyAccessModeField(x => x.PickedUser, Book.PickedUserField);
        }
    }
}
