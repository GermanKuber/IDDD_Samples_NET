﻿using System;
using System.Collections.Generic;
using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems.Task
{
    public class TaskId : ValueObject
    {
        public TaskId()
            : this(Guid.NewGuid().ToString().ToUpper().Substring(0, 8))
        {
        }

        public TaskId(string id)
        {
            AssertionConcern.AssertArgumentNotEmpty(id, "The id must be provided.");
            AssertionConcern.AssertArgumentLength(id, 8, "The id must be 8 characters or less.");
            Id = id;
        }

        public string Id { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
