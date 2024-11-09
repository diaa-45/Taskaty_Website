using System.ComponentModel.DataAnnotations;

namespace Tsakaty.ViewModels
{
    public class UserLoginViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me ?")]
        public bool RemenberMe { get; set; }
    }
}
