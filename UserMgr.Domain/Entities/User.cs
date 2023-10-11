using UserMgr.Domain.ValueObjects;
using Zack.Commons;

namespace UserMgr.Domain.Entities
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }
        private string? passWordHash;
        public UserAccessFail UserAccessFail { get; private set; }

        private User()
        {
        }
        public User(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
            this.Id = Guid.NewGuid();
            this.UserAccessFail = new UserAccessFail(this);
        }
        //密码是否为空
        public bool HasPassWord()
        {
            return !string.IsNullOrEmpty(this.passWordHash);
        }
        //修改密码
        public void ChangePassWord(string password)
        {
            if (password.Length <= 3)
            {
                throw new ArgumentOutOfRangeException("密码长度必须大于3");
            }
            this.passWordHash = HashHelper.ComputeMd5Hash(password);
        }
        //校验密码
        public bool CheckPassWord(string password)
        {
            return this.passWordHash == HashHelper.ComputeMd5Hash(password);
        }
        //修改手机号码
        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
    }
}