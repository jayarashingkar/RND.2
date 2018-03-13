using System.Collections.Generic;

namespace RNDSystems.Models.ViewModels
{
    /// <summary>
    /// ApiViewModel
    /// </summary>
    public class ApiViewModel
    {
        public dynamic Custom { get; set; }
        public string Message { get; set; }
        public string Message1 { get; set; }
        public List<string> MessageList { get; set; }
        public bool Success { get; set; }
    }
}
