namespace Library.Core.Abstractions
{
    /// <summary>
	/// Провайдер даты
	/// </summary>
	public interface IDateTimeProvider
    {
        /// <summary>
        /// Текущее время
        /// </summary>
        DateTime UtcNow { get; }
    }
}
