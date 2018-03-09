using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNDSystems.Models.TestViewModels
{
    public class ResidualStrengthViewModel
    {
        public int RecID { get; set; }
        public string WorkStudyID { get; set; }
        public int TestNo { get; set; }
        public decimal SubConduct { get; set; }
        public decimal SurfConduct { get; set; }
        public string Validity { get; set; }
        public decimal ResidualStrength { get; set; }
        public decimal PmaxLBS { get; set; }
        public decimal WIn { get; set; }
        public decimal BIn { get; set; }
        public decimal AvgFinalPreCrack { get; set; }      
        public string SpeciComment { get; set; }
        public string Operator { get; set; }
        public string TestDate { get; set; }
        public string TestTime { get; set; }
        public string EntryBy { get; set; }
        public string EntryDate { get; set; }
        public char Completed { get; set; }
    }
}
