using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain;
using UserMgr.Domain.Entities;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Infrastracture
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        private readonly IDistributedCache _distributedCache;
        private readonly IMediator _mediator;
        public UserRepository(UserDbContext db, IDistributedCache distributedCache, IMediator mediator)
        {
            this._dbContext = db;
            this._distributedCache = distributedCache;
            this._mediator = mediator;
        }
        public async Task AddNewLoginHistory(PhoneNumber phoneNumber, string message)
        {
            User? user = await FindOneAsync(phoneNumber);
            Guid? userId = null;
            if (user != null)
            {
                userId = user.Id;
            }
            _dbContext.UserLoginHistorys.Add(new UserLoginHistory(userId, phoneNumber, message));
        }

        public async Task<User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            return await _dbContext.Users.Include(u=>u.UserAccessFail).SingleOrDefaultAsync(u => u.PhoneNumber.PhoneNumbers == phoneNumber.PhoneNumbers && u.PhoneNumber.RegionNumber == phoneNumber.RegionNumber);
        }

        public async Task<User?> FindOneAsync(Guid userId)
        {
            //return await _dbContext.Users.FindAsync(userId);
            return await _dbContext.Users.Include(u=>u.UserAccessFail).SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber)
        {
            string key = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.PhoneNumbers}";
            string? code= await _distributedCache.GetStringAsync(key);//从分布式缓存里获取验证码
            _distributedCache.Remove(key);//删除缓存中的当前验证码
            return code;
        }

        public Task PublishEventAsync(UserAccessResultEvent userAccessResultEvent)
        {
            return _mediator.Publish(userAccessResultEvent);
        }
        //保存手机验证码
        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            string key = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.PhoneNumbers}";
            return _distributedCache.SetStringAsync(key, code, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });//把验证码保存到分布式缓存中，设置有效期为5分钟
        }
    }
}
