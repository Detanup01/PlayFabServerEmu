using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemSteamInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemSteamInventoryItems?{args}")]
    public static bool RedeemSteamInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RedeemSteamInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<RedeemSteamInventoryItemsResponse>(new());
    }
}
