using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ChangeMemberRole")]
    [HTTP("POST", "/Group/ChangeMemberRole?{args}")]
    public static bool ChangeMemberRole(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ChangeMemberRoleRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
