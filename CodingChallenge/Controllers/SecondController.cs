using CodingChallenge.Interfases;
using CodingChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Controllers
{
    /// <summary>
    /// Segundo Controlador
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SecondController : ControllerBase
    {
        private readonly IEnumerable<ICommandStrategy> strategy;
        //Definimos el constructor del controlador con la implementacion de la interfaz
        public SecondController(IEnumerable<ICommandStrategy> _strategy)
        {
            strategy = _strategy;
        }

        //Metodo post
        [HttpPost]
        [Route("commandexecutor")]
        public async Task<IActionResult> ExecuteCommandsAsync()
        {
            //Ejecutamos las 3 tareas en paralelo y esperamos a que finalicen todas
            var tareas = new List<ICommandStrategy>
            {
                new GetUsedSpaceInHardDrive(),
                new GetHostname(), 
                new GetPrivateIpAddress()
            };


            var tasks = new List<Task<string>>();
            foreach (var task in tareas)
            {
                tasks.Add(task.ExecuteAsync());
            }

            await Task.WhenAll(tasks);

            //Definimos el objeto resultante con las respuestas a cada literal
            var result = new
            {
                UsedSpaceInHardDrive = tasks[0].Result,
                Hostname = tasks[1].Result,
                PrivateIpAddress = tasks[2].Result,
            };
            //Retornamos la respuesta a la peticion en un objeto JSON
            return Ok(result);
        }
    }
}
