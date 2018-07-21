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

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using Common.Domain.Model;

    public class TenantAdministratorRegistered : IDomainEvent
    {
        public TenantAdministratorRegistered(
            TenantId tenantId,
            string name,
            FullName administorName,
            EmailAddress emailAddress,
            string username,
            string temporaryPassword)
        {
            AdministorName = administorName;
            EventVersion = 1;
            Name = name;
            OccurredOn = DateTime.Now;
            TemporaryPassword = temporaryPassword;
            TenantId = tenantId.Id;
        }

        public FullName AdministorName { get; private set; }

        public int EventVersion { get; set; }

        public string Name { get; private set; }

        public DateTime OccurredOn { get; set; }

        public string TemporaryPassword { get; private set; }

        public string TenantId { get; private set; }
    }
}
