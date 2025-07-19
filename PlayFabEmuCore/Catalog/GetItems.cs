using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItems")]
    [HTTP("POST", "/Catalog/GetItems?{args}")]
    public static bool GetItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetItemsResponse>(new());
    }
}
