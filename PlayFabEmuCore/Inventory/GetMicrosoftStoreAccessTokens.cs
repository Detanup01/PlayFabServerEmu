using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetMicrosoftStoreAccessTokens")]
    [HTTP("POST", "/Inventory/GetMicrosoftStoreAccessTokens?{args}")]
    public static bool GetMicrosoftStoreAccessTokens(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetMicrosoftStoreAccessTokensRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetMicrosoftStoreAccessTokensResponse>(new());
    }
}
