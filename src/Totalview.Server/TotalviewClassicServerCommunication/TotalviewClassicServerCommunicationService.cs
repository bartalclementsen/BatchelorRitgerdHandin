using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Proxy.Streaming;
using System;
using System.Collections.Generic;
using Totalview.Communication;
using Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication
{
    internal interface ITotalviewClassicServerCommunicationService
    {
        void GetAllUserImages();

        void GetBasicData();

        void GetUserImage(int userRecID);

        void GetUserNumberLog(int userRecID, int maxResult = 50);

        void MakeCallFromNumberToNumber(string fromNumber, string toNumber);

        void SetEventsAccepted(bool accepted);

        void Start();

        void SendMessage(T5StreamBaseObject t5StreamBaseObject);
    }

    internal class TotalviewClassicServerCommunicationService : ITotalviewClassicServerCommunicationService
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private const int _maxRetries = 5;

        private readonly ILogger<TotalviewClassicServerCommunicationService> _logger;
        private readonly ITotalviewClient _totalviewClient;
        private readonly IOptions<TotalviewOptions> _totalviewOptions;

        private readonly IEnumerable<IStreamBaseObjectHandler> _streamBaseObjectHandlers;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewClassicServerCommunicationService(
            ILogger<TotalviewClassicServerCommunicationService> logger,
            ITotalviewClient totalviewClient,
            IEnumerable<IStreamBaseObjectHandler> streamBaseObjectHandlers,
            IOptions<TotalviewOptions> totalviewOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _totalviewClient = totalviewClient ?? throw new ArgumentNullException(nameof(totalviewClient));
            _streamBaseObjectHandlers = streamBaseObjectHandlers ?? throw new ArgumentNullException(nameof(streamBaseObjectHandlers));
            _totalviewOptions = totalviewOptions ?? throw new ArgumentNullException(nameof(totalviewOptions));

            _totalviewClient.PackedRecieved += TotalviewClient_PackedRecieved;
            _totalviewClient.Disconnected += TotalviewClient_Disconnected;
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public void Start()
        {
            _logger.LogInformation("Starting connection to Totalview Classic Server");
            ConnectToTotalviewClassServer();
        }

        public void SendMessage(T5StreamBaseObject t5StreamBaseObject)
        {
            _logger.LogDebug($"Sending message of type {t5StreamBaseObject.GetType()})");

            try
            {
                _totalviewClient.SendMessage(t5StreamBaseObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not send message");
                throw;
            }
        }

        public void GetAllAppointmentsAsync()
        {
            _logger.LogInformation($"{nameof(GetAllAppointmentsAsync)}()");

            _totalviewClient.SendMessage(new T5StreamProxyCommand()
            {
                CommandType = "GetAllAppointments"
            });
        }

        public void GetBasicData()
        {
            _totalviewClient.SendMessage(new T5StreamProxyCommand()
            {
                CommandType = "GetBasicData"
            });
        }

        public void SetEventsAccepted(bool accepted)
        {
            _logger.LogInformation($"{nameof(SetEventsAccepted)}(accepted: {accepted})");

            _totalviewClient.SendMessage(new T5StreamProxyCommand()
            {
                CommandParams = new T5StreamKeyValueList
                {
                    ItemList = new List<T5StreamKeyValue>()
                    {
                        new T5StreamKeyValue
                        {
                            Key = "Accepted",
                            Value = accepted ? "True" : "False"
                        }
                    }
                },
                CommandType = "SetEventsAccepted"
            });
        }

        public void GetUserImage(int userRecID)
        {
            _logger.LogInformation($"{nameof(GetUserImage)}(userRecID: {userRecID})");

            _totalviewClient.SendMessage(new T5StreamProxyCommand()
            {
                CommandParams = new T5StreamKeyValueList
                {
                    ItemList = new List<T5StreamKeyValue>()
                    {
                        new T5StreamKeyValue
                        {
                            Key = "UserRecID",
                            Value = userRecID.ToString()
                        }
                    }
                },
                CommandType = "GetUserImage"
            });
        }

        public void GetAllUserImages()
        {
            _logger.LogInformation($"{nameof(GetAllUserImages)}()");

            _totalviewClient.SendMessage(new T5StreamProxyCommand()
            {
                CommandType = "GetAllUserImages"
            });
        }

        public void GetUserNumberLog(int userRecID, int maxResult = 50)
        {
            _logger.LogInformation($"{nameof(GetUserNumberLog)}(userRecID: {userRecID}, maxResult: {maxResult})");

            _totalviewClient.SendMessage(new T5StreamProxyCommand()
            {
                CommandParams = new T5StreamKeyValueList
                {
                    ItemList = new List<T5StreamKeyValue>()
                    {
                        new T5StreamKeyValue
                        {
                            Key = "UserRecID",
                            Value = userRecID.ToString()
                        },
                        new T5StreamKeyValue
                        {
                            Key = "MaxResult",
                            Value = maxResult.ToString()
                        }
                    }
                },
                CommandType = "GetUserNumberLog"
            });
        }

        public void MakeCallFromNumberToNumber(string fromNumber, string toNumber)
        {
            _logger.LogInformation($"{nameof(MakeCallFromNumberToNumber)}(fromNumber: {fromNumber}, toNumber: {toNumber})");

            _totalviewClient.SendMessage(new T5StreamProxyCommand()
            {
                CommandParams = new T5StreamKeyValueList
                {
                    ItemList = new List<T5StreamKeyValue>()
                    {
                        new T5StreamKeyValue
                        {
                            Key = "FromNumber",
                            Value = fromNumber
                        },
                        new T5StreamKeyValue
                        {
                            Key = "ToNumber",
                            Value = toNumber
                        }
                    }
                },
                CommandType = "MakeCallFromNumberToNumber"
            });
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void ConnectToTotalviewClassServer()
        {
            _logger.LogInformation($"Connecting to Totalview Classic server on {_totalviewOptions.Value.HostNameOrAddress}:{_totalviewOptions.Value.Port}");

            Random jitterer = new();
            int retryAttempt = 0;
            bool _isConnected = false;

            while (_isConnected == false)
            {
                try
                {
                    _totalviewClient.ConnectAsync().Wait();

                    _logger.LogInformation("Connected to Totalview Classic Server");
                    _isConnected = true;
                }
                catch (Exception ex)
                {
                    if (retryAttempt >= _maxRetries)
                    {
                        _logger.LogError(ex, "Could not Totalview Classic Server");
                        throw;
                    }

                    TimeSpan timeout = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))  // exponential back-off: 2, 4, 8 etc
                    + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)); // plus some jitter: up to 1 second

                    int millisecondsTimeout = (int)timeout.TotalMilliseconds;

                    _logger.LogWarning($"Could not connect on try {retryAttempt}/{_maxRetries}. Will retry after {millisecondsTimeout}ms");
                    System.Threading.Thread.Sleep(millisecondsTimeout);
                }

                retryAttempt++;
            }
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void TotalviewClient_PackedRecieved(object? sender, PackedRecievedArgs e)
        {
            if (e.StreamBaseObject == null)
                return;

            _logger.LogDebug($"Packet recieved of type: {e.StreamBaseObject.GetType()}");

            bool wasHandled = false;
            foreach (IStreamBaseObjectHandler streamBaseObjectHandlers in _streamBaseObjectHandlers)
            {
                if (streamBaseObjectHandlers.HandlePacked(e.StreamBaseObject))
                {
                    wasHandled = true;
                }
            }

            if (wasHandled == false)
            {
                _logger.LogWarning($"Packet NOT HANDLED of type: {e.StreamBaseObject.GetType()}");
            }
        }

        private void TotalviewClient_Disconnected(object? sender, EventArgs e)
        {
            _logger.LogWarning("Disconnected from totalview server. Will try to reconnect.");
            ConnectToTotalviewClassServer();
        }
    }
}
