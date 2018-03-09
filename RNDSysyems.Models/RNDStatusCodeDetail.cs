using System;

namespace RNDSystems.Models
{
    /// <summary>
    ///  Status code detail column details
    /// </summary>
    public class RNDStatusCodeDetail : IModel
    {
        //[Key]
        //[StringLength(DataLengthConstant.LENGTH_CODE)]
        public string StatusCodeId { get; set; }

        //[Timestamp]
        public Byte[] RowVersion { get; set; }

        //[Required]
        //[StringLength(DataLengthConstant.LENGTH_DESCRIPTION)]
        public string StatusCodeName { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_CODE)]
        public string StatusCode { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public int total { get; set; }
    }
}

