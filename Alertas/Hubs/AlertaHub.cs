using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alertas.Hubs
{
    public class AlertaHub : Hub
    {
        public static int contador = 0;
        public override Task OnConnectedAsync()
        {
            contador = contador + 1;
            Clients.All.SendAsync("usuariosActivos",contador);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            contador = contador - 1;
            Clients.All.SendAsync("usuariosActivos",contador);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string grupo)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, grupo); 
            await Clients.All.SendAsync("unido", grupo);
        }

        public async Task ExitGroup(string nombre)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, nombre);
        }


        public async Task EnviarAlertaGlobal(string tipo, string mensaje, string receptor, DateTime fecha, string grupo)
        {
            await Clients.All.SendAsync("alertasGlobales", tipo, mensaje, receptor, fecha, grupo);
        }


        public async Task EnviarAlertaGrupo(string tipo, string mensaje, string receptor, DateTime fecha, string grupo)
        {
            await Clients.Group(grupo).SendAsync("alertasGrupo", tipo, mensaje, receptor, fecha, grupo);
        }
    }
}
