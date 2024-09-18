using Library.Domain.Exceptions;

namespace Library.Domain.Entities
{
    /// <summary>
    /// Издатель
    /// </summary>
    public class Publisher : EntityBase
    {
        private string _name = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Наименование</param>
        public Publisher(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected Publisher()
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
