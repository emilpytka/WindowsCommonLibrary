using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WindowsCommonLibrary.PCL.Interfaces;
using WindowsCommonLibrary.PCL.Model;

namespace WindowsCommonLibrary.WindowsStore.Common
{
    /// <summary>
    /// Windows Store implementation of IContractService
    /// </summary>
    public class ContactService : IContactService
    {
        /// <summary>
        /// Run Contact Picker to allow user pick one contact from others applications
        /// </summary>
        /// <returns></returns>
        public async Task<Contact> PickSingleContactAsync()
        {
            var contactPicker = new Windows.ApplicationModel.Contacts.ContactPicker();
            var contact = await contactPicker.PickContactAsync();

            return await ConvertContact(contact);
        }

        /// <summary>
        /// Run Contact Picker to allow user pick many contacts from other ContactPicker applications.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Contact>> PickMultipleContactsAsync()
        {
            var contactPicker = new Windows.ApplicationModel.Contacts.ContactPicker();
            var contacts = await contactPicker.PickMultipleContactsAsync();
            var contactsCount = contacts.Count;
            var result = new Contact[contactsCount];
            for (int i = 0; i < contactsCount; i++)
            {
                result[i] = await ConvertContactInfo(contacts[i]);
            }
            return result;
        }

        #region Multiple contact conversion

        private async Task<Contact> ConvertContactInfo(Windows.ApplicationModel.Contacts.ContactInformation contactInformation)
        {
            var result = new Contact();
            result.Name = contactInformation.Name;
            result.Mails = ConvertMails(contactInformation.Emails);
            result.PhoneNumbers = ConvertPhoneNumbers(contactInformation.PhoneNumbers);
            result.Thumbnail = ConvertThumbnail(await contactInformation.GetThumbnailAsync());
            return result;
        }

        private byte[] ConvertThumbnail(Windows.Storage.Streams.IRandomAccessStreamWithContentType stream)
        {
            byte[] result = null;
            using (var bReader = new BinaryReader(stream.AsStream()))
            {
                result = bReader.ReadBytes((int)stream.Size);
            }
            return result;
        }

        private IEnumerable<ContactMail> ConvertMails(IReadOnlyList<Windows.ApplicationModel.Contacts.ContactField> readOnlyList)
        {
            return readOnlyList.Select(e => new ContactMail { Type = e.Type.ToString(), MailAddress = e.Value });
        }

        private IEnumerable<ContactPhoneNumber> ConvertPhoneNumbers(IReadOnlyList<Windows.ApplicationModel.Contacts.ContactField> readOnlyList)
        {
            return readOnlyList.Select(e => new ContactPhoneNumber { Number = e.Value, Type = e.Type.ToString() });
        }
        
        #endregion

        #region Contacts conversions

        private async Task<PCL.Model.Contact> ConvertContact(Windows.ApplicationModel.Contacts.Contact contact)
        {
            var result = new PCL.Model.Contact();
            result.FirstName = contact.FirstName;
            result.MiddleName = contact.MiddleName;
            result.LastName = contact.LastName;
            result.Name = contact.Name;
            result.HonorificNamePrefix = contact.HonorificNamePrefix;
            result.HonorificNameSuffix = contact.HonorificNameSuffix;
            result.Thumbnail = await ConvertThumbnail(contact.Thumbnail);
            result.Addresses = ConvertAddress(contact.Addresses);
            result.PhoneNumbers = ConvertPhoneNumbers(contact.Phones);
            result.Mails = ConvertMails(contact.Emails);
            result.Websites = ConvertWebsites(contact.Websites);
            return result;
        }

        private IEnumerable<ContactWebsite> ConvertWebsites(IList<Windows.ApplicationModel.Contacts.ContactWebsite> list)
        {
            return list.Select(e => new ContactWebsite { Type = e.Description, Url = e.Uri.AbsoluteUri });
        }

        private IEnumerable<ContactMail> ConvertMails(IList<Windows.ApplicationModel.Contacts.ContactEmail> list)
        {
            return list.Select(e => new ContactMail { Type = e.Kind.ToString(), MailAddress = e.Address});
        }

        private IEnumerable<ContactPhoneNumber> ConvertPhoneNumbers(IList<Windows.ApplicationModel.Contacts.ContactPhone> list)
        {
            return list.Select(e => new ContactPhoneNumber { Type = e.Kind.ToString(), Number = e.Number });
        }

        private IEnumerable<PCL.Model.ContactAddress> ConvertAddress(IList<Windows.ApplicationModel.Contacts.ContactAddress> list)
        {

            return list.Select(e => new ContactAddress
            {
                Type = e.Kind.ToString(),
                Address = string.Format("{0} {1}\n{2} {3}\n {4}", e.StreetAddress, e.Locality, e.PostalCode, e.Region, e.Country)
            });
        }

        private async Task<byte[]> ConvertThumbnail(Windows.Storage.Streams.IRandomAccessStreamReference stream)
        {
            var orStream = await stream.OpenReadAsync();
            byte[] result = null;
            using (var bReader = new BinaryReader(orStream.AsStream()))
            {
                result = bReader.ReadBytes((int)orStream.Size);
            }
            return result;
        }

        #endregion
        
    }
}
