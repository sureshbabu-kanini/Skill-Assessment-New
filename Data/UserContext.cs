using Microsoft.EntityFrameworkCore;
using System;

namespace SkillAssessment.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Topics> Topics { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answer> answer { get; set; }
        public DbSet<QuestionAnswer> questionAnswers { get; set; }
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
    }

}
