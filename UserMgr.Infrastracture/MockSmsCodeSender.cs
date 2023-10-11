using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Infrastracture
{
    /// <summary>
    /// 模拟短信发送
    /// </summary>
    public class MockSmsCodeSender : ISmsCodeSender
    {
        public Task SendAsync(PhoneNumber phoneNumber, string code)
        {
            Console.WriteLine($"向{phoneNumber.RegionNumber}-{phoneNumber.PhoneNumbers}发送验证码：{code}");
            return Task.CompletedTask;
        }
    }
}
  