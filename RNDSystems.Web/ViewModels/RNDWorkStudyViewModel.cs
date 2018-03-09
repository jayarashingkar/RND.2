using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RNDSystems.Web.ViewModels
{
    public class RNDWorkStudyViewModel
    {
        public int RecId { get; set; }

        public string WorkStudyID { get; set; }

        public string StudyType { get; set; }

        public string StudyTypeDesc { get; set; }

        public string StudyTitle { get; set; }

        public string StudyDesc { get; set; }

        public decimal PlanOSCost { get; set; }

        public decimal AcctOSCost { get; set; }

        public string StudyStatus { get; set; }

        public string StudyStatusDesc { get; set; }

        public string StartDate { get; set; }

        public string DueDate { get; set; }

        public string CompleteDate { get; set; }

        public string Plant { get; set; }

        public string PlantDesc { get; set; }

        public string TempID { get; set; }

        public string Experimentation { get; set; }

        public string FinalSummary { get; set; }
        public string Uncertainty { get; set; }

    }
}