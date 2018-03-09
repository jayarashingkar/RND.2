using System;

namespace RNDSystems.Models
{
    /// <summary>
    /// Created by and Created date details
    /// </summary>
    interface IRNDModel
    {
        string EntryBy { get; set; }
        DateTime? EntryDate { get; set; }
        int total { get; set; }
    }
}
