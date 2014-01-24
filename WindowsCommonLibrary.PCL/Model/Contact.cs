using System;
using System.Collections.Generic;

namespace WindowsCommonLibrary.PCL.Model
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public string HonorificNamePrefix { get; set; }
        public string HonorificNameSuffix { get; set; }
        public byte[] Thumbnail { get; set; }
        public IEnumerable<ContactAddress> Addresses { get; set; }
        public IEnumerable<ContactPhoneNumber> PhoneNumbers { get; set; }
        public IEnumerable<ContactMail> Mails { get; set; }
        public IEnumerable<ContactWebsite> Websites { get; set; }
    }

    public class ContactPhoneNumber
    {
        public string Type { get; set; }
        public string Number { get; set; }
    }

    public class ContactMail
    {
        public string Type { get; set; }
        public string MailAddress { get; set; }
    }

    public class ContactAddress
    {
        public string Type { get; set; }
        public string Address { get; set; }
    }

    public class ContactWebsite
    {
        public string Type { get; set; }
        public string Url{ get; set; }
    }
}
