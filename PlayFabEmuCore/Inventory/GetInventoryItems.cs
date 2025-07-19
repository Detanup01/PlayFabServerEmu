using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetInventoryItems")]
    [HTTP("POST", "/Inventory/GetInventoryItems?{args}")]
    public static bool GetInventoryItems(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetInventoryItemsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetInventoryItemsResponse>(new());
    }
}
