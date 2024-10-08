﻿using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Library.Data.PostgreSql.Extensions;

namespace Library.Data.PostgreSql.Configurations
{
    /// <summary>
	/// Конфигурация для <see cref="RolePrivilege"/>
	/// </summary>
	internal class RolePrivilegeConfiguration : EntityBaseConfiguration<RolePrivilege>
    {
        /// <inheritdoc/>
        public override void ConfigureChild(EntityTypeBuilder<RolePrivilege> builder)
        {
            builder.ToTable("role_privilege", "public")
                .HasComment("Право доступа для роли");

            builder.Property(p => p.RoleId)
                .IsRequired()
                .HasComment("Идентификатор роли");

            builder.Property(p => p.Privilege)
                .IsRequired()
                .HasComment("Право доступа");

            builder.HasOne(x => x.Role)
                .WithMany(y => y!.Privileges)
                .HasForeignKey(x => x.RoleId)
                .HasPrincipalKey(y => y!.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.SetPropertyAccessModeField(x => x.Role, RolePrivilege.RoleField);
        }
    }
}
