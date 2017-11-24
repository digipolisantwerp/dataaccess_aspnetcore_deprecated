using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.SampleApp.Migrations
{
    public partial class lowercasedtablesandfieldswithcolumnattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appartment_Buildings_BuildingId",
                table: "Appartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Appartment_AppartmentId",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appartment",
                table: "Appartment");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "room");

            migrationBuilder.RenameTable(
                name: "Buildings",
                newName: "building");

            migrationBuilder.RenameTable(
                name: "Appartment",
                newName: "appartment");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "room",
                newName: "width");

            migrationBuilder.RenameColumn(
                name: "NumberOfDoors",
                table: "room",
                newName: "numberofdoors");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "room",
                newName: "mynewroomname");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "room",
                newName: "length");

            migrationBuilder.RenameColumn(
                name: "AppartmentId",
                table: "room",
                newName: "appartmentid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "room",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Room_AppartmentId",
                table: "room",
                newName: "IX_room_appartmentid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "building",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "building",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "appartment",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "Floor",
                table: "appartment",
                newName: "floor");

            migrationBuilder.RenameColumn(
                name: "BuildingId",
                table: "appartment",
                newName: "buildingid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "appartment",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Appartment_BuildingId",
                table: "appartment",
                newName: "IX_appartment_buildingid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_room",
                table: "room",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_building",
                table: "building",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appartment",
                table: "appartment",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_appartment_building_buildingid",
                table: "appartment",
                column: "buildingid",
                principalTable: "building",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_room_appartment_appartmentid",
                table: "room",
                column: "appartmentid",
                principalTable: "appartment",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appartment_building_buildingid",
                table: "appartment");

            migrationBuilder.DropForeignKey(
                name: "FK_room_appartment_appartmentid",
                table: "room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_room",
                table: "room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_building",
                table: "building");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appartment",
                table: "appartment");

            migrationBuilder.RenameTable(
                name: "room",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "building",
                newName: "Buildings");

            migrationBuilder.RenameTable(
                name: "appartment",
                newName: "Appartment");

            migrationBuilder.RenameColumn(
                name: "width",
                table: "Room",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "numberofdoors",
                table: "Room",
                newName: "NumberOfDoors");

            migrationBuilder.RenameColumn(
                name: "mynewroomname",
                table: "Room",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "length",
                table: "Room",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "appartmentid",
                table: "Room",
                newName: "AppartmentId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Room",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_room_appartmentid",
                table: "Room",
                newName: "IX_Room_AppartmentId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Buildings",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Buildings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "Appartment",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "floor",
                table: "Appartment",
                newName: "Floor");

            migrationBuilder.RenameColumn(
                name: "buildingid",
                table: "Appartment",
                newName: "BuildingId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Appartment",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_appartment_buildingid",
                table: "Appartment",
                newName: "IX_Appartment_BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appartment",
                table: "Appartment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appartment_Buildings_BuildingId",
                table: "Appartment",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Appartment_AppartmentId",
                table: "Room",
                column: "AppartmentId",
                principalTable: "Appartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
