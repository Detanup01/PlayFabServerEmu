using PlayFab.DataModels;

namespace PlayFabEmuCore;

internal partial class File
{
    [HTTP("POST", "/File/InitiateFileUploads")]
    [HTTP("POST", "/File/InitiateFileUploads?{args}")]
    public static bool InitiateFileUploads(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<InitiateFileUploadsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<InitiateFileUploadsResponse>(new());
    }
}
