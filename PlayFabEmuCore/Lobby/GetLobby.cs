using PlayFab.MultiplayerModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Lobby
{
    [HTTP("POST", "/Lobby/GetLobby")]
    public static bool GetLobby(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetLobbyRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;;
        var fabLobby = DBManager.FabLobby.GetOne(x=>x.Lobby.LobbyId == request.LobbyId);
        if (serverStruct.ReturnIfNull(fabLobby))
            return true; ;
        return serverStruct.SendSuccess<GetLobbyResult>(new()
        {
            Lobby = fabLobby.Lobby,
        });
    }
}
