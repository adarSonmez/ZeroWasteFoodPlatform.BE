using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Marketing");

            migrationBuilder.EnsureSchema(
                name: "Association");

            migrationBuilder.EnsureSchema(
                name: "Analytics");

            migrationBuilder.EnsureSchema(
                name: "Membership");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2047)", maxLength: 2047, nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "Analytics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuppliedAmount = table.Column<int>(type: "int", nullable: false),
                    SoldAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    LoginVerificationCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    ActiveToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginVerificationCodeExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UseMultiFactorAuthentication = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumberVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2047)", maxLength: 2047, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    CoverPhoto = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(2047)", maxLength: 2047, nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PercentDiscount = table.Column<double>(type: "float", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_BusinessId",
                        column: x => x.BusinessId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                schema: "Marketing",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalSchema: "Marketing",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "Marketing",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProducts",
                schema: "Association",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProducts", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CategoryProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Marketing",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Marketing",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                schema: "Marketing",
                table: "CategoryProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProducts_ProductId",
                schema: "Association",
                table: "CategoryProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BusinessId",
                schema: "Marketing",
                table: "Products",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OwnerId",
                schema: "Marketing",
                table: "Products",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "CategoryProducts",
                schema: "Association");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "Analytics");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Membership");
        }
    }
}
