using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/DeleteEntityItemReviews")]
    [HTTP("POST", "/Catalog/DeleteEntityItemReviews?{args}")]
    public static bool DeleteEntityItemReviews(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<DeleteEntityItemReviewsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<DeleteEntityItemReviewsResponse>(new());
    }
}
