using System.Threading.Tasks;
using System.Windows;
using WindowsCommonLibrary.PCL.Interfaces;

namespace WindowsCommonLibrary.WindowsPhone.Common
{
    public class MessageService : IMessageService
    {
        public async Task ShowDialogAsync(string content)
        {
            MessageBox.Show(content);
        }

        public async Task ShowDialogAsync(string title, string content)
        {
            MessageBox.Show(content, title, MessageBoxButton.OK);
        }
    }
}
