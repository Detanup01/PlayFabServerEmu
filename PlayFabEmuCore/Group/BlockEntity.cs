using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/BlockEntity")]
    [HTTP("POST", "/Group/BlockEntity?{args}")]
    public static bool BlockEntity(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<BlockEntityRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group == null)
            return serverStruct.SendError(new()
            {
                Error = PlayFab.PlayFabErrorCode.RoleNameNotAvailable,
                ErrorMessage = "RoleNameNotAvailable"
            });
        group.Blocked.Add(request.Entity.Id);
        DBManager.FabGroup.Update(group);
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
