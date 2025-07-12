using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupApplications")]
    [HTTP("POST", "/Group/ListGroupApplications?{args}")]
    public static bool ListGroupApplications(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ListGroupApplicationsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<ListGroupApplicationsResponse>(new());
    }
}
