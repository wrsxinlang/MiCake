using BaseMiCakeApplication.Domain.Events.IdeaEvents;
using MiCake.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.EventHandlers.IdeaHandlers
{
    public class AddIdeaHandler : IDomainEventHandler<AddIdenEvent>
    {
        public Task HandleAysnc(AddIdenEvent domainEvent, CancellationToken cancellationToken = default)
        {
            //TODO:消息通知审核人审核
            throw new NotImplementedException();
        }
    }

    public class UpdateIdeaHandler : IDomainEventHandler<UpdateIdenEvent>
    {
        public Task HandleAysnc(UpdateIdenEvent domainEvent, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
