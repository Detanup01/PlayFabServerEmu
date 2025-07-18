using PlayFabEmuCore.Models;

namespace PlayFabEmuCore.BackEnd;

internal static class DBManager
{
    public static DataBaseConnection<FabUser> FabUser;
    public static DataBaseConnection<FabLobby> FabLobby;
    public static DataBaseConnection<FabTitle> FabTitle;
    public static DataBaseConnection<FabGroup> FabGroup;


    static DBManager()
    {
        FabUser = new("Users");
        FabLobby = new("Lobbies");
        FabTitle = new("Titels");
        FabGroup = new("Groups");
    }
}
