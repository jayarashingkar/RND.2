namespace RNDSystems.Models.ViewModels
{
    /// <summary>
    /// DataGridoption
    /// </summary>
    public class DataGridoption
    {
        public string Screen { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string sortDirection { get; set; }
        public string sortBy { get; set; }
        public string filterBy { get; set; }
        public string searchBy { get; set; }
    }
}
