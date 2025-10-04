using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceMonitoringSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAfternoonAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AfternoonStatus",
                table: "Attendance",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AfternoonTimeIn",
                table: "Attendance",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AfternoonTimeOut",
                table: "Attendance",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfternoonStatus",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "AfternoonTimeIn",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "AfternoonTimeOut",
                table: "Attendance");
        }
    }
}
