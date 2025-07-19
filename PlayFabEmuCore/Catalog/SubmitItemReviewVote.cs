using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/SubmitItemReviewVote")]
    [HTTP("POST", "/Catalog/SubmitItemReviewVote?{args}")]
    public static bool SubmitItemReviewVote(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<SubmitItemReviewVoteRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<SubmitItemReviewVoteResponse>(new());
    }
}
