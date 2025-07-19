using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/CreateDraftItem")]
    [HTTP("POST", "/Catalog/CreateDraftItem?{args}")]
    public static bool CreateDraftItem(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<CreateDraftItemRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<CreateDraftItemResponse>(new());
    }
}
