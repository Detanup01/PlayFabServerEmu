using PlayFab.DataModels;

namespace PlayFabEmuCore;

internal partial class File
{
    [HTTP("POST", "/File/AbortFileUploads")]
    [HTTP("POST", "/File/AbortFileUploads?{args}")]
    public static bool AbortFileUploads(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<AbortFileUploadsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<AbortFileUploadsResponse>(new());
    }
}
