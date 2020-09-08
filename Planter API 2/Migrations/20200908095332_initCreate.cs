using Microsoft.EntityFrameworkCore.Migrations;

namespace Planter_API_2.Migrations
{
    public partial class initCreate : Migration
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
                    FK_UserTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Usertypes_FK_UserTypeID",
                        column: x => x.FK_UserTypeID,
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
                    FK_PlantTypeID = table.Column<int>(nullable: false),
                    FK_ClimateID = table.Column<int>(nullable: false),
                    FK_UserID = table.Column<int>(nullable: false),
                    FK_EdibleID = table.Column<int>(nullable: false),
                    FK_ApprovedTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.PlantID);
                    table.ForeignKey(
                        name: "FK_Plants_ApprovedTypes_FK_ApprovedTypeID",
                        column: x => x.FK_ApprovedTypeID,
                        principalTable: "ApprovedTypes",
                        principalColumn: "ApprovedTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Climates_FK_ClimateID",
                        column: x => x.FK_ClimateID,
                        principalTable: "Climates",
                        principalColumn: "ClimateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Edibles_FK_EdibleID",
                        column: x => x.FK_EdibleID,
                        principalTable: "Edibles",
                        principalColumn: "EdibleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_PlantTypes_FK_PlantTypeID",
                        column: x => x.FK_PlantTypeID,
                        principalTable: "PlantTypes",
                        principalColumn: "PlantTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Users_FK_UserID",
                        column: x => x.FK_UserID,
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
                    FK_ApprovedTypeID = table.Column<int>(nullable: false),
                    FK_PlantsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK_Articles_ApprovedTypes_FK_ApprovedTypeID",
                        column: x => x.FK_ApprovedTypeID,
                        principalTable: "ApprovedTypes",
                        principalColumn: "ApprovedTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Plants_FK_PlantsID",
                        column: x => x.FK_PlantsID,
                        principalTable: "Plants",
                        principalColumn: "PlantID");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_ArticleID = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_FK_ArticleID",
                        column: x => x.FK_ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_FK_ApprovedTypeID",
                table: "Articles",
                column: "FK_ApprovedTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_FK_PlantsID",
                table: "Articles",
                column: "FK_PlantsID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FK_ArticleID",
                table: "Comments",
                column: "FK_ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_FK_ApprovedTypeID",
                table: "Plants",
                column: "FK_ApprovedTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_FK_ClimateID",
                table: "Plants",
                column: "FK_ClimateID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_FK_EdibleID",
                table: "Plants",
                column: "FK_EdibleID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_FK_PlantTypeID",
                table: "Plants",
                column: "FK_PlantTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_FK_UserID",
                table: "Plants",
                column: "FK_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FK_UserTypeID",
                table: "Users",
                column: "FK_UserTypeID");
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
