using Library.Core.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Library.Core.Services
{
    /// <summary>
	/// Сервис хэширования паролей.
	/// </summary>
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        private readonly string _passwordHashSalt = "12345abcd";

        /// <inheritdoc/>
        public string EncodePassword(string password)
            => CreateHash(password, _passwordHashSalt);

        /// <inheritdoc/>
        public bool ValidatePassword(string password, string encodedPassword)
            => encodedPassword == CreateHash(password, _passwordHashSalt);

        /// <summary>
        /// Создать хэш пароля.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <param name="salt">Соль для хэширования.</param>
        /// <returns>Хэш пароля.</returns>
        private static string CreateHash(string password, string salt)
        {
            password ??= string.Empty;
            salt ??= string.Empty;

            var saltedValue = Encoding.UTF8.GetBytes($"{password}{salt}");
            using var sha = SHA512.Create();
            return Convert.ToBase64String(sha.ComputeHash(saltedValue));
        }
    }
}
