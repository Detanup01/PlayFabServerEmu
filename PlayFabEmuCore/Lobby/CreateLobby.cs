using PlayFab.MultiplayerModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Lobby
{
    [HTTP("POST", "/Lobby/CreateLobby")]
    [HTTP("POST", "/Lobby/CreateLobby?{args}")]
    public static bool CreateLobby(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<CreateLobbyRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        string LobbyId = Guid.NewGuid().ToString();
        DBManager.FabLobby.Create(new()
        { 
            Lobby = new()
            { 
                AccessPolicy = request.AccessPolicy ??= AccessPolicy.Public,
                SearchData = request.SearchData,
                ChangeNumber = 1,
                LobbyData = request.LobbyData,
                MaxPlayers = request.MaxPlayers,
                Owner = request.Owner,
                Members = request.Members,
                OwnerMigrationPolicy = request.OwnerMigrationPolicy,
                UseConnections = request.UseConnections,
                MembershipLock = MembershipLock.Unlocked,
                RestrictInvitesToLobbyOwner = request.RestrictInvitesToLobbyOwner,
                LobbyId = LobbyId,
                ConnectionString = LobbyId,
            }
        });

        return serverStruct.SendSuccess<CreateLobbyResult>(new()
        {
            LobbyId = LobbyId,
            ConnectionString = LobbyId
        });
    }
}
