using System;

namespace RNDSystems.Models
{
    /// <summary>
    /// Created and Modified Log details
    /// </summary>
    public interface IModel
    {
        int? CreatedBy { get; set; }

        DateTime? CreatedOn { get; set; }

        int? LastModifiedBy { get; set; }

        DateTime? LastModifiedOn { get; set; }

        ////[Timestamp]
        byte[] RowVersion { get; set; }

        ////[StringLength(DataLengthConstant.LENGTH_CODE)]
        string StatusCode { get; set; }
    }
}
