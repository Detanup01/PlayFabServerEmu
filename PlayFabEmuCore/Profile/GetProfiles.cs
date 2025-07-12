using PlayFab.ProfilesModels;

namespace PlayFabEmuCore;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetProfiles")]
    [HTTP("POST", "/Profile/GetProfiles?{args}")]
    public static bool GetProfiles(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetEntityProfilesRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<GetEntityProfilesResponse>();
    }
}
