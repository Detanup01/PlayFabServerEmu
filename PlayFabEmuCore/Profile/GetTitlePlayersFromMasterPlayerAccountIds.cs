using PlayFab.ProfilesModels;

namespace PlayFabEmuCore;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetTitlePlayersFromMasterPlayerAccountIds")]
    [HTTP("POST", "/Profile/GetTitlePlayersFromMasterPlayerAccountIds?{args}")]
    public static bool GetTitlePlayersFromMasterPlayerAccountIds(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetTitlePlayersFromMasterPlayerAccountIdsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<GetTitlePlayersFromMasterPlayerAccountIdsResponse>();
    }
}
