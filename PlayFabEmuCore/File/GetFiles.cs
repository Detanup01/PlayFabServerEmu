using PlayFab.DataModels;

namespace PlayFabEmuCore;

internal partial class File
{
    [HTTP("POST", "/File/GetFiles")]
    [HTTP("POST", "/File/GetFiles?{args}")]
    public static bool GetFiles(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetFilesRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetFilesResponse>(new());
    }
}
