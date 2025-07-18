using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;
using PlayFabEmuCore.Models;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/CreateGroup")]
    [HTTP("POST", "/Group/CreateGroup?{args}")]
    public static bool CreateGroup(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<CreateGroupRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        if (DBManager.FabGroup.GetOne(x=>x.Name == request.GroupName) != null)
        {
            return serverStruct.SendError(new()
            { 
                Error = PlayFab.PlayFabErrorCode.GroupNameNotAvailable,
                ErrorMessage = "GroupNameNotAvailable"
            });
        }
        FabGroup fabGroup = new()
        { 
            Id = FabId.RandomId,
            Name = request.GroupName,
            Roles = FabGroup.MainRoles,
        };
        DBManager.FabGroup.Create(fabGroup);
        return serverStruct.SendSuccess<CreateGroupResponse>(new()
        { 
            AdminRoleId = fabGroup.AdminId,
            Created = fabGroup.CreatedAt,
            Group = new()
            { 
                Id = fabGroup.Id,
                Type = "group",
            },
            GroupName = request.GroupName,
            MemberRoleId = fabGroup.MemberId,
            ProfileVersion = 0,
            Roles = fabGroup.Roles,
        });
    }
}
