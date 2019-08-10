using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sleek.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    act_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    act_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    act_cusid = table.Column<int>(nullable: false),
                    act_usrid = table.Column<int>(nullable: false),
                    act_description = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    act_type = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.act_id);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    req_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    req_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    req_type = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    req_first = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    req_last = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    req_address = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    req_city = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    req_state = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    req_zip = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    req_phone = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    req_email = table.Column<string>(unicode: false, maxLength: 160, nullable: false),
                    req_subject = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    req_content = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.req_id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    sta_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sta_code = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    sta_description = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.sta_id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    cus_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cus_number = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    cus_company = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    cus_first = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    cus_last = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    cus_address1 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    cus_address2 = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    cus_city = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    cus_state = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    cus_zip = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    cus_zip4 = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
                    cus_phone = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    cus_fax = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    cus_email = table.Column<string>(unicode: false, maxLength: 160, nullable: true),
                    cus_password = table.Column<string>(unicode: false, maxLength: 256, nullable: true),
                    cus_note = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    cus_staid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.cus_id);
                    table.ForeignKey(
                        name: "FK_Customer_Status_cus_staid",
                        column: x => x.cus_staid,
                        principalTable: "Status",
                        principalColumn: "sta_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    usr_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    usr_cusid = table.Column<int>(nullable: false),
                    usr_first = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    usr_last = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    usr_title = table.Column<string>(maxLength: 30, nullable: true),
                    usr_name = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    usr_email = table.Column<string>(unicode: false, maxLength: 160, nullable: true),
                    usr_password = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    usr_note = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    usr_role = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    usr_token = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    usr_tokendate = table.Column<DateTime>(type: "datetime", nullable: true),
                    usr_staid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.usr_id);
                    table.ForeignKey(
                        name: "FK_User_Customer_usr_cusid",
                        column: x => x.usr_cusid,
                        principalTable: "Customer",
                        principalColumn: "cus_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Status_usr_staid",
                        column: x => x.usr_staid,
                        principalTable: "Status",
                        principalColumn: "sta_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    pro_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    pro_cusid = table.Column<int>(nullable: false),
                    pro_usrid = table.Column<int>(nullable: false),
                    pro_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    pro_description = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    pro_localpath = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    pro_remotepath = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    pro_sourcepath = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    pro_staid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.pro_id);
                    table.ForeignKey(
                        name: "FK_Project_Customer_pro_cusid",
                        column: x => x.pro_cusid,
                        principalTable: "Customer",
                        principalColumn: "cus_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_Status_pro_staid",
                        column: x => x.pro_staid,
                        principalTable: "Status",
                        principalColumn: "sta_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_User_pro_usrid",
                        column: x => x.pro_usrid,
                        principalTable: "User",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ord_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ord_cusid = table.Column<int>(nullable: false),
                    ord_usrid = table.Column<int>(nullable: false),
                    ord_proid = table.Column<int>(nullable: false),
                    ord_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ord_subject = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ord_description = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    ord_comments = table.Column<string>(unicode: false, nullable: true),
                    ord_staid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ord_id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_ord_cusid",
                        column: x => x.ord_cusid,
                        principalTable: "Customer",
                        principalColumn: "cus_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Project_ord_proid",
                        column: x => x.ord_proid,
                        principalTable: "Project",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Status_ord_staid",
                        column: x => x.ord_staid,
                        principalTable: "Status",
                        principalColumn: "sta_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_User_ord_usrid",
                        column: x => x.ord_usrid,
                        principalTable: "User",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_cus_staid",
                table: "Customer",
                column: "cus_staid",
                unique: true,
                filter: "[cus_staid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ord_cusid",
                table: "Order",
                column: "ord_cusid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ord_proid",
                table: "Order",
                column: "ord_proid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ord_staid",
                table: "Order",
                column: "ord_staid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ord_usrid",
                table: "Order",
                column: "ord_usrid");

            migrationBuilder.CreateIndex(
                name: "IX_Project_pro_cusid",
                table: "Project",
                column: "pro_cusid");

            migrationBuilder.CreateIndex(
                name: "IX_Project_pro_staid",
                table: "Project",
                column: "pro_staid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_pro_usrid",
                table: "Project",
                column: "pro_usrid");

            migrationBuilder.CreateIndex(
                name: "IX_User_usr_cusid",
                table: "User",
                column: "usr_cusid");

            migrationBuilder.CreateIndex(
                name: "IX_User_usr_staid",
                table: "User",
                column: "usr_staid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
