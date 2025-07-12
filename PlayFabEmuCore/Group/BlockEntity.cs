using PlayFab.GroupsModels;

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
        return serverStruct.SendSuccess<EmptyResponse>();
    }
}
