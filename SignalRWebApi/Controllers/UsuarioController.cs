using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRWebApi.Hubs;
using SignalRWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IHubContext<UsuariosHub> _hubContext;

        public UsuarioController(IHubContext<UsuariosHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuario(Usuario usuario)
        {
            await _hubContext.Clients.All.SendAsync("recibir",usuario.nombre,usuario.edad);
            return Ok();
        }
    }
}
