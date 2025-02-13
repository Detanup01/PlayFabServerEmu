using Newtonsoft.Json;
using PlayFabEmuCore;
using PlayFabEmuCore.Models;

namespace PlayFabEmuConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var id = FabId.Empty;
        Console.WriteLine(JsonConvert.SerializeObject(id));
        ServerManager.Start();
        Console.ReadLine();
        ServerManager.Stop();
    }
}
