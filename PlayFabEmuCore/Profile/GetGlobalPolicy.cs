using PlayFab.ProfilesModels;

namespace PlayFabEmuCore;

internal partial class Profile
{
    [HTTP("POST", "/Profile/GetGlobalPolicy")]
    [HTTP("POST", "/Profile/GetGlobalPolicy?{args}")]
    public static bool GetGlobalPolicy(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetGlobalPolicyRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<GetGlobalPolicyResponse>();
    }
}
