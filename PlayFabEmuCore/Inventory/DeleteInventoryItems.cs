using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/DeleteInventoryItems")]
    [HTTP("POST", "/Inventory/DeleteInventoryItems?{args}")]
    public static bool DeleteInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<DeleteInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<DeleteInventoryItemsResponse>(new());
    }
}
