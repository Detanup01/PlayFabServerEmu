using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

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
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.GroupInvitationNotFound,
                ErrorMessage = "GroupInvitationNotFound"
            });
        if (group.Blocked.Contains(request.Entity.Id))
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
            });
        if (group.MembersAndRoles.ContainsKey(request.Entity.Id))
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.EntityIsAlreadyMember,
                ErrorMessage = "EntityIsAlreadyMember"
            });
        if (group.Invitations.TryGetValue(request.Entity.Id, out string? role))
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.GroupInvitationNotFound,
                ErrorMessage = "GroupInvitationNotFound"
            });
        if (string.IsNullOrEmpty(role) || group.Roles.ContainsKey(role))
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        group.MembersAndRoles.Add(request.Entity.Id, role);
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
