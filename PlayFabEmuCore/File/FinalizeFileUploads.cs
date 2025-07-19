using PlayFab.DataModels;

namespace PlayFabEmuCore;

internal partial class File
{
    [HTTP("POST", "/File/FinalizeFileUploads")]
    [HTTP("POST", "/File/FinalizeFileUploads?{args}")]
    public static bool FinalizeFileUploads(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<FinalizeFileUploadsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<FinalizeFileUploadsResponse>(new());
    }
}
