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
                name: "answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer", x => x.Id);
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
                    User_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_Departmenr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    User_EduLevel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_PhoneNo = table.Column<long>(type: "bigint", nullable: true),
                    User_Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QnInWords = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Option1 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Option2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Option3 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Option4 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<int>(type: "int", nullable: false),
                    topicsTopic_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QnId);
                    table.ForeignKey(
                        name: "FK_Questions_Topics_topicsTopic_Id",
                        column: x => x.topicsTopic_Id,
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
                    Assessment_TimeDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_Requested_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assessment_DateOfCompletion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assesment_Departmenr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_NoOfQuestions = table.Column<int>(type: "int", nullable: false),
                    UsersUser_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Assessment_ID);
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
                    result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    AnsweredQuestions = table.Column<int>(type: "int", nullable: false),
                    UnansweredQuestions = table.Column<int>(type: "int", nullable: false),
                    WrongAnsweredQuestions = table.Column<int>(type: "int", nullable: false),
                    TimeLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    points = table.Column<int>(type: "int", nullable: false),
                    UsersUser_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.result_id);
                    table.ForeignKey(
                        name: "FK_Results_Users_UsersUser_ID",
                        column: x => x.UsersUser_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "questionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionsQnId = table.Column<int>(type: "int", nullable: true),
                    SelectedOption = table.Column<int>(type: "int", nullable: false),
                    topic_id = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questionAnswers_Questions_QuestionsQnId",
                        column: x => x.QuestionsQnId,
                        principalTable: "Questions",
                        principalColumn: "QnId");
                    table.ForeignKey(
                        name: "FK_questionAnswers_answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "answer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_UsersUser_ID",
                table: "Assessments",
                column: "UsersUser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_questionAnswers_AnswerId",
                table: "questionAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_questionAnswers_QuestionsQnId",
                table: "questionAnswers",
                column: "QuestionsQnId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_topicsTopic_Id",
                table: "Questions",
                column: "topicsTopic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Results_UsersUser_ID",
                table: "Results",
                column: "UsersUser_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "questionAnswers");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
