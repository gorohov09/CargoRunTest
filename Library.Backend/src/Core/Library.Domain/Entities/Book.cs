using Library.Domain.Exceptions;

namespace Library.Domain.Entities
{
    /// <summary>
    /// Книга
    /// </summary>
    public class Book : EntityBase
    {
        /// <summary>
		/// Поле для <see cref="_genre"/>
		/// </summary>
		public const string GenreField = nameof(_genre);

        /// <summary>
		/// Поле для <see cref="_publisher"/>
		/// </summary>
		public const string PublisherField = nameof(_publisher);

        /// <summary>
		/// Поле для <see cref="_author"/>
		/// </summary>
		public const string AuthorField = nameof(_author);

        /// <summary>
		/// Поле для <see cref="_blockedUser"/>
		/// </summary>
		public const string BlockedUserField = nameof(_blockedUser);

        /// <summary>
		/// Поле для <see cref="_pickedUser"/>
		/// </summary>
		public const string PickedUserField = nameof(_pickedUser);

        private string _name = default!;
        private Genre? _genre;
        private Publisher? _publisher;
        private Author? _author;
        private User? _blockedUser;
        private User? _pickedUser;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="genre">Жанр</param>
        /// <param name="publisher">Издатель</param>
        /// <param name="author">Автор</param>
        public Book(
            string name,
            Genre genre,
            Publisher publisher,
            Author author)
        {
            Name = name;
            Genre = genre;
            Publisher = publisher;
            Author = author;
        }

        /// <summary>
		/// Конструктор
		/// </summary>
		private Book()
        {
        }

        /// <summary>
		/// Название
		/// </summary>
		public string Name
        {
            get => _name;
            private set => _name = value
                    ?? throw new RequiredFieldNotSpecifiedException("Название");
        }

        /// <summary>
		/// Идентификатор жанра
		/// </summary>
		public Guid GenreId { get; private set; }

        /// <summary>
		/// Идентификатор издателя
		/// </summary>
		public Guid PublisherId { get; private set; }

        /// <summary>
		/// Идентификатор автора
		/// </summary>
		public Guid AuthorId { get; private set; }

        /// <summary>
        /// Идентификатор пользователя, забронировавшего книгу
        /// </summary>
        public Guid? BlockedUserId { get; private set; }

        /// <summary>
        /// Идентификатор пользователя, читающего книгу в библиотеке
        /// </summary>
        public Guid? PickedUserId { get; private set; }

        #region Navigation properties

        /// <summary>
        /// Жанр
        /// </summary>
        public Genre? Genre
        {
            get => _genre;
            set
            {
                _genre = value
                    ?? throw new RequiredFieldNotSpecifiedException("Жанр");
                GenreId = value.Id;
            }
        }

        /// <summary>
        /// Издатель
        /// </summary>
        public Publisher? Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value
                    ?? throw new RequiredFieldNotSpecifiedException("Издатель");
                PublisherId = value.Id;
            }
        }

        /// <summary>
        /// Автор
        /// </summary>
        public Author? Author
        {
            get => _author;
            set
            {
                _author = value
                    ?? throw new RequiredFieldNotSpecifiedException("Жанр");
                AuthorId = value.Id;
            }
        }

        /// <summary>
        /// Пользователь, забронировавший книгу
        /// </summary>
        public User? BlockedUser
        {
            get => _blockedUser;
            set
            {
                _blockedUser = value;
                BlockedUserId = value?.Id;
            }
        }

        /// <summary>
        /// Пользователь, которому выдана книга
        /// </summary>
        public User? PickedUser
        {
            get => _pickedUser;
            set
            {
                _pickedUser = value;
                PickedUserId = value?.Id;
            }
        }

        #endregion

        /// <summary>
        /// Забронировать книгу
        /// </summary>
        /// <param name="user">Пользователь, бронирующий книгу</param>
        public void Block(User blockedUser)
        {
            ArgumentNullException.ThrowIfNull(blockedUser);

            if (BlockedUserId.HasValue && BlockedUserId == blockedUser.Id)
                throw new ApplicationExceptionBase("Вы уже забронировали эту книгу");

            if (BlockedUserId.HasValue)
                throw new ApplicationExceptionBase("Книга уже забронирована другим пользователем");

            if (PickedUserId.HasValue)
                throw new ApplicationExceptionBase("Книга уже выдана другому пользователю");

            BlockedUser = blockedUser;
        }

        /// <summary>
        /// Снять бронь
        /// </summary>
        /// <param name="user">Пользователь, снимающий бронь</param>
        /// <exception cref="ApplicationExceptionBase"></exception>
        public void CancelBlock(User user)
        {
            if (!BlockedUserId.HasValue)
                throw new ApplicationExceptionBase("Для снятия брони, книга должна быть забронирована");

            if (BlockedUserId != user.Id)
                throw new ApplicationExceptionBase("Бронь может снять только пользователь, поставивший ее");

            BlockedUser = default;
        }

        /// <summary>
        /// Выдать книгу
        /// </summary>
        /// <param name="user">Пользователь, которому выдается книга</param>
        public void Give(User pickedUser)
        {
            ArgumentNullException.ThrowIfNull(pickedUser);

            if (PickedUserId.HasValue && PickedUserId == pickedUser.Id)
                throw new ApplicationExceptionBase("Текущему пользователю уже выдана книга");

            if (PickedUserId.HasValue)
                throw new ApplicationExceptionBase("Книга уже выдана другому пользователю");

            if (!BlockedUserId.HasValue)
                throw new ApplicationExceptionBase("Для выдачи книги, книга должна быть забронирована");

            if (BlockedUserId.HasValue && pickedUser.Id != BlockedUserId)
                throw new ApplicationExceptionBase("Книга уже забронирована другим пользователем");

            BlockedUser = default;
            PickedUser = pickedUser;
        }

        /// <summary>
        /// Принять книгу
        /// </summary>
        /// <param name="user">Пользователь. от которого принимают книгу</param>
        public void Take(User user)
        {
            if (!PickedUserId.HasValue)
                throw new ApplicationExceptionBase("Чтобы принять книгу, она должна быть выдана пользователю");

            if (PickedUserId != user.Id)
                throw new ApplicationExceptionBase("Можно принять книгу только от пользователя, которому книга была выдана");

            PickedUser = default;
        }
    }
}
