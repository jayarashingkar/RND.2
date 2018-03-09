using System;


namespace RNDSystems.Models
{
    /// <summary>
    /// Fatigue testing results column details
    /// </summary>
    public class RNDFatigueTestingResults : IRNDModel
    {
        public int RecId { get; set; }

        //check fk
        ////[StringLength(DataLengthConstant.LENGHT_ID)]
        public string WorkStudyID { get; set; }
        public int TestNo { get; set; }
        ////[StringLength(50)]
        public string SpecimenDrawing { get; set; }
        //check numeric(4,1)
        public decimal MinStress { get; set; }
        //check numeric(4,1)
        public decimal MaxStress { get; set; }
        //check numeric(7,2)
        public decimal MinLoad { get; set; }
        //check numeric(7,2)
        public decimal MaxLoad { get; set; }
        //check numeric(8,5)
        public decimal WidthOrDia { get; set; }
        //check numeric(8,5)
        public decimal Thickness { get; set; }
        //check numeric(8,5)
        public decimal HoleDia { get; set; }
        //check numeric(8,5)
        public decimal AvgChamferDepth { get; set; }
        ////[StringLength(5)]
        public string Frequency { get; set; }
        //check numeric(8,0)
        public decimal CyclesToFailure { get; set; }
        //check numeric(5,2)
        public decimal Roughness { get; set; }
        ////[StringLength(5)]
        public string TestFrame { get; set; }
        ////[StringLength(50)]
        public string Comment { get; set; }
        ////[StringLength(50)]
        public string FractureLocation { get; set; }
        ////[StringLength(20)]
        public string Operator { get; set; }
        public DateTime? TestDate { get; set; }
        ////[StringLength(15)]
        public string TestTime { get; set; }

        public char Completed { get; set; }
        #region IRNDModel

        ////[StringLength(25)]
        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        #endregion
        public int total { get; set; }
    }
}
