using Alertas.Hubs;
using Alertas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alertas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController : ControllerBase
    {
        private IHubContext<AlertaHub> _hubContext;

        public AlertaController(IHubContext<AlertaHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("grupo")]
        [HttpGet]
        public async Task<IActionResult> JoinGroup(string grupo)
        {
            await _hubContext.Groups.AddToGroupAsync("JoinGroup", grupo);
            return Ok();
        }

        [Route("grupo")]
        [HttpPost]
        public async Task<IActionResult> AlertasGrupo(Alerta alertas)
        {
            await _hubContext.Clients.Group(alertas.Grupo).SendAsync("alertasGrupo", alertas.Tipo, alertas.Mensaje, alertas.Receptor, DateTime.Now, alertas.Grupo);
            return Ok();

        }

        [Route("global")]
        [HttpPost]
        public async Task<IActionResult> AlertasGlobales(Alerta alertas)
        {
            await _hubContext.Clients.All.SendAsync("alertasGlobales", alertas.Tipo, alertas.Mensaje, alertas.Receptor, DateTime.Now, alertas.Grupo);
            return Ok();
        }
    }
}
