using PlayFab.DataModels;

namespace PlayFabEmuCore;

internal partial class File
{
    [HTTP("POST", "/File/DeleteFiles")]
    [HTTP("POST", "/File/DeleteFiles?{args}")]
    public static bool DeleteFiles(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<DeleteFilesRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<DeleteFilesResponse>(new());
    }
}
