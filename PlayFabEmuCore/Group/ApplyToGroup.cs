using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ApplyToGroup")]
    [HTTP("POST", "/Group/ApplyToGroup?{args}")]
    public static bool ApplyToGroup(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ApplyToGroupRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
            });
        var id = request.Entity.Id;
        DateTime time = DateTime.UtcNow;
        if (group.Applications.TryGetValue(id, out time) && time < DateTime.UtcNow && request.AutoAcceptOutstandingInvite.HasValue && request.AutoAcceptOutstandingInvite.Value)
        {
            group.MembersAndRoles.Add(id, group.MemberId);
            group.Applications.Remove(id);
            DBManager.FabGroup.Update(group);
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.OutstandingInvitationAcceptedInstead,
                ErrorMessage = "OutstandingInvitationAcceptedInstead"
            });
        }
        time = DateTime.UtcNow.AddDays(7);
        group.Applications.Add(id, time);
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<ApplyToGroupResponse>(new()
        { 
            Entity = new()
            {
                Key = new()
                {
                    Id = id,
                    Type = "title_player_account"
                },
                Lineage = [],
            },
            Expires = time,
            Group = request.Group
        });
    }
}
