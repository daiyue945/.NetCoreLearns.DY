using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgr.Domain.Entities
{
    public record UserAccessFail
    {
        public Guid Id { get; init; }
        public User User { get; init; }
        public Guid UserId { get; init; }
        private bool isLockOut;
        public DateTime? LockEnd { get; private set; }
        public int AccessFailCount { get; private set; }

        private UserAccessFail()
        {

        }

        public UserAccessFail(User user)
        {
            this.User = user;
            this.Id = Guid.NewGuid();
        }
        //登录失败重置
        public void Reset()
        {
            this.AccessFailCount = 0;
            this.isLockOut = false;
            this.LockEnd = null;
        }
        //登录失败
        public void Fail()
        {
            this.AccessFailCount++;
            if (this.AccessFailCount >= 3)//登录失败超过三次
            {
                this.LockEnd = DateTime.Now.AddMinutes(5);//锁定5分钟
                this.isLockOut = true;
            }
        }
        public bool IsLockOut()
        {
            //如果已经锁定
            if (this.isLockOut)
            {
                //但是当前时间大于锁定时间，则锁定过期，重置登录失败信息
                if (DateTime.Now > this.LockEnd)
                {
                    Reset();
                    return false;
                }
                else

                { return true; }
            }
            else
            {
                return false;
            }
        }

    }
}
