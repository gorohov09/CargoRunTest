using System.Linq.Expressions;

namespace Library.Core.Extensions
{
    /// <summary>
	/// Расширения <see cref="IQueryable"/>
	/// </summary>
	public static class IQueryableExtensions
    {
        /// <summary>
		/// Условная фильтрация последовательности
		/// </summary>
		/// <typeparam name="T">Тип сущностей IQueryable</typeparam>
		/// <param name="query">IQueryable</param>
		/// <param name="condition">Проводить или нет фильтрацию</param>
		/// <param name="predicate">Предикат для отбора элементов</param>
		/// <returns>Отфильтрованное выражение</returns>
		public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
            => condition ? query.Where(predicate) : query;
    }
}
