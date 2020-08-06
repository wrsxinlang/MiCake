using MiCake.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Events.IdeaEvents
{
    public class AddIdenEvent : DomainEvent
    {
        public Guid NewIdeaID { get; set; }

        public AddIdenEvent(Guid Id)
        {
            NewIdeaID = Id;
        }
    }

    public class UpdateIdenEvent : DomainEvent
    {
        public Guid NewIdeaID { get; set; }

        public UpdateIdenEvent(Guid Id)
        {
            NewIdeaID = Id;
        }
    }
}
