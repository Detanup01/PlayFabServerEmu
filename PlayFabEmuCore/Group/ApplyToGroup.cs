using PlayFab.GroupsModels;

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
        return serverStruct.SendSuccess<ApplyToGroupResponse>(new());
    }
}
