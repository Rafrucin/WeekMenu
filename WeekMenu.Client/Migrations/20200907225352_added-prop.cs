using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeekMenu.Client.Migrations
{
    public partial class addedprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipesDBSet",
                columns: table => new
                {
                    RecipeModelID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeName = table.Column<string>(maxLength: 200, nullable: false),
                    How = table.Column<string>(nullable: true),
                    Ingredients = table.Column<string>(nullable: true),
                    IsVegan = table.Column<bool>(nullable: false),
                    IsBreakfast = table.Column<bool>(nullable: false),
                    IsSecondBreakfast = table.Column<bool>(nullable: false),
                    IsLunch = table.Column<bool>(nullable: false),
                    IsAfternoonTea = table.Column<bool>(nullable: false),
                    IsDinner = table.Column<bool>(nullable: false),
                    RecipeImageName = table.Column<string>(nullable: true),
                    RecipeImageFile = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesDBSet", x => x.RecipeModelID);
                });

            migrationBuilder.CreateTable(
                name: "DaysDBSet",
                columns: table => new
                {
                    DayMenuModelId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BreakfastRecipeModelID = table.Column<int>(nullable: true),
                    SecondBreakfastRecipeModelID = table.Column<int>(nullable: true),
                    LunchRecipeModelID = table.Column<int>(nullable: true),
                    AfternoonTeaRecipeModelID = table.Column<int>(nullable: true),
                    DinnerRecipeModelID = table.Column<int>(nullable: true),
                    DayMenuDate = table.Column<DateTime>(nullable: false),
                    DayMenuOwner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysDBSet", x => x.DayMenuModelId);
                    table.ForeignKey(
                        name: "FK_DaysDBSet_RecipesDBSet_AfternoonTeaRecipeModelID",
                        column: x => x.AfternoonTeaRecipeModelID,
                        principalTable: "RecipesDBSet",
                        principalColumn: "RecipeModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DaysDBSet_RecipesDBSet_BreakfastRecipeModelID",
                        column: x => x.BreakfastRecipeModelID,
                        principalTable: "RecipesDBSet",
                        principalColumn: "RecipeModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DaysDBSet_RecipesDBSet_DinnerRecipeModelID",
                        column: x => x.DinnerRecipeModelID,
                        principalTable: "RecipesDBSet",
                        principalColumn: "RecipeModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DaysDBSet_RecipesDBSet_LunchRecipeModelID",
                        column: x => x.LunchRecipeModelID,
                        principalTable: "RecipesDBSet",
                        principalColumn: "RecipeModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DaysDBSet_RecipesDBSet_SecondBreakfastRecipeModelID",
                        column: x => x.SecondBreakfastRecipeModelID,
                        principalTable: "RecipesDBSet",
                        principalColumn: "RecipeModelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaysDBSet_AfternoonTeaRecipeModelID",
                table: "DaysDBSet",
                column: "AfternoonTeaRecipeModelID");

            migrationBuilder.CreateIndex(
                name: "IX_DaysDBSet_BreakfastRecipeModelID",
                table: "DaysDBSet",
                column: "BreakfastRecipeModelID");

            migrationBuilder.CreateIndex(
                name: "IX_DaysDBSet_DinnerRecipeModelID",
                table: "DaysDBSet",
                column: "DinnerRecipeModelID");

            migrationBuilder.CreateIndex(
                name: "IX_DaysDBSet_LunchRecipeModelID",
                table: "DaysDBSet",
                column: "LunchRecipeModelID");

            migrationBuilder.CreateIndex(
                name: "IX_DaysDBSet_SecondBreakfastRecipeModelID",
                table: "DaysDBSet",
                column: "SecondBreakfastRecipeModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaysDBSet");

            migrationBuilder.DropTable(
                name: "RecipesDBSet");
        }
    }
}
