using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/DeleteGroup")]
    [HTTP("POST", "/Group/DeleteGroup?{args}")]
    public static bool DeleteGroup(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<DeleteGroupRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        DBManager.FabGroup.Delete(x=>x.Id == request.Group.Id);
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
