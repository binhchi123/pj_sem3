using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sem3.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: true),
                    Created_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceProvider",
                columns: table => new
                {
                    ServiceProviderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServiceName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProvider", x => x.ServiceProviderId);
                });

            migrationBuilder.CreateTable(
                name: "RechargePlans",
                columns: table => new
                {
                    RechargePlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServiceProviderId = table.Column<int>(nullable: false),
                    RechargePlanName = table.Column<string>(nullable: false),
                    RechargePlanValidity = table.Column<string>(nullable: false),
                    RechargePlanPrice = table.Column<int>(nullable: false),
                    RechargePlanData = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargePlans", x => x.RechargePlanId);
                    table.ForeignKey(
                        name: "FK_RechargePlans_ServiceProvider_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProvider",
                        principalColumn: "ServiceProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServiceProviderId = table.Column<int>(nullable: false),
                    RechargePlanId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(32)", nullable: false),
                    Email = table.Column<string>(type: "varchar(128)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(10)", nullable: false),
                    Password = table.Column<string>(type: "varchar(256)", nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_RechargePlans_RechargePlanId",
                        column: x => x.RechargePlanId,
                        principalTable: "RechargePlans",
                        principalColumn: "RechargePlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_ServiceProvider_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProvider",
                        principalColumn: "ServiceProviderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    DateOfMessage = table.Column<DateTime>(nullable: false),
                    Reply = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contact_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RechargeLogs",
                columns: table => new
                {
                    RechargeLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RechargePlanId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    RechargeDate = table.Column<DateTime>(nullable: false),
                    RechargeValidity = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeLogs", x => x.RechargeLogId);
                    table.ForeignKey(
                        name: "FK_RechargeLogs_RechargePlans_RechargePlanId",
                        column: x => x.RechargePlanId,
                        principalTable: "RechargePlans",
                        principalColumn: "RechargePlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RechargeLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RechargeReport",
                columns: table => new
                {
                    ReportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RechargePlanId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    DateOfRecharge = table.Column<DateTime>(nullable: false),
                    ValidTill = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeReport", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_RechargeReport_RechargePlans_RechargePlanId",
                        column: x => x.RechargePlanId,
                        principalTable: "RechargePlans",
                        principalColumn: "RechargePlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RechargeReport_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    WalletId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Wallet_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserId",
                table: "Contact",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeLogs_RechargePlanId",
                table: "RechargeLogs",
                column: "RechargePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeLogs_UserId",
                table: "RechargeLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargePlans_ServiceProviderId",
                table: "RechargePlans",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeReport_RechargePlanId",
                table: "RechargeReport",
                column: "RechargePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeReport_UserId",
                table: "RechargeReport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RechargePlanId",
                table: "Users",
                column: "RechargePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ServiceProviderId",
                table: "Users",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "RechargeLogs");

            migrationBuilder.DropTable(
                name: "RechargeReport");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RechargePlans");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "ServiceProvider");
        }
    }
}
