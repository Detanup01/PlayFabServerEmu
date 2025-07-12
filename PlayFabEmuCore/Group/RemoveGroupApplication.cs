using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/RemoveGroupApplication")]
    [HTTP("POST", "/Group/RemoveGroupApplication?{args}")]
    public static bool RemoveGroupApplication(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<RemoveGroupApplicationRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
