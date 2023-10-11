using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_Core充血模型.Migrations
{
    public partial class addBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_Chinese = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_English = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body_Chinese = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body_English = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
