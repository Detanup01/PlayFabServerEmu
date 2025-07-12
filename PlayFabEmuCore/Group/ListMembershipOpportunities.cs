using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ListMembershipOpportunities")]
    [HTTP("POST", "/Group/ListMembershipOpportunities?{args}")]
    public static bool ListMembershipOpportunities(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ListMembershipOpportunitiesRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<ListMembershipOpportunitiesResponse>(new());
    }
}
