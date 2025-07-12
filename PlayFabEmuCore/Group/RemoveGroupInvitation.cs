using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/RemoveGroupInvitation")]
    [HTTP("POST", "/Group/RemoveGroupInvitation?{args}")]
    public static bool RemoveGroupInvitation(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RemoveGroupInvitationRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
