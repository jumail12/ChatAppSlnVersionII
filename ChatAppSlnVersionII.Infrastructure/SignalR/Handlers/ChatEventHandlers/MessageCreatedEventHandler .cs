using ChatAppSlnVersionII.Application.Features.Chat.Events;
using ChatAppSlnVersionII.Infrastructure.SignalR.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Infrastructure.SignalR.Handlers.ChatEventHandlers
{
    public class MessageCreatedEventHandler : INotificationHandler<MessageCreatedEvent>
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageCreatedEventHandler(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients
             .Group(notification.cm_room_id)
             .SendAsync("ReceiveMessage", notification);
        }
    }
}
