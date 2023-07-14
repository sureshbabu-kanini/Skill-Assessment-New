using System.ComponentModel.DataAnnotations;

namespace SkillAssessment.Models
{
    public class Result
    {
        [Key]
        public int Result_Id { get; set; }
        public int TotalPoints { get; set; }
        public int AnsweredCount { get; set; }
        public int UnansweredCount { get; set; }
        public int SkippedCount { get; set; }
        public Questions Questions { get; set; }
    }

}
