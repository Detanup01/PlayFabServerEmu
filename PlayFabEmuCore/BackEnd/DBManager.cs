using PlayFabEmuCore.Models;

namespace PlayFabEmuCore.BackEnd;

internal static class DBManager
{
    public static DataBaseConnection<FabUser> FabUser;
    public static DataBaseConnection<FabLobby> FabLobby;
    public static DataBaseConnection<FabTitle> FabTitle;

    static DBManager()
    {
        FabUser = new("Users");
        FabLobby = new("Lobbies");
        FabTitle = new("Titels");
    }
}
