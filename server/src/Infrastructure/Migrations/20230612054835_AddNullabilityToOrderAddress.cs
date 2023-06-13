using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddNullabilityToOrderAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_address_address_id",
                table: "order");

            migrationBuilder.AlterColumn<Guid>(
                name: "address_id",
                table: "order",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("26e52bab-4c7e-4f92-b46e-8708b1936302"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("26e52bab-4c7e-4f92-b46e-8708b1e4f302"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("9dd4a5ef-bb70-4acd-8c3b-1f5942bedcd0"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("bb3de5c0-daa1-4b80-8a01-6558005db3c5"),
                column: "deleted_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("f6112b01-2549-4a80-98a8-8bb1eaeca160"),
                column: "deleted_at",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "fk_order_address_address_id",
                table: "order",
                column: "address_id",
                principalTable: "address",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_address_address_id",
                table: "order");

            migrationBuilder.AlterColumn<Guid>(
                name: "address_id",
                table: "order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("26e52bab-4c7e-4f92-b46e-8708b1936302"),
                column: "deleted_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("26e52bab-4c7e-4f92-b46e-8708b1e4f302"),
                column: "deleted_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("9dd4a5ef-bb70-4acd-8c3b-1f5942bedcd0"),
                column: "deleted_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("bb3de5c0-daa1-4b80-8a01-6558005db3c5"),
                column: "deleted_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: new Guid("f6112b01-2549-4a80-98a8-8bb1eaeca160"),
                column: "deleted_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "fk_order_address_address_id",
                table: "order",
                column: "address_id",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
