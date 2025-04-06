using System.Net;

namespace PlayFabEmuCore.Helpers;

public class AppTickets
{
    /*
    For full readme before everything:

    Sig Len = Always 128!

    Full Len = Sig Len + Ownership Len + (IF GC == 56 else 0)

    GCLen = Sig + OWLen

    If has GC tokenLen = 20
    else set to OWLen

    unk is always zero??

    CConTime is NOT EPOCH

    IP For OT is same every req

    GC IP is different every req

    */

    public class DlcDetails
    {
        public uint AppId { get; set; }
        public List<uint> Licenses { get; set; } = [];

        public override string ToString()
        {
            string ret = $"\nAppId: {this.AppId}, LicensesCount: {Licenses.Count}";
            if (Licenses.Count > 0)
            {
                ret += $"Licenses: {string.Join(" ", this.Licenses)}";
            }

            return ret;
        }
    }

    public const int SigLen = 128;
    public const uint GCWriteLen = 20;
    public const uint SessionLen = 24;
    public class TicketRequest
    {
        public required bool HasGCToken { get; set; }
        public required ulong GcToken { get; set; }
        public required uint Version { get; set; }
        public required ulong SteamId { get; set; }
        public required uint AppId { get; set; }
        public required IPAddress OwnershipTicketExternalIP { get; set; }
        public required IPAddress OwnershipTicketInternalIP { get; set; }
        public required uint OwnershipFlags { get; set; }
        public required List<uint> Licenses { get; set; } = [];
        public required List<DlcDetails> DLC { get; set; } = [];
    }

    public class GCStruct
    {
        public ulong GcToken { get; set; }
        public required ulong GCSteamID { get; set; }
        public DateTime TokenGenerated { get; set; }
        public IPAddress SessionInternalIP { get; set; } = IPAddress.None;
        public IPAddress SessionExternalIP { get; set; } = IPAddress.None;
        public uint SessionConnectionTime { get; set; }
        public uint SessionConnectionCount { get; set; }

        public uint GCLen { get; set; }

        public override string ToString()
        {
            return $"\nGCToken: {GcToken}, TokenGenerated: {TokenGenerated}, InternalIP: {SessionInternalIP}, ExternalIP: {SessionExternalIP}, CConnectionTime: {SessionConnectionTime}, CConnectionCount: {SessionConnectionCount}, GCLen: {GCLen}";
        }
    }

    public class TicketStruct
    {
        //Values that are useful
        public int FullLen { get; set; }
        public uint TicketLength { get; set; }
        public int OwnershipLength { get; set; }

        //the Ticket

        //GC related
        public GCStruct? GC { get; set; }

        // normal token related
        public uint Version { get; set; }
        public ulong SteamID { get; set; }
        public uint AppID { get; set; }
        public IPAddress OwnershipTicketExternalIP { get; set; } = IPAddress.None;
        public IPAddress OwnershipTicketInternalIP { get; set; } = IPAddress.None;
        public uint OwnershipFlags { get; set; }
        public DateTime OwnershipTicketGenerated { get; set; }
        public DateTime OwnershipTicketExpires { get; set; }
        public List<uint> Licenses { get; set; } = [];
        public List<DlcDetails> DLC { get; set; } = [];
        public byte[] Signature { get; set; } = [];

        public uint Unk { get; set; }

        public override string ToString()
        {
            string gc_specific = "";
            if (GC != null)
            {
                gc_specific = GC.ToString();
            }
            return $"FullLen: {FullLen}, TicketLen : {TicketLength}, OwnershipLength: {OwnershipLength}, Version: {Version}, SteamID: {SteamID}, AppId: {AppID}, " +
                $"OTExtIP: {OwnershipTicketExternalIP}, " +
                $"OTIntIP: {OwnershipTicketInternalIP}, " +
                $"OFlags: {OwnershipFlags}, " +
                $"OWGenTime: {OwnershipTicketGenerated}, " +
                $"OWExp: {OwnershipTicketExpires}, " +
                $"Licenses: {string.Join(" ", this.Licenses)}," +
                $"DLC Count: {DLC.Count}, " +
                $"DLC's: {string.Join(" ", this.DLC)}, " +
                $"Signature: {BitConverter.ToString(Signature).Replace("-", "")}, SigLen: {Signature.Length} unk: {Unk}" + gc_specific;
        }

        public string ToCensored()
        {
            return $"TicketLen : {TicketLength}, Version: {Version}, AppId: {AppID}, " +
                    $"OFlags: {OwnershipFlags}, " +
                    $"OWGenTime: {OwnershipTicketGenerated}, " +
                    $"OWExp: {OwnershipTicketExpires}, " +
                    $"Licenses: {string.Join(" ", this.Licenses)} " +
                    $"DLC Count: {DLC.Count}, " +
                    $"DLC's: {string.Join(" ", this.DLC)}, " +
                    $"Signature: {BitConverter.ToString(Signature).Replace("-", "")}, SigLen: {Signature.Length} unk: {Unk}";
        }
    }

