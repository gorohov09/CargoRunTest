namespace Library.Domain.Entities
{
    /// <summary>
	/// Базовая сущность
	/// </summary>
    public abstract class EntityBase
    {
        /// <summary>
		/// Идентификатор сущности
		/// </summary>
		public Guid Id { get; protected set; }
    }
}
