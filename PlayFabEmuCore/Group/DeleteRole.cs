using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/DeleteRole")]
    [HTTP("POST", "/Group/DeleteRole?{args}")]
    public static bool DeleteRole(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<DeleteRoleRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
