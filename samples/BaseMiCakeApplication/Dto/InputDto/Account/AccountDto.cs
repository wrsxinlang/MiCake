using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseMiCakeApplication.Dto.InputDto.Account
{
    public class AccountDto
    {
    }

    public class LoginUserInfo
    {
        [Required(ErrorMessage = "The Act is Required")]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Act")]
        public string Act { get; set; }

        [Required(ErrorMessage = "The Pwd is Required")]
        [MinLength(6)]
        [MaxLength(32)]
        [DisplayName("Pwd")]
        public string Pwd { get; set; }

        [Required(ErrorMessage = "The Code is Required")]
        [MinLength(6)]
        [MaxLength(32)]
        [DisplayName("Code")]
        public string Code { get; set; }
    }
}
