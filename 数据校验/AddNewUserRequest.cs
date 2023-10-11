using System.ComponentModel.DataAnnotations;

namespace 数据校验
{
    public class AddNewUserRequest
    {
        //[MinLength(3)]
        public string UserName { get; set; }
        //[Required]
        public string Email { get; set; }

        public string Password { get; set; }
        //[Compare(nameof(Password))]
        public string Password2 { get; set; }
    }
}