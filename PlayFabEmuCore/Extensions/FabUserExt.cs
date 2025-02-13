using Newtonsoft.Json;
using PlayFabEmuCore.Models;
using System.Security.Cryptography;

namespace PlayFabEmuCore.Extensions;

internal static class FabUserExt
{
    public static string GenerateSessionTicket(this FabUser user)
    {
        return $"{user.PlayFabId}-{user.GameId}-{user.TitleAccountId}-{user.TitleId}-{user.RandomId}-{Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))}";
    }

    public static string CreateEntityCredential(this FabUser user)
    {
        return $"{user.GameId}/{user.TitleId}/{user.PlayFabId}/{user.TitleId}/{user.TitleAccountId}/";
    }

    public static string CreateEntityToken(this FabUser user)
    {
        return $"4!{RandomNumberGenerator.GetString("0123456789qwertzuiopasdfghjklyxcvbnm", 44)}|{JsonConvert.SerializeObject(user.CreateFabEntityToken())}";
    }

    public static FabEntityToken CreateFabEntityToken(this FabUser user)
    {
        return new()
        { 
            CreatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            IdentityPlatform = user.PlatformType,
            IdentityId = user.PlatformId,
            CreatedAt2 = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            EntityType = "title_player_account",
            EntityCredentials = "title_player_account!" + user.CreateEntityCredential(),
            EntityId = user.TitleAccountId,
            ExpAt = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ"),
            Host = "internal",
            UnkownId = RandomNumberGenerator.GetString("0123456789qwertzuiopasdfghjklyxcvbnm", 11)
        };
    }
}
