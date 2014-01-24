using Microsoft.Phone.UserData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsCommonLibrary.PCL.Interfaces;

namespace WindowsCommonLibrary.WindowsPhone.Common
{
    public class ContactService : IContactService
    {
        public string SearchPhrase { get; set; }

        public Task<PCL.Model.Contact> PickSingleContactAsync()
        {
            Contacts cons = new Contacts();
            cons.SearchAsync(SearchPhrase, FilterKind.DisplayName | FilterKind.EmailAddress | FilterKind.Identifier | FilterKind.PhoneNumber, "PCL Contact lib");
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PCL.Model.Contact>> PickMultipleContactsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
