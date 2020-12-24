using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlAccesoPersonal.Data.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46d94d45-d30f-42f6-99bd-10ae13126805", "1b8e3113-d563-415e-a44e-ffaf9af0521f", "admin", "admin" },
                    { "30a23d9d-4e5e-4fab-8d6b-30ee04030e90", "72f7cb1e-c2d0-477a-8c20-7aeeee819767", "auditor", "auditor" },
                    { "d9fa2c77-fec1-452b-b375-8a9bd415d399", "209aa6ad-ce01-4e36-be7d-e786cca86811", "user", "user" },
                    { "e80c2272-fa07-46db-9b09-bc8690a66a8d", "5332885e-7033-4619-9ba1-925659629924", "register", "register" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30a23d9d-4e5e-4fab-8d6b-30ee04030e90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46d94d45-d30f-42f6-99bd-10ae13126805");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9fa2c77-fec1-452b-b375-8a9bd415d399");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e80c2272-fa07-46db-9b09-bc8690a66a8d");
        }
    }
}
