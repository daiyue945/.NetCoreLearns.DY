using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Zack.Commons;
using 领域事件发布的时机.Controllers;

namespace 领域事件发布的时机
{
    public class User : BaseEntity
    {
        //属性是只读的或者是只能被类内部的代码修改
        public long Id { get; init; }
        public DateTime CreateDateTime { get; init; }

        public string UserName { get; private set; }
        public int Credits { get; set; }

        private string? PasswordHash;
        private string? remark;
        public string? Remark
        {
            get
            {
                return this.remark;
            }
        }
        public string? Tag { get; set; }
        //给EF Core从数据库中加载数据然后生成User对象返回用的。
        private User()
        {
            //如果有参构造函数的参数和属性名称不一样，则需要创建无参构造函数
        }
        public User(string yhm)//给程序员用的
        {
            this.UserName = yhm;
            this.CreateDateTime = DateTime.Now;
            this.Credits = 10;
            //注册创建用户的通知事件
            AddDomianEvent(new NewUserNotification(yhm, this.CreateDateTime));
        }

        public void ChangeUserName(string username)
        {
            if (username.Length > 5)
            {
                Console.WriteLine("用户名长度不能大于5");
                return;
            }
            string oldusername = this.UserName;
            this.UserName = username;
            //注册修改用户名的通知事件
            AddDomianEvent(new ChangeUserNameNotification(oldusername, username));
        }

        public void ChangePassword(string password)
        {
            if (password.Length < 3)
            {
                Console.WriteLine("密码长度不能小于3");
                return;
            }
            this.PasswordHash = HashHelper.ComputeMd5Hash(password);
        }
    }
}
