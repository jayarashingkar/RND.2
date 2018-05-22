using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNDSystems.Common.Utilities
{
   public static class ConfigReader
    {
        public static string GetValue(string key)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            return value!=null?value:"";
        }
    }
}
