using System.ComponentModel.DataAnnotations;

namespace RNDSystems.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "* User name required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "* Password required")]
        public string Password { get; set; }
    }
}