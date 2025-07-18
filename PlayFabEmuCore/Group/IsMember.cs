using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/IsMember")]
    [HTTP("POST", "/Group/IsMember?{args}")]
    public static bool IsMember(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<IsMemberRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        bool contains = group.MembersAndRoles.ContainsKey(request.Entity.Id);
        if (contains && !string.IsNullOrEmpty(request.RoleId))
            contains = group.MembersAndRoles.TryGetValue(request.RoleId, out var role) && role == request.RoleId;
        return serverStruct.SendSuccess<IsMemberResponse>(new()
        { 
            IsMember = contains,
        });
    }
}
