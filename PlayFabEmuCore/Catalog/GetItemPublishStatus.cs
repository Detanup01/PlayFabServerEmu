using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItemPublishStatus")]
    [HTTP("POST", "/Catalog/GetItemPublishStatus?{args}")]
    public static bool GetItemPublishStatus(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetItemPublishStatusRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetItemPublishStatusResponse>(new());
    }
}
