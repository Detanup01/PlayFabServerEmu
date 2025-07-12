using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/RemoveMembers")]
    [HTTP("POST", "/Group/RemoveMembers?{args}")]
    public static bool RemoveMembers(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RemoveMembersRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
