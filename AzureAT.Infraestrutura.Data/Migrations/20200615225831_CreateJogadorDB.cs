using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureAT.Infraestrutura.Data.Migrations
{
    public partial class CreateJogadorDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogador",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    FreeAgent = table.Column<bool>(nullable: false),
                    ImageUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogador", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogador");
        }
    }
}
