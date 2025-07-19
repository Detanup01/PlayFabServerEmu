using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/UpdateDraftItem")]
    [HTTP("POST", "/Catalog/UpdateDraftItem?{args}")]
    public static bool UpdateDraftItem(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<UpdateDraftItemRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<UpdateDraftItemResponse>(new());
    }
}
