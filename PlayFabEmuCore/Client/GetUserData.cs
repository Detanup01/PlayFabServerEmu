using PlayFab.ClientModels;

using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Client
{
    [HTTP("POST", "/Client/GetUserData?{args}")]
    public static bool GetUserData(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetUserDataRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        var fabUser = DBManager.FabUser.GetOne(x => x.TitleAccountId == token.Value.TitleAccountId && x.TitleId == token.Value.TitleId);
        if (serverStruct.ReturnIfNull(fabUser))
            return true;
        DBManager.FabUser.Update(fabUser);
        Dictionary<string, UserDataRecord> Data = [];
        foreach (var item in fabUser.CustomData)
        {
            if (request.Keys.Contains(item.Key))
                Data.Add(item.Key, item.Value);
        }
        return serverStruct.SendSuccess<GetUserDataResult>(new()
        {
            DataVersion = fabUser.DataVersion,
            Data = Data
        });
    }
}
