using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeleAfiaPersonal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename existing PasswordHash column to Password
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            // Add the DateOfBirth column as date type
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            // Add the Location column after IdNumber
            migrationBuilder.Sql(@"
                -- Step 1: Create a new table with the desired column order
                CREATE TABLE Users_New (
                    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                    FirstName NVARCHAR(100) NOT NULL,
                    LastName NVARCHAR(100) NOT NULL,
                    Email NVARCHAR(200) NOT NULL,
                    PhoneNumber NVARCHAR(15),
                    IdNumber NVARCHAR(50),
                    Location NVARCHAR(200) NOT NULL DEFAULT '',
                    Password NVARCHAR(MAX) NOT NULL,
                    CreatedDate DATETIME NOT NULL,
                    UpdatedDate DATETIME NOT NULL,
                    DateOfBirth DATE NOT NULL DEFAULT '0001-01-01'
                );

                -- Step 2: Copy data from the existing table to the new table
                INSERT INTO Users_New (Id, FirstName, LastName, Email, PhoneNumber, IdNumber, Location, Password, CreatedDate, UpdatedDate, DateOfBirth)
                SELECT Id, FirstName, LastName, Email, PhoneNumber, IdNumber, '', Password, CreatedDate, UpdatedDate, '0001-01-01'
                FROM Users;

                -- Step 3: Drop the existing table
                DROP TABLE Users;

                -- Step 4: Rename the new table to the original table name
                EXEC sp_rename 'Users_New', 'Users';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the Location column
            migrationBuilder.Sql(@"
                -- Step 1: Create a new table without the Location column
                CREATE TABLE Users_Old (
                    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                    FirstName NVARCHAR(100) NOT NULL,
                    LastName NVARCHAR(100) NOT NULL,
                    Email NVARCHAR(200) NOT NULL,
                    PhoneNumber NVARCHAR(15),
                    IdNumber NVARCHAR(50),
                    Password NVARCHAR(MAX) NOT NULL,
                    CreatedDate DATETIME NOT NULL,
                    UpdatedDate DATETIME NOT NULL,
                    DateOfBirth DATE NOT NULL DEFAULT '0001-01-01'
                );

                -- Step 2: Copy data from the current table to the old table
                INSERT INTO Users_Old (Id, FirstName, LastName, Email, PhoneNumber, IdNumber, Password, CreatedDate, UpdatedDate, DateOfBirth)
                SELECT Id, FirstName, LastName, Email, PhoneNumber, IdNumber, Password, CreatedDate, UpdatedDate, DateOfBirth
                FROM Users;

                -- Step 3: Drop the current table
                DROP TABLE Users;

                -- Step 4: Rename the old table back to the original table name
                EXEC sp_rename 'Users_Old', 'Users';
            ");

            // Revert the column rename from Password back to PasswordHash
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");
        }
    }
}
