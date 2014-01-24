using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsCommonLibrary.PCL.Interfaces;

namespace WindowsCommonLibrary.WindowsStore.Common
{
    public class NavigationService : INavigationService
    {
        private NavigationState _state;

        public NavigationService(NavigationState state)
        {
            this._state = state;
        }

        public int BackStackDepth
        {
            get
            {
                var frame = ((Frame)Window.Current.Content);
                return frame.BackStackDepth;
            }
        }

        public Type CurrentPageType
        {
            get
            {
                var frame = ((Frame)Window.Current.Content);
                return frame.CurrentSourcePageType;
            }
        }

        public bool CanGoBack
        {
            get
            {
                var frame = ((Frame)Window.Current.Content);
                return frame.CanGoBack;
            }
        }

        public void GoBack()
        {
            var frame = ((Frame)Window.Current.Content);

            if (frame.CanGoBack)
                frame.GoBack();
        }

        public void Navigate(Type pageType)
        {
            ((Frame)Window.Current.Content).Navigate(pageType);
        }

        public void Navigate(Type pageType, object parameter)
        {
            ((Frame)Window.Current.Content).Navigate(pageType, parameter);
        }

        public void Navigate(string pageKey)
        {
            var pageType = Type.GetType(string.Format("{0}.{1}", _state.PageNamespace, pageKey));
            ((Frame)Window.Current.Content).Navigate(pageType);
        }

        public void Navigate(string pageKey, object parameter)
        {
            var pageType = Type.GetType(string.Format("{0}.{1}", _state.PageNamespace, pageKey));
            ((Frame)Window.Current.Content).Navigate(pageType, parameter);
        }
    }

    public class NavigationState
    {
        public string PageNamespace { get; set; }
    }
}
