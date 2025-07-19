using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetInventoryCollectionIds")]
    [HTTP("POST", "/Inventory/GetInventoryCollectionIds?{args}")]
    public static bool GetInventoryCollectionIds(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetInventoryCollectionIdsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetInventoryCollectionIdsResponse>(new());
    }
}
