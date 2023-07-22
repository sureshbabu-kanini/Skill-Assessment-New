﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Timers;

namespace SkillAssessment.Models
{
    public class Assessment
    {
        [Key]
        public int Assessment_ID { get; set; }

        public string Assessment_SelectedTopic { get; set; }

        public string Assessment_SelectedLevel { get; set; }

        public int Assessment_Points { get; set; }

        public string Assessment_TimeDuration { get; set; }

        public DateTime Assessment_Requested_Date { get; set; }

        public DateTime Assessment_DateOfCompletion { get; set; }

        public string Assesment_Departmenr { get; set; }

        public int Assessment_NoOfQuestions { get; set; }
        public User? Users { get; set; }
    }
}
