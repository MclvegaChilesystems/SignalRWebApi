using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alertas.Models
{
    public class Alerta
    {
        public string Tipo { get; set; }
        public string Mensaje { get; set; }
        public string Receptor { get; set; }
        public DateTime Fecha { get; set; }
        public string Grupo { get; set; }
    }
}
