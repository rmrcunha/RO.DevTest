using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RO.DevTest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDateTimeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Products"
                ALTER COLUMN "UpdatedAt" DROP DEFAULT;
            """);

            migrationBuilder.Sql("""
                ALTER TABLE "Products"
                ALTER COLUMN "CreatedAt" DROP DEFAULT;
            """);

            migrationBuilder.Sql("""
                UPDATE "Products"
                SET "UpdatedAt" = '2000-01-01T00:00:00Z'
                WHERE "UpdatedAt" = '';
            """);

            migrationBuilder.Sql("""
                UPDATE "Products"
                SET "CreatedAt" = '2000-01-01T00:00:00Z'
                WHERE "CreatedAt" = '';
            """);

            migrationBuilder.Sql("""
                ALTER TABLE "Products"
                ALTER COLUMN "UpdatedAt" TYPE timestamp with time zone
                USING "UpdatedAt"::timestamp with time zone;
            """);

            migrationBuilder.Sql("""
                ALTER TABLE "Products"
                ALTER COLUMN "CreatedAt" TYPE timestamp with time zone
                USING "CreatedAt"::timestamp with time zone;
            """);

            migrationBuilder.Sql("""
                ALTER TABLE "Products"
                ALTER COLUMN "UpdatedAt" SET DEFAULT now();
            """);

            migrationBuilder.Sql("""
                ALTER TABLE "Products"
                ALTER COLUMN "CreatedAt" SET DEFAULT now();
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedAt",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
