using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/SubtractInventoryItems")]
    [HTTP("POST", "/Inventory/SubtractInventoryItems?{args}")]
    public static bool SubtractInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<SubtractInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<SubtractInventoryItemsResponse>(new());
    }
}
