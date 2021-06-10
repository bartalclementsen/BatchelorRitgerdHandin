// ProxyEnumStreamUnit.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Proxy.Streaming
{
    public enum CallStateEnum
    { // RecId: 1000
        Idle = 0,
        Initiated = 1,
        Alerting = 2,
        Connected = 3,
        Queued = 4,
        Error = 5,
        Hold = 6,
        Fail = 7,
        Unknown = 8,
        Offline = 9,
    }

    public enum AgentStateEnum
    { // RecId: 1001
        Null = 0,
        Busy = 1,
        NotReady = 2,
        Ready = 3,
        WorkAfterCall = 4,
    }

    public enum DeviceTypeEnum
    { // RecId: 1002
        Unknown = 0,
        Device = 1,
        Trunk = 2,
        Mobile = 3,
        External = 4,
    }

    public enum DeviceSubTypeEnum
    { // RecId: 1003
        Unknown = 0,
        ACD = 1,
        Group = 2,
        Trunk = 3,
        IPTrunk = 4,
        System = 5,
        DECT = 6,
        AnalogOrVirtual = 7,
        ISDN = 8,
        VoiceUnit = 9,
        Other = 10,
    }

    public enum DeviceStateEnum
    { // RecId: 1004
        Null = 0,
        NotFound = 1,
        Connected = 2,
        Running = 3,
        Disconnected = 4,
        Error = 5,
    }

    public enum SuggestedRuleEnum
    { // RecId: 1005
        ssrFixed = 0,
        ssr5min = 1,
        ssr10min = 2,
        ssr15min = 3,
        ssr30min = 4,
        ssr1h = 5,
    }

    public enum SuggestedRoundTypeEnum
    { // RecId: 1006
        Regular = 0,
        Up = 1,
        Down = 2,
    }

    public enum ForwardTypeEnum
    { // RecId: 1007
        Ignore = 0,
        None = 1,
        All = 2,
        External = 3,
        Internal = 4,
    }

    public enum DNDTypeEnum
    { // RecId: 1008
        Ignore = 0,
        On = 1,
        Off = 2,
    }

    public enum ResourceDetailLinkEnum
    { // RecId: 1009
        WrongType = 0,
        Unknown = 1,
        Local = 2,
        Work = 3,
        Home = 4,
        Mobile = 5,
        Fax = 6,
        Mail = 7,
        Web = 8,
        Other = 9,
        Mulap = 10,
        SipAddr = 11,
    }

    public enum ReservationSettingTypeEnum
    { // RecId: 1010
        rstStart = 0,
        rstEnd = 1,
    }

    public enum AppointmentTypeEnum
    { // RecId: 1011
        Normal = 0,
        Recurring = 1,
    }

    public enum ReservationTypeEnum
    { // RecId: 1012
        Current = 0,
        Reservation = 1,
    }

    public enum ScopeTypeEnum
    { // RecId: 1013
        Public = 0,
        Private = 1,
    }

    public enum ClientTypeEnum
    { // RecId: 1014
        Client = 0,
        Terminal = 1,
        Admin = 2,
        Switchboard = 3,
        Receptionist = 4,
        Provider = 5,
        Proxy = 6,
        Custom = 7,
        SmartClient = 8,
    }

    public enum OriginExecutionEnum
    { // RecId: 1015
        Unknown = 0,
        SetCurrent = 1,
        AppPromotin = 2,
        AppEnd = 3,
        ResetAtMidnight = 4,
    }

    public enum EndTypeEnum
    { // RecId: 1016
        Undefined = 0,
        Expected = 1,
        Actual = 2,
    }

    public enum PendingStateEnum
    { // RecId: 1017
        None = 0,
        PendingCreate = 1,
        PendingUpdate = 2,
        PendingDelete = 3,
        PendingCreateFail = 4,
        PendingUpdateFail = 5,
        PendingDeleteFail = 6,
    }

    public enum NotifyCommandEnum
    { // RecId: 1018
        cmdSendStaticData = 0,
        cmdMaximizeSwitchBoard = 1,
    }

    public enum EndingTimeEnum
    { // RecId: 1019
        NoRequired = 0,
        SameDay = 1,
        Required = 2,
    }

    public enum StateLengthEnum
    { // RecId: 1020
        OneDay = 0,
        ExpectedEnd = 1,
        UntilAnotherState = 2,
    }

    public enum EndStampEnum
    { // RecId: 1021
        esBeginning = 0,
        esEnd = 1,
    }

    public enum StateTypeEnum
    { // RecId: 1022
        UserState = 0,
        ResourceState = 1,
        SystemState = 2,
    }

    public enum StateClassEnum
    { // RecId: 1023
        Free = 0,
        Tentative = 1,
        Busy = 2,
        OutOfOffice = 3,
    }

    public enum CallStatisticsTypeEnum
    { // RecId: 1024
        cstIdle = 0,
        cstBusy = 1,
    }

    public enum ResourceDetailTypeEnum
    { // RecId: 1025
        Unknown = 0,
        Link = 1,
    }

    public enum ExpectedResponse
    { // RecId: 1026
        NoncePwd = 0,
        Nonce = 1,
        Password = 2,
        Nothing = 3,
        GlobalPassword = 4,
    }

    public enum ResponseCoding
    { // RecId: 1027
        Plain = 0,
        MD5 = 1,
        SHA = 2,
    }

    public enum ChallengeReason
    { // RecId: 1028
        Login = 0,
        NotAuthenticated = 1,
        LicenseProblem = 2,
        TooManyTries = 3,
        Authenticated = 4,
        NoPrivilege = 5,
        NotWinAuthenticated = 6,
        UserNotActive = 7,
    }

    public enum CallLogType
    { // RecId: 1029
        IsCallingAnswered = 0,
        IsCalledAnswered = 1,
        IsCallingNoAnswer = 2,
        IsCalledNoAnswer = 3,
        Answered = 4,
    }

    public enum CallLogDirectionType
    { // RecId: 1030
        IncomingAndOutgoing = 0,
        Outgoing = 1,
        Incoming = 2,
    }

    public enum AuthenticationType
    { // RecId: 1031
        Basic = 0,
        AuthenticationPortal = 1,
    }


    public class ProxyEnumStream : ProxyBaseStream
    {
        public static CallStateEnum ReadCallStateEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (CallStateEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteCallStateEnum(Stream stream, string PropertyName, CallStateEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static AgentStateEnum ReadAgentStateEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (AgentStateEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteAgentStateEnum(Stream stream, string PropertyName, AgentStateEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static DeviceTypeEnum ReadDeviceTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (DeviceTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteDeviceTypeEnum(Stream stream, string PropertyName, DeviceTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static DeviceSubTypeEnum ReadDeviceSubTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (DeviceSubTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteDeviceSubTypeEnum(Stream stream, string PropertyName, DeviceSubTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static DeviceStateEnum ReadDeviceStateEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (DeviceStateEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteDeviceStateEnum(Stream stream, string PropertyName, DeviceStateEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static SuggestedRuleEnum ReadSuggestedRuleEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (SuggestedRuleEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteSuggestedRuleEnum(Stream stream, string PropertyName, SuggestedRuleEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static SuggestedRoundTypeEnum ReadSuggestedRoundTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (SuggestedRoundTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteSuggestedRoundTypeEnum(Stream stream, string PropertyName, SuggestedRoundTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ForwardTypeEnum ReadForwardTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (ForwardTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteForwardTypeEnum(Stream stream, string PropertyName, ForwardTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static DNDTypeEnum ReadDNDTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (DNDTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteDNDTypeEnum(Stream stream, string PropertyName, DNDTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ResourceDetailLinkEnum ReadResourceDetailLinkEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (ResourceDetailLinkEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteResourceDetailLinkEnum(Stream stream, string PropertyName, ResourceDetailLinkEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ReservationSettingTypeEnum ReadReservationSettingTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (ReservationSettingTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteReservationSettingTypeEnum(Stream stream, string PropertyName, ReservationSettingTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static AppointmentTypeEnum ReadAppointmentTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (AppointmentTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteAppointmentTypeEnum(Stream stream, string PropertyName, AppointmentTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ReservationTypeEnum ReadReservationTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (ReservationTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteReservationTypeEnum(Stream stream, string PropertyName, ReservationTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ScopeTypeEnum ReadScopeTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (ScopeTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteScopeTypeEnum(Stream stream, string PropertyName, ScopeTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ClientTypeEnum ReadClientTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (ClientTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteClientTypeEnum(Stream stream, string PropertyName, ClientTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static OriginExecutionEnum ReadOriginExecutionEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (OriginExecutionEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteOriginExecutionEnum(Stream stream, string PropertyName, OriginExecutionEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static EndTypeEnum ReadEndTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (EndTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteEndTypeEnum(Stream stream, string PropertyName, EndTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static PendingStateEnum ReadPendingStateEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (PendingStateEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WritePendingStateEnum(Stream stream, string PropertyName, PendingStateEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static NotifyCommandEnum ReadNotifyCommandEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (NotifyCommandEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteNotifyCommandEnum(Stream stream, string PropertyName, NotifyCommandEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static EndingTimeEnum ReadEndingTimeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (EndingTimeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteEndingTimeEnum(Stream stream, string PropertyName, EndingTimeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static StateLengthEnum ReadStateLengthEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (StateLengthEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteStateLengthEnum(Stream stream, string PropertyName, StateLengthEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static EndStampEnum ReadEndStampEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (EndStampEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteEndStampEnum(Stream stream, string PropertyName, EndStampEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static StateTypeEnum ReadStateTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (StateTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteStateTypeEnum(Stream stream, string PropertyName, StateTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static StateClassEnum ReadStateClassEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (StateClassEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteStateClassEnum(Stream stream, string PropertyName, StateClassEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static CallStatisticsTypeEnum ReadCallStatisticsTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (CallStatisticsTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteCallStatisticsTypeEnum(Stream stream, string PropertyName, CallStatisticsTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ResourceDetailTypeEnum ReadResourceDetailTypeEnum(Stream stream, String PropertyName, int MaxPos)
        {
            return (ResourceDetailTypeEnum)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteResourceDetailTypeEnum(Stream stream, string PropertyName, ResourceDetailTypeEnum value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ExpectedResponse ReadExpectedResponse(Stream stream, String PropertyName, int MaxPos)
        {
            return (ExpectedResponse)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteExpectedResponse(Stream stream, string PropertyName, ExpectedResponse value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ResponseCoding ReadResponseCoding(Stream stream, String PropertyName, int MaxPos)
        {
            return (ResponseCoding)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteResponseCoding(Stream stream, string PropertyName, ResponseCoding value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static ChallengeReason ReadChallengeReason(Stream stream, String PropertyName, int MaxPos)
        {
            return (ChallengeReason)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteChallengeReason(Stream stream, string PropertyName, ChallengeReason value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static CallLogType ReadCallLogType(Stream stream, String PropertyName, int MaxPos)
        {
            return (CallLogType)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteCallLogType(Stream stream, string PropertyName, CallLogType value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static CallLogDirectionType ReadCallLogDirectionType(Stream stream, String PropertyName, int MaxPos)
        {
            return (CallLogDirectionType)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteCallLogDirectionType(Stream stream, string PropertyName, CallLogDirectionType value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

        public static AuthenticationType ReadAuthenticationType(Stream stream, String PropertyName, int MaxPos)
        {
            return (AuthenticationType)ReadInt32(stream, PropertyName, MaxPos);
        }

        public static void WriteAuthenticationType(Stream stream, string PropertyName, AuthenticationType value)
        {
            WriteInt32(stream, PropertyName, Convert.ToInt32(value));
        }

    }
}
