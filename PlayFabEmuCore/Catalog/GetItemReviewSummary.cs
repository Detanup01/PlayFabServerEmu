using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/GetItemReviewSummary")]
    [HTTP("POST", "/Catalog/GetItemReviewSummary?{args}")]
    public static bool GetItemReviewSummary(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetItemReviewSummaryRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<GetItemReviewSummaryResponse>(new());
    }
}
