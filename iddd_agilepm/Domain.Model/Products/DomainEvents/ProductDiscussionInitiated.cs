﻿// Copyright 2012,2013 Vaughn Vernon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using SaaSOvation.AgilePM.Domain.Model.Tenants;
using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.DomainEvents
{
    public class ProductDiscussionInitiated : IDomainEvent
    {
        public ProductDiscussionInitiated(TenantId tenantId, ProductId productId, ProductDiscussion productDiscussion)
        {
            EventVersion = 1;
            OccurredOn = DateTime.Now;
            ProductDiscussion = productDiscussion;
            ProductId = productId;
            TenantId = tenantId;
        }

        public int EventVersion { get; set; }

        public DateTime OccurredOn { get; set; }

        public ProductDiscussion ProductDiscussion { get; }

        public ProductId ProductId { get; }

        public TenantId TenantId { get; }
    }
}
