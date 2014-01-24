using System.Threading.Tasks;

namespace WindowsCommonLibrary.PCL.Interfaces
{
    public interface IMessageService
    {
        Task ShowDialogAsync(string content);

        Task ShowDialogAsync(string title, string content);
    }
}
