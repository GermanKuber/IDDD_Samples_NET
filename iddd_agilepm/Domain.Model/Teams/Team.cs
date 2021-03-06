﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SaaSOvation.AgilePM.Domain.Model.Tenants;
using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Teams
{
    public class Team : Entity
    {
        public Team(TenantId tenantId, string name, ProductOwner productOwner = null)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "The tenantId must be provided.");

            this._tenantId = tenantId;
            Name = name;
            if (productOwner != null)
                ProductOwner = productOwner;
            _teamMembers = new HashSet<TeamMember>();
        }

        private readonly TenantId _tenantId;
        private string _name;
        private ProductOwner _productOwner;
        private readonly HashSet<TeamMember> _teamMembers;

        public TenantId TenantId
        {
            get { return _tenantId; }
        }

        public string Name
        {
            get { return _name; }
            private set
            {
                AssertionConcern.AssertArgumentNotEmpty(value, "The name must be provided.");
                AssertionConcern.AssertArgumentLength(value, 100, "The name must be 100 characters or less.");
                _name = value;
            }
        }

        public ProductOwner ProductOwner
        {
            get { return _productOwner; }
            private set
            {
                AssertionConcern.AssertArgumentNotNull(value, "The productOwner must be provided.");
                AssertionConcern.AssertArgumentEquals(_tenantId, value.TenantId, "The productOwner must be of the same tenant.");
                _productOwner = value;
            }
        }

        public ReadOnlyCollection<TeamMember> AllTeamMembers
        {
            get { return new ReadOnlyCollection<TeamMember>(_teamMembers.ToArray()); }
        }

        public void AssignProductOwner(ProductOwner productOwner)
        {
            ProductOwner = productOwner;
        }

        public void AssignTeamMember(TeamMember teamMember)
        {
            AssertValidTeamMember(teamMember);
            _teamMembers.Add(teamMember);
        }

        public bool IsTeamMember(TeamMember teamMember)
        {
            AssertValidTeamMember(teamMember);
            return GetTeamMemberByUserName(teamMember.Username) != null;
        }

        public void RemoveTeamMember(TeamMember teamMember)
        {
            AssertValidTeamMember(teamMember);
            var existingTeamMember = GetTeamMemberByUserName(teamMember.Username);
            if (existingTeamMember != null)
            {
                _teamMembers.Remove(existingTeamMember);
            }
        }

        private void AssertValidTeamMember(TeamMember teamMember)
        {
            AssertionConcern.AssertArgumentNotNull(teamMember, "A team member must be provided.");
            AssertionConcern.AssertArgumentEquals(TenantId, teamMember.TenantId, "Team member must be of the same tenant.");
        }

        private TeamMember GetTeamMemberByUserName(string userName)
        {
            return _teamMembers.FirstOrDefault(x => x.Username.Equals(userName));
        }
    }
}
