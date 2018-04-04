using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RNDSystems.Models.ReportsViewModel
{
    public class ReportsViewModel
    {
         public int RecID { get; set; }
        public string WorkStudyID { get; set; }
        public List<SelectListItem> ddWorkStudyID { get; set; }
        public int TestNo { get; set; }
        public List<SelectListItem> ddTestNo { get; set; }
        public string TestType { get; set; }
        public List<SelectListItem> ddTestType { get; set; }

    }
}
