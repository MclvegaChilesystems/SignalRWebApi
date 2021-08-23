using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alertas.Models
{
    public class Alert
    {
        public int AlertType { get; set; }
        public string AlertTitle { get; set; }
        public string Message { get; set; }
        public List<string> Groups { get; set; }
    }
}
