using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Library.Data.PostgreSql.Extensions;

namespace Library.Data.PostgreSql.Configurations
{
    /// <summary>
	/// Конфигурация для <see cref="User"/>
	/// </summary>
	internal class UserConfiguration : EntityBaseConfiguration<User>
    {
        /// <inheritdoc/>
        public override void ConfigureChild(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", "public")
                .HasComment("Пользователь");

            builder.Property(p => p.LastName)
                .HasComment("Фамилия")
                .IsRequired();

            builder.Property(p => p.FirstName)
                .HasComment("Имя")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasComment("Электронная почта")
                .IsRequired();

            builder.Property(p => p.PasswordHash)
                .HasComment("Хеш пароля")
                .IsRequired();

            builder.Property(p => p.RoleId)
                .HasComment("Идентификатор роли");

            builder.HasOne(x => x.Role)
                .WithMany(y => y!.Users)
                .HasForeignKey(x => x.RoleId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(x => x.BlockedBooks)
                .WithOne(x => x.BlockedUser)
                .HasForeignKey(x => x.BlockedUserId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(x => x.GivenBooks)
                .WithOne(x => x.PickedUser)
                .HasForeignKey(x => x.PickedUserId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.SetPropertyAccessModeField(x => x.Role, User.RoleField);
            builder.SetPropertyAccessModeField(x => x.BlockedBooks, User.BlockedBooksField);
            builder.SetPropertyAccessModeField(x => x.GivenBooks, User.GivenBooksField);
        }
    }
}
