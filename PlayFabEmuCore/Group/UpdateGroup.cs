using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/UpdateGroup")]
    [HTTP("POST", "/Group/UpdateGroup?{args}")]
    public static bool UpdateGroup(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<UpdateGroupRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<UpdateGroupResponse>();
    }
}
