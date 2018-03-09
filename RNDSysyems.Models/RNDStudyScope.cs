namespace RNDSystems.Models
{
    /// <summary>
    /// Study scope detail column details
    /// </summary>
    class RNDStudyScope
    {
        public int RecId { get; set; }
        public string WorkStudyID { get; set; }

        //char(2)
        public string ScopeType { get; set; }

        //char(900)
        public string StudyScope { get; set; }
    }
}
