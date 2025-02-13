using PlayFabEmuCore;

namespace PlayFabEmuConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        ServerManager.Start();
        Console.ReadLine();
        ServerManager.Stop();
    }
}
