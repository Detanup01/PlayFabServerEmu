using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/AddMembers")]
    [HTTP("POST", "/Group/AddMembers?{args}")]
    public static bool AddMembers(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<AddMembersRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        if (!group.Roles.ContainsKey(request.RoleId))
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleDoesNotExist,
                ErrorMessage = "RoleDoesNotExist"
            });
        foreach (var item in request.Members)
            group.MembersAndRoles.Add(item.Id, request.RoleId);
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
