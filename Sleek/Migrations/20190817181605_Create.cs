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
                    usr_cusid = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Status_usr_staid",
                        column: x => x.usr_staid,
                        principalTable: "Status",
                        principalColumn: "sta_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    act_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    act_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    act_cusid = table.Column<int>(nullable: true),
                    act_usrid = table.Column<int>(nullable: true),
                    act_description = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    act_type = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.act_id);
                    table.ForeignKey(
                        name: "FK_Activity_Customer_act_cusid",
                        column: x => x.act_cusid,
                        principalTable: "Customer",
                        principalColumn: "cus_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activity_User_act_usrid",
                        column: x => x.act_usrid,
                        principalTable: "User",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    pro_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    pro_cusid = table.Column<int>(nullable: true),
                    pro_usrid = table.Column<int>(nullable: true),
                    pro_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    pro_description = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    pro_localpath = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    pro_remotepath = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    pro_sourcepath = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    pro_staid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.pro_id);
                    table.ForeignKey(
                        name: "FK_Project_Customer_pro_cusid",
                        column: x => x.pro_cusid,
                        principalTable: "Customer",
                        principalColumn: "cus_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Status_pro_staid",
                        column: x => x.pro_staid,
                        principalTable: "Status",
                        principalColumn: "sta_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_User_pro_usrid",
                        column: x => x.pro_usrid,
                        principalTable: "User",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ord_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ord_proid = table.Column<int>(nullable: true),
                    ord_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ord_subject = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ord_description = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    ord_comments = table.Column<string>(unicode: false, nullable: true),
                    ord_staid = table.Column<int>(nullable: true),
                    CustomerCusId = table.Column<int>(nullable: true),
                    UserUsrId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ord_id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerCusId",
                        column: x => x.CustomerCusId,
                        principalTable: "Customer",
                        principalColumn: "cus_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Project_ord_proid",
                        column: x => x.ord_proid,
                        principalTable: "Project",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Status_ord_staid",
                        column: x => x.ord_staid,
                        principalTable: "Status",
                        principalColumn: "sta_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User_UserUsrId",
                        column: x => x.UserUsrId,
                        principalTable: "User",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('Activity', RESEED, 10000);");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Customer', RESEED, 10000);");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Order', RESEED, 10000);");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Project', RESEED, 10000);");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Request', RESEED, 10000);");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Status', RESEED, 10000);");
            migrationBuilder.Sql("DBCC CHECKIDENT ('User', RESEED, 10000);");

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "sta_id", "sta_code", "sta_description" },
                values: new object[] { 10000, "Active", "Active" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "sta_id", "sta_code", "sta_description" },
                values: new object[] { 10001, "Inactive", "Inactive" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "cus_id", "cus_address1", "cus_address2", "cus_city", "cus_company", "cus_email", "cus_fax", "cus_first", "cus_last", "cus_note", "cus_number", "cus_password", "cus_phone", "cus_staid", "cus_state", "cus_zip", "cus_zip4" },
                values: new object[] { 10000, "123 Main Street", "Suite 100", "Anytown", "Company One", "customer@companyone.com", "(800) 555-1212", "John", "Doe", "Seed Data", "A1013100", null, "(800) 555-1212", 10000, "CA", "12345", "1234" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "cus_id", "cus_address1", "cus_address2", "cus_city", "cus_company", "cus_email", "cus_fax", "cus_first", "cus_last", "cus_note", "cus_number", "cus_password", "cus_phone", "cus_staid", "cus_state", "cus_zip", "cus_zip4" },
                values: new object[] { 10001, "231 Main Street", "Suite 200", "Anytown", "Company Two", "customer@companytwo.com", "(800) 555-1212", "Mary", "Doe", "Seed Data", "A2013100", null, "(800) 555-1212", 10000, "CA", "12345", "1234" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "cus_id", "cus_address1", "cus_address2", "cus_city", "cus_company", "cus_email", "cus_fax", "cus_first", "cus_last", "cus_note", "cus_number", "cus_password", "cus_phone", "cus_staid", "cus_state", "cus_zip", "cus_zip4" },
                values: new object[] { 10002, "331 Main Street", "Suite 300", "Anytown", "Company Three", "customer@companythree.com", "(800) 555-1212", "Davis", "Doe", "Seed Data", "A3013100", null, "(800) 555-1212", 10000, "CA", "12345", "1234" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "usr_id", "usr_cusid", "usr_email", "usr_first", "usr_last", "usr_name", "usr_note", "usr_password", "usr_role", "usr_staid", "usr_title", "usr_token", "usr_tokendate" },
                values: new object[] { 10000, 10000, "admin@companyone.com", "Admin", "Account", "admin", "Default Administrator Account", "Testing123", "Admin", 10000, "Administrator", null, null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "usr_id", "usr_cusid", "usr_email", "usr_first", "usr_last", "usr_name", "usr_note", "usr_password", "usr_role", "usr_staid", "usr_title", "usr_token", "usr_tokendate" },
                values: new object[] { 10001, 10000, "manager@companyone.com", "Manager", "Account", "manager", "Default Managment Account", "Testing123", "Manager", 10000, "Manager", null, null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "usr_id", "usr_cusid", "usr_email", "usr_first", "usr_last", "usr_name", "usr_note", "usr_password", "usr_role", "usr_staid", "usr_title", "usr_token", "usr_tokendate" },
                values: new object[] { 10002, 10000, "user@companyone.com", "User", "Account", "user", "Default User Account", "Testing123", "User", 10000, "Associate", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_act_cusid",
                table: "Activity",
                column: "act_cusid");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_act_usrid",
                table: "Activity",
                column: "act_usrid");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_cus_staid",
                table: "Customer",
                column: "cus_staid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerCusId",
                table: "Order",
                column: "CustomerCusId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ord_proid",
                table: "Order",
                column: "ord_proid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ord_staid",
                table: "Order",
                column: "ord_staid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserUsrId",
                table: "Order",
                column: "UserUsrId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_pro_cusid",
                table: "Project",
                column: "pro_cusid");

            migrationBuilder.CreateIndex(
                name: "IX_Project_pro_staid",
                table: "Project",
                column: "pro_staid");

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
