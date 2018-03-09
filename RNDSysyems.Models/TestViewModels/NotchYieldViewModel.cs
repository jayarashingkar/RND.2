using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.TestViewModels
{
    public class NotchYieldViewModel
    {
        public int RecID { get; set; }
        public string WorkStudyID { get; set; }
        public int TestNo { get; set; }
        public decimal SubConduct { get; set; }
        public decimal SurfConduct { get; set; }
        public decimal NotchStrengthKsi { get; set; }
        public decimal YieldStrengthKsi { get; set; }
        public decimal NotchYieldRatio { get; set; }        
        public string SpeciComment { get; set; }
        public string Operator { get; set; }
        public string TestDate { get; set; }
        public string TestTime { get; set; }
        public string EntryBy { get; set; }
        public string EntryDate { get; set; }
        public char Completed { get; set; }
    }
}
