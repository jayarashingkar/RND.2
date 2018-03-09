using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RNDSystems.Models
{
    /// <summary>
    /// Processing column details
    /// </summary>
    public class RNDProcessing
    {
        public int RecID { get; set; }

        public string WorkStudyID { get; set; }

        public int MillLotNo { get; set; }

        public List<SelectListItem> ddMillLotNo { get; set; }
        public string Sonum { get; set; }

        public byte ProcessNo { get; set; }

        public string ProcessID { get; set; }

        public int HTLogNo { get; set; }

        public string HTLogID { get; set; }

        public List<SelectListItem> ddHTLogID { get; set; }

        public int AgeLotNo { get; set; }

        public string AgeLotID { get; set; }

        public List<SelectListItem> ddAgeLotID { get; set; }

        public string Hole { get; set; }
        public List<SelectListItem> ddHole { get; set; }


        public string PieceNo { get; set; }
        public List<SelectListItem> ddPieceNo { get; set; }
        
        public string SHTTemp { get; set; }

        public string SHSoakHrs { get; set; }
        
        public string SHSoakMns { get; set; }        

        public string SHTStartHrs { get; set; }
        //  public List<SelectListItem> SHTStartHours { get; set; }
        public List<SelectListItem> ddSHTStartHrs { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string SHTStartMns { get; set; }
        // public List<SelectListItem> SHTStartMinutes { get; set; }
        public List<SelectListItem> ddSHTStartMin { get; set; }

        public string SHTDate { get; set; }

        public string StretchPct { get; set; }

        public string AfterSHTHrs { get; set; }

        public string AfterSHTMns { get; set; }

        public string NatAgingHrs { get; set; }

        public string NatAgingMns { get; set; }

        public string ArtStartHrs { get; set; }
       // public List<SelectListItem> ArtStartHours { get; set; }
        public List<SelectListItem> ddArtStartHrs { get; set; }
        public string ArtStartMns { get; set; }
        //public List<SelectListItem> ArtStartMinutes { get; set; }
        public List<SelectListItem> ddArtStartMin { get; set; }

        //public DateTime? ArtAgeDate { get; set; }
        public string ArtAgeDate { get; set; }

        public string ArtAgeTemp1 { get; set; }

        public string ArtAgeHrs1 { get; set; }

        public string ArtAgeMns1 { get; set; }

        public string ArtAgeTemp2 { get; set; }

        public string ArtAgeHrs2 { get; set; }

      public string ArtAgeMns2 { get; set; }
       public string ArtAgeTemp3 { get; set; }
      public string ArtAgeHrs3 { get; set; }

        public string ArtAgeMns3 { get; set; }

        public string FinalTemper { get; set; }
        public string TargetCount { get; set; }
        public string ActualCount { get; set; }

        public string RCS { get; set; }
              
        public string RNDLotID{ get; set; }

        public int total { get; set; }

        public bool IsCopy { get; set; }

    }
}
