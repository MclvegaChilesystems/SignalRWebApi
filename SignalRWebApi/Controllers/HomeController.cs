using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRWebApi.Hubs;
using SignalRWebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRWebApi.Controllers
{
    public class HomeController : Controller
    {
        private IHubContext<UsuariosHub> _hubContext;

        public HomeController(IHubContext<UsuariosHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Formulario()
        {
            return View();
        }
        public async Task<IActionResult> Agregar(Usuario usuario)
        {
            await _hubContext.Clients.All.SendAsync("recibir",usuario.nombre,usuario.edad);
            return View("Formulario");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
