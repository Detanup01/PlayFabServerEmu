using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/ExecuteInventoryOperations")]
    [HTTP("POST", "/Inventory/ExecuteInventoryOperations?{args}")]
    public static bool ExecuteInventoryOperations(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ExecuteInventoryOperationsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<ExecuteInventoryOperationsResponse>(new());
    }
}
