using Alertas.Hubs;
using Alertas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alertas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private IHubContext<AlertHub> _hubContext;

        public AlertController(IHubContext<AlertHub> hubContext)
        {
            _hubContext = hubContext;
        }


        [Route("groupAlerts")]
        [HttpPost]
        public async Task<IActionResult> GroupAlerts(Alert alert)
        {
            await _hubContext.Clients.Groups(alert.Groups).SendAsync("groupAlerts", alert.AlertType, alert.Message, alert.Groups);
            return Ok();

        }

        [Route("globalAlerts")]
        [HttpPost]
        public async Task<IActionResult> GlobalAlerts(Alert alert)
        {
            await _hubContext.Clients.All.SendAsync("globalAlerts", alert.AlertType, alert.Message, alert.Groups);
            return Ok();
        }
    }
}
