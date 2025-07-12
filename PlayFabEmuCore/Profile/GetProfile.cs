using PlayFab.ProfilesModels;

namespace PlayFabEmuCore;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetProfile")]
    [HTTP("POST", "/Profile/GetProfile?{args}")]
    public static bool GetProfile(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetEntityProfileRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<GetEntityProfileResponse>();
    }
}
