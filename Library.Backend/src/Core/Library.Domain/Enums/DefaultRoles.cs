using System.ComponentModel;

namespace Library.Domain.Enums
{
    /// <summary>
	/// Идентификаторы ролей пол умолчанию
	/// </summary>
    public static class DefaultRoles
    {
        /// <summary>
		/// Идентификатор роли "Администратор"
		/// </summary>
		[Description("Администратор")]
        public static readonly Guid AdminId = new("e15a85fd-4736-4b05-b215-576ce2386f27");

        /// <summary>
		/// Идентификатор роли "Библиотекарь"
		/// </summary>
		[Description("Библиотекарь")]
        public static readonly Guid LibrarianId = new("8a3ee818-0de0-4269-952a-2478cf8c76ce");

        /// <summary>
        /// Идентификатор роли "Клиент"
        /// </summary>
        [Description("Клиент")]
        public static readonly Guid ClientId = new("8375b0de-10a7-4684-8bb2-317cb24dae43");

        /// <summary>
		/// Идентификатор ролей к списку привилегий
		/// </summary>
		public static readonly IReadOnlyDictionary<Guid, List<Privileges>> RolesIdsToPrivileges =
            new Dictionary<Guid, List<Privileges>>
            {
                [AdminId] = new()
                {
                    Privileges.CreateUser,
                    Privileges.DeleteUser,
                    Privileges.SetPassword,
                },

                [LibrarianId] = new()
                {
                    Privileges.CreateBook,
                    Privileges.GiveBook,
                    Privileges.TakeBook,

                },
                [ClientId] = new()
                {
                    Privileges.BlockBook,
                    Privileges.CancelBook,
                },
            };
    }
}
