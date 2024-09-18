using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Data.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "author",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Название")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_author", x => x.id);
                },
                comment: "Автор");

            migrationBuilder.CreateTable(
                name: "genre",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Название")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genre", x => x.id);
                },
                comment: "Жанр");

            migrationBuilder.CreateTable(
                name: "publisher",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Название")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publisher", x => x.id);
                },
                comment: "Издатель");

            migrationBuilder.CreateTable(
                name: "role",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Наименование")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                },
                comment: "Роль");

            migrationBuilder.CreateTable(
                name: "role_privilege",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор роли"),
                    privilege = table.Column<int>(type: "integer", nullable: false, comment: "Право доступа")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_privilege", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_privilege_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "role",
                        principalColumn: "id");
                },
                comment: "Право доступа для роли");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    last_name = table.Column<string>(type: "text", nullable: false, comment: "Фамилия"),
                    first_name = table.Column<string>(type: "text", nullable: false, comment: "Имя"),
                    password_hash = table.Column<string>(type: "text", nullable: false, comment: "Хеш пароля"),
                    email = table.Column<string>(type: "text", nullable: false, comment: "Электронная почта"),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор роли")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "role",
                        principalColumn: "id");
                },
                comment: "Пользователь");

            migrationBuilder.CreateTable(
                name: "book",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Название"),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор жанра"),
                    publisher_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор издателя"),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор автора"),
                    blocked_user_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор пользователя, забронировавшего книгу"),
                    picked_user_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор пользователя, читающего книгу в библиотеке")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book", x => x.id);
                    table.ForeignKey(
                        name: "fk_book_author_author_id",
                        column: x => x.author_id,
                        principalSchema: "public",
                        principalTable: "author",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_book_genre_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "public",
                        principalTable: "genre",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_book_publisher_publisher_id",
                        column: x => x.publisher_id,
                        principalSchema: "public",
                        principalTable: "publisher",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_book_user_blocked_user_id",
                        column: x => x.blocked_user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_book_user_picked_user_id",
                        column: x => x.picked_user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                },
                comment: "Книга");

            migrationBuilder.CreateIndex(
                name: "ix_book_author_id",
                schema: "public",
                table: "book",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_blocked_user_id",
                schema: "public",
                table: "book",
                column: "blocked_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_genre_id",
                schema: "public",
                table: "book",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_picked_user_id",
                schema: "public",
                table: "book",
                column: "picked_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_publisher_id",
                schema: "public",
                table: "book",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_privilege_role_id",
                schema: "public",
                table: "role_privilege",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_id",
                schema: "public",
                table: "user",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book",
                schema: "public");

            migrationBuilder.DropTable(
                name: "role_privilege",
                schema: "public");

            migrationBuilder.DropTable(
                name: "author",
                schema: "public");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "public");

            migrationBuilder.DropTable(
                name: "publisher",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "role",
                schema: "public");
        }
    }
}
