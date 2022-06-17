using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class constraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var CreditHoursLimit = "ALTER TABLE Courses ADD CONSTRAINT CreditHoursLimit CHECK(CreditHours BETWEEN 2 AND 7);";
            var SGenderValues = "ALTER TABLE Students ADD CONSTRAINT SGenderValues CHECK(Gender IN ('Female', 'Male'));";
            var MajorValues = "ALTER TABLE Students ADD CONSTRAINT MajorValues CHECK(Major IN ('Computer Science', 'Software Engineering', 'Artificial Intelligence'));";
            var StatusValues = "ALTER TABLE Students ADD CONSTRAINT StatusValues CHECK(Status IN ('Active', 'Inactive'));";
            var GPALimit = "ALTER TABLE Students ADD CONSTRAINT GPALimit CHECK(GPA BETWEEN 2 AND 5);";
            var TGenderValues = "ALTER TABLE Teachers ADD CONSTRAINT TGenderValues CHECK(Gender IN ('Female', 'Male'));";
            var DepartmentValues = "ALTER TABLE Teachers ADD CONSTRAINT DepartmentValues CHECK(Department IN ('Computer Science', 'Software Engineering', 'Artificial Intelligence'));";
            var SalaryLimit = "ALTER TABLE Teachers ADD CONSTRAINT SalaryLimit CHECK(Salary BETWEEN 5000 AND 15000);";

            migrationBuilder.Sql(CreditHoursLimit);
            migrationBuilder.Sql(SGenderValues);
            migrationBuilder.Sql(MajorValues);
            migrationBuilder.Sql(StatusValues);
            migrationBuilder.Sql(GPALimit);
            migrationBuilder.Sql(TGenderValues);
            migrationBuilder.Sql(DepartmentValues);
            migrationBuilder.Sql(SalaryLimit);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
