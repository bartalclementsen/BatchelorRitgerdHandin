using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using System.Linq;
using Totalview.Server.Services;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamResourceDetailCollectionHandler : StreamBaseObjectHandlerBase<T5StreamResourceDetailCollection>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<T5StreamResourceDetailCollectionHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITotalviewDataService _totalviewCommunicationService;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public T5StreamResourceDetailCollectionHandler(
            ILogger<T5StreamResourceDetailCollectionHandler> logger,
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
        protected override void HandlePacket(T5StreamResourceDetailCollection streamResourceDetailCollection)
        {
            _logger.LogDebug($"Handeling packet of type {streamResourceDetailCollection.GetType()}");

            try
            {
                ResourceDetailCollection resourceDetailCollection = _mapper.Map<ResourceDetailCollection>(streamResourceDetailCollection);

                foreach (ResourceDetail resourceDetail in resourceDetailCollection.Items)
                {
                    Resource? foundResource = _totalviewCommunicationService.ResourceCollection.Items.FirstOrDefault(r => r.RecId == resourceDetail.OwnerId);
                    if (foundResource == null)
                        continue;

                    ResourceDetail? foundReourceDetail = foundResource.DetailCollection.Items.FirstOrDefault(d => d.Recid == resourceDetail.Recid);
                    if (foundReourceDetail != null)
                    {
                        foundResource.DetailCollection.Items.Remove(foundReourceDetail);
                    }

                    if (streamResourceDetailCollection.MessageType != Proxy.Streaming.T5MessageTypeEnum.Delete)
                    {
                        foundResource.DetailCollection.Items.Add(resourceDetail);
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
