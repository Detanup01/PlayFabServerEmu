using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using ModdableWebServer;
using NetCoreServer;
using Newtonsoft.Json;
using PlayFab.ClientModels;

namespace PlayFabEmuCore.Client;

internal class Trade
{
    [HTTP("POST", "/Client/AcceptTrade?{args}")]
    public static bool AcceptTrade(HttpRequest req, ServerStruct serverStruct)
    {
        foreach (var item in serverStruct.Parameters)
        {
            Console.WriteLine(item.Key + " " + item.Value);
        }
        _ = JsonConvert.DeserializeObject<AcceptTradeRequest>(req.Body);
        serverStruct.Response.MakeOkResponse(204);
        serverStruct.SendResponse();
        return true;
    }
}
