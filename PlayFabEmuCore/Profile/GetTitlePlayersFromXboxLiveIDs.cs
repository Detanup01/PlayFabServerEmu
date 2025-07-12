using PlayFab.ProfilesModels;

namespace PlayFabEmuCore;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetTitlePlayersFromXboxLiveIDs")]
    [HTTP("POST", "/Profile/GetTitlePlayersFromXboxLiveIDs?{args}")]
    public static bool GetTitlePlayersFromXboxLiveIDs(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetTitlePlayersFromXboxLiveIDsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<GetTitlePlayersFromProviderIDsResponse>();
    }
}
