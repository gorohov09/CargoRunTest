using Library.Domain.Exceptions;

namespace Library.Domain.Entities
{
    /// <summary>
    /// Автор
    /// </summary>
    public class Author : EntityBase
    {
        private string _name = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Наименование</param>
        public Author(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected Author()
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
