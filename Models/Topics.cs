using System.ComponentModel.DataAnnotations;

namespace SkillAssessment.Models
{
    public class Topics
    {
        [Key] 
        public int Topic_Id { get; set; }
        public string Topic_Name { get; set; }
        public ICollection<Questions> Questions { get; set; }
    }
}
