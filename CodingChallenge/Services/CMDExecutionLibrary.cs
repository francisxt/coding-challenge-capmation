using System.Diagnostics;

namespace CodingChallenge.Services
{
    /// <summary>
    /// Libreria para ejecutar un comando con CMD
    /// </summary>
    public static class CMDExecutionLibrary
    {
        //Metodo para ejecutar un comando con respuesta de una tupla con el resultado y la respuesta
        public static async Task<(bool result, string response)> RunCommandAsync(string command)
        {
            //Definimos el proceso
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var process = new Process
            {
                StartInfo = processStartInfo
            };
            //Iniciamos el proceso y esperamos por la respuesta o el error devuelto
            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();
            //Si la respuesta es satistaftoria retornamos la ejecucion correcta
            if (!string.IsNullOrEmpty(output))
            {
                return (true, output);
            }
            else
            {
                return (false, error);
            }
        }
    }
}
