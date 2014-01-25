using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WCL.WP8Test.Resources;
using WindowsCommonLibrary.PCL.Interfaces;
using WindowsCommonLibrary.WindowsPhone.Common;

namespace WCL.WP8Test
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void btMessageService_Click(object sender, RoutedEventArgs e)
        {
            IMessageService mService = new MessageService();
            await mService.ShowDialogAsync("Ala ma kota");
        }

        private async void btMessageServiceWithTitle_Click(object sender, RoutedEventArgs e)
        {
            IMessageService mService = new MessageService();
            await mService.ShowDialogAsync("test title", "Ala ma kota");
        }

        private async void btTakePhotoFromCamera_Click(object sender, RoutedEventArgs e)
        {
            IPhotoService pService = new PhotoService();
            var photo = await pService.TakePhotoFromCamera();
            if (photo != null)
                MessageBox.Show(string.Format("You've got a byte array with length = {0}", photo.Length));
            else
                MessageBox.Show("Error");
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}