using System.ComponentModel.DataAnnotations;

namespace SkillAssessment.Models
{
    public class Result
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int result_id { get; set; }
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
        public int UnansweredQuestions { get; set; }
        public int WrongAnsweredQuestions { get; set; }
        public string TimeLeft { get; set; }
        public int points { get; set; }
        public User? Users { get; set; }
    }

}
