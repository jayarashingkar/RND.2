using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace RNDSystems.Models
{
    /// <summary>
    /// Work study column details
    /// </summary>
    public class RNDWorkStudy : IRNDModel
    {
        //check
        // not null - primary key
        //[Key]
        public int RecId { get; set; }

        //check
        //not null
        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string WorkStudyID { get; set; }

        //check
        //fk not specified in the db
        //public RNDStudyType RNDStudyType { get; set; }
        public string StudyType { get; set; }

        public string StudyTypeDesc { get; set; }


        public List<SelectListItem> StudyTypes { get; set; }

        public string StudyTitle { get; set; }
        //[StringLength(40)]
        public string StudyDesc { get; set; }


        //numeric(9,2)
        // public PlanOSCost { get; set; }
        public decimal PlanOSCost { get; set; }


        //numeric(9,2)
        //    public AcctOSCost { get; set; }

        public decimal AcctOSCost { get; set; }


        //FK RNDStudyStatus
        //public RNDStudyStatus RNDStudyStatus { get; set; }
        public string StudyStatus { get; set; }
        public string StudyStatusDesc { get; set; }
        public List<SelectListItem> Status { get; set; }


        public string  StartDate { get; set; }
        public string DueDate { get; set; }
        public string CompleteDate { get; set; }

        //fk - Plant is same as PlantState from RNDLocation
        //public RNDLocation RNDLocation { get; set; }
        public List<SelectListItem> Locations { get; set; }
        public string Plant { get; set; }
        public string PlantDesc { get; set; }

        //[StringLength(50)]
        public string TempID { get; set; }
        
        public string Experimentation { get; set; }

        public string FinalSummary { get; set; }
               
        public string Uncertainty { get; set; }
        //[NotMapped]
        

        //public string StrStartDate
        //{
        //    get { return (StartDate.HasValue) ? StartDate.Value.ToString("MM/dd/yyyy") : "-"; }
        //}
        //public string StrDueDate
        //{
        //    get { return (DateTime.TryParse(DueDate.ToString(), out DueDate)) ? DueDate.ToString("MM/dd/yyyy") : "-"; }
        //}
        //public string StrCompleteDate
        //{
        //    get { return (CompleteDate.HasValue) ? CompleteDate.Value.ToString("MM/dd/yyyy") : "-"; }
        //}

        //check for [RNDStudyScope]

        //public int scope_RecId { get; set; }
        //public string ScopeType { get; set; }
        public string StudyScope { get; set; }

        //check for [RNDStudyScope]


        #region IRNDModel

        //[StringLength(25)]
        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        #endregion

        public int total { get; set; }
    }

}
