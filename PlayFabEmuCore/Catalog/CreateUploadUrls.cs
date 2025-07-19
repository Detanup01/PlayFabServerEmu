using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/CreateUploadUrls")]
    [HTTP("POST", "/Catalog/CreateUploadUrls?{args}")]
    public static bool CreateUploadUrls(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<CreateUploadUrlsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<CreateUploadUrlsResponse>(new());
    }
}
