using PlayFabEmuCore.Models;

namespace PlayFabEmuCore.BackEnd;

internal static class DBManager
{
    public static DataBaseConnection<FabUser> FabUser;

    static DBManager()
    {
        FabUser = new("Users");
    }
}
