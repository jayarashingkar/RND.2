using System;

namespace RNDSystems.Models
{
    /// <summary>
    /// Curve results column details
    /// </summary>
    public class RNDRCurveResults : IRNDModel
    {
        public int RecId { get; set; }

        //check fk
        //[StringLength(DataLengthConstant.LENGHT_ID)]
        public string WorkStudyID { get; set; }
        public int TestNo { get; set; }
        //check numeric(4,1)
        public decimal SubConduct { get; set; }

        //check numeric(4,1)
        public decimal SurfConduct { get; set; }
        //check numeric(4,1)
        public decimal KCKsiIn { get; set; }
        //check numeric(4,1)
        public decimal KAPPKsiIn { get; set; }
        //check numeric(7,4)
        public decimal aOIn { get; set; }
        //check numeric(7,4)
        public decimal afIn { get; set; }
        //check numeric(7,4)
        public decimal WIn { get; set; }
        //check numeric(7,4)
        public decimal BIn { get; set; }
        //check numeric(7,4)
        public decimal BnIn { get; set; }
        //check numeric(7,4)
        public decimal AvgFinalPreCrack { get; set; }
        public string SpeciComment { get; set; }
        //[StringLength(20)]
        public string Operator { get; set; }
        public DateTime? TestDate { get; set; }
        //[StringLength(15)]
        public string TestTime { get; set; }

        public char Completed { get; set; }

        #region IRNDModel

        //[StringLength(25)]
        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        #endregion

        public int total { get; set; }
    }
}
