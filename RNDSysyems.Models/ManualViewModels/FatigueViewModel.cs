using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.ManualViewModels
{
    public class FatigueViewModel
    {
        public string SelectedTests { get; set; }
        public int TestingNo { get; set; }
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
        public DateTime? TestDate { get; set; }       
        public string TestTime { get; set; }
        public string EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public char Completed { get; set; }
    }
}
