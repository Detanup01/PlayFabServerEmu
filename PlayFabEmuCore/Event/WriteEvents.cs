using PlayFab.EventsModels;
using System.Security.Cryptography;

namespace PlayFabEmuCore;

internal partial class Event
{
    [HTTP("POST", "/Event/WriteEvents")]
    [HTTP("POST", "/Event/WriteEvents?{args}")]
    public static bool WriteEvents(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<WriteEventsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        List<string> ids = [];
        for (int i = 0; i < request.Events.Count; i++)
        {
            ids.Add(RandomNumberGenerator.GetString("0123456789abcdef", 32));
        }

        return serverStruct.SendSuccess<WriteEventsResponse>(new()
        {
            AssignedEventIds = ids
        });
    }
}
