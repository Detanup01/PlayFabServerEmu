using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/InviteToGroup")]
    [HTTP("POST", "/Group/InviteToGroup?{args}")]
    public static bool InviteToGroup(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<InviteToGroupRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<InviteToGroupResponse>(new());
    }
}
