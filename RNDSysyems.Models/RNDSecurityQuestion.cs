using System;

namespace RNDSystems.Models
{
    public class RNDSecurityQuestion : IModel
    {
        /// <summary>
        /// Security question column details
        /// </summary>
        public int RNDSecurityQuestionId { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_NOTES)]
        public string Question { get; set; }

        #region IModel

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        ////[Timestamp]
        public Byte[] RowVersion { get; set; }
        //[StringLength(DataLengthConstant.LENGTH_CODE)]
        public string StatusCode { get; set; }
        //[ForeignKey("StatusCode")]
        //public virtual RNDStatusCodeDetail StatusCodeDetail { get; set; }

        #endregion

        public int total { get; set; }
    }
}
