using PlayFab.ProfilesModels;

namespace PlayFabEmuCore;

internal partial class Profile
{
    [HTTP("POST", "/Profile/SetProfileLanguage")]
    [HTTP("POST", "/Profile/SetProfileLanguage?{args}")]
    public static bool SetProfileLanguage(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<SetProfileLanguageRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<SetProfileLanguageResponse>();
    }
}
