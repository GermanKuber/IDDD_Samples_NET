﻿using System;
using System.Collections.Generic;
using SaaSOvation.Common.Domain.Model;
using SaaSOvation.AgilePM.Domain.Model.Discussions;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class BacklogItemDiscussion : ValueObject
    {
        public static BacklogItemDiscussion FromAvailability(DiscussionAvailability availability)
        {
            if (availability == DiscussionAvailability.Ready)
                throw new ArgumentException("Cannot be created ready.");

            return new BacklogItemDiscussion(
                new DiscussionDescriptor(DiscussionDescriptor.UndefinedId),
                availability);
        }

        public BacklogItemDiscussion(DiscussionDescriptor descriptor, DiscussionAvailability availability)
        {
            Descriptor = descriptor;
            Availability = availability;
        }        

        public DiscussionDescriptor Descriptor { get; private set; }

        public DiscussionAvailability Availability { get; private set; }

        public BacklogItemDiscussion NowReady(DiscussionDescriptor descriptor)
        {
            if (descriptor == null || descriptor.IsUndefined)
                throw new InvalidOperationException("The discussion descriptor must be defined.");

            if (Availability != DiscussionAvailability.Requested)
                throw new InvalidOperationException("The discussion must be requested first.");

            return new BacklogItemDiscussion(descriptor, DiscussionAvailability.Ready);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Availability;
            yield return Descriptor;
        }
    }
}
