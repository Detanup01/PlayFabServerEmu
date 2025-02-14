using NetCoreServer;
using System;
using System.Net;

namespace PlayFabEmuCore.UDP;

internal class TestUDP : UdpServer
{
    public TestUDP(int port) : base(IPAddress.Any, port)
    {

    }

    protected override void OnStarted()
    {
        ReceiveAsync();
    }

    protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
    {
        var buff = buffer.Skip((int)offset).Take((int)size).ToArray();
        var str = BitConverter.ToString(buff).Replace("-", string.Empty);
        Console.WriteLine($"UDP received: from {endpoint.ToString()} str: {str}");
        Send(endpoint, buff);
        ReceiveAsync();
    }
}
