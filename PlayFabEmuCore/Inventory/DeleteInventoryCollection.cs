using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/DeleteInventoryCollection")]
    [HTTP("POST", "/Inventory/DeleteInventoryCollection?{args}")]
    public static bool DeleteInventoryCollection(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<DeleteInventoryCollectionRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<DeleteInventoryCollectionResponse>(new());
    }
}
