
namespace RNDSystems.Models
{
    /// <summary>
    /// User details
    /// </summary>
    public class CurrentUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PermissionLevel { get; set; }
        public string FullName { get; set; }
        public bool IsSecurityApplied { get; set; }
        public string Token { get; set; }
        public string StatusCode { get; set; }
    }
}
