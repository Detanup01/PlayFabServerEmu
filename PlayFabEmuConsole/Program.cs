using PlayFabEmuCore;

namespace PlayFabEmuConsole;

internal class Program
{
    static void Main(string[] _)
    {
        Console.WriteLine("Hello, World!");
        ServerManager.Start();
        Console.ReadLine();
        ServerManager.Stop();
    }
}
