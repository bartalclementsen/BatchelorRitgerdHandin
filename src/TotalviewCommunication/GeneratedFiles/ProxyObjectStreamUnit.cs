// ProxyObjectStreamUnit.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Proxy.Streaming
{
    public class ProxyStream : ProxyEnumStream
    {
        public static T5StreamDeviceS ReadT5StreamDeviceS(Stream stream, String PropertyName)
        {
            T5StreamDeviceS result = new T5StreamDeviceS();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamDeviceS(Stream stream, String PropertyName, T5StreamDeviceS Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamDeviceM ReadT5StreamDeviceM(Stream stream, String PropertyName)
        {
            T5StreamDeviceM result = new T5StreamDeviceM();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamDeviceM(Stream stream, String PropertyName, T5StreamDeviceM Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamDeviceL ReadT5StreamDeviceL(Stream stream, String PropertyName)
        {
            T5StreamDeviceL result = new T5StreamDeviceL();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamDeviceL(Stream stream, String PropertyName, T5StreamDeviceL Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamDeviceTransferedConnection ReadT5StreamDeviceTransferedConnection(Stream stream, String PropertyName)
        {
            T5StreamDeviceTransferedConnection result = new T5StreamDeviceTransferedConnection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamDeviceTransferedConnection(Stream stream, String PropertyName, T5StreamDeviceTransferedConnection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamDeviceTransferedConnectionCollection ReadT5StreamDeviceTransferedConnectionCollection(Stream stream, String PropertyName)
        {
            T5StreamDeviceTransferedConnectionCollection result = new T5StreamDeviceTransferedConnectionCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamDeviceTransferedConnectionCollection(Stream stream, String PropertyName, T5StreamDeviceTransferedConnectionCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamDeviceConnection ReadT5StreamDeviceConnection(Stream stream, String PropertyName)
        {
            T5StreamDeviceConnection result = new T5StreamDeviceConnection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamDeviceConnection(Stream stream, String PropertyName, T5StreamDeviceConnection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamDeviceConnectionCollection ReadT5StreamDeviceConnectionCollection(Stream stream, String PropertyName)
        {
            T5StreamDeviceConnectionCollection result = new T5StreamDeviceConnectionCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamDeviceConnectionCollection(Stream stream, String PropertyName, T5StreamDeviceConnectionCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamLyncState ReadT5StreamLyncState(Stream stream, String PropertyName)
        {
            T5StreamLyncState result = new T5StreamLyncState();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamLyncState(Stream stream, String PropertyName, T5StreamLyncState Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamResourceDetail ReadT5StreamResourceDetail(Stream stream, String PropertyName)
        {
            T5StreamResourceDetail result = new T5StreamResourceDetail();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamResourceDetail(Stream stream, String PropertyName, T5StreamResourceDetail Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamResourceDetailCollection ReadT5StreamResourceDetailCollection(Stream stream, String PropertyName)
        {
            T5StreamResourceDetailCollection result = new T5StreamResourceDetailCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamResourceDetailCollection(Stream stream, String PropertyName, T5StreamResourceDetailCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamResource ReadT5StreamResource(Stream stream, String PropertyName)
        {
            T5StreamResource result = new T5StreamResource();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamResource(Stream stream, String PropertyName, T5StreamResource Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamResourceCollection ReadT5StreamResourceCollection(Stream stream, String PropertyName)
        {
            T5StreamResourceCollection result = new T5StreamResourceCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamResourceCollection(Stream stream, String PropertyName, T5StreamResourceCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamCallerHistory ReadT5StreamCallerHistory(Stream stream, String PropertyName)
        {
            T5StreamCallerHistory result = new T5StreamCallerHistory();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamCallerHistory(Stream stream, String PropertyName, T5StreamCallerHistory Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamCallerHistoryCollection ReadT5StreamCallerHistoryCollection(Stream stream, String PropertyName)
        {
            T5StreamCallerHistoryCollection result = new T5StreamCallerHistoryCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamCallerHistoryCollection(Stream stream, String PropertyName, T5StreamCallerHistoryCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamReservationDetail ReadT5StreamReservationDetail(Stream stream, String PropertyName)
        {
            T5StreamReservationDetail result = new T5StreamReservationDetail();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamReservationDetail(Stream stream, String PropertyName, T5StreamReservationDetail Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamReservationDetailCollection ReadT5StreamReservationDetailCollection(Stream stream, String PropertyName)
        {
            T5StreamReservationDetailCollection result = new T5StreamReservationDetailCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamReservationDetailCollection(Stream stream, String PropertyName, T5StreamReservationDetailCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamReservation ReadT5StreamReservation(Stream stream, String PropertyName)
        {
            T5StreamReservation result = new T5StreamReservation();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamReservation(Stream stream, String PropertyName, T5StreamReservation Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamReservationCollection ReadT5StreamReservationCollection(Stream stream, String PropertyName)
        {
            T5StreamReservationCollection result = new T5StreamReservationCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamReservationCollection(Stream stream, String PropertyName, T5StreamReservationCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamReservationUser ReadT5StreamReservationUser(Stream stream, String PropertyName)
        {
            T5StreamReservationUser result = new T5StreamReservationUser();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamReservationUser(Stream stream, String PropertyName, T5StreamReservationUser Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamNotifyCommand ReadT5StreamNotifyCommand(Stream stream, String PropertyName)
        {
            T5StreamNotifyCommand result = new T5StreamNotifyCommand();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamNotifyCommand(Stream stream, String PropertyName, T5StreamNotifyCommand Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamStaticData ReadT5StreamStaticData(Stream stream, String PropertyName)
        {
            T5StreamStaticData result = new T5StreamStaticData();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamStaticData(Stream stream, String PropertyName, T5StreamStaticData Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamLyncStateCollection ReadT5StreamLyncStateCollection(Stream stream, String PropertyName)
        {
            T5StreamLyncStateCollection result = new T5StreamLyncStateCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamLyncStateCollection(Stream stream, String PropertyName, T5StreamLyncStateCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamState ReadT5StreamState(Stream stream, String PropertyName)
        {
            T5StreamState result = new T5StreamState();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamState(Stream stream, String PropertyName, T5StreamState Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamStateCollection ReadT5StreamStateCollection(Stream stream, String PropertyName)
        {
            T5StreamStateCollection result = new T5StreamStateCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamStateCollection(Stream stream, String PropertyName, T5StreamStateCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamStateDetail ReadT5StreamStateDetail(Stream stream, String PropertyName)
        {
            T5StreamStateDetail result = new T5StreamStateDetail();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamStateDetail(Stream stream, String PropertyName, T5StreamStateDetail Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamStateDetailCollection ReadT5StreamStateDetailCollection(Stream stream, String PropertyName)
        {
            T5StreamStateDetailCollection result = new T5StreamStateDetailCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamStateDetailCollection(Stream stream, String PropertyName, T5StreamStateDetailCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamSmallDeviceList ReadT5StreamSmallDeviceList(Stream stream, String PropertyName)
        {
            T5StreamSmallDeviceList result = new T5StreamSmallDeviceList();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamSmallDeviceList(Stream stream, String PropertyName, T5StreamSmallDeviceList Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamLargeDeviceList ReadT5StreamLargeDeviceList(Stream stream, String PropertyName)
        {
            T5StreamLargeDeviceList result = new T5StreamLargeDeviceList();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamLargeDeviceList(Stream stream, String PropertyName, T5StreamLargeDeviceList Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamChallengeResponse ReadT5StreamChallengeResponse(Stream stream, String PropertyName)
        {
            T5StreamChallengeResponse result = new T5StreamChallengeResponse();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamChallengeResponse(Stream stream, String PropertyName, T5StreamChallengeResponse Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamTemplate ReadT5StreamTemplate(Stream stream, String PropertyName)
        {
            T5StreamTemplate result = new T5StreamTemplate();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamTemplate(Stream stream, String PropertyName, T5StreamTemplate Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamTemplateList ReadT5StreamTemplateList(Stream stream, String PropertyName)
        {
            T5StreamTemplateList result = new T5StreamTemplateList();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamTemplateList(Stream stream, String PropertyName, T5StreamTemplateList Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamProxyRequest ReadT5StreamProxyRequest(Stream stream, String PropertyName)
        {
            T5StreamProxyRequest result = new T5StreamProxyRequest();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamProxyRequest(Stream stream, String PropertyName, T5StreamProxyRequest Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamKeyValue ReadT5StreamKeyValue(Stream stream, String PropertyName)
        {
            T5StreamKeyValue result = new T5StreamKeyValue();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamKeyValue(Stream stream, String PropertyName, T5StreamKeyValue Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamKeyValueList ReadT5StreamKeyValueList(Stream stream, String PropertyName)
        {
            T5StreamKeyValueList result = new T5StreamKeyValueList();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamKeyValueList(Stream stream, String PropertyName, T5StreamKeyValueList Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamProxyCommand ReadT5StreamProxyCommand(Stream stream, String PropertyName)
        {
            T5StreamProxyCommand result = new T5StreamProxyCommand();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamProxyCommand(Stream stream, String PropertyName, T5StreamProxyCommand Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamReservationUserList ReadT5StreamReservationUserList(Stream stream, String PropertyName)
        {
            T5StreamReservationUserList result = new T5StreamReservationUserList();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamReservationUserList(Stream stream, String PropertyName, T5StreamReservationUserList Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamLoginData ReadT5StreamLoginData(Stream stream, String PropertyName)
        {
            T5StreamLoginData result = new T5StreamLoginData();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamLoginData(Stream stream, String PropertyName, T5StreamLoginData Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamUserPreferences ReadT5StreamUserPreferences(Stream stream, String PropertyName)
        {
            T5StreamUserPreferences result = new T5StreamUserPreferences();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamUserPreferences(Stream stream, String PropertyName, T5StreamUserPreferences Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamConnectionStartup ReadT5StreamConnectionStartup(Stream stream, String PropertyName)
        {
            T5StreamConnectionStartup result = new T5StreamConnectionStartup();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamConnectionStartup(Stream stream, String PropertyName, T5StreamConnectionStartup Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamProxyCommandResult ReadT5StreamProxyCommandResult(Stream stream, String PropertyName)
        {
            T5StreamProxyCommandResult result = new T5StreamProxyCommandResult();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamProxyCommandResult(Stream stream, String PropertyName, T5StreamProxyCommandResult Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamUserImage ReadT5StreamUserImage(Stream stream, String PropertyName)
        {
            T5StreamUserImage result = new T5StreamUserImage();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamUserImage(Stream stream, String PropertyName, T5StreamUserImage Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamUserImageList ReadT5StreamUserImageList(Stream stream, String PropertyName)
        {
            T5StreamUserImageList result = new T5StreamUserImageList();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamUserImageList(Stream stream, String PropertyName, T5StreamUserImageList Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamNumberLog ReadT5StreamNumberLog(Stream stream, String PropertyName)
        {
            T5StreamNumberLog result = new T5StreamNumberLog();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamNumberLog(Stream stream, String PropertyName, T5StreamNumberLog Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamNumberLogList ReadT5StreamNumberLogList(Stream stream, String PropertyName)
        {
            T5StreamNumberLogList result = new T5StreamNumberLogList();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamNumberLogList(Stream stream, String PropertyName, T5StreamNumberLogList Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamUserNumberLog ReadT5StreamUserNumberLog(Stream stream, String PropertyName)
        {
            T5StreamUserNumberLog result = new T5StreamUserNumberLog();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamUserNumberLog(Stream stream, String PropertyName, T5StreamUserNumberLog Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamUserAttachmentsDefinition ReadT5StreamUserAttachmentsDefinition(Stream stream, String PropertyName)
        {
            T5StreamUserAttachmentsDefinition result = new T5StreamUserAttachmentsDefinition();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamUserAttachmentsDefinition(Stream stream, String PropertyName, T5StreamUserAttachmentsDefinition Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamUserAttachmentsData ReadT5StreamUserAttachmentsData(Stream stream, String PropertyName)
        {
            T5StreamUserAttachmentsData result = new T5StreamUserAttachmentsData();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamUserAttachmentsData(Stream stream, String PropertyName, T5StreamUserAttachmentsData Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamUserAttachmentsDataLastUsed ReadT5StreamUserAttachmentsDataLastUsed(Stream stream, String PropertyName)
        {
            T5StreamUserAttachmentsDataLastUsed result = new T5StreamUserAttachmentsDataLastUsed();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamUserAttachmentsDataLastUsed(Stream stream, String PropertyName, T5StreamUserAttachmentsDataLastUsed Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamUserAttachmentsDataLastUsedCollection ReadT5StreamUserAttachmentsDataLastUsedCollection(Stream stream, String PropertyName)
        {
            T5StreamUserAttachmentsDataLastUsedCollection result = new T5StreamUserAttachmentsDataLastUsedCollection();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamUserAttachmentsDataLastUsedCollection(Stream stream, String PropertyName, T5StreamUserAttachmentsDataLastUsedCollection Value)
        {
            Value.StreamOut(stream, PropertyName);
        }

        public static T5StreamBaseObject CreateT5BaseObjectFromClassEnum(int ClassEnum, Stream stream, String PropertyName)
        {
            switch (ClassEnum)
            {
                case 10002:
                    return ReadT5StreamDeviceS(stream, PropertyName);
                case 10003:
                    return ReadT5StreamDeviceM(stream, PropertyName);
                case 10004:
                    return ReadT5StreamDeviceL(stream, PropertyName);
                case 10006:
                    return ReadT5StreamDeviceTransferedConnection(stream, PropertyName);
                case 10007:
                    return ReadT5StreamDeviceTransferedConnectionCollection(stream, PropertyName);
                case 10008:
                    return ReadT5StreamDeviceConnection(stream, PropertyName);
                case 10009:
                    return ReadT5StreamDeviceConnectionCollection(stream, PropertyName);
                case 10010:
                    return ReadT5StreamLyncState(stream, PropertyName);
                case 10012:
                    return ReadT5StreamResourceDetail(stream, PropertyName);
                case 10013:
                    return ReadT5StreamResourceDetailCollection(stream, PropertyName);
                case 10014:
                    return ReadT5StreamResource(stream, PropertyName);
                case 10015:
                    return ReadT5StreamResourceCollection(stream, PropertyName);
                case 10016:
                    return ReadT5StreamCallerHistory(stream, PropertyName);
                case 10017:
                    return ReadT5StreamCallerHistoryCollection(stream, PropertyName);
                case 10018:
                    return ReadT5StreamReservationDetail(stream, PropertyName);
                case 10019:
                    return ReadT5StreamReservationDetailCollection(stream, PropertyName);
                case 10020:
                    return ReadT5StreamReservation(stream, PropertyName);
                case 10021:
                    return ReadT5StreamReservationCollection(stream, PropertyName);
                case 10022:
                    return ReadT5StreamReservationUser(stream, PropertyName);
                case 10023:
                    return ReadT5StreamNotifyCommand(stream, PropertyName);
                case 10024:
                    return ReadT5StreamStaticData(stream, PropertyName);
                case 10026:
                    return ReadT5StreamLyncStateCollection(stream, PropertyName);
                case 10027:
                    return ReadT5StreamState(stream, PropertyName);
                case 10028:
                    return ReadT5StreamStateCollection(stream, PropertyName);
                case 10029:
                    return ReadT5StreamStateDetail(stream, PropertyName);
                case 10030:
                    return ReadT5StreamStateDetailCollection(stream, PropertyName);
                case 10031:
                    return ReadT5StreamSmallDeviceList(stream, PropertyName);
                case 10032:
                    return ReadT5StreamLargeDeviceList(stream, PropertyName);
                case 10033:
                    return ReadT5StreamChallengeResponse(stream, PropertyName);
                case 10034:
                    return ReadT5StreamTemplate(stream, PropertyName);
                case 10035:
                    return ReadT5StreamTemplateList(stream, PropertyName);
                case 10036:
                    return ReadT5StreamProxyRequest(stream, PropertyName);
                case 10037:
                    return ReadT5StreamKeyValue(stream, PropertyName);
                case 10038:
                    return ReadT5StreamKeyValueList(stream, PropertyName);
                case 10039:
                    return ReadT5StreamProxyCommand(stream, PropertyName);
                case 10040:
                    return ReadT5StreamReservationUserList(stream, PropertyName);
                case 10041:
                    return ReadT5StreamLoginData(stream, PropertyName);
                case 10042:
                    return ReadT5StreamUserPreferences(stream, PropertyName);
                case 10043:
                    return ReadT5StreamConnectionStartup(stream, PropertyName);
                case 10044:
                    return ReadT5StreamProxyCommandResult(stream, PropertyName);
                case 10045:
                    return ReadT5StreamUserImage(stream, PropertyName);
                case 10046:
                    return ReadT5StreamUserImageList(stream, PropertyName);
                case 10047:
                    return ReadT5StreamNumberLog(stream, PropertyName);
                case 10048:
                    return ReadT5StreamNumberLogList(stream, PropertyName);
                case 10049:
                    return ReadT5StreamUserNumberLog(stream, PropertyName);
                case 10050:
                    return ReadT5StreamUserAttachmentsDefinition(stream, PropertyName);
                case 10052:
                    return ReadT5StreamUserAttachmentsData(stream, PropertyName);
                case 10053:
                    return ReadT5StreamUserAttachmentsDataLastUsed(stream, PropertyName);
                case 10054:
                    return ReadT5StreamUserAttachmentsDataLastUsedCollection(stream, PropertyName);
                default:
                    // Class Enum unknown
                    Debugger.Break();
                    return null;
            }
        }

        public static T5StreamBaseObject CreateFromStream(Stream stream, String PropertyName)
        {
            T5StreamBaseObject baseObj = new T5StreamBaseObject();
            long pos = stream.Position;
            baseObj.StreamIn(stream, PropertyName);
            stream.Position = pos;
            int classEnum = baseObj.ClassEnum;

            return CreateT5BaseObjectFromClassEnum(classEnum, stream, PropertyName);
        }

    }

    public class T5StreamDeviceS : T5StreamBaseObject
    {
        public CallStateEnum CallState;
        public Int32 QueueLength;
        public Boolean DND;
        public Boolean Forwarded;
        public Boolean IsGroupMember;
        public AgentStateEnum AgentState;
        public Boolean IsPriorityCalls;
        public String DeviceId;
        public Int32 NoOfHeld;
        public DeviceTypeEnum DeviceType;
        public DeviceSubTypeEnum SubType;
        public DeviceStateEnum DeviceState;
        public String ConnectorName;
        public Boolean Mute;

        public override int GetClassEnum()
        {
            return 10002;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            CallState = ProxyStream.ReadCallStateEnum(stream, "CallState", MaxPos);
            QueueLength = ProxyStream.ReadInt32(stream, "QueueLength", MaxPos);
            DND = ProxyStream.ReadBoolean(stream, "DND", MaxPos);
            Forwarded = ProxyStream.ReadBoolean(stream, "Forwarded", MaxPos);
            IsGroupMember = ProxyStream.ReadBoolean(stream, "IsGroupMember", MaxPos);
            AgentState = ProxyStream.ReadAgentStateEnum(stream, "AgentState", MaxPos);
            IsPriorityCalls = ProxyStream.ReadBoolean(stream, "IsPriorityCalls", MaxPos);
            DeviceId = ProxyStream.ReadString(stream, "DeviceId", MaxPos);
            NoOfHeld = ProxyStream.ReadInt32(stream, "NoOfHeld", MaxPos);
            DeviceType = ProxyStream.ReadDeviceTypeEnum(stream, "DeviceType", MaxPos);
            SubType = ProxyStream.ReadDeviceSubTypeEnum(stream, "SubType", MaxPos);
            DeviceState = ProxyStream.ReadDeviceStateEnum(stream, "DeviceState", MaxPos);
            ConnectorName = ProxyStream.ReadString(stream, "ConnectorName", MaxPos);
            Mute = ProxyStream.ReadBoolean(stream, "Mute", MaxPos);
            if (ClassEnum == 10002)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteCallStateEnum(stream, "CallState", CallState);
            ProxyStream.WriteInt32(stream, "QueueLength", QueueLength);
            ProxyStream.WriteBoolean(stream, "DND", DND);
            ProxyStream.WriteBoolean(stream, "Forwarded", Forwarded);
            ProxyStream.WriteBoolean(stream, "IsGroupMember", IsGroupMember);
            ProxyStream.WriteAgentStateEnum(stream, "AgentState", AgentState);
            ProxyStream.WriteBoolean(stream, "IsPriorityCalls", IsPriorityCalls);
            ProxyStream.WriteString(stream, "DeviceId", DeviceId);
            ProxyStream.WriteInt32(stream, "NoOfHeld", NoOfHeld);
            ProxyStream.WriteDeviceTypeEnum(stream, "DeviceType", DeviceType);
            ProxyStream.WriteDeviceSubTypeEnum(stream, "SubType", SubType);
            ProxyStream.WriteDeviceStateEnum(stream, "DeviceState", DeviceState);
            ProxyStream.WriteString(stream, "ConnectorName", ConnectorName);
            ProxyStream.WriteBoolean(stream, "Mute", Mute);
            SetPacketSize(stream);
        }
    }

    public class T5StreamDeviceM : T5StreamDeviceS
    {
        public T5StreamDeviceConnectionCollection ConnectionList = new T5StreamDeviceConnectionCollection();

        public override int GetClassEnum()
        {
            return 10003;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            ConnectionList = ProxyStream.ReadT5StreamDeviceConnectionCollection(stream, "ConnectionList");
            if (ClassEnum == 10003)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ConnectionList.StreamOut(stream, "ConnectionList");
            SetPacketSize(stream);
        }
    }

    public class T5StreamDeviceL : T5StreamDeviceM
    {
        public T5StreamDeviceTransferedConnectionCollection TransferList = new T5StreamDeviceTransferedConnectionCollection();
        public Int32 ForwardType;
        public String ForwardedTo;
        public String TransferredTo;
        public Boolean RemoteOfficeEnabled;
        public Boolean RemoteOfficeActive;
        public String RemoteOfficeNumber;
        public Boolean OutboundCallerIdEnabled;
        public String OutboundCallerId;

        public override int GetClassEnum()
        {
            return 10004;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            TransferList = ProxyStream.ReadT5StreamDeviceTransferedConnectionCollection(stream, "TransferList");
            ForwardType = ProxyStream.ReadInt32(stream, "ForwardType", MaxPos);
            ForwardedTo = ProxyStream.ReadString(stream, "ForwardedTo", MaxPos);
            TransferredTo = ProxyStream.ReadString(stream, "TransferredTo", MaxPos);
            RemoteOfficeEnabled = ProxyStream.ReadBoolean(stream, "RemoteOfficeEnabled", MaxPos);
            RemoteOfficeActive = ProxyStream.ReadBoolean(stream, "RemoteOfficeActive", MaxPos);
            RemoteOfficeNumber = ProxyStream.ReadString(stream, "RemoteOfficeNumber", MaxPos);
            OutboundCallerIdEnabled = ProxyStream.ReadBoolean(stream, "OutboundCallerIdEnabled", MaxPos);
            OutboundCallerId = ProxyStream.ReadString(stream, "OutboundCallerId", MaxPos);
            if (ClassEnum == 10004)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            TransferList.StreamOut(stream, "TransferList");
            ProxyStream.WriteInt32(stream, "ForwardType", ForwardType);
            ProxyStream.WriteString(stream, "ForwardedTo", ForwardedTo);
            ProxyStream.WriteString(stream, "TransferredTo", TransferredTo);
            ProxyStream.WriteBoolean(stream, "RemoteOfficeEnabled", RemoteOfficeEnabled);
            ProxyStream.WriteBoolean(stream, "RemoteOfficeActive", RemoteOfficeActive);
            ProxyStream.WriteString(stream, "RemoteOfficeNumber", RemoteOfficeNumber);
            ProxyStream.WriteBoolean(stream, "OutboundCallerIdEnabled", OutboundCallerIdEnabled);
            ProxyStream.WriteString(stream, "OutboundCallerId", OutboundCallerId);
            SetPacketSize(stream);
        }
    }

    public class T5StreamDeviceTransferedConnection : T5StreamBaseObject
    {
        public String CallId;
        public String DeviceId;
        public String TransferredTo;
        public CallStateEnum State;
        public String CallingDevice;
        public String CalledDevice;
        public Boolean IsMonitored;
        public DateTime CallCreatedTime;
        public DateTime ConnectionCreatedTime;
        public DateTime CreatedTime;

        public override int GetClassEnum()
        {
            return 10006;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            CallId = ProxyStream.ReadString(stream, "CallId", MaxPos);
            DeviceId = ProxyStream.ReadString(stream, "DeviceId", MaxPos);
            TransferredTo = ProxyStream.ReadString(stream, "TransferredTo", MaxPos);
            State = ProxyStream.ReadCallStateEnum(stream, "State", MaxPos);
            CallingDevice = ProxyStream.ReadString(stream, "CallingDevice", MaxPos);
            CalledDevice = ProxyStream.ReadString(stream, "CalledDevice", MaxPos);
            IsMonitored = ProxyStream.ReadBoolean(stream, "IsMonitored", MaxPos);
            CallCreatedTime = ProxyStream.ReadDateTime(stream, "CallCreatedTime", MaxPos);
            ConnectionCreatedTime = ProxyStream.ReadDateTime(stream, "ConnectionCreatedTime", MaxPos);
            CreatedTime = ProxyStream.ReadDateTime(stream, "CreatedTime", MaxPos);
            if (ClassEnum == 10006)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "CallId", CallId);
            ProxyStream.WriteString(stream, "DeviceId", DeviceId);
            ProxyStream.WriteString(stream, "TransferredTo", TransferredTo);
            ProxyStream.WriteCallStateEnum(stream, "State", State);
            ProxyStream.WriteString(stream, "CallingDevice", CallingDevice);
            ProxyStream.WriteString(stream, "CalledDevice", CalledDevice);
            ProxyStream.WriteBoolean(stream, "IsMonitored", IsMonitored);
            ProxyStream.WriteDateTime(stream, "CallCreatedTime", CallCreatedTime);
            ProxyStream.WriteDateTime(stream, "ConnectionCreatedTime", ConnectionCreatedTime);
            ProxyStream.WriteDateTime(stream, "CreatedTime", CreatedTime);
            SetPacketSize(stream);
        }
    }

    public class T5StreamDeviceTransferedConnectionCollection : T5StreamBaseObjectCollection<T5StreamDeviceTransferedConnection>
    {
        public Int32 ForwardType;
        public String ForwardedTo;
        public String TransferredTo;

        public override int GetClassEnum()
        {
            return 10007;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            ForwardType = ProxyStream.ReadInt32(stream, "ForwardType", MaxPos);
            ForwardedTo = ProxyStream.ReadString(stream, "ForwardedTo", MaxPos);
            TransferredTo = ProxyStream.ReadString(stream, "TransferredTo", MaxPos);
            if (ClassEnum == 10007)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "ForwardType", ForwardType);
            ProxyStream.WriteString(stream, "ForwardedTo", ForwardedTo);
            ProxyStream.WriteString(stream, "TransferredTo", TransferredTo);
            SetPacketSize(stream);
        }
    }

    public class T5StreamDeviceConnection : T5StreamBaseObject
    {
        public String CallId;
        public CallStateEnum State;
        public String CallingDevice;
        public String CalledDevice;
        public CallStateEnum RemoteState;
        public DateTime CallCreatedTime;
        public DateTime ConnectionCreatedTime;
        public DateTime CreatedTime;

        public override int GetClassEnum()
        {
            return 10008;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            CallId = ProxyStream.ReadString(stream, "CallId", MaxPos);
            State = ProxyStream.ReadCallStateEnum(stream, "State", MaxPos);
            CallingDevice = ProxyStream.ReadString(stream, "CallingDevice", MaxPos);
            CalledDevice = ProxyStream.ReadString(stream, "CalledDevice", MaxPos);
            RemoteState = ProxyStream.ReadCallStateEnum(stream, "RemoteState", MaxPos);
            CallCreatedTime = ProxyStream.ReadDateTime(stream, "CallCreatedTime", MaxPos);
            ConnectionCreatedTime = ProxyStream.ReadDateTime(stream, "ConnectionCreatedTime", MaxPos);
            CreatedTime = ProxyStream.ReadDateTime(stream, "CreatedTime", MaxPos);
            if (ClassEnum == 10008)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "CallId", CallId);
            ProxyStream.WriteCallStateEnum(stream, "State", State);
            ProxyStream.WriteString(stream, "CallingDevice", CallingDevice);
            ProxyStream.WriteString(stream, "CalledDevice", CalledDevice);
            ProxyStream.WriteCallStateEnum(stream, "RemoteState", RemoteState);
            ProxyStream.WriteDateTime(stream, "CallCreatedTime", CallCreatedTime);
            ProxyStream.WriteDateTime(stream, "ConnectionCreatedTime", ConnectionCreatedTime);
            ProxyStream.WriteDateTime(stream, "CreatedTime", CreatedTime);
            SetPacketSize(stream);
        }
    }

    public class T5StreamDeviceConnectionCollection : T5StreamBaseObjectCollection<T5StreamDeviceConnection>
    {

        public override int GetClassEnum()
        {
            return 10009;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10009)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamLyncState : T5StreamBaseObject
    {
        public String SipAddress;
        public UInt32 Availability;
        public String AvailabilityName;
        public Int32 DeviceType;
        public String DeviceTypeName;
        public String Location;
        public String PersonalNote;
        public String OutOfOfficeNote;
        public String ActivityToken;

        public override int GetClassEnum()
        {
            return 10010;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            SipAddress = ProxyStream.ReadString(stream, "SipAddress", MaxPos);
            Availability = ProxyStream.ReadUInt32(stream, "Availability", MaxPos);
            AvailabilityName = ProxyStream.ReadString(stream, "AvailabilityName", MaxPos);
            DeviceType = ProxyStream.ReadInt32(stream, "DeviceType", MaxPos);
            DeviceTypeName = ProxyStream.ReadString(stream, "DeviceTypeName", MaxPos);
            Location = ProxyStream.ReadString(stream, "Location", MaxPos);
            PersonalNote = ProxyStream.ReadString(stream, "PersonalNote", MaxPos);
            OutOfOfficeNote = ProxyStream.ReadString(stream, "OutOfOfficeNote", MaxPos);
            ActivityToken = ProxyStream.ReadString(stream, "ActivityToken", MaxPos);
            if (ClassEnum == 10010)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "SipAddress", SipAddress);
            ProxyStream.WriteUInt32(stream, "Availability", Availability);
            ProxyStream.WriteString(stream, "AvailabilityName", AvailabilityName);
            ProxyStream.WriteInt32(stream, "DeviceType", DeviceType);
            ProxyStream.WriteString(stream, "DeviceTypeName", DeviceTypeName);
            ProxyStream.WriteString(stream, "Location", Location);
            ProxyStream.WriteString(stream, "PersonalNote", PersonalNote);
            ProxyStream.WriteString(stream, "OutOfOfficeNote", OutOfOfficeNote);
            ProxyStream.WriteString(stream, "ActivityToken", ActivityToken);
            SetPacketSize(stream);
        }
    }

    public class T5StreamResourceDetail : T5StreamBaseObject
    {
        public Int32 Recid;
        public Int32 OwnerId;
        public ResourceDetailTypeEnum DetailType;
        public UInt16 DetailSubtype;
        public String Parameter;
        public String Value;
        public Boolean Enabled;
        public Boolean Default;
        public Boolean Changed;
        public Int32 SortOrder;
        public Boolean PrivateInfo;
        public Boolean ExternalMonitor;

        public override int GetClassEnum()
        {
            return 10012;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            Recid = ProxyStream.ReadInt32(stream, "Recid", MaxPos);
            OwnerId = ProxyStream.ReadInt32(stream, "OwnerId", MaxPos);
            DetailType = ProxyStream.ReadResourceDetailTypeEnum(stream, "DetailType", MaxPos);
            DetailSubtype = ProxyStream.ReadUInt16(stream, "DetailSubtype", MaxPos);
            Parameter = ProxyStream.ReadString(stream, "Parameter", MaxPos);
            Value = ProxyStream.ReadString(stream, "Value", MaxPos);
            Enabled = ProxyStream.ReadBoolean(stream, "Enabled", MaxPos);
            Default = ProxyStream.ReadBoolean(stream, "Default", MaxPos);
            Changed = ProxyStream.ReadBoolean(stream, "Changed", MaxPos);
            SortOrder = ProxyStream.ReadInt32(stream, "SortOrder", MaxPos);
            PrivateInfo = ProxyStream.ReadBoolean(stream, "PrivateInfo", MaxPos);
            ExternalMonitor = ProxyStream.ReadBoolean(stream, "ExternalMonitor", MaxPos);
            if (ClassEnum == 10012)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "Recid", Recid);
            ProxyStream.WriteInt32(stream, "OwnerId", OwnerId);
            ProxyStream.WriteResourceDetailTypeEnum(stream, "DetailType", DetailType);
            ProxyStream.WriteUInt16(stream, "DetailSubtype", DetailSubtype);
            ProxyStream.WriteString(stream, "Parameter", Parameter);
            ProxyStream.WriteString(stream, "Value", Value);
            ProxyStream.WriteBoolean(stream, "Enabled", Enabled);
            ProxyStream.WriteBoolean(stream, "Default", Default);
            ProxyStream.WriteBoolean(stream, "Changed", Changed);
            ProxyStream.WriteInt32(stream, "SortOrder", SortOrder);
            ProxyStream.WriteBoolean(stream, "PrivateInfo", PrivateInfo);
            ProxyStream.WriteBoolean(stream, "ExternalMonitor", ExternalMonitor);
            SetPacketSize(stream);
        }
    }

    public class T5StreamResourceDetailCollection : T5StreamBaseObjectCollection<T5StreamResourceDetail>
    {

        public override int GetClassEnum()
        {
            return 10013;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10013)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamResource : T5StreamBaseObject
    {
        public Int32 RecId;
        public String UserId;
        public String FirstName;
        public String LastName;
        public T5StreamResourceDetailCollection DetailCollection = new T5StreamResourceDetailCollection();
        public Boolean HasRemoteCallingRights;
        public Boolean HasNumberLog;
        public Boolean LogExternalCalls;
        public Boolean SynchronizeCalendar;
        public Boolean UseAttachments;
        public Boolean RequireAttachment;
        public Boolean HasSendSmsAnonomousRights;
        public String UserImageVersion;

        public override int GetClassEnum()
        {
            return 10014;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            RecId = ProxyStream.ReadInt32(stream, "RecId", MaxPos);
            UserId = ProxyStream.ReadString(stream, "UserId", MaxPos);
            FirstName = ProxyStream.ReadString(stream, "FirstName", MaxPos);
            LastName = ProxyStream.ReadString(stream, "LastName", MaxPos);
            DetailCollection = ProxyStream.ReadT5StreamResourceDetailCollection(stream, "DetailCollection");
            HasRemoteCallingRights = ProxyStream.ReadBoolean(stream, "HasRemoteCallingRights", MaxPos);
            HasNumberLog = ProxyStream.ReadBoolean(stream, "HasNumberLog", MaxPos);
            LogExternalCalls = ProxyStream.ReadBoolean(stream, "LogExternalCalls", MaxPos);
            SynchronizeCalendar = ProxyStream.ReadBoolean(stream, "SynchronizeCalendar", MaxPos);
            UseAttachments = ProxyStream.ReadBoolean(stream, "UseAttachments", MaxPos);
            RequireAttachment = ProxyStream.ReadBoolean(stream, "RequireAttachment", MaxPos);
            HasSendSmsAnonomousRights = ProxyStream.ReadBoolean(stream, "HasSendSmsAnonomousRights", MaxPos);
            UserImageVersion = ProxyStream.ReadString(stream, "UserImageVersion", MaxPos);
            if (ClassEnum == 10014)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "RecId", RecId);
            ProxyStream.WriteString(stream, "UserId", UserId);
            ProxyStream.WriteString(stream, "FirstName", FirstName);
            ProxyStream.WriteString(stream, "LastName", LastName);
            DetailCollection.StreamOut(stream, "DetailCollection");
            ProxyStream.WriteBoolean(stream, "HasRemoteCallingRights", HasRemoteCallingRights);
            ProxyStream.WriteBoolean(stream, "HasNumberLog", HasNumberLog);
            ProxyStream.WriteBoolean(stream, "LogExternalCalls", LogExternalCalls);
            ProxyStream.WriteBoolean(stream, "SynchronizeCalendar", SynchronizeCalendar);
            ProxyStream.WriteBoolean(stream, "UseAttachments", UseAttachments);
            ProxyStream.WriteBoolean(stream, "RequireAttachment", RequireAttachment);
            ProxyStream.WriteBoolean(stream, "HasSendSmsAnonomousRights", HasSendSmsAnonomousRights);
            ProxyStream.WriteString(stream, "UserImageVersion", UserImageVersion);
            SetPacketSize(stream);
        }
    }

    public class T5StreamResourceCollection : T5StreamBaseObjectCollection<T5StreamResource>
    {

        public override int GetClassEnum()
        {
            return 10015;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10015)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamCallerHistory : T5StreamBaseObject
    {
        public String DeviceId;
        public DateTime CallTime;

        public override int GetClassEnum()
        {
            return 10016;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            DeviceId = ProxyStream.ReadString(stream, "DeviceId", MaxPos);
            CallTime = ProxyStream.ReadDateTime(stream, "CallTime", MaxPos);
            if (ClassEnum == 10016)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "DeviceId", DeviceId);
            ProxyStream.WriteDateTime(stream, "CallTime", CallTime);
            SetPacketSize(stream);
        }
    }

    public class T5StreamCallerHistoryCollection : T5StreamBaseObjectCollection<T5StreamCallerHistory>
    {
        public String ExternalNumber;
        public String OwnerName;

        public override int GetClassEnum()
        {
            return 10017;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            ExternalNumber = ProxyStream.ReadString(stream, "ExternalNumber", MaxPos);
            OwnerName = ProxyStream.ReadString(stream, "OwnerName", MaxPos);
            if (ClassEnum == 10017)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "ExternalNumber", ExternalNumber);
            ProxyStream.WriteString(stream, "OwnerName", OwnerName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamReservationDetail : T5StreamBaseObject
    {
        public ReservationSettingTypeEnum SettingType;
        public String ParamName;
        public String ParamValue;

        public override int GetClassEnum()
        {
            return 10018;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            SettingType = ProxyStream.ReadReservationSettingTypeEnum(stream, "SettingType", MaxPos);
            ParamName = ProxyStream.ReadString(stream, "ParamName", MaxPos);
            ParamValue = ProxyStream.ReadString(stream, "ParamValue", MaxPos);
            if (ClassEnum == 10018)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteReservationSettingTypeEnum(stream, "SettingType", SettingType);
            ProxyStream.WriteString(stream, "ParamName", ParamName);
            ProxyStream.WriteString(stream, "ParamValue", ParamValue);
            SetPacketSize(stream);
        }
    }

    public class T5StreamReservationDetailCollection : T5StreamBaseObjectCollection<T5StreamReservationDetail>
    {

        public override int GetClassEnum()
        {
            return 10019;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10019)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamReservation : T5StreamBaseObject
    {
        public Int32 GlobalId;
        public Int32 Id;
        public String Key;
        public Int32 ResourceId;
        public Int32 StartCode;
        public String Subject;
        public String Location;
        public DateTime StartTime;
        public DateTime EndTime;
        public PendingStateEnum PendingState;
        public String PendingError;
        public ReservationTypeEnum ReservationType;
        public T5StreamReservationDetail Details = new T5StreamReservationDetail();
        public EndTypeEnum EndType;
        public Boolean History;
        public Boolean DeleteWhenSent;
        public AppointmentTypeEnum AppointmentType;
        public ScopeTypeEnum Scope;
        public ClientTypeEnum MadeFrom;
        public String OriginSystem;
        public OriginExecutionEnum OriginExecution;
        public Int32 OriginResourceId;
        public String Attachment;
        public String ServiceCode;

        public override int GetClassEnum()
        {
            return 10020;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            GlobalId = ProxyStream.ReadInt32(stream, "GlobalId", MaxPos);
            Id = ProxyStream.ReadInt32(stream, "Id", MaxPos);
            Key = ProxyStream.ReadString(stream, "Key", MaxPos);
            ResourceId = ProxyStream.ReadInt32(stream, "ResourceId", MaxPos);
            StartCode = ProxyStream.ReadInt32(stream, "StartCode", MaxPos);
            Subject = ProxyStream.ReadString(stream, "Subject", MaxPos);
            Location = ProxyStream.ReadString(stream, "Location", MaxPos);
            StartTime = ProxyStream.ReadDateTime(stream, "StartTime", MaxPos);
            EndTime = ProxyStream.ReadDateTime(stream, "EndTime", MaxPos);
            PendingState = ProxyStream.ReadPendingStateEnum(stream, "PendingState", MaxPos);
            PendingError = ProxyStream.ReadString(stream, "PendingError", MaxPos);
            ReservationType = ProxyStream.ReadReservationTypeEnum(stream, "ReservationType", MaxPos);
            Details = ProxyStream.ReadT5StreamReservationDetail(stream, "Details");
            EndType = ProxyStream.ReadEndTypeEnum(stream, "EndType", MaxPos);
            History = ProxyStream.ReadBoolean(stream, "History", MaxPos);
            DeleteWhenSent = ProxyStream.ReadBoolean(stream, "DeleteWhenSent", MaxPos);
            AppointmentType = ProxyStream.ReadAppointmentTypeEnum(stream, "AppointmentType", MaxPos);
            Scope = ProxyStream.ReadScopeTypeEnum(stream, "Scope", MaxPos);
            MadeFrom = ProxyStream.ReadClientTypeEnum(stream, "MadeFrom", MaxPos);
            OriginSystem = ProxyStream.ReadString(stream, "OriginSystem", MaxPos);
            OriginExecution = ProxyStream.ReadOriginExecutionEnum(stream, "OriginExecution", MaxPos);
            OriginResourceId = ProxyStream.ReadInt32(stream, "OriginResourceId", MaxPos);
            Attachment = ProxyStream.ReadString(stream, "Attachment", MaxPos);
            ServiceCode = ProxyStream.ReadString(stream, "ServiceCode", MaxPos);
            if (ClassEnum == 10020)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "GlobalId", GlobalId);
            ProxyStream.WriteInt32(stream, "Id", Id);
            ProxyStream.WriteString(stream, "Key", Key);
            ProxyStream.WriteInt32(stream, "ResourceId", ResourceId);
            ProxyStream.WriteInt32(stream, "StartCode", StartCode);
            ProxyStream.WriteString(stream, "Subject", Subject);
            ProxyStream.WriteString(stream, "Location", Location);
            ProxyStream.WriteDateTime(stream, "StartTime", StartTime);
            ProxyStream.WriteDateTime(stream, "EndTime", EndTime);
            ProxyStream.WritePendingStateEnum(stream, "PendingState", PendingState);
            ProxyStream.WriteString(stream, "PendingError", PendingError);
            ProxyStream.WriteReservationTypeEnum(stream, "ReservationType", ReservationType);
            Details.StreamOut(stream, "Details");
            ProxyStream.WriteEndTypeEnum(stream, "EndType", EndType);
            ProxyStream.WriteBoolean(stream, "History", History);
            ProxyStream.WriteBoolean(stream, "DeleteWhenSent", DeleteWhenSent);
            ProxyStream.WriteAppointmentTypeEnum(stream, "AppointmentType", AppointmentType);
            ProxyStream.WriteScopeTypeEnum(stream, "Scope", Scope);
            ProxyStream.WriteClientTypeEnum(stream, "MadeFrom", MadeFrom);
            ProxyStream.WriteString(stream, "OriginSystem", OriginSystem);
            ProxyStream.WriteOriginExecutionEnum(stream, "OriginExecution", OriginExecution);
            ProxyStream.WriteInt32(stream, "OriginResourceId", OriginResourceId);
            ProxyStream.WriteString(stream, "Attachment", Attachment);
            ProxyStream.WriteString(stream, "ServiceCode", ServiceCode);
            SetPacketSize(stream);
        }
    }

    public class T5StreamReservationCollection : T5StreamBaseObjectCollection<T5StreamReservation>
    {

        public override int GetClassEnum()
        {
            return 10021;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10021)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamReservationUser : T5StreamBaseObject
    {
        public Int32 RecordId;
        public String UserKey;
        public Boolean Located;
        public String LocatedError;
        public Boolean Syncronize;
        public Int32 InstanceRecId;
        public T5StreamReservationCollection ReservationList = new T5StreamReservationCollection();

        public override int GetClassEnum()
        {
            return 10022;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            RecordId = ProxyStream.ReadInt32(stream, "RecordId", MaxPos);
            UserKey = ProxyStream.ReadString(stream, "UserKey", MaxPos);
            Located = ProxyStream.ReadBoolean(stream, "Located", MaxPos);
            LocatedError = ProxyStream.ReadString(stream, "LocatedError", MaxPos);
            Syncronize = ProxyStream.ReadBoolean(stream, "Syncronize", MaxPos);
            InstanceRecId = ProxyStream.ReadInt32(stream, "InstanceRecId", MaxPos);
            ReservationList = ProxyStream.ReadT5StreamReservationCollection(stream, "ReservationList");
            if (ClassEnum == 10022)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "RecordId", RecordId);
            ProxyStream.WriteString(stream, "UserKey", UserKey);
            ProxyStream.WriteBoolean(stream, "Located", Located);
            ProxyStream.WriteString(stream, "LocatedError", LocatedError);
            ProxyStream.WriteBoolean(stream, "Syncronize", Syncronize);
            ProxyStream.WriteInt32(stream, "InstanceRecId", InstanceRecId);
            ReservationList.StreamOut(stream, "ReservationList");
            SetPacketSize(stream);
        }
    }

    public class T5StreamNotifyCommand : T5StreamBaseObject
    {
        public NotifyCommandEnum Operation;

        public override int GetClassEnum()
        {
            return 10023;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            Operation = ProxyStream.ReadNotifyCommandEnum(stream, "Operation", MaxPos);
            if (ClassEnum == 10023)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteNotifyCommandEnum(stream, "Operation", Operation);
            SetPacketSize(stream);
        }
    }

    public class T5StreamStaticData : T5StreamBaseObject
    {
        public T5StreamResourceCollection ResourceList = new T5StreamResourceCollection();
        public T5StreamLyncStateCollection LyncStateList = new T5StreamLyncStateCollection();
        public T5StreamSmallDeviceList SmallDeviceList = new T5StreamSmallDeviceList();
        public T5StreamLargeDeviceList LargeDeviceList = new T5StreamLargeDeviceList();
        public T5StreamReservationCollection StateCurrentList = new T5StreamReservationCollection();
        public T5StreamStateCollection StateList = new T5StreamStateCollection();
        public T5StreamTemplateList TemplateList = new T5StreamTemplateList();

        public override int GetClassEnum()
        {
            return 10024;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            ResourceList = ProxyStream.ReadT5StreamResourceCollection(stream, "ResourceList");
            LyncStateList = ProxyStream.ReadT5StreamLyncStateCollection(stream, "LyncStateList");
            SmallDeviceList = ProxyStream.ReadT5StreamSmallDeviceList(stream, "SmallDeviceList");
            LargeDeviceList = ProxyStream.ReadT5StreamLargeDeviceList(stream, "LargeDeviceList");
            StateCurrentList = ProxyStream.ReadT5StreamReservationCollection(stream, "StateCurrentList");
            StateList = ProxyStream.ReadT5StreamStateCollection(stream, "StateList");
            TemplateList = ProxyStream.ReadT5StreamTemplateList(stream, "TemplateList");
            if (ClassEnum == 10024)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ResourceList.StreamOut(stream, "ResourceList");
            LyncStateList.StreamOut(stream, "LyncStateList");
            SmallDeviceList.StreamOut(stream, "SmallDeviceList");
            LargeDeviceList.StreamOut(stream, "LargeDeviceList");
            StateCurrentList.StreamOut(stream, "StateCurrentList");
            StateList.StreamOut(stream, "StateList");
            TemplateList.StreamOut(stream, "TemplateList");
            SetPacketSize(stream);
        }
    }

    public class T5StreamLyncStateCollection : T5StreamBaseObjectCollection<T5StreamLyncState>
    {

        public override int GetClassEnum()
        {
            return 10026;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10026)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamState : T5StreamBaseObject
    {
        public Int32 StateId;
        public T5StreamStateDetail Detail = new T5StreamStateDetail();
        public String Caption;
        public String ShortName;
        public UInt16 SortOrder;
        public EndingTimeEnum ExpectedEnd;
        public StateLengthEnum StateLength;
        public EndStampEnum EndStamp;
        public UInt32 OptionsDugiIkkiSett;
        public StateTypeEnum StateType;
        public StateClassEnum StateClass;
        public Int32 SuggestNextProfile;
        public String SuggestNextProfileName;
        public UInt32 StateColor;
        public CallStatisticsTypeEnum CallStatisticsType;

        public override int GetClassEnum()
        {
            return 10027;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            StateId = ProxyStream.ReadInt32(stream, "StateId", MaxPos);
            Detail = ProxyStream.ReadT5StreamStateDetail(stream, "Detail");
            Caption = ProxyStream.ReadString(stream, "Caption", MaxPos);
            ShortName = ProxyStream.ReadString(stream, "ShortName", MaxPos);
            SortOrder = ProxyStream.ReadUInt16(stream, "SortOrder", MaxPos);
            ExpectedEnd = ProxyStream.ReadEndingTimeEnum(stream, "ExpectedEnd", MaxPos);
            StateLength = ProxyStream.ReadStateLengthEnum(stream, "StateLength", MaxPos);
            EndStamp = ProxyStream.ReadEndStampEnum(stream, "EndStamp", MaxPos);
            OptionsDugiIkkiSett = ProxyStream.ReadUInt32(stream, "OptionsDugiIkkiSett", MaxPos);
            StateType = ProxyStream.ReadStateTypeEnum(stream, "StateType", MaxPos);
            StateClass = ProxyStream.ReadStateClassEnum(stream, "StateClass", MaxPos);
            SuggestNextProfile = ProxyStream.ReadInt32(stream, "SuggestNextProfile", MaxPos);
            SuggestNextProfileName = ProxyStream.ReadString(stream, "SuggestNextProfileName", MaxPos);
            StateColor = ProxyStream.ReadUInt32(stream, "StateColor", MaxPos);
            CallStatisticsType = ProxyStream.ReadCallStatisticsTypeEnum(stream, "CallStatisticsType", MaxPos);
            if (ClassEnum == 10027)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "StateId", StateId);
            Detail.StreamOut(stream, "Detail");
            ProxyStream.WriteString(stream, "Caption", Caption);
            ProxyStream.WriteString(stream, "ShortName", ShortName);
            ProxyStream.WriteUInt16(stream, "SortOrder", SortOrder);
            ProxyStream.WriteEndingTimeEnum(stream, "ExpectedEnd", ExpectedEnd);
            ProxyStream.WriteStateLengthEnum(stream, "StateLength", StateLength);
            ProxyStream.WriteEndStampEnum(stream, "EndStamp", EndStamp);
            ProxyStream.WriteUInt32(stream, "OptionsDugiIkkiSett", OptionsDugiIkkiSett);
            ProxyStream.WriteStateTypeEnum(stream, "StateType", StateType);
            ProxyStream.WriteStateClassEnum(stream, "StateClass", StateClass);
            ProxyStream.WriteInt32(stream, "SuggestNextProfile", SuggestNextProfile);
            ProxyStream.WriteString(stream, "SuggestNextProfileName", SuggestNextProfileName);
            ProxyStream.WriteUInt32(stream, "StateColor", StateColor);
            ProxyStream.WriteCallStatisticsTypeEnum(stream, "CallStatisticsType", CallStatisticsType);
            SetPacketSize(stream);
        }
    }

    public class T5StreamStateCollection : T5StreamBaseObjectCollection<T5StreamState>
    {

        public override int GetClassEnum()
        {
            return 10028;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10028)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamStateDetail : T5StreamBaseObject
    {
        public Int32 StateId;
        public Int32 SuggestedLength;
        public SuggestedRuleEnum SuggestRoundRule;
        public SuggestedRoundTypeEnum SuggestRoundType;
        public String Description;
        public String Location;
        public ResourceDetailLinkEnum ForwardToDevice;
        public String ForwardToNumber;
        public ForwardTypeEnum ForwardToType;
        public DNDTypeEnum DNDType;
        public Int32 PublicStateId;
        public Int32 PublishedStateId;

        public override int GetClassEnum()
        {
            return 10029;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            StateId = ProxyStream.ReadInt32(stream, "StateId", MaxPos);
            SuggestedLength = ProxyStream.ReadInt32(stream, "SuggestedLength", MaxPos);
            SuggestRoundRule = ProxyStream.ReadSuggestedRuleEnum(stream, "SuggestRoundRule", MaxPos);
            SuggestRoundType = ProxyStream.ReadSuggestedRoundTypeEnum(stream, "SuggestRoundType", MaxPos);
            Description = ProxyStream.ReadString(stream, "Description", MaxPos);
            Location = ProxyStream.ReadString(stream, "Location", MaxPos);
            ForwardToDevice = ProxyStream.ReadResourceDetailLinkEnum(stream, "ForwardToDevice", MaxPos);
            ForwardToNumber = ProxyStream.ReadString(stream, "ForwardToNumber", MaxPos);
            ForwardToType = ProxyStream.ReadForwardTypeEnum(stream, "ForwardToType", MaxPos);
            DNDType = ProxyStream.ReadDNDTypeEnum(stream, "DNDType", MaxPos);
            PublicStateId = ProxyStream.ReadInt32(stream, "PublicStateId", MaxPos);
            PublishedStateId = ProxyStream.ReadInt32(stream, "PublishedStateId", MaxPos);
            if (ClassEnum == 10029)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "StateId", StateId);
            ProxyStream.WriteInt32(stream, "SuggestedLength", SuggestedLength);
            ProxyStream.WriteSuggestedRuleEnum(stream, "SuggestRoundRule", SuggestRoundRule);
            ProxyStream.WriteSuggestedRoundTypeEnum(stream, "SuggestRoundType", SuggestRoundType);
            ProxyStream.WriteString(stream, "Description", Description);
            ProxyStream.WriteString(stream, "Location", Location);
            ProxyStream.WriteResourceDetailLinkEnum(stream, "ForwardToDevice", ForwardToDevice);
            ProxyStream.WriteString(stream, "ForwardToNumber", ForwardToNumber);
            ProxyStream.WriteForwardTypeEnum(stream, "ForwardToType", ForwardToType);
            ProxyStream.WriteDNDTypeEnum(stream, "DNDType", DNDType);
            ProxyStream.WriteInt32(stream, "PublicStateId", PublicStateId);
            ProxyStream.WriteInt32(stream, "PublishedStateId", PublishedStateId);
            SetPacketSize(stream);
        }
    }

    public class T5StreamStateDetailCollection : T5StreamBaseObjectCollection<T5StreamStateDetail>
    {

        public override int GetClassEnum()
        {
            return 10030;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10030)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamSmallDeviceList : T5StreamBaseObjectCollection<T5StreamDeviceS>
    {

        public override int GetClassEnum()
        {
            return 10031;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10031)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamLargeDeviceList : T5StreamBaseObjectCollection<T5StreamDeviceL>
    {

        public override int GetClassEnum()
        {
            return 10032;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10032)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamChallengeResponse : T5StreamBaseObject
    {
        public String Nonce;
        public String NoncePassword;
        public String User;
        public Int32 UserRecId;
        public Boolean UseWindowsAuthentication;
        public ExpectedResponse ExpectedResponse;
        public ResponseCoding HashCoding;
        public ChallengeReason ChallengeReason;
        public String AuthenticationToken;
        public AuthenticationType AuthenticationType;
        public String AuthenticationPortalURL;

        public override int GetClassEnum()
        {
            return 10033;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            Nonce = ProxyStream.ReadString(stream, "Nonce", MaxPos);
            NoncePassword = ProxyStream.ReadString(stream, "NoncePassword", MaxPos);
            User = ProxyStream.ReadString(stream, "User", MaxPos);
            UserRecId = ProxyStream.ReadInt32(stream, "UserRecId", MaxPos);
            UseWindowsAuthentication = ProxyStream.ReadBoolean(stream, "UseWindowsAuthentication", MaxPos);
            ExpectedResponse = ProxyStream.ReadExpectedResponse(stream, "ExpectedResponse", MaxPos);
            HashCoding = ProxyStream.ReadResponseCoding(stream, "HashCoding", MaxPos);
            ChallengeReason = ProxyStream.ReadChallengeReason(stream, "ChallengeReason", MaxPos);
            AuthenticationToken = ProxyStream.ReadString(stream, "AuthenticationToken", MaxPos);
            AuthenticationType = ProxyStream.ReadAuthenticationType(stream, "AuthenticationType", MaxPos);
            AuthenticationPortalURL = ProxyStream.ReadString(stream, "AuthenticationPortalURL", MaxPos);
            if (ClassEnum == 10033)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "Nonce", Nonce);
            ProxyStream.WriteString(stream, "NoncePassword", NoncePassword);
            ProxyStream.WriteString(stream, "User", User);
            ProxyStream.WriteInt32(stream, "UserRecId", UserRecId);
            ProxyStream.WriteBoolean(stream, "UseWindowsAuthentication", UseWindowsAuthentication);
            ProxyStream.WriteExpectedResponse(stream, "ExpectedResponse", ExpectedResponse);
            ProxyStream.WriteResponseCoding(stream, "HashCoding", HashCoding);
            ProxyStream.WriteChallengeReason(stream, "ChallengeReason", ChallengeReason);
            ProxyStream.WriteString(stream, "AuthenticationToken", AuthenticationToken);
            ProxyStream.WriteAuthenticationType(stream, "AuthenticationType", AuthenticationType);
            ProxyStream.WriteString(stream, "AuthenticationPortalURL", AuthenticationPortalURL);
            SetPacketSize(stream);
        }
    }

    public class T5StreamTemplate : T5StreamBaseObject
    {
        public Int32 RecId;
        public String Name;
        public String ShortName;
        public Int32 NextProfileId;
        public T5StreamStateDetail Current = new T5StreamStateDetail();
        public T5StreamStateDetail Next = new T5StreamStateDetail();
        public Int32 SortOrder;
        public EndTypeEnum EndType;
        public Int32 QuickStateOrder;
        public Boolean QuickStateActive;
        public Boolean QuickStateShowStateDialog;
        public Int32 TaskbarOrder;
        public Boolean TaskbarActive;
        public Boolean ActivateFromClient;
        public Boolean ActivateFromCalendar;
        public Boolean ActivateFromSMS;
        public Boolean ActivateFromSwitchBoard;
        public Boolean ActivateFromSmartClient;
        public Boolean Active;

        public override int GetClassEnum()
        {
            return 10034;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            RecId = ProxyStream.ReadInt32(stream, "RecId", MaxPos);
            Name = ProxyStream.ReadString(stream, "Name", MaxPos);
            ShortName = ProxyStream.ReadString(stream, "ShortName", MaxPos);
            NextProfileId = ProxyStream.ReadInt32(stream, "NextProfileId", MaxPos);
            Current = ProxyStream.ReadT5StreamStateDetail(stream, "Current");
            Next = ProxyStream.ReadT5StreamStateDetail(stream, "Next");
            SortOrder = ProxyStream.ReadInt32(stream, "SortOrder", MaxPos);
            EndType = ProxyStream.ReadEndTypeEnum(stream, "EndType", MaxPos);
            QuickStateOrder = ProxyStream.ReadInt32(stream, "QuickStateOrder", MaxPos);
            QuickStateActive = ProxyStream.ReadBoolean(stream, "QuickStateActive", MaxPos);
            QuickStateShowStateDialog = ProxyStream.ReadBoolean(stream, "QuickStateShowStateDialog", MaxPos);
            TaskbarOrder = ProxyStream.ReadInt32(stream, "TaskbarOrder", MaxPos);
            TaskbarActive = ProxyStream.ReadBoolean(stream, "TaskbarActive", MaxPos);
            ActivateFromClient = ProxyStream.ReadBoolean(stream, "ActivateFromClient", MaxPos);
            ActivateFromCalendar = ProxyStream.ReadBoolean(stream, "ActivateFromCalendar", MaxPos);
            ActivateFromSMS = ProxyStream.ReadBoolean(stream, "ActivateFromSMS", MaxPos);
            ActivateFromSwitchBoard = ProxyStream.ReadBoolean(stream, "ActivateFromSwitchBoard", MaxPos);
            ActivateFromSmartClient = ProxyStream.ReadBoolean(stream, "ActivateFromSmartClient", MaxPos);
            Active = ProxyStream.ReadBoolean(stream, "Active", MaxPos);
            if (ClassEnum == 10034)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "RecId", RecId);
            ProxyStream.WriteString(stream, "Name", Name);
            ProxyStream.WriteString(stream, "ShortName", ShortName);
            ProxyStream.WriteInt32(stream, "NextProfileId", NextProfileId);
            Current.StreamOut(stream, "Current");
            Next.StreamOut(stream, "Next");
            ProxyStream.WriteInt32(stream, "SortOrder", SortOrder);
            ProxyStream.WriteEndTypeEnum(stream, "EndType", EndType);
            ProxyStream.WriteInt32(stream, "QuickStateOrder", QuickStateOrder);
            ProxyStream.WriteBoolean(stream, "QuickStateActive", QuickStateActive);
            ProxyStream.WriteBoolean(stream, "QuickStateShowStateDialog", QuickStateShowStateDialog);
            ProxyStream.WriteInt32(stream, "TaskbarOrder", TaskbarOrder);
            ProxyStream.WriteBoolean(stream, "TaskbarActive", TaskbarActive);
            ProxyStream.WriteBoolean(stream, "ActivateFromClient", ActivateFromClient);
            ProxyStream.WriteBoolean(stream, "ActivateFromCalendar", ActivateFromCalendar);
            ProxyStream.WriteBoolean(stream, "ActivateFromSMS", ActivateFromSMS);
            ProxyStream.WriteBoolean(stream, "ActivateFromSwitchBoard", ActivateFromSwitchBoard);
            ProxyStream.WriteBoolean(stream, "ActivateFromSmartClient", ActivateFromSmartClient);
            ProxyStream.WriteBoolean(stream, "Active", Active);
            SetPacketSize(stream);
        }
    }

    public class T5StreamTemplateList : T5StreamBaseObjectCollection<T5StreamTemplate>
    {

        public override int GetClassEnum()
        {
            return 10035;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10035)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamProxyRequest : T5StreamBaseObject
    {
        public T5StreamBaseObject BaseObject = new T5StreamBaseObject();
        public String ClientRequestChain;
        public T5StreamKeyValueList ExtraInfo = new T5StreamKeyValueList();

        public override int GetClassEnum()
        {
            return 10036;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            T5StreamBaseObject baseObj = new T5StreamBaseObject();
            long pos = stream.Position;
            baseObj.StreamIn(stream, PropertyName);
            stream.Position = pos;
            int classEnum = baseObj.ClassEnum;
            BaseObject = ProxyStream.CreateT5BaseObjectFromClassEnum(classEnum, stream, PropertyName);
            ClientRequestChain = ProxyStream.ReadString(stream, "ClientRequestChain", MaxPos);
            ExtraInfo = ProxyStream.ReadT5StreamKeyValueList(stream, "ExtraInfo");
            if (ClassEnum == 10036)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            BaseObject.StreamOut(stream, "BaseObject");
            ProxyStream.WriteString(stream, "ClientRequestChain", ClientRequestChain);
            ExtraInfo.StreamOut(stream, "ExtraInfo");
            SetPacketSize(stream);
        }
    }

    public class T5StreamKeyValue : T5StreamBaseObject
    {
        public String Key;
        public String Value;

        public override int GetClassEnum()
        {
            return 10037;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            Key = ProxyStream.ReadString(stream, "Key", MaxPos);
            Value = ProxyStream.ReadString(stream, "Value", MaxPos);
            if (ClassEnum == 10037)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "Key", Key);
            ProxyStream.WriteString(stream, "Value", Value);
            SetPacketSize(stream);
        }
    }

    public class T5StreamKeyValueList : T5StreamBaseObjectCollection<T5StreamKeyValue>
    {

        public override int GetClassEnum()
        {
            return 10038;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10038)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamProxyCommand : T5StreamBaseObject
    {
        public String CommandType;
        public T5StreamKeyValueList CommandParams = new T5StreamKeyValueList();

        public override int GetClassEnum()
        {
            return 10039;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            CommandType = ProxyStream.ReadString(stream, "CommandType", MaxPos);
            CommandParams = ProxyStream.ReadT5StreamKeyValueList(stream, "CommandParams");
            if (ClassEnum == 10039)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "CommandType", CommandType);
            CommandParams.StreamOut(stream, "CommandParams");
            SetPacketSize(stream);
        }
    }

    public class T5StreamReservationUserList : T5StreamBaseObjectCollection<T5StreamReservationUser>
    {

        public override int GetClassEnum()
        {
            return 10040;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10040)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamLoginData : T5StreamBaseObject
    {
        public String UserName;
        public String UserPassword;
        public String ServerUri;
        public String ServerName;
        public Int32 ServerPort;
        public Boolean DataValidated;

        public override int GetClassEnum()
        {
            return 10041;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            UserName = ProxyStream.ReadString(stream, "UserName", MaxPos);
            UserPassword = ProxyStream.ReadString(stream, "UserPassword", MaxPos);
            ServerUri = ProxyStream.ReadString(stream, "ServerUri", MaxPos);
            ServerName = ProxyStream.ReadString(stream, "ServerName", MaxPos);
            ServerPort = ProxyStream.ReadInt32(stream, "ServerPort", MaxPos);
            DataValidated = ProxyStream.ReadBoolean(stream, "DataValidated", MaxPos);
            if (ClassEnum == 10041)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteString(stream, "UserName", UserName);
            ProxyStream.WriteString(stream, "UserPassword", UserPassword);
            ProxyStream.WriteString(stream, "ServerUri", ServerUri);
            ProxyStream.WriteString(stream, "ServerName", ServerName);
            ProxyStream.WriteInt32(stream, "ServerPort", ServerPort);
            ProxyStream.WriteBoolean(stream, "DataValidated", DataValidated);
            SetPacketSize(stream);
        }
    }

    public class T5StreamUserPreferences : T5StreamBaseObject
    {
        public Boolean ShowProfileImagesInContactsList;

        public override int GetClassEnum()
        {
            return 10042;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            ShowProfileImagesInContactsList = ProxyStream.ReadBoolean(stream, "ShowProfileImagesInContactsList", MaxPos);
            if (ClassEnum == 10042)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteBoolean(stream, "ShowProfileImagesInContactsList", ShowProfileImagesInContactsList);
            SetPacketSize(stream);
        }
    }

    public class T5StreamConnectionStartup : T5StreamBaseObject
    {
        public Int32 NetVersion;
        public ClientTypeEnum ConnectionType;
        public String InstanceName;
        public String ClientDescription;
        public String MachineName;
        public String ClientVersion;

        public override int GetClassEnum()
        {
            return 10043;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            NetVersion = ProxyStream.ReadInt32(stream, "NetVersion", MaxPos);
            ConnectionType = ProxyStream.ReadClientTypeEnum(stream, "ConnectionType", MaxPos);
            InstanceName = ProxyStream.ReadString(stream, "InstanceName", MaxPos);
            ClientDescription = ProxyStream.ReadString(stream, "ClientDescription", MaxPos);
            MachineName = ProxyStream.ReadString(stream, "MachineName", MaxPos);
            ClientVersion = ProxyStream.ReadString(stream, "ClientVersion", MaxPos);
            if (ClassEnum == 10043)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "NetVersion", NetVersion);
            ProxyStream.WriteClientTypeEnum(stream, "ConnectionType", ConnectionType);
            ProxyStream.WriteString(stream, "InstanceName", InstanceName);
            ProxyStream.WriteString(stream, "ClientDescription", ClientDescription);
            ProxyStream.WriteString(stream, "MachineName", MachineName);
            ProxyStream.WriteString(stream, "ClientVersion", ClientVersion);
            SetPacketSize(stream);
        }
    }

    public class T5StreamProxyCommandResult : T5StreamBaseObject
    {
        public Boolean Result;
        public String ResultMessage;
        public Int32 ResultMessageID;
        public T5StreamKeyValueList ResultParams = new T5StreamKeyValueList();
        public Int32 RequestCommandMessageId;
        public String RequestCommandType;
        public T5StreamKeyValueList RequestCommandParams = new T5StreamKeyValueList();

        public override int GetClassEnum()
        {
            return 10044;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            Result = ProxyStream.ReadBoolean(stream, "Result", MaxPos);
            ResultMessage = ProxyStream.ReadString(stream, "ResultMessage", MaxPos);
            ResultMessageID = ProxyStream.ReadInt32(stream, "ResultMessageID", MaxPos);
            ResultParams = ProxyStream.ReadT5StreamKeyValueList(stream, "ResultParams");
            RequestCommandMessageId = ProxyStream.ReadInt32(stream, "RequestCommandMessageId", MaxPos);
            RequestCommandType = ProxyStream.ReadString(stream, "RequestCommandType", MaxPos);
            RequestCommandParams = ProxyStream.ReadT5StreamKeyValueList(stream, "RequestCommandParams");
            if (ClassEnum == 10044)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteBoolean(stream, "Result", Result);
            ProxyStream.WriteString(stream, "ResultMessage", ResultMessage);
            ProxyStream.WriteInt32(stream, "ResultMessageID", ResultMessageID);
            ResultParams.StreamOut(stream, "ResultParams");
            ProxyStream.WriteInt32(stream, "RequestCommandMessageId", RequestCommandMessageId);
            ProxyStream.WriteString(stream, "RequestCommandType", RequestCommandType);
            RequestCommandParams.StreamOut(stream, "RequestCommandParams");
            SetPacketSize(stream);
        }
    }

    public class T5StreamUserImage : T5StreamBaseObject
    {
        public Int32 UserRecId;
        public String UserImage;
        public String UserImageVersion;

        public override int GetClassEnum()
        {
            return 10045;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            UserRecId = ProxyStream.ReadInt32(stream, "UserRecId", MaxPos);
            UserImage = ProxyStream.ReadString(stream, "UserImage", MaxPos);
            UserImageVersion = ProxyStream.ReadString(stream, "UserImageVersion", MaxPos);
            if (ClassEnum == 10045)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "UserRecId", UserRecId);
            ProxyStream.WriteString(stream, "UserImage", UserImage);
            ProxyStream.WriteString(stream, "UserImageVersion", UserImageVersion);
            SetPacketSize(stream);
        }
    }

    public class T5StreamUserImageList : T5StreamBaseObjectCollection<T5StreamUserImage>
    {

        public override int GetClassEnum()
        {
            return 10046;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10046)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamNumberLog : T5StreamBaseObject
    {
        public Int32 RecID;
        public String DeviceID;
        public String PhoneNumber;
        public String PhoneNumberOwner;
        public CallLogType LogType;
        public CallLogDirectionType Direction;
        public DateTime StartTime;
        public DateTime EndTime;

        public override int GetClassEnum()
        {
            return 10047;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            RecID = ProxyStream.ReadInt32(stream, "RecID", MaxPos);
            DeviceID = ProxyStream.ReadString(stream, "DeviceID", MaxPos);
            PhoneNumber = ProxyStream.ReadString(stream, "PhoneNumber", MaxPos);
            PhoneNumberOwner = ProxyStream.ReadString(stream, "PhoneNumberOwner", MaxPos);
            LogType = ProxyStream.ReadCallLogType(stream, "LogType", MaxPos);
            Direction = ProxyStream.ReadCallLogDirectionType(stream, "Direction", MaxPos);
            StartTime = ProxyStream.ReadDateTime(stream, "StartTime", MaxPos);
            EndTime = ProxyStream.ReadDateTime(stream, "EndTime", MaxPos);
            if (ClassEnum == 10047)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "RecID", RecID);
            ProxyStream.WriteString(stream, "DeviceID", DeviceID);
            ProxyStream.WriteString(stream, "PhoneNumber", PhoneNumber);
            ProxyStream.WriteString(stream, "PhoneNumberOwner", PhoneNumberOwner);
            ProxyStream.WriteCallLogType(stream, "LogType", LogType);
            ProxyStream.WriteCallLogDirectionType(stream, "Direction", Direction);
            ProxyStream.WriteDateTime(stream, "StartTime", StartTime);
            ProxyStream.WriteDateTime(stream, "EndTime", EndTime);
            SetPacketSize(stream);
        }
    }

    public class T5StreamNumberLogList : T5StreamBaseObjectCollection<T5StreamNumberLog>
    {

        public override int GetClassEnum()
        {
            return 10048;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            if (ClassEnum == 10048)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            SetPacketSize(stream);
        }
    }

    public class T5StreamUserNumberLog : T5StreamBaseObject
    {
        public Int32 UserRecID;
        public T5StreamNumberLogList NumberLogList = new T5StreamNumberLogList();

        public override int GetClassEnum()
        {
            return 10049;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            UserRecID = ProxyStream.ReadInt32(stream, "UserRecID", MaxPos);
            NumberLogList = ProxyStream.ReadT5StreamNumberLogList(stream, "NumberLogList");
            if (ClassEnum == 10049)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "UserRecID", UserRecID);
            NumberLogList.StreamOut(stream, "NumberLogList");
            SetPacketSize(stream);
        }
    }

    public class T5StreamUserAttachmentsDefinition : T5StreamBaseObject
    {
        public Int32 ResourceId;
        public String DefinitionXML;

        public override int GetClassEnum()
        {
            return 10050;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            ResourceId = ProxyStream.ReadInt32(stream, "ResourceId", MaxPos);
            DefinitionXML = ProxyStream.ReadString(stream, "DefinitionXML", MaxPos);
            if (ClassEnum == 10050)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "ResourceId", ResourceId);
            ProxyStream.WriteString(stream, "DefinitionXML", DefinitionXML);
            SetPacketSize(stream);
        }
    }

    public class T5StreamUserAttachmentsData : T5StreamBaseObject
    {
        public Int32 ResourceID;
        public String StateID;
        public String AttachmentId;
        public String ParentFieldId;
        public String ParentId;
        public String DataXML;
        public String ClientId;
        public String ParentUserData;

        public override int GetClassEnum()
        {
            return 10052;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            ResourceID = ProxyStream.ReadInt32(stream, "ResourceID", MaxPos);
            StateID = ProxyStream.ReadString(stream, "StateID", MaxPos);
            AttachmentId = ProxyStream.ReadString(stream, "AttachmentId", MaxPos);
            ParentFieldId = ProxyStream.ReadString(stream, "ParentFieldId", MaxPos);
            ParentId = ProxyStream.ReadString(stream, "ParentId", MaxPos);
            DataXML = ProxyStream.ReadString(stream, "DataXML", MaxPos);
            ClientId = ProxyStream.ReadString(stream, "ClientId", MaxPos);
            ParentUserData = ProxyStream.ReadString(stream, "ParentUserData", MaxPos);
            if (ClassEnum == 10052)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "ResourceID", ResourceID);
            ProxyStream.WriteString(stream, "StateID", StateID);
            ProxyStream.WriteString(stream, "AttachmentId", AttachmentId);
            ProxyStream.WriteString(stream, "ParentFieldId", ParentFieldId);
            ProxyStream.WriteString(stream, "ParentId", ParentId);
            ProxyStream.WriteString(stream, "DataXML", DataXML);
            ProxyStream.WriteString(stream, "ClientId", ClientId);
            ProxyStream.WriteString(stream, "ParentUserData", ParentUserData);
            SetPacketSize(stream);
        }
    }

    public class T5StreamUserAttachmentsDataLastUsed : T5StreamBaseObject
    {
        public DateTime LastUsed;
        public String DataXML;

        public override int GetClassEnum()
        {
            return 10053;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            LastUsed = ProxyStream.ReadDateTime(stream, "LastUsed", MaxPos);
            DataXML = ProxyStream.ReadString(stream, "DataXML", MaxPos);
            if (ClassEnum == 10053)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteDateTime(stream, "LastUsed", LastUsed);
            ProxyStream.WriteString(stream, "DataXML", DataXML);
            SetPacketSize(stream);
        }
    }

    public class T5StreamUserAttachmentsDataLastUsedCollection : T5StreamBaseObjectCollection<T5StreamUserAttachmentsDataLastUsed>
    {
        public Int32 ResourceId;

        public override int GetClassEnum()
        {
            return 10054;
        }

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            ResourceId = ProxyStream.ReadInt32(stream, "ResourceId", MaxPos);
            if (ClassEnum == 10054)
                MoveToEnd(stream);
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            base.StreamOut(stream, PropertyName);
            ProxyStream.WriteInt32(stream, "ResourceId", ResourceId);
            SetPacketSize(stream);
        }
    }

}
