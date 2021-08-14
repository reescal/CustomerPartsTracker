using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerPartsTracker.Server.Migrations
{
    public partial class GeneratedIdsAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK__Trackers",
            //    table: "Trackers");

            //migrationBuilder.DropColumn(
            //    name: "CustomerId",
            //    table: "Trackers");

            //migrationBuilder.AlterColumn<string>(
            //    name: "PoNo",
            //    table: "Trackers",
            //    type: "nvarchar(25)",
            //    maxLength: 25,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(25)",
            //    oldMaxLength: 25);

            //migrationBuilder.AlterColumn<short>(
            //    name: "TrackingId",
            //    table: "Trackers",
            //    type: "smallint",
            //    nullable: false,
            //    oldClrType: typeof(short),
            //    oldType: "smallint")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<short>(
            //    name: "PartId",
            //    table: "Parts",
            //    type: "smallint",
            //    nullable: false,
            //    oldClrType: typeof(short),
            //    oldType: "smallint")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<short>(
            //    name: "CustomerId",
            //    table: "Customers",
            //    type: "smallint",
            //    nullable: false,
            //    oldClrType: typeof(short),
            //    oldType: "smallint")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_TrackingId",
            //    table: "Trackers",
            //    column: "TrackingId");

            migrationBuilder.CreateIndex(
                name: "IX_Trackers_PartId",
                table: "Trackers",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_CustomerId",
                table: "Parts",
                column: "CustomerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Parts_Customers_CustomerId",
            //    table: "Parts",
            //    column: "CustomerId",
            //    principalTable: "Customers",
            //    principalColumn: "CustomerId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Trackers_Parts_PartId",
            //    table: "Trackers",
            //    column: "PartId",
            //    principalTable: "Parts",
            //    principalColumn: "PartId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Parts_Customers_CustomerId",
            //    table: "Parts");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Trackers_Parts_PartId",
            //    table: "Trackers");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_TrackingId",
            //    table: "Trackers");

            migrationBuilder.DropIndex(
                name: "IX_Trackers_PartId",
                table: "Trackers");

            migrationBuilder.DropIndex(
                name: "IX_Parts_CustomerId",
                table: "Parts");

            //migrationBuilder.AlterColumn<string>(
            //    name: "PoNo",
            //    table: "Trackers",
            //    type: "nvarchar(25)",
            //    maxLength: 25,
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(25)",
            //    oldMaxLength: 25,
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<short>(
            //    name: "TrackingId",
            //    table: "Trackers",
            //    type: "smallint",
            //    nullable: false,
            //    oldClrType: typeof(short),
            //    oldType: "smallint")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddColumn<short>(
            //    name: "CustomerId",
            //    table: "Trackers",
            //    type: "smallint",
            //    nullable: false,
            //    defaultValue: (short)0);

            //migrationBuilder.AlterColumn<short>(
            //    name: "PartId",
            //    table: "Parts",
            //    type: "smallint",
            //    nullable: false,
            //    oldClrType: typeof(short),
            //    oldType: "smallint")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<short>(
            //    name: "CustomerId",
            //    table: "Customers",
            //    type: "smallint",
            //    nullable: false,
            //    oldClrType: typeof(short),
            //    oldType: "smallint")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK__Trackers",
            //    table: "Trackers",
            //    column: "TrackingId");
        }
    }
}
