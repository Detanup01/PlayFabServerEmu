using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

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
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        if (group.Roles.ContainsKey(request.RoleId))
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.DuplicateRoleId,
                ErrorMessage = "DuplicateRoleId"
            });
        if (group.Roles.ContainsValue(request.RoleName))
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        if (request.RoleName.Length is <1 or >100)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        // TODO: Additional checks.
        group.Roles.Add(request.RoleId, request.RoleName);
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<CreateGroupRoleResponse>(new()
        {
            ProfileVersion = 0,
            RoleId = request.RoleId,
            RoleName = request.RoleName,
        });
    }
}
