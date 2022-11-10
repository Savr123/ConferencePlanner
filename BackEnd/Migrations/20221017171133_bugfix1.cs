using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class bugfix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSpeaker_Attendees_AttendeeId",
                table: "SessionSpeaker");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSpeaker_Speakers_Speakerid",
                table: "SessionSpeaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionSpeaker",
                table: "SessionSpeaker");

            migrationBuilder.DropIndex(
                name: "IX_SessionSpeaker_AttendeeId",
                table: "SessionSpeaker");

            migrationBuilder.DropColumn(
                name: "AttendeeId",
                table: "SessionSpeaker");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SessionSpeaker");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SessionAttendee");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Speakers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Speakerid",
                table: "SessionSpeaker",
                newName: "SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionSpeaker_Speakerid",
                table: "SessionSpeaker",
                newName: "IX_SessionSpeaker_SpeakerId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Attendees",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "SpeakerId",
                table: "SessionSpeaker",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Attendees",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionSpeaker",
                table: "SessionSpeaker",
                columns: new[] { "SessionId", "SpeakerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSpeaker_Speakers_SpeakerId",
                table: "SessionSpeaker",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSpeaker_Speakers_SpeakerId",
                table: "SessionSpeaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionSpeaker",
                table: "SessionSpeaker");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Speakers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SpeakerId",
                table: "SessionSpeaker",
                newName: "Speakerid");

            migrationBuilder.RenameIndex(
                name: "IX_SessionSpeaker_SpeakerId",
                table: "SessionSpeaker",
                newName: "IX_SessionSpeaker_Speakerid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Attendees",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "Speakerid",
                table: "SessionSpeaker",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "AttendeeId",
                table: "SessionSpeaker",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SessionSpeaker",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SessionAttendee",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Attendees",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionSpeaker",
                table: "SessionSpeaker",
                columns: new[] { "SessionId", "AttendeeId" });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSpeaker_AttendeeId",
                table: "SessionSpeaker",
                column: "AttendeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSpeaker_Attendees_AttendeeId",
                table: "SessionSpeaker",
                column: "AttendeeId",
                principalTable: "Attendees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSpeaker_Speakers_Speakerid",
                table: "SessionSpeaker",
                column: "Speakerid",
                principalTable: "Speakers",
                principalColumn: "id");
        }
    }
}
