using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/IsMember")]
    [HTTP("POST", "/Group/IsMember?{args}")]
    public static bool IsMember(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<IsMemberRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<IsMemberResponse>(new());
    }
}
