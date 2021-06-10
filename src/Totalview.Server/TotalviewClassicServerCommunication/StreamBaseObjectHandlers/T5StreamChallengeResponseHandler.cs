using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamChallengeResponseHandler : StreamBaseObjectHandlerBase<T5StreamChallengeResponse>
    {
        private readonly ILogger<T5StreamChallengeResponseHandler> _logger;

        public T5StreamChallengeResponseHandler(ILogger<T5StreamChallengeResponseHandler> logger) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override void HandlePacket(T5StreamChallengeResponse streamChallengeResponse)
        {
            _logger.LogDebug($"Handeling packet of type {streamChallengeResponse.GetType()}");

            // TODO: FIGURE OUT WHAT TO DO HERE!
        }
    }
}
