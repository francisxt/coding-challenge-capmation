namespace CodingChallenge.Interfases
{
    //Definimos la interfaz para la estrategia de ejecucion
    public interface ICommandStrategy
    {
        public Task<string> GetUsedSpaceInHardDriveAsync();
        public Task<string> GetHostnameAsync();
        public Task<string> GetPrivateIpAddressAsync();
    }
}
