using SimpleManager.Handlers;
using SimpleManager.Views.OtherViews;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SimpleManager
{
    public partial class App : Application
    {
        protected override void OnNavigated(NavigationEventArgs e)
        {
            base.OnNavigated(e);
            Page page = e.Content as Page;
            if (page != null)
            {
                if (page.GetType() == typeof(MenuView)) NavigationHandler.MenuNavigationService = page.NavigationService;
                else NavigationHandler.NavigationService = page.NavigationService;
            }
        }
    }
}
