using System;

namespace RNDSystems.Models
{
    /// <summary>
    /// Location column details
    /// </summary>
    public class RNDLocation
    {
        public int RedId { get; set; }

        //smallint
        public Int16 Plant { get; set; }

        ////[StringLength(DataLengthConstant.LENGTH_DESC)]
        public string PlantDesc { get; set; }

        ////[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string PlantState { get; set; }

        public byte PlantType { get; set; }

        public int total { get; set; }
    }
}
