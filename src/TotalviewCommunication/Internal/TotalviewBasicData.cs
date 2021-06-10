using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Proxy.Streaming;
using System;
using Totalview.Client.Communication.Application.Hashing;
using Totalview.Client.Communication.Model;

namespace Totalview.Communication
{
    internal class TotalviewBasicData : ITotalviewBasicData
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public event EventHandler<BaseObjectHandledArgs>? BaseObjectHandled;

        private readonly ILogger? _logger;
        private readonly ITotalviewState _totalviewState;
        private readonly IOptions<TotalviewOptions> _totalviewOptions;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewBasicData(
            ILoggerFactory loggerFactory, 
            ITotalviewState totalviewState,
            IOptions<TotalviewOptions> totalviewOptions)
        {
            _logger = loggerFactory?.CreateLogger<TotalviewBasicData>();

            _totalviewState = totalviewState ?? throw new ArgumentNullException(nameof(totalviewState));
            _totalviewOptions = totalviewOptions ?? throw new ArgumentNullException(nameof(totalviewOptions));

            _totalviewState.TotalviewBasicData = this;
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public void HandleBaseObject(ITcpConnection connection, T5StreamBaseObject baseObject)
        {
            if (baseObject is T5StreamChallengeResponse challengeResponse)
                HandleChallengeResponse(challengeResponse);
            else if (baseObject is T5StreamStaticData streamStaticData)
                HandleStaticData(streamStaticData);
            else if (baseObject is T5StreamReservationUserList streamReservationUserList)
                HandleReservationUserList(streamReservationUserList);
            else
                _logger?.LogWarning($"Unhandled packet type {baseObject.GetType()}");

            BaseObjectHandled?.Invoke(this, new BaseObjectHandledArgs(baseObject));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void HandleReservationUserList(T5StreamReservationUserList streamReservationUserList)
        {
            _logger?.LogDebug($"Recieved reservations with {streamReservationUserList.ItemList.Count} users");
            foreach (var item in streamReservationUserList.ItemList)
            {
                _logger?.LogDebug($"  User id: {item.RecordId} contains {item.ReservationList.ItemList.Count} reservations");
            }
        }

        private void HandleStaticData(T5StreamStaticData streamStaticData)
        {
            _logger?.LogDebug($"Recieved static data:");
            _logger?.LogDebug($"  number of resources: {streamStaticData.ResourceList.ItemList.Count}");
            _logger?.LogDebug($"  number of devices: {streamStaticData.LargeDeviceList.ItemList.Count}");
            _logger?.LogDebug($"  number of states: {streamStaticData.StateList.ItemList.Count}");
            _logger?.LogDebug($"  number of current states: {streamStaticData.StateCurrentList.ItemList.Count}");
        }

        private void HandleChallengeResponse(T5StreamChallengeResponse StreamChallengeResponse)
        {
            if (StreamChallengeResponse.ChallengeReason == ChallengeReason.Authenticated)
            {
                int LoggedInUserRecId = StreamChallengeResponse.UserRecId;
                _logger?.LogInformation($"authenticated as {StreamChallengeResponse.UserRecId}");

                SendObject(T5StreamProxyCommandFactory.CreateGetBasicData());
                SendObject(T5StreamProxyCommandFactory.CreateGetAllAppointments());
                SendObject(T5StreamProxyCommandFactory.CreateGetUserNumberLog(LoggedInUserRecId, 50));
            }
            else if (StreamChallengeResponse.ChallengeReason == ChallengeReason.Login)
            {
                T5StreamChallengeResponse ChallengeResponse = new T5StreamChallengeResponse
                {
                    UseWindowsAuthentication = false,
                    User = _totalviewOptions.Value.UserName,
                    HashCoding = ResponseCoding.SHA,
                    NoncePassword = HashResult.EncryptTotalviewPassword(_totalviewOptions.Value.Password).ToLower()
                };

                SendObject(ChallengeResponse);
            }
            else if (StreamChallengeResponse.ChallengeReason == ChallengeReason.NotAuthenticated)
            {
                _logger?.LogWarning("Using hardcoded authentication token-this will problably fail:(");
                StreamChallengeResponse.AuthenticationToken = "16fed3e9-56da-4d32-b64c-553a01623545";
                SendObject(StreamChallengeResponse);
            }
            else
            {
                _logger?.LogWarning($"Unhandled challenge reason: {StreamChallengeResponse.ChallengeReason}");
                //LoggedInResource = null;
                //NeedAuthentication = true;
                //RaiseBasicTVEvent(TVEventId.LoginFailed);
                //SaveLoginData(true);
                //GotoAuthentication(true);
            }
        }

        public void SendObject(T5StreamBaseObject baseObject) =>
            _totalviewState?.SendObject(baseObject);
    }
}
