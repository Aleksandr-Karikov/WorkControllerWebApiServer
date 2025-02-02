﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiWorkControllerServer.Migrations
{
    public partial class Period : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScreenShotPeriod",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenShotPeriod",
                table: "Users");
        }
    }
}
