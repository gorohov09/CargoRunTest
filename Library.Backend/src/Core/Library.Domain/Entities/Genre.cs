using Library.Domain.Exceptions;

namespace Library.Domain.Entities
{
    /// <summary>
    /// Жанр
    /// </summary>
    public class Genre : EntityBase
    {
        private string _name = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Наименование</param>
        public Genre(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected Genre()
        {
        }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value)
                ? throw new RequiredFieldNotSpecifiedException("Наименование")
                : value;
        }
    }
}
