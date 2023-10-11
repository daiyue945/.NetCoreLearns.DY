using MediatR;

namespace MediatRTest
{
    public record PostNotification(string Body) : INotification;
    
}
