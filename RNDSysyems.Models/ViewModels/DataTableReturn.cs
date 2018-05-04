using System.Data;

namespace RNDSystems.Models.ViewModels
{
    /// <summary>
    /// DataGridoption
    /// </summary>
    public class DataTableReturn
    {
        public dynamic Custom { get; set; }
        public string Message { get; set; }
        public DataTable data { get; set; }
        public bool Success { get; set; }
    }
}
