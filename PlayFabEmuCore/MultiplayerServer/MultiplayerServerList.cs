using ModdableWebServer.Attributes;
using ModdableWebServer;
using NetCoreServer;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.Internal;
using ModdableWebServer.Helper;
using PlayFab.MultiplayerModels;

namespace PlayFabEmuCore.MultiplayerServer;

internal class MultiplayerServerList
{
    [HTTP("POST", "/MultiplayerServer/ListPartyQosServers?{args}")]
    public static bool ListPartyQosServers(HttpRequest req, ServerStruct serverStruct)
    {
        var ret = new PlayFabJsonSuccess<ListPartyQosServersResponse>()
        {
            code = 200,
            status = "OK",
            data = new()
            {
                PageSize = 1,
                QosServers = new()
                {
                    new()
                    {
                        ServerUrl = "192.168.3.50",
                        Region = "WestEurope"
                    }
                }
            }
        };
        serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(ret));
        serverStruct.SendResponse();
        return true;
    }
}
