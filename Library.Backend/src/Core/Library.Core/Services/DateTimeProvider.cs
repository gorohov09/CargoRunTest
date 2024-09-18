using Library.Core.Abstractions;

namespace Library.Core.Services
{
    /// <summary>
	/// Провайдер даты
	/// </summary>
	public class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc/>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
