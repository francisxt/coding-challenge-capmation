using CodingChallenge.Interfases;
using System.Text.RegularExpressions;

namespace CodingChallenge.Services
{
    //Realizamos la implementacion de la interfaz con cada uno de los metodos solicitados
    public class CommandStrategyService : ICommandStrategy
    {
        //Metodo para retornar el espacio utilizado en el disco
        public async Task<string> GetUsedSpaceInHardDriveAsync()
        {
            try
            {
                //Usamos la liberia para ejecutar un comando con CMD
                var cmd = await CMDExecutionLibrary.RunCommandAsync(@"dir C:\");
                if (cmd.result)
                {
                    //Seleccionamos la ultima linea de la ejecucion que no este en blanco y la separamos por espacios en blanco
                    var ultimaLinea = cmd.response.Split("\r\n").Where(x => !string.IsNullOrEmpty(x)).Last().Trim().Split(" ").ToList();
                    //buscamos el indice del item que dice bytes
                    int index = ultimaLinea.IndexOf("bytes");
                    //Escogemos el numero de bytes y transformamos a gigabytes
                    var gigaBytes = Math.Round(ConvertBytesToGigabytes(long.Parse(ultimaLinea[index - 1].Replace(",", "").Replace(".", ""))), 2);
                    return $"{gigaBytes} GB Libres";
                }
                else
                {
                    //No se ejecuto el comando 
                    return $"Error en la ejecucion del comando. {cmd.response}";
                }
            }
            catch (Exception ex)
            {
                return $"Hubo un problema {ex.Message}";
            }
        }
        //Metodo para obtener el nombre del host
        public async Task<string> GetHostnameAsync()
        {
            try
            {
                //Usamos la liberia para ejecutar un comando con CMD
                var cmd = await CMDExecutionLibrary.RunCommandAsync("hostname");
                if (cmd.result)
                {
                    //Retornamos el nombre del host
                    return cmd.response.Replace("\r\n", "");
                }
                else
                {
                    //No se ejecuto el comando 
                    return ($"Error en la ejecucion del comando. {cmd.response}");
                }
            }
            catch (Exception ex)
            {
                return $"Hubo un problema {ex.Message}";
            }
        }
        //Metodo para retornar las direcciones IP del equipo
        public async Task<string> GetPrivateIpAddressAsync()
        {
            try
            {
                //Usamos la liberia para ejecutar un comando con CMD
                var cmd = await CMDExecutionLibrary.RunCommandAsync("ipconfig | findstr /r /c:\"IPv4\"");
                if (cmd.result)
                {
                    //Buscamos las cadenas donde no se tenga vacios
                    var direccionesIP = cmd.response.Split("\r\n").Where(x => !string.IsNullOrEmpty(x));
                    //Usamos un patron para buscar la cadena de IP formado por grupo de 4 entre 1 y 3 digitos separados por punto 
                    var patron = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";
                    List<string> ips = new List<string>();
                    foreach (var ip in direccionesIP)
                    {
                        //Buscamos la IP en la linea
                        ips.Add(Regex.Matches(ip, patron).First().Value);
                    }
                    //retornamos todas las direcciones IP encontradas separadas por guion
                    return string.Join(" - ", ips);
                }
                else
                {
                    //No se ejecuto el comando 
                    return ($"Error en la ejecucion del comando. {cmd.response}");
                }
            }
            catch (Exception ex)
            {
                return $"Hubo un problema {ex.Message}";
            }
        }

        //Metodo para transformar Bytes y GigaBytes
        private double ConvertBytesToGigabytes(long bytes)
        {
            const long BytesInGigabyte = 1073741824L;
            return bytes / (double)BytesInGigabyte;
        }
    }
}
