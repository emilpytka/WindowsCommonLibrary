using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using WindowsCommonLibrary.PCL.Interfaces;

namespace WindowsCommonLibrary.WindowsStore.Common
{
    /// <summary>
    /// Windows Store implementation of IMessage Service. 
    /// </summary>
    public class MessageService : IMessageService
    {
        public async Task ShowDialogAsync(string content)
        {
            var dialog = new MessageDialog(content);
            await dialog.ShowAsync();
        }

        public async Task ShowDialogAsync(string title, string content)
        {
            var dialog = new MessageDialog(content, title);
            await dialog.ShowAsync();
        }
    }
}
