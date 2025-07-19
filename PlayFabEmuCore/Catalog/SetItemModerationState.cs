using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/SetItemModerationState")]
    [HTTP("POST", "/Catalog/SetItemModerationState?{args}")]
    public static bool SetItemModerationState(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<SetItemModerationStateRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<SetItemModerationStateResponse>(new());
    }
}
