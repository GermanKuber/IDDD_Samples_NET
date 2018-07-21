﻿using System;
using System.Collections.Generic;
using SaaSOvation.Common.Domain.Model;
using SaaSOvation.Collaboration.Domain.Model.Tenants;
using SaaSOvation.Collaboration.Domain.Model.Collaborators;

namespace SaaSOvation.Collaboration.Domain.Model.Calendars
{
    public class CalendarEntryScheduled : IDomainEvent
    {
        public CalendarEntryScheduled(
            Tenant tenant, 
            CalendarId calendarId, 
            CalendarEntryId calendarEntryId, 
            string description, 
            string location, 
            Owner owner, 
            DateRange timeSpan, 
            Repetition repetition,
            Alarm alarm,
            IEnumerable<Participant> invitees)
        {
            Tenant = tenant;
            CalendarId = calendarId;
            CalendarEntryId = calendarEntryId;
            Description = description;
            Location = location;
            Owner = owner;
            TimeSpan = timeSpan;
            Repetition = repetition;
            Alarm = alarm;
            Invitees = invitees;
        }

        public Tenant Tenant { get; private set; }
        public CalendarId CalendarId { get; private set; }
        public CalendarEntryId CalendarEntryId { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public Owner Owner { get; private set; }
        public DateRange TimeSpan { get; private set; }
        public Repetition Repetition { get; private set; }
        public Alarm Alarm { get; private set; }
        public IEnumerable<Participant> Invitees { get; private set; }


        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
