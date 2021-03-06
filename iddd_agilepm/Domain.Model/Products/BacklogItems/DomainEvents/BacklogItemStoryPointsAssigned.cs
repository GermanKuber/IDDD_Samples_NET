using System;
using SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems.BacklogItem;
using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems.DomainEvents
{
    public class BacklogItemStoryPointsAssigned : IDomainEvent
    {
        public BacklogItemStoryPointsAssigned(Tenants.TenantId tenantId, BacklogItemId backlogItemId, StoryPoints storyPoints)
        {
            TenantId = tenantId;
            EventVersion = 1;
            OccurredOn = DateTime.UtcNow;

            BacklogItemId = backlogItemId;
            StoryPoints = storyPoints;
        }

        public Tenants.TenantId TenantId { get; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }

        public BacklogItemId BacklogItemId { get; }
        public StoryPoints StoryPoints { get; }
    }
}
