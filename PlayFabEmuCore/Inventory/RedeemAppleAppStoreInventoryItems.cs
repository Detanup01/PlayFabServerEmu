using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemAppleAppStoreInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemAppleAppStoreInventoryItems?{args}")]
    public static bool RedeemAppleAppStoreInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RedeemAppleAppStoreInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<RedeemAppleAppStoreInventoryItemsResponse>(new());
    }
}
