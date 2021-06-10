using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using System.Linq;
using Totalview.Mediators;
using Totalview.Server.Notifications;
using Totalview.Server.Services;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamReservationHandler : StreamBaseObjectHandlerBase<T5StreamReservation>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<T5StreamReservationUserHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITotalviewDataService _totalviewDataService;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public T5StreamReservationHandler(
            ILogger<T5StreamReservationUserHandler> logger,
            IMapper mapper,
            IMediator mediator,
            ITotalviewDataService totalviewDataService,
            ITotalviewSubscriptionsHandler totalviewSubscriptionsHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _totalviewDataService = totalviewDataService ?? throw new ArgumentNullException(nameof(totalviewDataService));
            _totalviewSubscriptionsHandler = totalviewSubscriptionsHandler ?? throw new ArgumentNullException(nameof(totalviewSubscriptionsHandler));
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override void HandlePacket(T5StreamReservation streamReservation)
        {
            _logger.LogDebug($"Handeling packet of type {streamReservation.GetType()}");

            try
            {
                Verb verb = streamReservation.MessageType switch
                {
                    T5MessageTypeEnum.Delete => Verb.Delete,
                    T5MessageTypeEnum.Create => Verb.Post,
                    T5MessageTypeEnum.Update => Verb.Put,
                    _ => Verb.Unknown
                };

                Reservation? reservation = _mapper.Map<Reservation>(streamReservation);
                Reservation? foundReservation = _totalviewDataService.ReservationCollection.Items?.FirstOrDefault(r => r.Id == reservation.Id);
                if (foundReservation != null)
                {
                    _totalviewDataService.ReservationCollection.Items?.Remove(foundReservation);
                }

                if (streamReservation.MessageType != T5MessageTypeEnum.Delete)
                {
                    _totalviewDataService.ReservationCollection.Items?.Add(reservation);
                }

                _totalviewSubscriptionsHandler.SendToAll(new TotalviewEvent()
                {
                    Verb = verb,
                    ReservationCollection = new()
                    {
                        Items =
                        {
                            reservation
                        }
                    }
                });

                _mediator.SendNotificationAsync(new StreamReservationRecievedNotification(streamReservation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not handle packet");
            }
        }
    }
}
