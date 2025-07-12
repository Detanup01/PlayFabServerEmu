using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupMembers")]
    [HTTP("POST", "/Group/ListGroupMembers?{args}")]
    public static bool ListGroupMembers(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ListGroupMembersRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<ListGroupMembersResponse>(new());
    }
}
