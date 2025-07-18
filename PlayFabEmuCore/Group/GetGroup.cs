using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/GetGroup")]
    [HTTP("POST", "/Group/GetGroup?{args}")]
    public static bool GetGroup(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetGroupRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var group = DBManager.FabGroup.GetOne(x=>x.Id == request.Group.Id && x.Name == request.GroupName);
        if (group == null)
            return serverStruct.SendError(new()
            { 
                Error = PlayFab.PlayFabErrorCode.GroupNameNotAvailable,
                ErrorMessage = "GroupNameNotAvailable"
            });
        return serverStruct.SendSuccess<GetGroupResponse>(new()
        { 
            AdminRoleId = group.AdminId,
            Created = group.CreatedAt,
            Group = new()
            { 
                Id = group.Id,
                Type = "group"
            },
            GroupName = group.Name,
            MemberRoleId = group.MemberId,
            ProfileVersion = 0,
            Roles = group.Roles
        });
    }
}
