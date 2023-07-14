using System.ComponentModel.DataAnnotations;

namespace SkillAssessment.Models
{
    public class StartAssessment
    {
        [Key]
        public int Question_ID { get; set; }
        public string Question_Name { get; set; }
        public string Option_1 { get; set; }
        public string Option_2 { get; set; }
        public string Option_3 { get; set; }
        public string Option_4 { get; set; }
        public Nullable<int> Question_Answer { get; set; }
        public int Points { get; set; }
        public ICollection<Assessment> Assessment { get; set; } 
    }
}
