using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRWebApi.Hubs
{
    public class UsuariosHub:Hub
    {
        public async Task Enviar(string nombre,int edad)
        {
            await Clients.All.SendAsync("recibir", nombre, edad);
        }
    }
}
