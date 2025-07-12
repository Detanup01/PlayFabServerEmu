using PlayFab.ProfilesModels;

namespace PlayFabEmuCore;

internal partial class Profile
{
    [HTTP("POST", "/Profile/SetProfilePolicy")]
    [HTTP("POST", "/Profile/SetProfilePolicy?{args}")]
    public static bool SetProfilePolicy(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<SetEntityProfilePolicyRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<SetEntityProfilePolicyResponse>();
    }
}
