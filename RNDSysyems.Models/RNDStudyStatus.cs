namespace RNDSystems.Models
{
    /// <summary>
    /// Status status detail column details
    /// </summary>
    public class RNDStudyStatus
    {
        public int RecId { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_KEY)]
        public string StudyStatus { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_DESC)]
        public string StatusDesc { get; set; }

        public int total { get; set; }
    }
}
