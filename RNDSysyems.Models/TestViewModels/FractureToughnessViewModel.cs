using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.TestViewModels
{
    public class FractureToughnessViewModel
    {
        public int RecID { get; set; }
        public string WorkStudyID { get; set; }
        public int TestNo { get; set; }
        public decimal SubConduct { get; set; }
        public decimal SurfConduct { get; set; }
        public string Validity { get; set; }
        public decimal KKsiIn { get; set; }
        public decimal KmaxKsiIn { get; set; }
        public decimal PqLBS { get; set; }
        public decimal PmaxLBS { get; set; }
        public decimal aOIn { get; set; }
        public decimal WIn { get; set; }
        public decimal BIn { get; set; }
        public decimal BnIn { get; set; }
        public decimal AvgFinalPreCrack { get; set; }
        public string SpeciComment { get; set; }
        public string Operator { get; set; }
        public string TestDate { get; set; }
        public string EntryDate { get; set; }
        public string TestTime { get; set; }        
        public string EntryTime { get; set; }
        public string EntryBy { get; set; }      
        public char Completed { get; set; }
        public string blank1 { get; set; }
        public string blank2 { get; set; }
        public string blank3 { get; set; }
        public string blank4 { get; set; }
    }
}
