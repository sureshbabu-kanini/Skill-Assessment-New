using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillAssessment.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StartAssessment",
                columns: table => new
                {
                    Question_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Option_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Option_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Option_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Option_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question_Answer = table.Column<int>(type: "int", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartAssessment", x => x.Question_ID);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Topic_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Topic_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    User_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    User_Departmenr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    User_EduLevel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_PhoneNo = table.Column<long>(type: "bigint", nullable: false),
                    User_Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QnID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Option1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Option2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Option3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Option4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<int>(type: "int", nullable: true),
                    Explaination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicsTopic_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QnID);
                    table.ForeignKey(
                        name: "FK_Questions_Topics_TopicsTopic_Id",
                        column: x => x.TopicsTopic_Id,
                        principalTable: "Topics",
                        principalColumn: "Topic_Id");
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Assessment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assessment_SelectedTopic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_SelectedLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_Points = table.Column<int>(type: "int", nullable: false),
                    Assessment_TimeDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Assessment_Requested_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assessment_DateOfCompletion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assesment_Departmenr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_NoOfQuestions = table.Column<int>(type: "int", nullable: false),
                    UsersUser_ID = table.Column<int>(type: "int", nullable: true),
                    StartAssessmentQuestion_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Assessment_ID);
                    table.ForeignKey(
                        name: "FK_Assessments_StartAssessment_StartAssessmentQuestion_ID",
                        column: x => x.StartAssessmentQuestion_ID,
                        principalTable: "StartAssessment",
                        principalColumn: "Question_ID");
                    table.ForeignKey(
                        name: "FK_Assessments_Users_UsersUser_ID",
                        column: x => x.UsersUser_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Result_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    AnsweredCount = table.Column<int>(type: "int", nullable: false),
                    UnansweredCount = table.Column<int>(type: "int", nullable: false),
                    SkippedCount = table.Column<int>(type: "int", nullable: false),
                    QuestionsQnID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Result_Id);
                    table.ForeignKey(
                        name: "FK_Results_Questions_QuestionsQnID",
                        column: x => x.QuestionsQnID,
                        principalTable: "Questions",
                        principalColumn: "QnID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_StartAssessmentQuestion_ID",
                table: "Assessments",
                column: "StartAssessmentQuestion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_UsersUser_ID",
                table: "Assessments",
                column: "UsersUser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TopicsTopic_Id",
                table: "Questions",
                column: "TopicsTopic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Results_QuestionsQnID",
                table: "Results",
                column: "QuestionsQnID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "StartAssessment");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
