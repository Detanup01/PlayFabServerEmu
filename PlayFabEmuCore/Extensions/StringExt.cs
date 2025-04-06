using PlayFabEmuCore.Models;
using System.Buffers.Text;
using System.Text;

namespace PlayFabEmuCore.Extensions;

public static class StringExt
{
    public static FabEntityToken? GetFabEntityToken(this string token)
    {
        if (!Base64.IsValid(token))
            return null;
        string parsedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        if (!parsedString.StartsWith("4|"))
            return null;
        if (!parsedString.Contains("|{"))
            return null;
        // removing the first 4 chars and splitting by |
        string jsonString = parsedString[2..].Split("|")[1];
        return JsonConvert.DeserializeObject<FabEntityToken>(jsonString);
    }
}
