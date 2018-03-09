namespace RNDSystems.Models
{
    /// <summary>
    /// Study type detail column details
    /// </summary>
    public class RNDStudyType
    {
        public int RecId { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string TypeStudy { get; set; }

        //[StringLength(30)]
        public string TypeDesc { get; set; }

        public int total { get; set; }
    }
}
