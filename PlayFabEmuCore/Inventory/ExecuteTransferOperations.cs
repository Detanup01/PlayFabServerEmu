using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Inventory
{
    [HTTP("POST", "/Inventory/ExecuteTransferOperations")]
    [HTTP("POST", "/Inventory/ExecuteTransferOperations?{args}")]
    public static bool ExecuteTransferOperations(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ExecuteTransferOperationsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<ExecuteTransferOperationsResponse>(new());
    }
}
