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

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.Collaboration.Domain.Model.Collaborators
{
    using System;

    public abstract class Collaborator : ValueObject, IComparable<Collaborator>
    {
        protected Collaborator() { }

        protected Collaborator(string identity, string name, string emailAddress)
        {
            Identity = identity;
            Name = name;
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; }
        public string Identity { get; }
        public string Name { get; }

        public int CompareTo(Collaborator collaborator)
        {
            var diff = string.Compare(Identity, collaborator.Identity, StringComparison.Ordinal);
            if (diff == 0)
            {
                diff = string.Compare(EmailAddress, collaborator.EmailAddress, StringComparison.Ordinal);
                if (diff == 0)
                {
                    diff = string.Compare(Name, collaborator.Name, StringComparison.Ordinal);
                }
            }
            return diff;
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
            yield return Identity;
            yield return Name;
        }

        public override string ToString()
        {
            return GetType().Name +
               " [emailAddress=" + EmailAddress + ", identity=" + Identity + ", Name=" + Name + "]";
        }
    }
}
