using Library.Core.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Core.Services
{
    public class TokenAuthenticationService : ITokenAuthenticationService
    {
        /// <summary>
		/// Вид авторизации по токену (Bearer)
		/// </summary>
		public string AuthTokenType => "Bearer";

        /// <summary>
        /// УЦ токенов
        /// </summary>
        public string Authority => "Auth";

        /// <summary>
        /// Аудитория токенов. Для каждого типа токенов задаем разную аудиторию.
        /// </summary>
        public string Audience => "Aud";

        /// <summary>
        /// Выпускатель токенов
        /// </summary>
        public string ClaimsIssuer => "Iss";

        /// <summary>
        /// Приватный ключ шифрования.
        /// </summary>
        protected SecurityKey SecurityKey
        {
            get
            {
                var privateKey = "Very very long and super secret key, veeeery loooooooooooooooooooooooooooooooooooooooooooonger";
                var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
                return issuerSigningKey;
            }
        }

        /// <summary>
        /// Ключ шифрования с шифром.
        /// </summary>
        protected SigningCredentials SigningCredentials => new(SecurityKey, SecurityAlgorithms.HmacSha512);

        /// <summary>
        /// Создать токен для пользователя с клаймами.
        /// </summary>
        /// <param name="identity">Пользователь с клаймами.</param>
        /// <param name="tokenType">Тип токена.</param>
        /// <returns>Токен для пользователя с клаймами.</returns>
        public string CreateToken(ClaimsIdentity identity)
        {
            var handler = new JwtSecurityTokenHandler();
            var createdOn = DateTime.UtcNow;
            var expiresOn = createdOn + GetTokenLifetimeByTokenType();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = ClaimsIssuer,
                Audience = "audience",
                SigningCredentials = SigningCredentials,
                Subject = identity,
                Expires = expiresOn,
                IssuedAt = createdOn,
                NotBefore = createdOn,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        /// <summary>
        /// Получить параметры валидации токенов.
        /// </summary>
        /// <param name="tokenType">Тип токенов.</param>
        /// <returns>Параметры валидации токенов.</returns>
        public TokenValidationParameters GetTokenValidationParameters() => new()
        {
            IssuerSigningKey = SecurityKey,
            ValidAudience = "audience",
            ValidIssuer = ClaimsIssuer,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(0),
        };

        /// <summary>
        /// Получить время жизни токена по его типу.
        /// </summary>
        /// <param name="tokenType">Тип токена.</param>
        /// <returns>Время жизни.</returns>
        private TimeSpan GetTokenLifetimeByTokenType()
            => TimeSpan.FromMinutes(5);
    }
}
