using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain.Entities;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Domain
{
    public class UserDomainService
    {
        private IUserRepository _userRepository;
        private ISmsCodeSender _smsSender;
        public UserDomainService(IUserRepository userRepository, ISmsCodeSender smsSender)
        {
            this._userRepository = userRepository;
            this._smsSender = smsSender;
        }

        public void ResetAccessFail(User user)
        {
            user.UserAccessFail.Reset();
        }
        public bool IsLockOut(User user)
        {
            return user.UserAccessFail.IsLockOut();
        }
        public void AccessFail(User user)
        {
            user.UserAccessFail.Fail();
        }
        public async Task<UserAccessResult> CheckPassWord(PhoneNumber phoneNumber, string password)
        {
            UserAccessResult accessResult;
            var user = await _userRepository.FindOneAsync(phoneNumber);//根据手机号查找用户
            if (user == null)
            {
                accessResult = UserAccessResult.PhoneNumberNotFound;//用户为空返回手机号不存在
            }
            else if (IsLockOut(user))
            {
                accessResult = UserAccessResult.LockOut;//用户锁定返回已锁定
            }
            else if (user.HasPassWord() == false)
            {
                accessResult = UserAccessResult.NoPassWord;//用户密码为空返回密码为空
            }
            else if (user.CheckPassWord(password))
            {
                accessResult = UserAccessResult.Ok;//密码验证通过返回OK
            }
            else
                accessResult = UserAccessResult.PassWordError;//否则返回密码验证失败
            if (user != null)//如果用户不为空
            {
                if (accessResult == UserAccessResult.Ok)
                {
                    ResetAccessFail(user);//登录成功则重置登录失败信息
                }
                else
                {
                    AccessFail(user);//登录失败
                }
            }
            //推送登录结果 
            await _userRepository.PublishEventAsync(new UserAccessResultEvent(phoneNumber, accessResult));
            return accessResult;
        }

        public async Task<CheckCodeResult> CheckPhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            var user = await _userRepository.FindOneAsync(phoneNumber);//根据手机号查找用户
            if (user == null)
            {
                return CheckCodeResult.PhoneNumberNotFound;//用户为空返回手机号不存在
            }
            else if (IsLockOut(user))//是否被锁定
            {
                return CheckCodeResult.LockOut; 
            }
            string? CodeInServer=await _userRepository.FindPhoneNumberCodeAsync(phoneNumber);
            if (!string.IsNullOrWhiteSpace(CodeInServer))
            {
                return CheckCodeResult.CodeError;
            }
            if(code==CodeInServer)
            {
                return CheckCodeResult.Ok;
            }
            else
            {
                AccessFail(user);//验证码错误，登录失败
                return CheckCodeResult.CodeError;
            }
        }
    }
}
