using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.ManualViewModels
{
    public class OpticalMountViewModel
    {
        public int RecID { get; set; }
        public string WorkStudyID { get; set; }

        public string SelectedTests { get; set; }
        public int TestingNo { get; set; }
        public string SpeciComment { get; set; }
        public string Operator { get; set; }
        public DateTime? TestDate { get; set; }
        public string TimeHrs { get; set; }
        public string TimeMns { get; set; }
        public string EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public char Completed { get; set; }
        
        public string Alloy { get; set; }
        public string Temper { get; set; }
        public string CustPart { get; set; }
        public decimal UACPart { get; set; }
        public int total { get; set; }

    }
}

