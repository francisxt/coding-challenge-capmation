using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Controllers
{
    /// <summary>
    /// Primer Controlador
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FirstController : ControllerBase
    {

        //Metodo Get
        [HttpGet]
        //Ruta del endpoint con longitud entre 5 y 20 
        [Route("code/{length:int:min(5):max(20)}")]
        //Retorno de objeto dinamico tomando como argumento el número de caracteres
        public dynamic GetRandomCode(int length)
        {
            //Caracteres validos
            var caracteres = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ._-%$&";
            //lista caracteres
            var lista = caracteres.ToArray();
            //Declaracion random
            var random = new Random();
            //Declaracion codigo
            string codigo = "";
            for (int i = 0; i < length; i++)
            {
                //Numero aleatorio de caracter
                var numeroCaracter = random.Next(caracteres.Length);
                //Se agrega el caracter al codigo
                codigo += lista[numeroCaracter];
            }
            //Se obtiene el hash del codigo
            var hash = codigo.GetHashCode();
            //Se retorna el objeto con el codigo y el hash
            return new
            {
                Code = codigo,
                Hash = hash
            };
        }
    }
}
