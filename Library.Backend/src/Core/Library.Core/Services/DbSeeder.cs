using Library.Core.Abstractions;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace Library.Core.Services
{
    /// <summary>
	/// Сервис добавления данных в БД
	/// </summary>
	public class DbSeeder : IDbSeeder
    {
        private static readonly Type RolesEnumType = typeof(DefaultRoles);

        private readonly IReadOnlyDictionary<Guid, string> _roles = new Dictionary<Guid, string>
        {
            [DefaultRoles.AdminId] = GetDefaultValueDescription(nameof(DefaultRoles.AdminId), RolesEnumType),
            [DefaultRoles.LibrarianId] = GetDefaultValueDescription(nameof(DefaultRoles.LibrarianId), RolesEnumType),
            [DefaultRoles.ClientId] = GetDefaultValueDescription(nameof(DefaultRoles.ClientId), RolesEnumType),
        };

        private readonly IPasswordEncryptionService _passwordEncryptionService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="passwordEncryptionService">Сервис хэширования паролей.</param>
        public DbSeeder(IPasswordEncryptionService passwordEncryptionService)
            => _passwordEncryptionService = passwordEncryptionService;

        /// <inheritdoc/>
        public async Task SeedAsync(IDbContext dbContext, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dbContext);

            await SeedRolesAsync(dbContext, cancellationToken);
            await SeedRolesPrivilegesAsync(dbContext, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await SeedAdminAsync(dbContext, cancellationToken);
            SeedBookInfoAsync(dbContext, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private static async Task SeedRolesPrivilegesAsync(IDbContext dbContext, CancellationToken cancellationToken)
        {
            var existRolesInDB = await dbContext.Roles
                .Include(x => x.Privileges)
                .Where(x => DefaultRoles.RolesIdsToPrivileges.Keys.Contains(x.Id))
                .ToListAsync(cancellationToken);

            existRolesInDB.ForEach(x =>
            {
                if (!DefaultRoles.RolesIdsToPrivileges.TryGetValue(x.Id, out var privileges))
                    throw new ApplicationExceptionBase($"Не удалось получить список привилегий для роли {x.Id}");

                var currentPrivileges = x.Privileges!.Select(y => y.Privilege).ToList();
                currentPrivileges.AddRange(privileges);
                currentPrivileges = currentPrivileges.Distinct().ToList();

                x.UpdatePrivileges(currentPrivileges);
            });
        }

        private async Task SeedRolesAsync(IDbContext dbContext, CancellationToken cancellationToken)
        {
            var existRolesIdsInDB = await dbContext.Roles
                .Where(x => _roles.Keys.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            var rolesToSeed = _roles
                .Where(x => !existRolesIdsInDB.Contains(x.Key))
                .Select(x => new Role(x.Key, x.Value))
                .ToList();

            rolesToSeed.ForEach(x =>
            {
                if (!DefaultRoles.RolesIdsToPrivileges.TryGetValue(x.Id, out var privileges))
                    throw new ApplicationExceptionBase($"Не удалось получить список привилегий для роли {x.Id}");

                x.UpdatePrivileges(privileges);
            });

            await dbContext.Roles.AddRangeAsync(rolesToSeed, cancellationToken);
        }


        private async Task SeedAdminAsync(IDbContext dbContext, CancellationToken cancellationToken)
        {
            var roleAdmin = await dbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == DefaultRoles.AdminId, cancellationToken)
                ?? throw new NotFoundException("Не найдена роль администратора");

            var passwordHash = _passwordEncryptionService.EncodePassword("123456");

            var user = new User(
                lastName: "Админ",
                firstName: "Админ",
                passwordHash: passwordHash,
                email: "admin@mail.ru",
                role: roleAdmin);

            if (await dbContext.Users.AnyAsync(
                x => x.Email == user.Email,
                cancellationToken: cancellationToken))
                return;

            await dbContext.Users.AddRangeAsync(user);
        }

        private void SeedBookInfoAsync(IDbContext dbContext, CancellationToken cancellationToken)
        {
            var genre = new Genre(
                name: "Детектив");

            var publisher = new Publisher(
                name: "Юный следопыт");

            var author = new Author(
                name: "Артур Конан Дойл");

            dbContext.Genres.AddRange(genre);
            dbContext.Publishers.AddRange(publisher);
            dbContext.Authors.AddRange(author);
        }

        private static string GetDefaultValueDescription(string fieldName, Type enumWithDefaultValue)
        {
            var memberInfo = enumWithDefaultValue.GetMember(fieldName)
                ?? throw new ApplicationExceptionBase("Не удалось получить свойство");

            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length < 1 ? fieldName : ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
