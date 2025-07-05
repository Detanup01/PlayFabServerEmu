using PlayFab.EventsModels;
using System.Security.Cryptography;

namespace PlayFabEmuCore;

internal partial class Event
{
    [HTTP("POST", "/Event/WriteTelemetryEvents")]
    public static bool WriteTelemetryEvents(HttpRequest req, ServerStruct serverStruct)
    {
        return WriteTelemetryEventsArgs(req, serverStruct);
    }

    [HTTP("POST", "/Event/WriteTelemetryEvents?{args}")]
    public static bool WriteTelemetryEventsArgs(HttpRequest req, ServerStruct serverStruct)
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
