using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using System.Linq;
using Totalview.Server.Services;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal class T5StreamReservationUserHandler : StreamBaseObjectHandlerBase<T5StreamReservationUser>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<T5StreamReservationUserHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITotalviewDataService _totalviewCommunicationService;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public T5StreamReservationUserHandler(
            ILogger<T5StreamReservationUserHandler> logger,
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
        protected override void HandlePacket(T5StreamReservationUser streamReservationUser)
        {
            _logger.LogDebug($"Handeling packet of type {streamReservationUser.GetType()}");

            try
            {
                ReservationUser reservationUser = _mapper.Map<ReservationUser>(streamReservationUser);

                ReservationUser? foundReservationUser = _totalviewCommunicationService.ReservationUserList.Items.FirstOrDefault(ru => ru.RecordId == reservationUser.RecordId);
                if (foundReservationUser != null)
                {
                    _totalviewCommunicationService.ReservationUserList.Items.Remove(foundReservationUser);
                }

                if (streamReservationUser.MessageType != T5MessageTypeEnum.Delete)
                {
                    _totalviewCommunicationService.ReservationUserList.Items.Add(reservationUser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not handle packet");
            }
        }
    }
}
