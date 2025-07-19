using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/TakedownItemReviews")]
    [HTTP("POST", "/Catalog/TakedownItemReviews?{args}")]
    public static bool TakedownItemReviews(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<TakedownItemReviewsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<TakedownItemReviewsResponse>(new());
    }
}
