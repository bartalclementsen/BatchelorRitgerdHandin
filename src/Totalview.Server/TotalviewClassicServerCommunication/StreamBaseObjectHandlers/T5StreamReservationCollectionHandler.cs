using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using System.Linq;
using Totalview.Server.Services;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamReservationCollectionHandler : StreamBaseObjectHandlerBase<T5StreamReservationCollection>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<T5StreamReservationCollectionHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITotalviewDataService _totalviewDataService;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public T5StreamReservationCollectionHandler(
            ILogger<T5StreamReservationCollectionHandler> logger,
            IMapper mapper,
            ITotalviewDataService totalviewDataService,
            ITotalviewSubscriptionsHandler totalviewSubscriptionsHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _totalviewDataService = totalviewDataService ?? throw new ArgumentNullException(nameof(totalviewDataService));
            _totalviewSubscriptionsHandler = totalviewSubscriptionsHandler ?? throw new ArgumentNullException(nameof(totalviewSubscriptionsHandler));
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override void HandlePacket(T5StreamReservationCollection streamReservation)
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

                var reservationCollection = _mapper.Map<ReservationCollection>(streamReservation);

                foreach (Reservation reservation in reservationCollection.Items)
                {
                    Reservation? foundReservation = _totalviewDataService.ReservationCollection.Items?.FirstOrDefault(r => r.Id == reservation.Id);
                    if (foundReservation != null)
                    {
                        _totalviewDataService.ReservationCollection.Items?.Remove(foundReservation);
                    }

                    if (streamReservation.MessageType != T5MessageTypeEnum.Delete)
                    {
                        _totalviewDataService.ReservationCollection.Items?.Add(reservation);
                    }
                }

                _totalviewSubscriptionsHandler.SendToAll(new TotalviewEvent()
                {
                    Verb = verb,
                    ReservationCollection = reservationCollection
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not handle packet");
            }
        }
    }
}
