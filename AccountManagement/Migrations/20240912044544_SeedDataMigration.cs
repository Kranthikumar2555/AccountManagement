using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "914Wg44sPcaNbcTfwEQbJ5JNdNIxB3GvSwAx0Ga2+sI7aTmx9Y/eSxMsA9+eTyTrNfbWmzMuaZJufsDMP/AV7R/7M1Ppnv9DpIRONBZSarDDGzrka6G6hhpbJkfi+DymAqcR/66oaoFfmeb6giuC5nbWW8Jce/gIC6Dd8FwK9XoGn8N2r3WtBcqTPUpTCdJ1Dn/TYYOnbxKHqzPSsMr91Be1tVVcb4OQnBlgUPpZWHQhpKypzoqGeXt5MhXU51yW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "gbwxz9jPelLmM4LdIVBoHs8GfVJlnAv+V7NAT6bxen59Aqk5WopzW2dVo2PL+Ovn2fj5MQmQwGXxjom0zqZ4YA==");
        }
    }
}
