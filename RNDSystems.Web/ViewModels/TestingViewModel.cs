using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RNDSystems.Web.ViewModels
{
    //public class TestingViewModel
    //{
    //    public int TestingNo { get; set; }
    //    public string WorkStudyID { get; set; }
    //    public string LotID { get; set; }
    //    public string Hole { get; set; }
    //    public string PieceNo { get; set; }
    //    public string Alloy { get; set; }
    //    public string Temper { get; set; }
    //    public decimal UACPart { get; set; }
    //    public string GageThickness { get; set; }
    //    public string Orientation { get; set; }
    //    public string Location1 { get; set; }
    //    public string Location2 { get; set; }
    //    public string Location3 { get; set; }
    //    public string SpeciComment { get; set; }
    //    public string TestType { get; set; }
    //    public string SubTestType { get; set; }
    //    public string TestLab { get; set; }
    //    public int total { get; set; }

    //}

    public class TestingViewModel
    {
        public string Alloy { get; set; }
        public string GageThickness { get; set; }
        public string Hole { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string LotID { get; set; }
        public string Orientation { get; set; }
        public string PieceNo { get; set; }
        public string SpeciComment { get; set; }
        public string TestType { get; set; }
        public string SubTestType { get; set; }
        public string Temper { get; set; }

        public int TestingNo { get; set; }
        public string TestLab { get; set; }
        public decimal UACPart { get; set; }

        public string WorkStudyID { get; set; }     
   
     //   public int total { get; set; }

    }
}


