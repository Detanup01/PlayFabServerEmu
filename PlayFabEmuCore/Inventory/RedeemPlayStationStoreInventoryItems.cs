using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemPlayStationStoreInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemPlayStationStoreInventoryItems?{args}")]
    public static bool RedeemPlayStationStoreInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RedeemPlayStationStoreInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<RedeemPlayStationStoreInventoryItemsResponse>(new());
    }
}
