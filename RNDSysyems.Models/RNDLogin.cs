using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RNDSystems.Models
{
    /// <summary>
    /// Login column details
    /// </summary>
    public class RNDLogin : IModel
    {
        //[Key]
        public int UserId { get; set; }
        ////[StringLength(DataLengthConstant.LENGTH_NAME)]
        public string UserName { get; set; }

        ////[StringLength(DataLengthConstant.LENGTH_NAME)]
        public string FirstName { get; set; }
        ////[StringLength(DataLengthConstant.LENGTH_NAME)]
        public string LastName { get; set; }

        public string Token { get; set; }

        public bool IsSecurityApplied { get; set; }

        ////[StringLength(DataLengthConstant.LENGTH_CODE)]
        public string UserType { get; set; }
        public List<SelectListItem> UserTypes { get; set; }

        ////[NotMapped]
        public bool IsRememberMe { get; set; }
        ////[NotMapped]
        public string Password { get; set; }
        ////[NotMapped]
        public string ConfirmPassword { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string PermissionLevel { get; set; }
        public List<SelectListItem> UserPermissionLevel { get; set; }

        public DateTime? IssueDate { get; set; }

        public string Created_By { get; set; }
        public string Created_On { get; set; }

        ////[NotMapped]
        public string IssueDateInFormat
        {
            get { return (IssueDate.HasValue) ? IssueDate.Value.ToString("MM/dd/yyyy") : "-"; }
        }

        ////[NotMapped]
        public string Message { get; set; }


        #region IModel

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        ////[Timestamp]
        public Byte[] RowVersion { get; set; }
        ////[StringLength(DataLengthConstant.LENGTH_CODE)]
        public string StatusCode { get; set; }
        ////[ForeignKey("StatusCode")]
        //public virtual RNDStatusCodeDetail StatusCodeDetail { get; set; }

        #endregion

        public int total { get; set; }

    }
}
