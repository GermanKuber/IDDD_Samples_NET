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
	using System.Collections.Generic;

	using Common.Domain.Model;

	[CLSCompliant(true)]
	public class User : EntityWithCompositeId
	{
		#region [ Fields and Constructor Overloads ]

		private Enablement _userEnablement;

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class
		/// and publishes a <see cref="UserRegistered"/> event.
		/// </summary>
		/// <param name="tenantId">
		/// Initial value of the <see cref="TenantId"/> property.
		/// </param>
		/// <param name="username">
		/// Initial value of the <see cref="Username"/> property.
		/// </param>
		/// <param name="password">
		/// Initial value of the <see cref="Password"/> property.
		/// </param>
		/// <param name="enablement">
		/// Initial value of the <see cref="Enablement"/> property.
		/// </param>
		/// <param name="person">
		/// Initial value of the <see cref="Person"/> property.
		/// </param>
		public User(
			TenantId tenantId,
			string username,
			string password,
			Enablement enablement,
			Person person)
		{
			AssertionConcern.AssertArgumentNotNull(tenantId, "The tenantId is required.");
			AssertionConcern.AssertArgumentNotNull(person, "The person is required.");
			AssertionConcern.AssertArgumentNotEmpty(username, "The username is required.");
			AssertionConcern.AssertArgumentLength(username, 3, 250, "The username must be 3 to 250 characters.");

			// Defer validation to the property setters.
			Enablement = enablement;
			Person = person;
			TenantId = tenantId;
			Username = username;

			ProtectPassword(string.Empty, password);

			person.User = this;

			DomainEventPublisher
				.Instance
				.Publish(new UserRegistered(
						tenantId,
						username,
						person.Name,
						person.ContactInformation.EmailAddress));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class for a derived type,
		/// and otherwise blocks new instances from being created with an empty constructor.
		/// </summary>
		protected User()
		{
		}

		#endregion

		#region [ Public Properties ]

		public TenantId TenantId { get; private set; }

		public bool IsEnabled
		{
			get { return Enablement.IsEnablementEnabled(); }
		}

		public Enablement Enablement
		{
			get
			{
				return _userEnablement;
			}

			private set
			{
				AssertionConcern.AssertArgumentNotNull(value, "The enablement is required.");

				_userEnablement = value;
			}
		}

		public string Password { get; private set; }

		public Person Person { get; private set; }

		public UserDescriptor UserDescriptor
		{
			get
			{
				return new UserDescriptor(
					TenantId,
					Username,
					Person.EmailAddress.Address);
			}
		}

		public string Username { get; private set; }

		#endregion

		#region [ Command Methods which Publish Domain Events ]

		public void ChangePassword(string currentPassword, string changedPassword)
		{
			AssertionConcern.AssertArgumentNotEmpty(
				currentPassword, "Current and new password must be provided.");

			AssertionConcern.AssertArgumentEquals(
				Password, AsEncryptedValue(currentPassword), "Current password not confirmed.");

			ProtectPassword(currentPassword, changedPassword);

			DomainEventPublisher
				.Instance
				.Publish(new UserPasswordChanged(
						TenantId,
						Username));
		}

		public void ChangePersonalContactInformation(ContactInformation contactInformation)
		{
			Person.ChangeContactInformation(contactInformation);
		}

		public void ChangePersonalName(FullName personalName)
		{
			Person.ChangeName(personalName);
		}

		public void DefineEnablement(Enablement enablement)
		{
			Enablement = enablement;

			DomainEventPublisher
				.Instance
				.Publish(new UserEnablementChanged(
						TenantId,
						Username,
						Enablement));
		}

		#endregion

		#region [ Additional Methods ]

		/// <summary>
		/// Returns a string that represents the current entity.
		/// </summary>
		/// <returns>
		/// A unique string representation of an instance of this entity.
		/// </returns>
		public override string ToString()
		{
			const string format = "User [tenantId={0}, username={1}, person={2}, enablement={3}]";
			return string.Format(format, TenantId, Username, Person, Enablement);
		}

		/// <summary>
		/// Creates a <see cref="GroupMember"/> value of
		/// type <see cref="GroupMemberType.User"/>
		/// based on this <see cref="User"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="GroupMember"/> value of type
		/// <see cref="GroupMemberType.User"/>
		/// based on this <see cref="User"/>.
		/// </returns>
		internal GroupMember ToGroupMember()
		{
			return new GroupMember(TenantId, Username, GroupMemberType.User);
		}

		/// <summary>
		/// Gets the values which identify a <see cref="User"/> entity,
		/// which are the <see cref="TenantId"/> and the unique <see cref="Username"/>.
		/// </summary>
		/// <returns>
		/// A sequence of values which uniquely identifies an instance of this entity.
		/// </returns>
		protected override IEnumerable<object> GetIdentityComponents()
		{
			yield return TenantId;
			yield return Username;
		}

		private static string AsEncryptedValue(string plainTextPassword)
		{
			return DomainRegistry.EncryptionService.EncryptedValue(plainTextPassword);
		}

		private void ProtectPassword(string currentPassword, string changedPassword)
		{
			AssertionConcern.AssertArgumentNotEquals(currentPassword, changedPassword, "The password is unchanged.");
			AssertionConcern.AssertArgumentFalse(DomainRegistry.PasswordService.IsWeak(changedPassword), "The password must be stronger.");
			AssertionConcern.AssertArgumentNotEquals(Username, changedPassword, "The username and password must not be the same.");

			Password = AsEncryptedValue(changedPassword);
		}

		#endregion
	}
}