using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/CreateRole")]
    [HTTP("POST", "/Group/CreateRole?{args}")]
    public static bool CreateRole(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<CreateGroupRoleRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<CreateGroupRoleResponse>(new());
    }
}
