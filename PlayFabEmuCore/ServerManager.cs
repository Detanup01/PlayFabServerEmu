using ModdableWebServer.Helper;
using ModdableWebServer.Servers;
using NetCoreServer;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace PlayFabEmuCore;

public class ServerManager
{

    static HTTP_Server? HTTP;
    static HTTPS_Server? HTTPS;

    public static void Start()
    {
        DebugPrinter.PrintToConsole = true;
        DebugPrinter.EnableLogs = true;
        if (ServerSettings.Instance().UseHTTPS)
        {

            // These is required! Sadly it doesnt work with PAM but it works as PFX. (Thanks microsoft.)
            var cert = CertHelper.GetCertWithPath(ServerSettings.Instance().SSL.CertPath, ServerSettings.Instance().SSL.KeyPath);
            var new_cert = new X509Certificate2(cert.Export(X509ContentType.Pfx));
            SslContext sslContext = new SslContext(SslProtocols.Tls12 | SslProtocols.Tls13, new_cert);
            HTTPS = new(sslContext, ServerSettings.Instance().HostOn, 443);
            HTTPS.MergeAttributes(Assembly.GetAssembly(typeof(ServerManager))!);
            HTTPS.ReceivedFailed += ReceivedFailed;
            HTTPS.OnSocketError += OnSocketError;
            HTTPS.ReceivedRequestError += RecvError;
            HTTPS.Start();
        }
        else
        {
            HTTP = new(ServerSettings.Instance().HostOn, 80);
            HTTP.MergeAttributes(Assembly.GetAssembly(typeof(ServerManager))!);
            HTTP.ReceivedFailed += ReceivedFailed;
            HTTP.OnSocketError += OnSocketError;
            HTTP.ReceivedRequestError += RecvError;
            HTTP.Start();
        }
    }

    private static void RecvError(object? sender, (HttpRequest request, string error) e)
    {
        Console.WriteLine("RecvError");
        Console.WriteLine("e: " + e.error);
        File.AppendAllText("REQUESTED.txt", e.request.Url + "\n" + e.request.Method + "\n" + e.request.Body + "\n");
        Console.WriteLine(e.request.Url + "\n" + e.request);

    }

    private static void OnSocketError(object? sender, SocketError e)
    {
        Console.WriteLine("OnSocketError");
        Console.WriteLine("e: " + e);
        Console.WriteLine("sender: " + sender);
    }

    public static void Stop()
    {
        if (HTTP != null)
            HTTP?.Stop();
        if (HTTPS != null)
            HTTPS?.Stop();
        HTTP = null;
        HTTPS = null;
    }


    private static void ReceivedFailed(object? sender, HttpRequest request)
    {
        File.AppendAllText("REQUESTED.txt", request.Url + "\n" + request.Method + "\n" + request.Body + "\n");
        Console.WriteLine(request.Url + "\n" + request);
        Console.WriteLine("something isnt good");
    }

}
