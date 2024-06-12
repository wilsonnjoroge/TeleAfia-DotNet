using Microsoft.EntityFrameworkCore.Migrations;

namespace TeleAfiaPersonal.Infrastructure.Migrations
{
    public partial class ReorderUserTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Step 1: Create a new temporary table with the columns in the desired order
                CREATE TABLE Users_Temp (
                    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                    FirstName NVARCHAR(100) NOT NULL,
                    LastName NVARCHAR(100) NOT NULL,
                    Email NVARCHAR(200) NOT NULL,
                    PhoneNumber NVARCHAR(15),
                    IdNumber NVARCHAR(50),
                    Location NVARCHAR(200) NOT NULL DEFAULT '',
                    DateOfBirth DATE NOT NULL DEFAULT '0001-01-01',
                    IsDeleted BIT NOT NULL DEFAULT 0,
                    IsEmailConfirmed BIT NOT NULL DEFAULT 0,
                    ResetToken NVARCHAR(MAX) NULL,
                    Password NVARCHAR(MAX) NOT NULL,
                    CreatedDate DATETIME NOT NULL,
                    UpdatedDate DATETIME NOT NULL
                );

                -- Step 2: Copy data from the original table to the new temporary table
                INSERT INTO Users_Temp (Id, FirstName, LastName, Email, PhoneNumber, IdNumber, Location, DateOfBirth, IsDeleted, IsEmailConfirmed, ResetToken, Password, CreatedDate, UpdatedDate)
                SELECT Id, FirstName, LastName, Email, PhoneNumber, IdNumber, Location, DateOfBirth, IsDeleted, IsEmailConfirmed, ResetToken, Password, CreatedDate, UpdatedDate
                FROM Users;

                -- Step 3: Drop the original table
                DROP TABLE Users;

                -- Step 4: Rename the temporary table to the original table name
                EXEC sp_rename 'Users_Temp', 'Users';
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Reverse the steps to restore the original structure

                -- Step 1: Create a new temporary table with the original order of columns
                CREATE TABLE Users_Original (
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
                    DateOfBirth DATE NOT NULL DEFAULT '0001-01-01',
                    IsDeleted BIT NOT NULL DEFAULT 0,
                    IsEmailConfirmed BIT NOT NULL DEFAULT 0,
                    ResetToken NVARCHAR(MAX) NULL
                );

                -- Step 2: Copy data from the current table to the original structure table
                INSERT INTO Users_Original (Id, FirstName, LastName, Email, PhoneNumber, IdNumber, Location, Password, CreatedDate, UpdatedDate, DateOfBirth, IsDeleted, IsEmailConfirmed, ResetToken)
                SELECT Id, FirstName, LastName, Email, PhoneNumber, IdNumber, Location, Password, CreatedDate, UpdatedDate, DateOfBirth, IsDeleted, IsEmailConfirmed, ResetToken
                FROM Users;

                -- Step 3: Drop the current table
                DROP TABLE Users;

                -- Step 4: Rename the original structure table to the original table name
                EXEC sp_rename 'Users_Original', 'Users';
            ");
        }
    }
}
