using UserMgr.Domain.ValueObjects;

namespace UserMgr.WebAPI
{
    public record AddUserRequest(PhoneNumber phoneNumber, string password);
}
