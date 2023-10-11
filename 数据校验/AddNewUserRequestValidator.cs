using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace 数据校验
{
    public class AddNewUserRequestValidator : AbstractValidator<AddNewUserRequest>
    {
        public AddNewUserRequestValidator(UserManager<MyUser> userManager)
        {
            RuleFor(x => x.Email).NotNull().EmailAddress()
                .WithMessage("邮箱必须是合法的")
                .Must(x => x.EndsWith("@163.com") || x.EndsWith("@qq.com"))
                .WithMessage("只支持163或QQ邮箱");

            RuleFor(x => x.UserName).NotNull().Length(3, 10)
                .MustAsync(async (x, _) => await userManager.FindByNameAsync(x) == null)
                //.WithMessage("用户名已经存在");//该用户在数据库必须为空
                .WithMessage(x => $"用户名{x.UserName}已经存在");

            RuleFor(x => x.Password).Equal(x => x.Password2)
                .WithMessage("两次密码必须一致");
        }
    }
}
