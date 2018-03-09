using System.Collections.Generic;

namespace RNDSystems.Models.ViewModels
{
    /// <summary>
    /// DataSearch
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataSearch<T> where T : class
    {
        public List<T> items { get; set; }
        public int total { get; set; }
    }
}
