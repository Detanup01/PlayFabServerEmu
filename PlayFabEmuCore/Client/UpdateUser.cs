using PlayFab.ClientModels;

using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Client
{
    [HTTP("POST", "/Client/UpdateUserTitleDisplayName?{args}")]
    public static bool UpdateUserTitleDisplayName(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<UpdateUserTitleDisplayNameRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetEntityToken().GetFabEntityToken();
        if (serverStruct.ReturnIfNull(token))
            return true;
        var fabUser = DBManager.FabUser.GetOne(x => x.TitleAccountId == token.EntityId);
        fabUser.DisplayName = request.DisplayName;
        DBManager.FabUser.Update(fabUser);
        return serverStruct.SendSuccess<UpdateUserTitleDisplayNameResult>(new()
        { 
            DisplayName = request.DisplayName,
        });
    }

    [HTTP("POST", "/Client/UpdateUserData?{args}")]
    public static bool UpdateUserData(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<UpdateUserDataRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetEntityToken().GetFabEntityToken();
        if (serverStruct.ReturnIfNull(token))
            return true;
        var fabUser = DBManager.FabUser.GetOne(x => x.TitleAccountId == token.EntityId);
        foreach (var item in request.Data)
        {
            fabUser.CustomData.Add(item.Key, new()
            { 
                LastUpdated = DateTime.Now,
                Permission = request.Permission,
                Value = item.Value
            });
        }
        foreach (var key in request.KeysToRemove)
        {
            fabUser.CustomData.Remove(key);
        }
        fabUser.DataVersion++;
        DBManager.FabUser.Update(fabUser);
        return serverStruct.SendSuccess<UpdateUserDataResult>(new()
        {
            DataVersion = fabUser.DataVersion,
        });
    }
}
