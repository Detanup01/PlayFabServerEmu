using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/InviteToGroup")]
    [HTTP("POST", "/Group/InviteToGroup?{args}")]
    public static bool InviteToGroup(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<InviteToGroupRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var sessionInfo  = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(sessionInfo))
            return true;
        var inviter = sessionInfo.Value.TitleAccountId;
        // need to get the user here.
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.EntityBlockedByGroup,
                ErrorMessage = "EntityBlockedByGroup"
            });
        var roleid = string.IsNullOrEmpty(request.RoleId) ? group.MemberId : request.RoleId;
        var id = request.Entity.Id;
        DateTime time = DateTime.UtcNow;
        if (group.Applications.TryGetValue(id, out time) && time < DateTime.UtcNow && request.AutoAcceptOutstandingApplication.HasValue && request.AutoAcceptOutstandingApplication.Value)
        {
            group.MembersAndRoles.Add(id, roleid);
            group.Applications.Remove(id);
            group.Invitations.Remove(id);
            DBManager.FabGroup.Update(group);
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.OutstandingApplicationAcceptedInstead,
                ErrorMessage = "OutstandingApplicationAcceptedInstead"
            });
        }
        time = DateTime.UtcNow.AddDays(7);
        group.Applications.Add(id, time);
        group.Invitations.Add(id, roleid);
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<InviteToGroupResponse>(new()
        { 
            Expires = time,
            Group = request.Group,
            RoleId = roleid,
            InvitedByEntity = new()
            { 
                Key = new()
                { 
                    Id = inviter,
                    Type = "title_player_account"
                },
                Lineage = []
            },
            InvitedEntity = new()
            {
                Key = new()
                {
                    Id = id,
                    Type = "title_player_account"
                },
                Lineage = []
            }
        });
    }
}
