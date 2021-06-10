using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using System.Linq;
using Totalview.Server.Services;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamResourceHandler : StreamBaseObjectHandlerBase<T5StreamResource>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<T5StreamResourceHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITotalviewDataService _totalviewCommunicationService;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public T5StreamResourceHandler(
            ILogger<T5StreamResourceHandler> logger,
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
        protected override void HandlePacket(T5StreamResource streamResource)
        {
            _logger.LogDebug($"Handeling packet of type {streamResource.GetType()}");

            try
            {

                if (streamResource == null)
                    return;

                Resource resource = _mapper.Map<Resource>(streamResource);
                Resource? foundResource = _totalviewCommunicationService.ResourceCollection.Items?.FirstOrDefault(r => r.RecId == resource.RecId);

                switch (streamResource.MessageType)
                {
                    case T5MessageTypeEnum.Create:
                        Add(resource, foundResource);
                        break;
                    case T5MessageTypeEnum.Update:
                        Update(resource, foundResource);
                        break;
                    case T5MessageTypeEnum.Delete:
                        Delete(foundResource);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not handle packet");
            }
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void Add(Resource item, Resource? foundItem)
        {
            if (foundItem != null)
            {
                _totalviewCommunicationService.ResourceCollection.Items?.Remove(foundItem);
            }

            _totalviewCommunicationService.ResourceCollection.Items?.Add(item);

            // Send event to all clients
            _totalviewSubscriptionsHandler.SendToAll(new TotalviewEvent()
            {
                Verb = Verb.Post,
                ResourceCollection = new ResourceCollection()
                {
                    Items =
                    {
                        item
                    }
                }
            });
        }

        private void Update(Resource item, Resource? foundItem)
        {
            if (foundItem != null)
            {
                _totalviewCommunicationService.ResourceCollection.Items?.Remove(foundItem);
            }

            _totalviewCommunicationService.ResourceCollection.Items?.Add(item);

            // Send event to all clients
            _totalviewSubscriptionsHandler.SendToAll(new TotalviewEvent()
            {
                Verb = Verb.Put,
                ResourceCollection = new ResourceCollection()
                {
                    Items =
                    {
                        item
                    }
                }
            });
        }

        private void Delete(Resource? foundItem)
        {
            if (foundItem != null)
            {
                _totalviewCommunicationService.ResourceCollection.Items?.Remove(foundItem);

                // Send event to all clients
                _totalviewSubscriptionsHandler.SendToAll(new TotalviewEvent()
                {
                    Verb = Verb.Delete,
                    ResourceCollection = new ResourceCollection()
                    {
                        Items =
                        {
                            foundItem
                        }
                    }
                });
            }
        }
    }
}
