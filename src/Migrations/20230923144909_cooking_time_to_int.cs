using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBook.Migrations
{
    /// <inheritdoc />
    public partial class cooking_time_to_int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookingTime",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "CookingTimeMins",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookingTimeMins",
                table: "Recipes");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "CookingTime",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
