using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Totalview.Communication
{
    internal class TcpConnection : ITcpConnection
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public event EventHandler<EventArgs>? Disconnected;

        private bool _isConnecting;
        private TaskCompletionSource? _connectTaskCompletionSource;
        private CancellationTokenSource? _connectCancellationTokenSource;
        private Stream? stream;

        private readonly ILogger? _logger;
        private readonly ITotalviewState _totalviewState;
        private readonly IOptions<TotalviewOptions> _totalviewOptions;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TcpConnection(ILoggerFactory loggerFactory, ITotalviewState totalviewState, IOptions<TotalviewOptions> totalviewOptions)
        {
            _logger = loggerFactory?.CreateLogger<TcpConnection>();
            _totalviewState = totalviewState ?? throw new ArgumentNullException(nameof(totalviewState));
            _totalviewOptions = totalviewOptions ?? throw new ArgumentNullException(nameof(totalviewOptions));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_isConnecting == true)
                {
                    throw new Exception("Is already connecting");
                }

                _connectCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                _connectTaskCompletionSource = new TaskCompletionSource();
                _ = _connectCancellationTokenSource.Token.Register(() =>
                {
                    _connectTaskCompletionSource?.SetCanceled();
                });
                _connectCancellationTokenSource.CancelAfter(_totalviewOptions.Value.ConnectionTimeoutMilliseconds);

                ConnectSocket();
                return _connectTaskCompletionSource.Task;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Could not connect");

                throw new TotalviewCommunicationException(ex);
            }
        }

        public void Disconnect()
        {

        }

        public void SendData(int dataSize, byte[] data)
        {
            if(stream == null)
            {
                throw new NotConnectedException();
            }

            try
            {
                byte[] buff = new byte[dataSize + 6];

                byte[] MagicNumber = new byte[2] { 0xDE, 0xFE };
                Buffer.BlockCopy(MagicNumber, 0, buff, 0, 2);

                byte[] DataSizeBuffer = BitConverter.GetBytes(dataSize);
                Buffer.BlockCopy(DataSizeBuffer, 0, buff, 2, 4);

                Buffer.BlockCopy(data, 0, buff, 6, dataSize);
                stream.Write(buff, 0, dataSize + 6);
            }
            catch (Exception ex)
            {
                throw new TotalviewCommunicationException(ex);
            }
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private Socket? ConnectSocket()
        {
            string server = _totalviewOptions.Value.HostNameOrAddress;
            int port = _totalviewOptions.Value.Port;

            _logger?.LogInformation($"Connecting to {server}:{port}");

            Socket? socket = null;
            IPHostEntry hostEntry = Dns.GetHostEntry(server);

            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new(address, port);
                Socket tempSocket = new(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Connect(ipe);

                if (tempSocket.Connected)
                {
                    socket = tempSocket;
                    stream = new NetworkStream(socket);
                    StartReadingFromStream(stream);
                    _logger?.LogInformation($"Connected to {server}:{port}");
                    break;
                }
                else
                {
                    continue;
                }
            }

            return socket;
        }

        private void StartReadingFromStream(Stream stream)
        {
            Thread thread = new(() => GetNextPacket(stream))
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void GetNextPacket(Stream stream)
        {
            try
            {
                byte[] header = new byte[9];
                ReadSpecifcAmountOfData(stream, header, 0, 6);

                if (header[0] == 36) // xml header only used for tls enabling
                {
                    ReadSpecifcAmountOfData(stream, header, 6, 3);

                    string headerstring = System.Text.Encoding.Default.GetString(header);
                    _logger?.LogInformation("Data header available: " + header);
                    int datasize = Convert.ToInt32(headerstring.Substring(1, 8));
                    byte[] data = new byte[datasize];
                    stream.Read(data, 0, datasize);
                    string datastring = System.Text.Encoding.Default.GetString(data).Trim();
                    _logger?.LogInformation("Data  available: " + datastring);
                    if (datastring.Contains("NOTLS"))
                        _logger?.LogInformation("Running without tls");
                    else
                    {
                        _logger?.LogInformation("Running with tls");
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                        AuthenticateAsClient(true);
                        _logger?.LogInformation("Tls negotiation completed");
                    }
                    ConnectionIsReady();

                }
                else
                {
                    _logger?.LogInformation("Got a proper packet");
                    int datasize = (int)BitConverter.ToUInt32(header, 2);
                    byte[] data = new byte[datasize];
                    ReadSpecifcAmountOfData(stream, data, 0, datasize);

                    _totalviewState.EvPacketAvailable(this, datasize, data);
                }
                GetNextPacket(stream);
            }
            catch (Exception)
            {
                Disconnected?.Invoke(this, new EventArgs());
            }
        }

        private void ReadSpecifcAmountOfData(Stream stream, byte[] data, int startIndex, int datasize)
        {
            int totaldataread = 0;
            while (totaldataread < datasize)
            {
                int dataread = stream.Read(data, startIndex + totaldataread, datasize - totaldataread);
                totaldataread += dataread;
            }
        }

        private void ConnectionIsReady()
        {
            _logger?.LogInformation("Connection ready");
            _totalviewState.EvConnectionStateChanged(this, ConnectionState.ConnnectionReady);
            
            _isConnecting = false;
            _connectTaskCompletionSource?.SetResult();
            _connectTaskCompletionSource = null;
            _connectCancellationTokenSource?.Dispose();
            _connectCancellationTokenSource = null;
        }

        private void AuthenticateAsClient(bool acceptAllCertificates)
        {
            RemoteCertificateValidationCallback? remoteCertificateValidationCallback = null;

            if (acceptAllCertificates)
                remoteCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllServerCertificate);

            SslStream sslStream = new (stream, false, remoteCertificateValidationCallback, null);
            try
            {
                sslStream.AuthenticateAsClient("");
                stream = sslStream;
            }
            catch (AuthenticationException e)
            {
                stream.Close();
                throw;
            }
        }

        private static bool AcceptAllServerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
