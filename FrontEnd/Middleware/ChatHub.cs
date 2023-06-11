using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FrontEnd.Middleware
{
    public class ChatHub : Hub
    {
        public async Task Send(string name, string message)
        {
            await Clients.All.SendAsync("boradcasMessage", name, message);
        }

    }
}
