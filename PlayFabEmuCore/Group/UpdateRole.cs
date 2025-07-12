using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/UpdateRole")]
    [HTTP("POST", "/Group/UpdateRole?{args}")]
    public static bool UpdateRole(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<UpdateGroupRoleRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<UpdateGroupRoleResponse>();
    }
}
