using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RNDSystems.Models.ViewModels
{
    public class OptionsViewModel
    {
        public int RecId { get; set; }
        // public int LocationRecID { get; set; }
        public string optionType { get; set; }
        public short Plant { get; set; }
        public byte PlantType { get; set; }
        public string PlantDesc { get; set; }
        public string PlantState { get; set; }

        //    public int StudyTypeRecID { get; set; }
         public string TypeStudy { get; set; }
         public string TypeDesc { get; set; }

    }
}
