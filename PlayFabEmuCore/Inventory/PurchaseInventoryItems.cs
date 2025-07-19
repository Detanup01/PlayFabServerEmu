using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/PurchaseInventoryItems")]
    [HTTP("POST", "/Inventory/PurchaseInventoryItems?{args}")]
    public static bool PurchaseInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<PurchaseInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<PurchaseInventoryItemsResponse>(new());
    }
}
