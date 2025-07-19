using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemNintendoEShopInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemNintendoEShopInventoryItems?{args}")]
    public static bool RedeemNintendoEShopInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RedeemNintendoEShopInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<RedeemNintendoEShopInventoryItemsResponse>(new());
    }
}
