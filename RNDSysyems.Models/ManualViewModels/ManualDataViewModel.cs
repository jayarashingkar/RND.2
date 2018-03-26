using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.ManualViewModels
{
    public class ManualDataViewModel
    {
        public string SelectedTests { get; set; }
        public List<SelectListItem> ddTestType { get; set; }
        public string TestType { get; set; }


        //public int TestingNo { get; set; }
        //public string StressKsi { get; set; }
        //public string TimeDays { get; set; }
        //public string TestStatus { get; set; }
        //public string SpeciComment { get; set; }
        //public string Operator { get; set; }
        //public DateTime? TestStartDate { get; set; }
        //public DateTime? TestEndDate { get; set; }
        //public string EntryBy { get; set; }
        //public DateTime? EntryDate { get; set; }
        //public char Completed { get; set; }
    }
}
