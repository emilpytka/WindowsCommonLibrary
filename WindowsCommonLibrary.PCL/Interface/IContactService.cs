using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsCommonLibrary.PCL.Model;

namespace WindowsCommonLibrary.PCL.Interfaces
{
    public interface IContactService
    {
        Task<Contact> PickSingleContactAsync();

        Task<IEnumerable<Contact>> PickMultipleContactsAsync();
    }
}
