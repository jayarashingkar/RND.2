using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.ViewModels
{
    public class ImportDataViewModel
    {

        public int TestingNo { get; set; }

        public List<SelectListItem> ddTestingNo { get; set; }
        public string WorkStudyID { get; set; }
        public List<SelectListItem> ddWorkStudyID { get; set; }
        public string LotID { get; set; }
        public int MillLotNo { get; set; }
        public string SoNum { get; set; }
        public string Hole { get; set; }
        public string PieceNo { get; set; }
        public string Alloy { get; set; }
        public string Temper { get; set; }
        public string CustPart { get; set; }
        public decimal UACPart { get; set; }
       public string GageThickness { get; set; }
       public string Orientation { get; set; }
      public string Location1 { get; set; }
       public string Location2 { get; set; }
       public string Location3 { get; set; }
      public string SpeciComment { get; set; }
       public string TestType { get; set; }
        public List<SelectListItem> ddTestType { get; set; }
        public string SubTestType { get; set; }
        public char Status { get; set; }
        public char Selected { get; set; }
        public string TestLab { get; set; }
        public char Printed { get; set; }
    }
}
