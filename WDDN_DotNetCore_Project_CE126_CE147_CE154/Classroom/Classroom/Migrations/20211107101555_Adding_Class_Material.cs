using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Classroom.Migrations
{
    public partial class Adding_Class_Material : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassCode = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Document = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsAssignment = table.Column<bool>(type: "bit", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "Date", nullable: true),
                    Deadline = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
