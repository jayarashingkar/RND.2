using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace RNDSystems.Models
{
    /// <summary>
    /// Material column details
    /// </summary>
    public class RNDMaterial : IRNDModel
    {

        public int RecID { get; set; }

        //check if FK
        public RNDWorkStudy RNDWorkStudy { get; set; }
        public string WorkStudyID { get; set; }

        ////[StringLength(DataLengthConstant.LENGHT_ID)]
        public string SoNum { get; set; }

        public int MillLotNo { get; set; }

        ////[StringLength(30)]
        public string CustPart { get; set; }

        //numeric(9,2)
        public decimal UACPart { get; set; }

        ////[StringLength(DataLengthConstant.LENGHT_ID)]
        public string Alloy { get; set; }

        public List<SelectListItem> ddlAlloy { get; set; }

        ////[StringLength(6)]
        public string Temper { get; set; }

        public List<SelectListItem> ddlTemper { get; set; }

        ////[StringLength(7)]
        public string GageThickness { get; set; }

        ////[StringLength(6)]
        public string Location2 { get; set; }

        ////[StringLength(2)]
        public string Hole { get; set; }

        ////[StringLength(2)]
        public string PieceNo { get; set; }

        ////[StringLength(40)]
        public string Comment { get; set; }

        ////[StringLength(3)]
        public string DBCntry { get; set; }

    //    public Char RCS { get; set; }

        public string records { get; set; }
        #region IRNDModel

        ////[StringLength(25)]
        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public string StrEntryDate
        {
            get { return (EntryDate.HasValue) ? EntryDate.Value.ToString("MM/dd/yyyy") : "-"; }
        }

        #endregion

        public int total { get; set; }

        public bool IsCopy { get; set; }

    //    public string DataBaseName { get; set; }
    }
}
