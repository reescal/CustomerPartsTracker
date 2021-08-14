using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerPartsTracker.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.CreateTable(
        //        name: "Customers",
        //        columns: table => new
        //        {
        //            CustomerId = table.Column<short>(type: "smallint", nullable: false),
        //            CustomerName = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_CustomerId", x => x.CustomerId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Parts",
        //        columns: table => new
        //        {
        //            PartId = table.Column<short>(type: "smallint", nullable: false),
        //            PartNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
        //            PartName = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
        //            CustomerId = table.Column<short>(type: "smallint", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_PartId", x => x.PartId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Trackers",
        //        columns: table => new
        //        {
        //            TrackingId = table.Column<short>(type: "smallint", nullable: false),
        //            CustomerId = table.Column<short>(type: "smallint", nullable: false),
        //            PartId = table.Column<short>(type: "smallint", nullable: false),
        //            SerialNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
        //            SecondaryId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
        //            LotNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
        //            PoNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
        //            ProjectNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
        //            ReceivedDate = table.Column<DateTime>(type: "date", nullable: false),
        //            ShippedDate = table.Column<DateTime>(type: "date", nullable: true),
        //            QaNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Trackers", x => x.TrackingId);
        //        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropTable(
        //        name: "Customers");

        //    migrationBuilder.DropTable(
        //        name: "Parts");

        //    migrationBuilder.DropTable(
        //        name: "Trackers");
        }
    }
}
