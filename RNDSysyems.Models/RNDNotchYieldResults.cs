using System;


namespace RNDSystems.Models
{
    /// <summary>
    /// Notch yield results column details
    /// </summary>
    public class RNDNotchYieldResults : IRNDModel
    {

        public int RecID { get; set; }

        //check if FK
        public RNDWorkStudy RNDWorkStudy { get; set; }
        public string WorkStudyID { get; set; }

        public int TestNo { get; set; }

        //check numeric(4,1)
        public decimal SubConduct { get; set; }
        //check numeric(4,1)
        public decimal SurfConduct { get; set; }
        //check numeric(4,1)
        public decimal NotchStrengthKsi { get; set; }
        //check numeric(3,0)
        public decimal YieldStrengthKsi { get; set; }
        //check numeric(5,3)
        public decimal NotchYieldRatio { get; set; }

        ////[StringLength(50)]
        public string SpeciComment { get; set; }

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
