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
    public class AlertasController : ControllerBase
    {
        private IHubContext<AlertasHub> _hubContext;

        public AlertasController(IHubContext<AlertasHub> hubContext)
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
        public async Task<IActionResult> AlertasGrupo(Alertas alertas)
        {
            try
            {
                await _hubContext.Clients.Group(alertas.Grupo).SendAsync("alertasGrupo", alertas.Tipo, alertas.Mensaje, alertas.Receptor, DateTime.Now, alertas.Grupo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
           
        }
        [Route("all")]
        [HttpPost]
        public async Task<IActionResult> AlertasGlobales(Alertas alertas)
        {
            await _hubContext.Clients.All.SendAsync("alertas", alertas.Tipo, alertas.Mensaje, alertas.Receptor, DateTime.Now, alertas.Grupo);
            return Ok();
        }
    }
}
