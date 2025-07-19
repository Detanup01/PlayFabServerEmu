using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/RedeemGooglePlayInventoryItems")]
    [HTTP("POST", "/Inventory/RedeemGooglePlayInventoryItems?{args}")]
    public static bool RedeemGooglePlayInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RedeemGooglePlayInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<RedeemGooglePlayInventoryItemsResponse>(new());
    }
}
