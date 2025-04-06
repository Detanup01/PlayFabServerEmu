using System.Net;

namespace PlayFabEmuCore.UDP;

internal class TestUDP(int port) : UdpServer(IPAddress.Any, port)
{
    protected override void OnStarted()
    {
        ReceiveAsync();
    }

    protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
    {
        var buff = buffer.Skip((int)offset).Take((int)size).ToArray();
        var str = BitConverter.ToString(buff).Replace("-", string.Empty);
        Console.WriteLine($"UDP received: from {endpoint} str: {str}");
        Send(endpoint, buff);
        ReceiveAsync();
    }
}
