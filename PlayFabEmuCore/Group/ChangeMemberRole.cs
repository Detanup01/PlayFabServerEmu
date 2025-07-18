using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

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
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        foreach (var item in group.MembersAndRoles.Where(x => x.Value == request.OriginRoleId && request.Members.Any(y => y.Id == x.Key)).Select(x => x.Key).ToList())
        {
            group.MembersAndRoles[item] = request.DestinationRoleId;
        }
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
