using PlayFab.GroupsModels;

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
        return serverStruct.SendSuccess<GetGroupResponse>(new());
    }
}
