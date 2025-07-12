using PlayFab.LocalizationModels;

namespace PlayFabEmuCore;

internal partial class Locale
{
    [HTTP("POST", "/Profile/GetLanguageList")]
    [HTTP("POST", "/Profile/GetLanguageList?{args}")]
    public static bool GetLanguageList(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetLanguageListRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<GetLanguageListResponse>();
    }
}
