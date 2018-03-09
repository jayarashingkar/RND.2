using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RNDSystems.Models
{
    /// <summary>
    /// User security answer column details
    /// Once registered the new user after first time login, It will asked for the secerect questions with answers.
    /// User need to select the question and they need to provide the appropriate answer. These details will be saved in to database.
    /// whether the RNDRegistered user forget the password, It will help to reset or retrieve the password.
    /// Retrieve user security question details and the user need to provide the same answer.
    /// </summary>
    public class RNDUserSecurityAnswer : IModel
    {
        public int RNDUserSecurityAnswerId { get; set; }

        public int RNDLoginId { get; set; }
        public RNDLogin RNDLogin { get; set; }

        public int RNDSecurityQuestionId { get; set; }
        public RNDSecurityQuestion RNDSecurityQuestion { get; set; }

        public List<SelectListItem> RNDSecurityQuestions { get; set; }

        //[StringLength(DataLengthConstant.LENGTH_DESCRIPTION)]
        public string SecurityAnswer { get; set; }

        //[NotMapped]
        public string UserName { get; set; }

        //[NotMapped]
        public string Password { get; set; }

        //[NotMapped]
        public string ConfirmPassword { get; set; }

        //[NotMapped]
        public bool HasQuestionId { get; set; }
        public bool IsSecurityApplied { get; set; }
        #region IModel

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        //[Timestamp]
        public Byte[] RowVersion { get; set; }
        //[StringLength(DataLengthConstant.LENGTH_CODE)]
        public string StatusCode { get; set; }
        //[ForeignKey("StatusCode")]
        //public virtual RNDStatusCodeDetail StatusCodeDetail { get; set; }

        #endregion
          
        public int total { get; set; }
    }
}
