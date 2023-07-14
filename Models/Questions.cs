using MessagePack;

namespace SkillAssessment.Models
{
    public class Questions
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int QnID { get; set; }
        public string Qn { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public Nullable<int> Answer { get; set; }
        public string Explaination { get; set; }
        public Topics? Topics { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}
