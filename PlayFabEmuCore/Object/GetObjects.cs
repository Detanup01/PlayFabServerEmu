using PlayFab.DataModels;

namespace PlayFabEmuCore;

internal partial class Objects
{
    [HTTP("POST", "/Objects/GetObjects")]
    [HTTP("POST", "/Objects/GetObjects?{args}")]
    public static bool GetObjects(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetObjectsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetObjectsResponse>(new());
    }
}
