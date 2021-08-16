
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cliente
{
    class Program
    {
        static Microsoft.AspNetCore.SignalR.Client.HubConnection connection;
        static async Task Main(string[] args)
        {
            connection = new HubConnectionBuilder().WithUrl("https://localhost:5001/alertashub").Build();
            await connection.StartAsync();

            Console.WriteLine("Ingresa nombre de grupo : ");
            var nombreGrupo = Console.ReadLine();

            await connection.SendAsync("JoinGroup", nombreGrupo);


            connection.On<string, string, string, DateTime, string>("Enviar", (tipo, mensaje, receptor, fecha, grupo) => {

                Console.WriteLine($"\nGrupo: " + grupo +
                                  $"\nTipo: " + tipo +
                                  $"\nMensaje: " + mensaje +
                                  $"\nFecha: " + fecha +
                                  $"\nReceptor: " + receptor
                                  );
            });
            while (true)
            {
                Console.WriteLine("Ingresa tipo de alerta : ");
                var tipo = Console.ReadLine();
                Console.WriteLine("Ingresa mensaje : ");
                var mensaje = Console.ReadLine();
                Console.WriteLine("Ingresa el receptor : ");
                var receptor = Console.ReadLine();
                var fecha = DateTime.Now;
                await connection.SendAsync("alertas", tipo,mensaje,receptor,fecha,nombreGrupo);
               
            }
        }
    }
}
