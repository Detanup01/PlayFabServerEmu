using PlayFabEmuCore;

namespace PlayFabEmuConsole;

internal class Program
{
    static void Main(string[] _)
    {
        ServerManager.Start();
        Console.ReadLine();
        ServerManager.Stop();
    }
}
