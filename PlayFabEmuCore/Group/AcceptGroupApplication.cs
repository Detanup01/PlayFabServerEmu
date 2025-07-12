using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/AcceptGroupApplication")]
    [HTTP("POST", "/Group/AcceptGroupApplication?{args}")]
    public static bool AcceptGroupApplication(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<AcceptGroupApplicationRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
