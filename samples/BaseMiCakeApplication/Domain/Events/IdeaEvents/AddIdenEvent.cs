using MiCake.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Events.IdeaEvents
{
    public class AddIdenEvent : DomainEvent
    {
        public Guid ItineraryID { get; set; }

        public AddIdenEvent(Guid Id)
        {
            ItineraryID = Id;
        }
    }
}
