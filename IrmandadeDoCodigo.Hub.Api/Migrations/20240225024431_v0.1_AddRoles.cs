using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrmandadeDoCodigo.Hub.Api.Migrations
{
    /// <inheritdoc />
    public partial class v01_AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO \"Roles\"(\"Id\",\"Name\",\"Slug\") VALUES (1,'user', 'user')");
            migrationBuilder.Sql($"INSERT INTO \"Roles\"(\"Id\",\"Name\",\"Slug\") VALUES (2,'admin', 'admin')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"TRUNCATE TABLE \"Roles\"");
        }
    }
}
