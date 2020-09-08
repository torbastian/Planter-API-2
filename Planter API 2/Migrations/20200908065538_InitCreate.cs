using Microsoft.EntityFrameworkCore.Migrations;

namespace Planter_API_2.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovedTypes",
                columns: table => new
                {
                    ApprovedTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedTypes", x => x.ApprovedTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Climates",
                columns: table => new
                {
                    ClimateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Climate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Climates", x => x.ClimateID);
                });

            migrationBuilder.CreateTable(
                name: "Edibles",
                columns: table => new
                {
                    EdibleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EdibleS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edibles", x => x.EdibleID);
                });

            migrationBuilder.CreateTable(
                name: "PlantTypes",
                columns: table => new
                {
                    PlantTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantTypes", x => x.PlantTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Usertypes",
                columns: table => new
                {
                    UserTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usertypes", x => x.UserTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Usertypes_UserTypeID",
                        column: x => x.UserTypeID,
                        principalTable: "Usertypes",
                        principalColumn: "UserTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    PlantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantName = table.Column<string>(nullable: true),
                    PlantType_ID = table.Column<int>(nullable: false),
                    Climate_ID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    EdibleID = table.Column<int>(nullable: false),
                    ApprovedTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.PlantID);
                    table.ForeignKey(
                        name: "FK_Plants_ApprovedTypes_ApprovedTypeID",
                        column: x => x.ApprovedTypeID,
                        principalTable: "ApprovedTypes",
                        principalColumn: "ApprovedTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Climates_Climate_ID",
                        column: x => x.Climate_ID,
                        principalTable: "Climates",
                        principalColumn: "ClimateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Edibles_EdibleID",
                        column: x => x.EdibleID,
                        principalTable: "Edibles",
                        principalColumn: "EdibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_PlantTypes_PlantType_ID",
                        column: x => x.PlantType_ID,
                        principalTable: "PlantTypes",
                        principalColumn: "PlantTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    Tips = table.Column<string>(nullable: true),
                    ApprovedTypeID = table.Column<int>(nullable: false),
                    PlantsPlantID = table.Column<int>(nullable: true),
                    PlantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK_Articles_ApprovedTypes_ApprovedTypeID",
                        column: x => x.ApprovedTypeID,
                        principalTable: "ApprovedTypes",
                        principalColumn: "ApprovedTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Plants_PlantsPlantID",
                        column: x => x.PlantsPlantID,
                        principalTable: "Plants",
                        principalColumn: "PlantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleID = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ApprovedTypeID",
                table: "Articles",
                column: "ApprovedTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PlantsPlantID",
                table: "Articles",
                column: "PlantsPlantID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleID",
                table: "Comments",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_ApprovedTypeID",
                table: "Plants",
                column: "ApprovedTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_Climate_ID",
                table: "Plants",
                column: "Climate_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_EdibleID",
                table: "Plants",
                column: "EdibleID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_PlantType_ID",
                table: "Plants",
                column: "PlantType_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_UserID",
                table: "Plants",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeID",
                table: "Users",
                column: "UserTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "ApprovedTypes");

            migrationBuilder.DropTable(
                name: "Climates");

            migrationBuilder.DropTable(
                name: "Edibles");

            migrationBuilder.DropTable(
                name: "PlantTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Usertypes");
        }
    }
}
