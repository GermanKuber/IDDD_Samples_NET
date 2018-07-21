﻿using System;
using System.Collections.Generic;
using SaaSOvation.AgilePM.Domain.Model.Tenants;

namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    public class TeamMember : Member
    {
        public TeamMember(TenantId tenantId, string userName, string firstName, string lastName, string emailAddress, DateTime initializedOn)
            : base(tenantId, userName, firstName, lastName, emailAddress, initializedOn)
        {
        }

        public TeamMemberId TeamMemberId
        {
            get 
            {
                // TODO: consider length restrictions on TeamMemberId.Id
                return new TeamMemberId(TenantId, Username); 
            }
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return TenantId;
            yield return Username;
        }
    }
}
