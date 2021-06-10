using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using Totalview.Mediators;
using Totalview.Server.Notifications;
using Totalview.Server.Services;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamReservationUserListHandler : StreamBaseObjectHandlerBase<T5StreamReservationUserList>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<T5StreamReservationUserListHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITotalviewDataService _totalviewCommunicationService;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;
        private readonly IMediator _mediator;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public T5StreamReservationUserListHandler(
            ILogger<T5StreamReservationUserListHandler> logger,
            IMapper mapper,
            ITotalviewDataService totalviewCommunicationService,
            ITotalviewSubscriptionsHandler totalviewSubscriptionsHandler,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _totalviewCommunicationService = totalviewCommunicationService ?? throw new ArgumentNullException(nameof(totalviewCommunicationService));
            _totalviewSubscriptionsHandler = totalviewSubscriptionsHandler ?? throw new ArgumentNullException(nameof(totalviewSubscriptionsHandler));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override void HandlePacket(T5StreamReservationUserList streamReservationUserList)
        {
            _logger.LogDebug($"Handeling packet of type {streamReservationUserList.GetType()}");

            try
            {
                ReservationUserList reservationUserList = _mapper.Map<ReservationUserList>(streamReservationUserList) ?? new ReservationUserList();
                switch (streamReservationUserList.MessageType)
                {
                    case T5MessageTypeEnum.Create:
                        break;
                    case T5MessageTypeEnum.Update:
                        break;
                    case T5MessageTypeEnum.Delete:
                        break;
                }

                _totalviewCommunicationService.ReservationUserList = _mapper.Map<ReservationUserList>(streamReservationUserList) ?? new ReservationUserList();

                // Send Totalview Event to all subscribers
                _totalviewSubscriptionsHandler.SendToAll(new TotalviewEvent()
                {
                    ReservationUserList = reservationUserList
                });

                // Send notification internally over the mediator
                _mediator.SendNotificationAsync(new StreamReservationUserListRecievedNotification(reservationUserList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not handle packet");
            }
        }
    }
}
