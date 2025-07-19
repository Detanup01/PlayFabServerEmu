using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemMicrosoftStoreInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemMicrosoftStoreInventoryItems?{args}")]
    public static bool RedeemMicrosoftStoreInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RedeemMicrosoftStoreInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<RedeemMicrosoftStoreInventoryItemsResponse>(new());
    }
}
