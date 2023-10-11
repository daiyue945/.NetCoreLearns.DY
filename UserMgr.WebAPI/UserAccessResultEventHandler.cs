using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserMgr.Domain;
using UserMgr.Infrastracture;

namespace UserMgr.WebAPI
{
    public class UserAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserDbContext _userDbContext;
        public UserAccessResultEventHandler(IUserRepository userRepository, UserDbContext userDbContext)
        {
            this._userRepository = userRepository;
            this._userDbContext = userDbContext;
        }
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        //public UserAccessResultEventHandler(IServiceScopeFactory serviceScopeFactory)
        //{
        //    this._serviceScopeFactory = serviceScopeFactory;
        //}        
        public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            //using var scope= _serviceScopeFactory.CreateScope();
            //IUserRepository _userRepository=scope.ServiceProvider.GetRequiredService<IUserRepository>();
            //UserDbContext _userDbContext=scope.ServiceProvider.GetRequiredService<UserDbContext>();

            await _userRepository.AddNewLoginHistory(notification.PhoneNumber, $"登录结果是:{notification.Result}");
            await _userDbContext.SaveChangesAsync();
        }
    }
}