    /// <summary>
    /// Getting Ticket Structure from ByteArray
    /// </summary>
    /// <param name="ticket">Ticket Data</param>
    /// <returns>The Ticket Structure</returns>
    public static TicketStruct GetTicket(byte[] ticket)
    {
        TicketStruct ticketStruct = new()
        {
            Licenses = [],
            DLC = [],
            FullLen = ticket.Length
        };
        using var ms = new MemoryStream(ticket);
        using var ticketReader = new BinaryReader(ms, System.Text.Encoding.UTF8, true);

        try
        {
            ticketStruct.TicketLength = ticketReader.ReadUInt32();
            if (ticketStruct.TicketLength == 20)
            {
                ticketStruct.GC = new()
                {
                    GcToken = ticketReader.ReadUInt64(),
                    GCSteamID = ticketReader.ReadUInt64(),
                    TokenGenerated = DateTimeOffset.FromUnixTimeSeconds(ticketReader.ReadUInt32()).DateTime
                };

                uint two_four = ticketReader.ReadUInt32();
                if (two_four != 24)
                {
                    throw new Exception("!24 | " + two_four);
                }
                //Console.WriteLine(ticketReader.ReadUInt32()); //always 1
                //Console.WriteLine(ticketReader.ReadUInt32());   //always 2
                ticketReader.BaseStream.Seek(8, SeekOrigin.Current);
                ticketStruct.GC.SessionExternalIP = new IPAddress(ticketReader.ReadUInt32());
                ticketStruct.GC.SessionInternalIP = new IPAddress(ticketReader.ReadUInt32());
                //ticketReader.BaseStream.Seek(4, SeekOrigin.Current);
                ticketStruct.GC.SessionConnectionTime = ticketReader.ReadUInt32();
                ticketStruct.GC.SessionConnectionCount = ticketReader.ReadUInt32();

                ticketStruct.GC.GCLen = ticketReader.ReadUInt32();
                int gcoffset = (int)ms.Position;
                //Console.WriteLine(gcoffset);
                if (ticketStruct.GC.GCLen + gcoffset != ms.Length)
                {
                    throw new Exception("gcoffset != ms.Length | " + ticketStruct.GC.GCLen);
                }
            }
            else
            {
                ms.Seek(-4, SeekOrigin.Current);
            }

            int ownershipTicketOffset = (int)ms.Position;
            //Console.WriteLine(ownershipTicketOffset);
            ticketStruct.OwnershipLength = ticketReader.ReadInt32();
            //Console.WriteLine($"OTO: {ownershipTicketOffset}, OL: {ticketStruct.OwnershipLength}, MSL: {ms.Length}, +: {(ownershipTicketOffset + ticketStruct.OwnershipLength)}");
            if (ownershipTicketOffset + ticketStruct.OwnershipLength != ms.Length &&
                ownershipTicketOffset + ticketStruct.OwnershipLength + 128 != ms.Length)
            {
                throw new Exception("ownershipTicketOffset + ownershipTicketLength | " + $"OTO: {ownershipTicketOffset}, OL: {ticketStruct.OwnershipLength}, MSL: {ms.Length}, +: {(ownershipTicketOffset + ticketStruct.OwnershipLength)}");
            }

            ticketStruct.Version = ticketReader.ReadUInt32();
            ticketStruct.SteamID = ticketReader.ReadUInt64();
            ticketStruct.AppID = ticketReader.ReadUInt32();
            ticketStruct.OwnershipTicketExternalIP = new IPAddress(ticketReader.ReadUInt32());
            ticketStruct.OwnershipTicketInternalIP = new IPAddress(ticketReader.ReadUInt32());
            ticketStruct.OwnershipFlags = ticketReader.ReadUInt32();
            ticketStruct.OwnershipTicketGenerated = DateTimeOffset.FromUnixTimeSeconds(ticketReader.ReadUInt32()).DateTime;
            ticketStruct.OwnershipTicketExpires = DateTimeOffset.FromUnixTimeSeconds(ticketReader.ReadUInt32()).DateTime;
            ticketStruct.Licenses = [];

            int licenseCount = ticketReader.ReadUInt16();
            for (int i = 0; i < licenseCount; i++)
            {
                ticketStruct.Licenses.Add(ticketReader.ReadUInt32());
            }

            int dlcCount = ticketReader.ReadUInt16();
            for (int i = 0; i < dlcCount; i++)
            {
                DlcDetails dlc = new()
                {
                    AppId = ticketReader.ReadUInt32(),
                    Licenses = []
                };

                licenseCount = ticketReader.ReadUInt16();

                for (int j = 0; j < licenseCount; j++)
                {
                    dlc.Licenses.Add(ticketReader.ReadUInt32());
                }

                ticketStruct.DLC.Add(dlc);
            }

            ticketStruct.Unk = ticketReader.ReadUInt16();

            if (ms.Position + 128 == ms.Length)
            {
                ticketStruct.Signature = ticketReader.ReadBytes(128);
            }
            else
            {
                ticketStruct.Signature = ""u8.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
        return ticketStruct;
    }

    /// <summary>
    /// Printing Ticket Data from Bytes
    /// </summary>
    /// <param name="ticket">Ticket as Bytes</param>
    public static void PrintTicket(byte[] ticket) => Console.WriteLine(GetTicket(ticket).ToCensored());
}
