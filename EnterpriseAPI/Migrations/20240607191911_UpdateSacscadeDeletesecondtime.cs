using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseAPI.Migrations
{
    public partial class UpdateSacscadeDeletesecondtime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedListings_Listings_ListingId",
                table: "SavedListings");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedListings_Listings_ListingId",
                table: "SavedListings",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "ListingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedListings_Listings_ListingId",
                table: "SavedListings");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedListings_Listings_ListingId",
                table: "SavedListings",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "ListingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
