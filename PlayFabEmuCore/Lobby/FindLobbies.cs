using PlayFab.MultiplayerModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Lobby
{
    [HTTP("POST", "/Lobby/FindLobbies")]
    public static bool FindLobbies(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<FindLobbiesRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        Console.WriteLine("Filter for Lobby: " + request.Filter);
        return serverStruct.SendSuccess<FindLobbiesResult>(new()
        {
            Lobbies = [],
            Pagination =
            { 
                ContinuationToken = string.Empty,
                TotalMatchedLobbyCount = 0,
            }
        });
    }

    [HTTP("POST", "/Lobby/FindFriendLobbies")]
    public static bool FindFriendLobbies(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<FindFriendLobbiesRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        Console.WriteLine("Filter for Lobby: " + request.Filter);
        return serverStruct.SendSuccess<FindLobbiesResult>(new()
        {
            Lobbies = [],
            Pagination =
            {
                ContinuationToken = string.Empty,
                TotalMatchedLobbyCount = 0,
            }
        });
    }
}
