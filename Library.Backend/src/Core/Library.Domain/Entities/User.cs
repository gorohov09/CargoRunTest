using Library.Domain.Exceptions;

namespace Library.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : EntityBase
    {
        /// <summary>
		/// Поле для <see cref="_blockedBooks"/>
		/// </summary>
		public const string BlockedBooksField = nameof(_blockedBooks);

        /// <summary>
		/// Поле для <see cref="_givenBooks"/>
		/// </summary>
		public const string GivenBooksField = nameof(_givenBooks);

        /// <summary>
		/// Поле для <see cref="_role"/>
		/// </summary>
		public const string RoleField = nameof(_role);

        private string _lastName = default!;
        private string _firstName = default!;
        private string _email = default!;
        private string _passwordHash = default!;
        private Role? _role;
        private List<Book>? _blockedBooks;
        private List<Book>? _givenBooks;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="lastName">Фамилия</param>
        /// <param name="firstName">Имя</param>
		/// <param name="passwordHash">Хеш пароля</param>
		/// <param name="email">Электронная почта</param>
		/// <param name="role">Роль</param>
		public User(
            string lastName,
            string firstName,
            string passwordHash,
            string email,
            Role role)
        {
            LastName = lastName;
            FirstName = firstName;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
        }

        /// <summary>
		/// Конструктор
		/// </summary>
		private User()
        {
        }

        /// <summary>
		/// Фамилия
		/// </summary>
		public string LastName
        {
            get => _lastName;
            private set => _lastName = value
                    ?? throw new RequiredFieldNotSpecifiedException("Фамилия");
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            private set => _firstName = value
                    ?? throw new RequiredFieldNotSpecifiedException("Имя");
        }

        /// <summary>
        /// Хеш пароля
        /// </summary>
        public string PasswordHash
        {
            get => _passwordHash;
            set => _passwordHash = value
                    ?? throw new RequiredFieldNotSpecifiedException("Хеш пароля");
        }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email
        {
            get => _email;
            private set => _email = value
                    ?? throw new RequiredFieldNotSpecifiedException("Электронная почта");
        }

        /// <summary>
		/// Идентификатор роли
		/// </summary>
		public Guid RoleId { get; private set; }

        #region Navigation properties

        /// <summary>
        /// Роль
        /// </summary>
        public Role? Role
        {
            get => _role;
            set
            {
                _role = value
                    ?? throw new RequiredFieldNotSpecifiedException("Роль");
                RoleId = value.Id;
            }
        }

        /// <summary>
        /// Забронированные книги пользователя
        /// </summary>
        public IReadOnlyList<Book>? BlockedBooks => _blockedBooks;

        /// <summary>
        /// Выданные книги пользователю
        /// </summary>
        public IReadOnlyList<Book>? GivenBooks => _givenBooks;

        #endregion
    }
}
