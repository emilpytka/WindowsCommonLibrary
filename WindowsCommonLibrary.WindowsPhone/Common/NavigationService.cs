using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using WindowsCommonLibrary.PCL.Interfaces;

namespace WindowsCommonLibrary.WindowsPhone.Common
{
    public class NavigationService : INavigationService
    {
        public const string PARAMETER_KEY = "NavigationParameter";

        private PhoneApplicationFrame _mainFrame;

        public event NavigatingCancelEventHandler Navigating;

        public void GoBack()
        {
            if (EnsureMainFrame() && _mainFrame.CanGoBack)
                _mainFrame.GoBack();
        }

        public void NavigateTo(Uri pageUri)
        {
            if (EnsureMainFrame())
            {
                _mainFrame.Navigate(pageUri);
            }
        }

        private bool EnsureMainFrame()
        {
            if (_mainFrame != null)
            {
                return true;
            }

            _mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (_mainFrame != null)
            {
                _mainFrame.Navigating += (s, e) =>
                {
                    if (Navigating != null)
                    {
                        Navigating(s, e);
                    }
                };

                return true;
            }
            return false;
        }

        public void Navigate(string pageKey)
        {
            NavigateTo(new Uri("/" + pageKey + ".xaml", UriKind.Relative));
        }

        public void Navigate(string pageKey, object parameter)
        {
            PhoneApplicationService.Current.State[PARAMETER_KEY] = parameter;
            NavigateTo(new Uri("/" + pageKey + ".xaml", UriKind.Relative));
        }
    }
}
