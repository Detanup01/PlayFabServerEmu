using PlayFabEmuCore.Models;

namespace PlayFabEmuCore.BackEnd;

internal static class DBManager
{
    public static DataBaseConnection<FabUser> FabUser;
    public static DataBaseConnection<FabLobby> FabLobby;

    static DBManager()
    {
        FabUser = new("Users");
        FabLobby = new("Lobbies");
    }
}
