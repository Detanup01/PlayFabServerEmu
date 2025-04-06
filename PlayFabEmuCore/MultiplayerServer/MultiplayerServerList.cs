using PlayFab.MultiplayerModels;

namespace PlayFabEmuCore.MultiplayerServer;

internal partial class MultiplayerServer
{
    [HTTP("POST", "/MultiplayerServer/ListPartyQosServers?{args}")]
    public static bool ListPartyQosServers(HttpRequest _, ServerStruct serverStruct)
    {
        return serverStruct.SendSuccess<ListPartyQosServersResponse>(new()
        {
            PageSize = 1,
            QosServers =
            [
                new()
                {
                    ServerUrl = "127.0.0.1:6666",
                    Region = "WestEurope"
                }
            ]
        });
    }
}
