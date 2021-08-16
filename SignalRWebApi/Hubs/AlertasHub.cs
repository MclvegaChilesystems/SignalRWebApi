using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRWebApi.Hubs
{
    public class AlertasHub : Hub
    {
        
        public async Task JoinGroup(string nombre)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, nombre);
        }

        public async Task ExitGroup(string nombre)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, nombre);
        }


        [HubMethodName("Enviar")]
        public async Task Enviar(string tipo, string mensaje, string receptor, DateTime fecha, string grupo)
        {
            await Clients.All.SendAsync("alertas", tipo, mensaje, receptor, fecha, grupo);
        }
        

        public async Task EnviarGrupo(string tipo, string mensaje, string receptor, DateTime fecha, string grupo)
        {
            await Clients.Group(grupo).SendAsync("alertasGrupo", tipo, mensaje, receptor, fecha, grupo);
        }
    }
}
