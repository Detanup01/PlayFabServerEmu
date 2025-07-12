using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupInvitations")]
    [HTTP("POST", "/Group/ListGroupInvitations?{args}")]
    public static bool ListGroupInvitations(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ListGroupInvitationsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<ListGroupInvitationsResponse>(new());
    }
}
