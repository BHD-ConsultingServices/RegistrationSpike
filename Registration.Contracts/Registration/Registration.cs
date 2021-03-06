﻿
namespace Registration.Contracts.Registration
{
    using System;

    public class Registration
    {
        public Guid Id { get; set; }

        public string IdentityNumber { get; set; }

        public string Name { get; set; }

        public DateTime BirthDay { get; set; }

        public bool Subscribed { get; set; }

        public string Email { get; set; }

        public Status RegistrationStatus { get; set; }
    }
}
