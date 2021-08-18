using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alertas.Hubs
{
    public class AlertaHub : Hub
    {
        public static int counter = 0;
        public override Task OnConnectedAsync()
        {
            counter = counter + 1;
            Clients.All.SendAsync("activeUsers", counter);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            counter = counter - 1;
            Clients.All.SendAsync("activeUsers", counter);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(List<string> groupNames)
        {
            foreach (var item in groupNames)
            {

                await Groups.AddToGroupAsync(Context.ConnectionId, item);
                await Clients.OthersInGroup(item).SendAsync("JoinGroupName", item);
            }

        }

        public async Task ExitGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendGlobalAlerts(string alertType, string alertTitle, string message, List<string> groupNames)
        {
            await Clients.All.SendAsync("globalAlerts", alertType, alertTitle, message, groupNames);
        }

        public async Task SendGroupAlerts(string alertType, string alertTitle, string message, List<string> groupNames)
        {
            foreach (var item in groupNames)
            {
                await Clients.Groups(item).SendAsync("groupAlerts", alertType, alertTitle, message, groupNames);
            }
            
        }
    }
}
