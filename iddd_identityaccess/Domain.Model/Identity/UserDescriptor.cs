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
    using Common.Domain.Model;

    public class UserDescriptor : ValueObject
    {
        public static UserDescriptor NullDescriptorInstance()
        {
            return new UserDescriptor();
        }

        public UserDescriptor(TenantId tenantId, string username, string emailAddress)
        {
            EmailAddress = emailAddress;
            TenantId = tenantId;
            Username = username;
        }

        private UserDescriptor() { }

        public string EmailAddress { get; }

        public TenantId TenantId { get; }

        public string Username { get; }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
            yield return TenantId;
            yield return Username;
        }

        public override string ToString()
        {
            return "UserDescriptor [emailAddress=" + EmailAddress
                    + ", tenantId=" + TenantId + ", username=" + Username + "]";
        }
    }
}
