using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Streaming
{
    public class ProxyBaseStream
    {
        public static byte ReadByte(Stream stream, string PropertyName)
        {
            byte[] buff = new byte[1];
            int numBytesRead = 0;
            stream.Read(buff, numBytesRead, 1);
            return buff[0];
        }

        public static void WriteByte(Stream stream, string PropertyName, byte value)
        {
            stream.WriteByte(value);
        }

        public static Boolean ReadBoolean(Stream stream, string PropertyName, int MaxPos)
        {
            if (stream.Position >= MaxPos) return false;
            return ReadByte(stream, PropertyName) != 0;
        }

        public static void WriteBoolean(Stream stream, string PropertyName, Boolean value)
        {
            if (value) WriteByte(stream, PropertyName, 1);
            else WriteByte(stream, PropertyName, 0);
        }

        public static int ReadInt32(Stream stream, string PropertyName, int MaxPos)
        {
            if (stream.Position >= MaxPos) return 0;
            byte[] buff = new byte[4];
            int numBytesRead = 0;
            stream.Read(buff, numBytesRead, 4);

            return BitConverter.ToInt32(buff, 0);
        }

        public static void WriteInt32(Stream stream, string PropertyName, int value)
        {
            byte[] buff = BitConverter.GetBytes(value);
            stream.Write(buff, 0, 4);
        }

        public static string ReadString(Stream stream, string PropertyName, int MaxPos)
        {
            if (stream.Position >= MaxPos) return String.Empty;
            int numChars = ReadInt32(stream, PropertyName, MaxPos);
            char[] charArray = new char[numChars];
            byte[] byteArray = new byte[2 * numChars];
            stream.Read(byteArray, 0, 2 * numChars);
            for (int i = 0; i < numChars; i++)
            {
                charArray[i] = (char)byteArray[2 * i];
            }
            return new String(charArray);
        }

        public static void WriteString(Stream stream, string PropertyName, string value)
        {
            if (value == null) value = "";
            WriteInt32(stream, PropertyName, value.Length);
            char[] charArray = value.ToCharArray();
            byte[] byteArray = new byte[value.Length * 2];
            for (int i = 0; i < value.Length; i++)
            {
                byteArray[2 * i] = 0;
                byteArray[2 * i] = Convert.ToByte(charArray[i]);
            }
            stream.Write(byteArray, 0, byteArray.Length);
        }

        public static DateTime ReadDateTime(Stream stream, string PropertyName, int MaxPos)
        {
            try { 
                if (stream.Position >= MaxPos) return DateTime.MinValue;
                String dataAsString = ReadString(stream, PropertyName, MaxPos);
                return DateTime.Parse(dataAsString); // Converterar automatiskt utc til lokala tíð
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in ReadDateTime: {ex.Message} property {PropertyName}");
                return DateTime.Now;
//                throw;
            }
            //DateTime utcDateTime = DateTime.Parse(dataAsString);
            //return utcDateTime.ToLocalTime();


            //return DateTime.Parse(dataAsString);
            //System.Diagnostics.Debugger.Break();
            //return DateTime.Now;

            //return Convert.ToDateTime(dataAsString);
        }

        public static void WriteDateTime(Stream stream, string PropertyName, DateTime value)
        {
            // System.Diagnostics.Debugger.Break();
            string dateAsString = value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffZ");//K");

            WriteString(stream, PropertyName, dateAsString);//"2012-01-01T00:00:00:000Z");
            //WriteString(stream, PropertyName, "2012-01-01T00:00:00:000Z");
        }

        public static void WriteDouble(Stream stream, string PropertyName, double value)
        {
            byte[] buff = BitConverter.GetBytes(value);
            stream.Write(buff, 0, 8);
        }

        public static double ReadDouble(Stream stream, string PropertyName, int MaxPos)
        {
            if (stream.Position >= MaxPos) return 0;
            byte[] buff = new byte[8];
            stream.Read(buff, 0, 8);
            return BitConverter.ToDouble(buff, 0);
        }

        public static void WriteUInt32(Stream stream, string PropertyName, UInt32 value)
        {
            byte[] buff = BitConverter.GetBytes(value);
            stream.Write(buff, 0, 4);
        }

        public static UInt32 ReadUInt32(Stream stream, string PropertyName, int MaxPos)
        {
            if (stream.Position >= MaxPos) return 0;
            byte[] buff = new byte[4];
            stream.Read(buff, 0, 4);
            return BitConverter.ToUInt32(buff, 0);
        }

        public static void WriteUInt16(Stream stream, string PropertyName, ushort value)
        {
            byte[] buff = BitConverter.GetBytes(value);
            stream.Write(buff, 0, 2);
        }

        public static ushort ReadUInt16(Stream stream, string PropertyName, int MaxPos)
        {
            if (stream.Position >= MaxPos) return 0;
            byte[] buff = new byte[2];
            stream.Read(buff, 0, 2);
            return BitConverter.ToUInt16(buff, 0);
        }


        public static T5StreamBaseObject ReadT5StreamBaseObject(Stream stream, String PropertyName)
        {
            T5StreamBaseObject result = new T5StreamBaseObject();
            result.StreamIn(stream, PropertyName);
            return result;
        }

        public static void WriteT5StreamBaseObject(Stream stream, String PropertyName, T5StreamBaseObject Value)
        {
            Value.StreamOut(stream, PropertyName);
        }
    }


    public enum T5RequestEnum { None, Get, Put, Error }
    public enum T5MessageTypeEnum { None, Get, Create, Update, Delete }
    public enum T5MessageResultEnum { None, RequestOK, RequestFail, Updated, Deleted }

    public class T5StreamBaseObject
    {
        protected long PacketSize = 0, PacketSizeStreamPos = 0;
        protected int MaxPos = 0;
        public Int32 ClassEnum;
        public T5RequestEnum Command = T5RequestEnum.None;
        public T5MessageTypeEnum MessageType = T5MessageTypeEnum.None;
        public T5MessageResultEnum MessageResult = T5MessageResultEnum.None;
        public int MessageId;
        public string MessageError;
        public DateTime StreamOutTime;

        public virtual int GetClassEnum()
        {
            return 0;
        }

        protected void SetPacketSize(Stream stream)
        {
            PacketSize = stream.Position - PacketSizeStreamPos;
            long CurrentPos = stream.Position;
            stream.Position = PacketSizeStreamPos;
            ProxyBaseStream.WriteInt32(stream, "PacketSize", (int)PacketSize);
            stream.Position = CurrentPos;
            MaxPos = (int)CurrentPos;
        }
        protected void MoveToEnd(Stream stream)
        {
            stream.Position = MaxPos;
        }

        public virtual void StreamIn(Stream stream, String PropertyName)
        {
            PacketSizeStreamPos = stream.Position;
            PacketSize = ProxyBaseStream.ReadInt32(stream, "PacketSize", (int)PacketSizeStreamPos + 4);
            MaxPos = (int)(PacketSizeStreamPos + PacketSize);
            ClassEnum = ProxyBaseStream.ReadInt32(stream, "ClassEnum", MaxPos);
            Command = (T5RequestEnum)ProxyBaseStream.ReadInt32(stream, "Command", MaxPos);
            MessageType = (T5MessageTypeEnum)ProxyBaseStream.ReadInt32(stream, "MessageType", MaxPos);
            MessageResult = (T5MessageResultEnum)ProxyBaseStream.ReadInt32(stream, "MessageResult", MaxPos);
            MessageId = ProxyBaseStream.ReadInt32(stream, "MessageId", MaxPos);
            MessageError = ProxyBaseStream.ReadString(stream, "MessageError", MaxPos);
            StreamOutTime = DateTime.Now;
            StreamOutTime = ProxyBaseStream.ReadDateTime(stream, "StreamOutTime", MaxPos);
        }

        public virtual void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = GetClassEnum();
            StreamOutTime = DateTime.Now;
            PacketSizeStreamPos = stream.Position;
            PacketSize = 0;
            ProxyBaseStream.WriteInt32(stream, "PacketSize", (int)PacketSize);
            ProxyBaseStream.WriteInt32(stream, "ClassEnum", ClassEnum);
            ProxyBaseStream.WriteInt32(stream, "Command", Convert.ToInt32(Command));
            ProxyBaseStream.WriteInt32(stream, "MessageType", Convert.ToInt32(MessageType));
            ProxyBaseStream.WriteInt32(stream, "MessageResult", Convert.ToInt32(MessageResult));
            ProxyBaseStream.WriteInt32(stream, "MessageId", MessageId);
            ProxyBaseStream.WriteString(stream, "MessageError", MessageError);
            ProxyBaseStream.WriteDateTime(stream, "StreamOutTime", StreamOutTime);
            SetPacketSize(stream);
        }

        public void AssignFrom(T5StreamBaseObject assignFrom)
        {
            MemoryStream ms = new MemoryStream();
            assignFrom.StreamOut(ms, "TEMP");
            ms.Position = 0;
            StreamIn(ms, "TEMP");
        }

        public static bool BinaryEqualTo(T5StreamBaseObject Object1, T5StreamBaseObject Object2)
        {
            if (Object1.GetType() != Object2.GetType())
                return false;

            MemoryStream ms1 = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();

            if (ms1.Length != ms2.Length)
                return false;

            Object1.StreamOut(ms1, "EQTEST");
            Object2.StreamOut(ms2, "EQTEST");

            var msArray1 = ms1.ToArray();
            var msArray2 = ms2.ToArray();

            return msArray1.SequenceEqual(msArray2);
        }
    }

    public class T5StreamBaseObjectCollection<T> : T5StreamBaseObject where T : T5StreamBaseObject, new()
    {
        public List<T> ItemList = new List<T>();

        public override void StreamIn(Stream stream, String PropertyName)
        {
            base.StreamIn(stream, PropertyName);
            int NumberOfItems = ProxyBaseStream.ReadInt32(stream, "NumberOfItems", MaxPos);
            ItemList = new List<T>();
            for (int i = 0; i < NumberOfItems; i++)
                ItemList.Add(StreamInItem(stream));
        }

        public override void StreamOut(Stream stream, String PropertyName)
        {
            ClassEnum = 3001;
            base.StreamOut(stream, PropertyName);
            if ((ItemList == null) || (ItemList.Count == 0))
            {
                ProxyBaseStream.WriteInt32(stream, "NumberOfItems", 0);
            }
            else
            {
                ProxyBaseStream.WriteInt32(stream, "NumberOfItems", ItemList.Count());
                for (int i = 0; i < ItemList.Count; i++)
                    StreamOutItem(stream, ItemList[i]);
            }
        }

        public virtual T StreamInItem(Stream stream)
        {
            T result = new T();
            result.StreamIn(stream, "ListItem");
            return result;
        }

        public virtual void StreamOutItem(Stream stream, T Item)
        {
            Item.StreamOut(stream, "ListItem");
        }
    }
}



