using Microsoft.AspNetCore.SignalR;

namespace SignalRAPI
{
    public class MyHub:Hub
    {
        public Task SendPublicMessage(string Message)
        {
            string  ConnId=this.Context.ConnectionId;
            string MsgToSend = $"{ConnId}在{DateTime.Now}:{Message}";
            return this.Clients.All.SendAsync("PublicMsgRecevied",MsgToSend);
        }


    }
}
