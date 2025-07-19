using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/GetInventoryOperationStatus")]
    [HTTP("POST", "/Inventory/GetInventoryOperationStatus?{args}")]
    public static bool GetInventoryOperationStatus(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetInventoryOperationStatusRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetInventoryOperationStatusResponse>(new());
    }
}
