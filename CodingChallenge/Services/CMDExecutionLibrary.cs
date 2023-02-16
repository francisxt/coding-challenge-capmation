using System.Diagnostics;

namespace CodingChallenge.Services
{
    public static class CMDExecutionLibrary
    {
        public static async Task<(bool result, string response)> RunCommandAsync(string command)
        {
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

            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

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
