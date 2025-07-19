using PlayFab.DataModels;

namespace PlayFabEmuCore;

internal partial class Objects
{
    [HTTP("POST", "/Objects/SetObjects")]
    [HTTP("POST", "/Objects/SetObjects?{args}")]
    public static bool SetObjects(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<SetObjectsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<SetObjectsResponse>(new());
    }
}
