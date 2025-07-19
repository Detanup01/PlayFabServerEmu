using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/AcceptGroupApplication")]
    [HTTP("POST", "/Group/AcceptGroupApplication?{args}")]
    public static bool AcceptGroupApplication(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<AcceptGroupApplicationRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
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
        if (!group.Applications.TryGetValue(request.Entity.Id, out DateTime time) || time < DateTime.Now)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.GroupApplicationNotFound,
                ErrorMessage = "GroupApplicationNotFound"
            });
        group.MembersAndRoles.Add(request.Entity.Id, group.MemberId);
        group.Applications.Remove(request.Entity.Id);
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
