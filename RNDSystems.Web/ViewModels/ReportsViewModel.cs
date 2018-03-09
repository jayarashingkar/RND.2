using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RNDSystems.Web.ViewModels
{
    public class ReportsViewModel
    {
        public int RecID { get; set; }
        public string WorkStudyID { get; set; }
        public int TestNo { get; set; }
        public double SubConduct { get; set; }
        public double SurfConduct { get; set; }
        public double FtuKsi { get; set; }
        public double FtyKsi { get; set; }
        public double eElongation { get; set; }
        public string SpeciComment { get; set; }
        public string Operator { get; set; }
        public DateTime TestDate { get; set; }
        public string TestTime { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public char Completed { get; set; }
        public string StudyDesc { get; set; }
    }
}