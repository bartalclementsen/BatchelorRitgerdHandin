using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using System.Linq;
using Totalview.Server.Services;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamResourceCollectionHandler : StreamBaseObjectHandlerBase<T5StreamResourceCollection>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<T5StreamResourceCollectionHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITotalviewDataService _totalviewCommunicationService;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public T5StreamResourceCollectionHandler(
            ILogger<T5StreamResourceCollectionHandler> logger,
            IMapper mapper,
            ITotalviewDataService totalviewCommunicationService,
            ITotalviewSubscriptionsHandler totalviewSubscriptionsHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _totalviewCommunicationService = totalviewCommunicationService ?? throw new ArgumentNullException(nameof(totalviewCommunicationService));
            _totalviewSubscriptionsHandler = totalviewSubscriptionsHandler ?? throw new ArgumentNullException(nameof(totalviewSubscriptionsHandler));
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override void HandlePacket(T5StreamResourceCollection streamResourceCollection)
        {
            _logger.LogDebug($"Handeling packet of type {streamResourceCollection.GetType()}");

            try
            {
                ResourceCollection resourceCollection = _mapper.Map<ResourceCollection>(streamResourceCollection);

                foreach (Resource resource in resourceCollection.Items)
                {
                    Resource? foundResource = _totalviewCommunicationService.ResourceCollection.Items?.FirstOrDefault(r => r.RecId == resource.RecId);
                    if (foundResource != null)
                    {
                        _totalviewCommunicationService.ResourceCollection.Items?.Remove(foundResource);
                    }

                    if (streamResourceCollection.MessageType != T5MessageTypeEnum.Delete)
                    {
                        _totalviewCommunicationService.ResourceCollection.Items?.Add(resource);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not handle packet");
            }
        }
    }
}
