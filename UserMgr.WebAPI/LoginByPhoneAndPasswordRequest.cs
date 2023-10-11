using UserMgr.Domain.ValueObjects;

namespace UserMgr.WebAPI
{
    public record LoginByPhoneAndPasswordRequest(PhoneNumber phoneNumber,string PassWord);
}
