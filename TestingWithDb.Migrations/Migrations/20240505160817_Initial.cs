using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingWithDb.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_pk", x => x.id);
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pk", x => x.id);
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "product_favorite",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_favorite_pk", x => x.id);
                    table.ForeignKey(
                        name: "product_favorite_product_fk",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "product_favorite_user_fk",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "product_review",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_review_pk", x => x.id);
                    table.ForeignKey(
                        name: "product_review_product_fk",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "product_review_user_fk",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "ix_product_favorite_product_id",
                table: "product_favorite",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_favorite_user_id",
                table: "product_favorite",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_review_product_id",
                table: "product_review",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_review_user_id",
                table: "product_review",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_favorite");

            migrationBuilder.DropTable(
                name: "product_review");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
