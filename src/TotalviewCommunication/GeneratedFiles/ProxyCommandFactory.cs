using Proxy.Streaming;

namespace Totalview.Client.Communication.Model
{
    public class T5StreamProxyCommandFactory : T5StreamProxyCommand
    {
        /// <summary>
        /// Requests all appointments for all users.
        /// </summary>
        public static T5StreamProxyCommand CreateGetAllAppointments()
        {
            T5StreamProxyCommand result = new T5StreamProxyCommand();
            result.CommandType = "GetAllAppointments";
            return result;
        }

        /// <summary>
        /// Requests static data containing users, states, templates etc..
        /// </summary>
        public static T5StreamProxyCommand CreateGetBasicData()
        {
            T5StreamProxyCommand result = new T5StreamProxyCommand();
            result.CommandType = "GetBasicData";
            return result;
        }

        /// <summary>
        /// Notifies server if server should send event over connection.
        /// Default for at connection is false. CreateGetBasicData() sets it to true
        /// </summary>
        /// <param name="accepted">true is server to send events</param>
        public static T5StreamProxyCommand CreateSetSendEvents(bool accepted)
        {
            T5StreamProxyCommand result = new T5StreamProxyCommand();
            result.CommandType = "SetEventsAccepted";
            result.CommandParams.ItemList.Add(new T5StreamKeyValue { Key = "Accepted", Value = accepted ? "true" : "false" });
            return result;
        }

        /// <summary>
        /// Request user image by user rec id. Returns image in T5StreamUserImage
        /// </summary>
        /// <param name="userRecID">the users totalview recId</param>
        /// <returns></returns>
        public static T5StreamProxyCommand CreateGetUserImage(int userRecID)
        {
            T5StreamProxyCommand result = new T5StreamProxyCommand();
            result.CommandType = "GetUserImage";
            result.CommandParams.ItemList.Add(new T5StreamKeyValue { Key = "UserRecID", Value = userRecID.ToString() });
            return result;
        }

        /// <summary>
        /// Request all user images. Returns images in T5StreamUserImageList
        /// </summary>
        /// <returns></returns>
        public static T5StreamProxyCommand CreateGetAllUserImages()
        {
            T5StreamProxyCommand result = new T5StreamProxyCommand();
            result.CommandType = "GetAllUserImages";
            return result;
        }
        /// <summary>
        /// Request number log for user. Returns log as T5StreamUserNumberLog
        /// </summary>
        /// <param name="userRecID">the users totalview recId</param>
        /// <param name="maxResult">max number of records returned in list</param>
        /// <returns></returns>
        public static T5StreamProxyCommand CreateGetUserNumberLog(int userRecID, int maxResult)
        {
            T5StreamProxyCommand result = new T5StreamProxyCommand();
            result.CommandType = "GetUserNumberLog";
            result.CommandParams.ItemList.Add(new T5StreamKeyValue { Key = "UserRecID", Value = userRecID.ToString() });
            result.CommandParams.ItemList.Add(new T5StreamKeyValue { Key = "MaxResult", Value = maxResult.ToString() });
            return result;
        }

        /// <summary>
        /// Requests a call to be made between two numbers
        /// </summary>
        /// <param name="fromNumber">Number to call from </param>
        /// <param name="toNumber">Number to call to</param>
        /// <returns></returns>
        public static T5StreamProxyCommand CreateMakeCallFromNumberToNumber(string fromNumber, string toNumber)
        {
            T5StreamProxyCommand result = new T5StreamProxyCommand();
            result.CommandType = "MakeCallFromNumberToNumber";
            result.CommandParams.ItemList.Add(new T5StreamKeyValue { Key = "FromNumber", Value = fromNumber });
            result.CommandParams.ItemList.Add(new T5StreamKeyValue { Key = "ToNumber", Value = toNumber });
            return result;
        }
    }
}
