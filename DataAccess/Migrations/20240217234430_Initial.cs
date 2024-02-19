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
                name: "Membership");

            migrationBuilder.EnsureSchema(
                name: "Marketing");

            migrationBuilder.EnsureSchema(
                name: "Association");

            migrationBuilder.EnsureSchema(
                name: "Analytics");

            migrationBuilder.CreateTable(
                name: "Businesses",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2047)", maxLength: 2047, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    CoverPhoto = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    LoginVerificationCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    LoginVerificationCodeExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UseMultiFactorAuthentication = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumberVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

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
                name: "Customers",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    LoginVerificationCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    LoginVerificationCodeExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UseMultiFactorAuthentication = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumberVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(2047)", maxLength: 2047, nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_Product", x => x.Id);
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
                name: "CategoryProduct",
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
                        name: "FK_CategoryProduct_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
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
                        name: "FK_CategoryProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoredProducts",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoredProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoredProducts_Product_Id",
                        column: x => x.Id,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProducts",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentDiscount = table.Column<double>(type: "float", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalSchema: "Membership",
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Product_Id",
                        column: x => x.Id,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProducts_ProductId",
                schema: "Association",
                table: "CategoryProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_BusinessId",
                schema: "Marketing",
                table: "StoreProducts",
                column: "BusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "CategoryProducts",
                schema: "Association");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "MonitoredProducts",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "Analytics");

            migrationBuilder.DropTable(
                name: "StoreProducts",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "Businesses",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
