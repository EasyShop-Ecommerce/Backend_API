﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ProductImages");
        }
    }
}
