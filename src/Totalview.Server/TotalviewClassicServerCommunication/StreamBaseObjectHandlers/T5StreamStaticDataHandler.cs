using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using Totalview.Server.Services;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamStaticDataHandler : StreamBaseObjectHandlerBase<T5StreamStaticData>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<T5StreamStaticDataHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITotalviewDataService _totalviewCommunicationService;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public T5StreamStaticDataHandler(
            ILogger<T5StreamStaticDataHandler> logger,
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
        protected override void HandlePacket(T5StreamStaticData streamStaticData)
        {
            _logger.LogDebug($"Handeling packet of type {streamStaticData.GetType()}");

            try
            {
                StaticData staticData = _mapper.Map<StaticData>(streamStaticData);

                _totalviewCommunicationService.LargeDeviceList.Items.AddRange(staticData.LargeDeviceList.Items);
                _totalviewCommunicationService.LyncStateCollection.Items.AddRange(staticData.LyncStateList.Items);
                _totalviewCommunicationService.ResourceCollection.Items.AddRange(staticData.ResourceList.Items);
                _totalviewCommunicationService.SmallDeviceList.Items.AddRange(staticData.SmallDeviceList.Items);
                _totalviewCommunicationService.ReservationCollection.Items.AddRange(staticData.StateCurrentList.Items);
                _totalviewCommunicationService.StateCollection.Items.AddRange(staticData.StateList.Items);
                _totalviewCommunicationService.TemplateList.Items.AddRange(staticData.TemplateList.Items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not handle packet");
            }
        }
    }
}
