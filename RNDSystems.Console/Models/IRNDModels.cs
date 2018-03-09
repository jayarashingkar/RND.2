using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNDSystems.Console.Models
{
    interface IRNDModel
    {
        string EntryBy { get; set; }
        DateTime? EntryDate { get; set; }
    }
}
