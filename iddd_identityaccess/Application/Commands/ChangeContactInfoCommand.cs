﻿namespace SaaSOvation.IdentityAccess.Application.Commands
{
    public class ChangeContactInfoCommand
    {
        public ChangeContactInfoCommand()
        {
        }

        public ChangeContactInfoCommand(string tenantId, string username, string emailAddress,
            string primaryTelephone, string secondaryTelephone, string addressStreetAddress,
            string addressCity, string addressStateProvince, string addressPostalCode, 
            string addressCountryCode)
        {
            TenantId = tenantId;
            Username = username;
            EmailAddress = emailAddress;
            PrimaryTelephone = primaryTelephone;
            SecondaryTelephone = secondaryTelephone;
            AddressStreetAddress = addressStreetAddress;
            AddressCity = addressCity;
            AddressStateProvince = addressStateProvince;
            AddressPostalCode = addressPostalCode;
            AddressCountryCode = addressCountryCode;
        }

        public string TenantId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string PrimaryTelephone { get; set; }
        public string SecondaryTelephone { get; set; }
        public string AddressStreetAddress { get; set; }
        public string AddressCity { get; set; }
        public string AddressStateProvince { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCountryCode { get; set; }
    }
}
