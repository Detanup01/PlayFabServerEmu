using PlayFab.ProfilesModels;

namespace PlayFabEmuCore;

internal partial class Profile
{
    [HTTP("POST", "/Profile/SetGlobalPolicy")]
    [HTTP("POST", "/Profile/SetGlobalPolicy?{args}")]
    public static bool SetGlobalPolicy(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<SetGlobalPolicyRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<SetGlobalPolicyResponse>();
    }
}
