using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/AcceptGroupInvitation")]
    [HTTP("POST", "/Group/AcceptGroupInvitation?{args}")]
    public static bool AcceptGroupInvitation(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<AcceptGroupInvitationRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
