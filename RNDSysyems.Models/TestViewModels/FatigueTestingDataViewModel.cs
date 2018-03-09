using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.TestViewModels
{
    public class FatigueTestingDataViewModel
    {
        public int RecID { get; set; }
        public string WorkStudyID { get; set; }
        public int TestNo { get; set; }
        public string SpecimenDrawing { get; set; }
        public decimal MinStress { get; set; }
        public decimal MaxStress { get; set; }
        public decimal MinLoad { get; set; }
        public decimal MaxLoad { get; set; }

        public decimal WidthOrDia { get; set; }
        public decimal Thickness { get; set; }
        public decimal HoleDia { get; set; }
        public decimal AvgChamferDepth { get; set; }
        public string Frequency { get; set; }
        public decimal CyclesToFailure { get; set; }
        public decimal Roughness { get; set; }

        public string TestFrame { get; set; }

        public string Comment { get; set; }
        public string FractureLocation { get; set; }

        public string Operator { get; set; }
        public string TestDate { get; set; }
        public string TestTime { get; set; }
        public string EntryBy { get; set; }
        public string EntryDate { get; set; }

        public string field1 { get; set; }
        public char Completed { get; set; }

    }
}
