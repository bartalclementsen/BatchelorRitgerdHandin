using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totalview.Mediators;
using Totalview.Server.Notifications;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.Services
{
    internal interface ITotalviewDataService
    {
        LargeDeviceList LargeDeviceList { get; }

        LyncStateCollection LyncStateCollection { get; }

        ResourceCollection ResourceCollection { get; }

        SmallDeviceList SmallDeviceList { get; }

        StateCollection StateCollection { get; }

        TemplateList TemplateList { get; }

        ReservationCollection ReservationCollection { get; set; }

        ReservationUserList ReservationUserList { get; set; }
    }

    internal class TotalviewDataService : ITotalviewDataService
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public DeviceTransferedConnectionCollection DeviceTransferedConnectionCollection { get; set; } = new DeviceTransferedConnectionCollection();

        public DeviceConnectionCollection DeviceConnectionCollection { get; set; } = new DeviceConnectionCollection();

        public ResourceDetailCollection ResourceDetailCollection { get; set; } = new ResourceDetailCollection();

        public ResourceCollection ResourceCollection { get; set; } = new ResourceCollection();

        public CallerHistoryCollection CallerHistoryCollection { get; set; } = new CallerHistoryCollection();

        public ReservationDetailCollection ReservationDetailCollection { get; set; } = new ReservationDetailCollection();

        public ReservationCollection ReservationCollection { get; set; } = new ReservationCollection();

        public LyncStateCollection LyncStateCollection { get; set; } = new LyncStateCollection();

        public StateCollection StateCollection { get; set; } = new StateCollection();

        public StateDetailCollection StateDetailCollection { get; set; } = new StateDetailCollection();

        public SmallDeviceList SmallDeviceList { get; set; } = new SmallDeviceList();

        public LargeDeviceList LargeDeviceList { get; set; } = new LargeDeviceList();

        public TemplateList TemplateList { get; set; } = new TemplateList();

        public KeyValueList KeyValueList { get; set; } = new KeyValueList();

        public ReservationUserList ReservationUserList { get; set; } = new ReservationUserList();

        public UserImageList UserImageList { get; set; } = new UserImageList();

        public NumberLogList NumberLogList { get; set; } = new NumberLogList();

        public UserAttachmentsDataLastUsedCollection UserAttachmentsDataLastUsedCollection { get; set; } = new UserAttachmentsDataLastUsedCollection();

        private readonly ILogger<TotalviewDataService> _logger;
        private readonly IMediator _mediator;

#pragma warning disable IDE0052 // Remove unread private members
        // Store the reference, to keep the subscription active.
        // When the token is disposed the subscription is unsubscribed
        private readonly ISubscription _subscriptionAddedMessageToken; 
#pragma warning restore IDE0052 // Remove unread private members

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewDataService(ILogger<TotalviewDataService> logger, IMediator mediator, ITotalviewSubscriptionsHandler totalviewSubscriptionsHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _subscriptionAddedMessageToken = _mediator.Subscribe<SubscriptionAddedNotification>(HandleSubscriptionAddedAsync);
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private Task HandleSubscriptionAddedAsync(SubscriptionAddedNotification message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle SubscriptionAddedMessage. Sending static data.");

            try
            {
                message.TotalviewSubscription.SendTotalviewEvent(new TotalviewEvent()
                {
                    Verb = Verb.Post,
                    LargeDeviceList = LargeDeviceList,
                    LyncStateCollection = LyncStateCollection,
                    ResourceCollection = ResourceCollection,
                    SmallDeviceList = SmallDeviceList,
                    StateCollection = StateCollection,
                    TemplateList = TemplateList,
                    ReservationUserList = ReservationUserList,
                    ReservationCollection = ReservationCollection
                });

                _logger.LogDebug("Static data sent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not send initial static data");
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
