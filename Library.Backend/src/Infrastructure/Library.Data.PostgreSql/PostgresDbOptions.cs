﻿namespace Library.Data.PostgreSql
{
    /// <summary>
	/// Конфиг проекта
	/// </summary>
	public class PostgresDbOptions
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string ConnectionString { get; set; } = default!;
    }
}
