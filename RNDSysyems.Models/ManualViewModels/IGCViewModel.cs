
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.ManualViewModels
{
    public class IGCViewModel
    {
        public string SelectedTests { get; set; }
        public int TestingNo { get; set; }
        public string SubConduct { get; set; }
        public string SurfConduct { get; set; }
        public string MinDepth { get; set; }
        public string MaxDepth { get; set; }
        public string AvgDepth { get; set; }
        public string SpeciComment { get; set; }
        public string Operator { get; set; }
        public DateTime? TestDate { get; set; }
        public string TimeHrs { get; set; }
        public string TimeMns { get; set; }
        public string EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public char Completed { get; set; }
    }
}

