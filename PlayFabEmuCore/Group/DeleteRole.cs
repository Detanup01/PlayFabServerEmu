using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;
using PlayFabEmuCore.Models;

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
        if (FabGroup.MainRoles.ContainsKey(request.RoleId))
        {
            return serverStruct.SendError(new()
            { 
                Error = PlayFab.PlayFabErrorCode.RoleIsGroupDefaultMember,
                ErrorMessage = "RoleIsGroupDefaultMember"
            });
        }
        var group = DBManager.FabGroup.GetOne(x=>x.Id == request.Group.Id);
        if (group == null)
            return serverStruct.SendSuccess<EmptyResponse>();
        if (!group.Roles.ContainsKey(request.RoleId))
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        group.Roles.Remove(request.RoleId);
        for (int i = 0; i < group.MembersAndRoles.Count; i++)
        {
            var member = group.MembersAndRoles.ElementAt(i);
            group.MembersAndRoles[member.Key] = group.MemberId;
        }
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
