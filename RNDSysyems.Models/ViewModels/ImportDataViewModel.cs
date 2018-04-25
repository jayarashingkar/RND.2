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

        public string WorkStudyID { get; set; }
        public string TestType { get; set; }
        public List<SelectListItem> ddTestType { get; set; }
        public char Active { get; set; }
        public int total { get; set; }
        public string SelectedTests { get; set; }
        public string results;
        public string filename;

    }
}
