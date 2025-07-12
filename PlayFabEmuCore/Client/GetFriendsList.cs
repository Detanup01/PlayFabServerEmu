using PlayFab.ClientModels;

namespace PlayFabEmuCore;

internal partial class Client
{

    [HTTP("POST", "/Client/GetFriendsList?{args}")]
    public static bool GetFriendsList(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetFriendsListRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<GetFriendsListResult>(new()
        {
            Friends = []
        });
    }
}
