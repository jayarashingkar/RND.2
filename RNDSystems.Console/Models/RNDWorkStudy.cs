using System;
using System.ComponentModel.DataAnnotations;

namespace RNDSystems.Console.Models
{
    public class RNDWorkStudy : IRNDModel
    {
        [Key]
        public int RecId { get; set; }
        public string WorkStudyID { get; set; }
        public string StudyType { get; set; }
        public string StudyTitle { get; set; }
        public string StudyDesc { get; set; }
        public decimal PlanOSCost { get; set; }
        public decimal AcctOSCost { get; set; }
        public string StudyStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string Plant { get; set; }
        public string TempID { get; set; }
        public string EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}