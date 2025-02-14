using ModdableWebServer.Attributes;
using ModdableWebServer;
using NetCoreServer;
using Newtonsoft.Json;
using PlayFab.Internal;
using ModdableWebServer.Helper;
using PlayFab.MultiplayerModels;
using PlayFab.Json;

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
                        ServerUrl = "127.0.0.1",
                        Region = "WestEurope"
                    }
                }
            }
        };
        serverStruct.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(ret), "application/json");
        serverStruct.SendResponse();
        return true;
    }
}
