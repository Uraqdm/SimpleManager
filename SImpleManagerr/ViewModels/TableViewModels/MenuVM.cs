using SimpleManager.Commands;
using SimpleManager.Handlers;
using SimpleManager.Views.OtherViews;
using System.Collections.ObjectModel;
using System.Linq;

namespace SimpleManager.ViewModels.TableViewModels
{
    class MenuVM : BaseVM
    {
        #region Properties

        public string FirstName => AuthVM.CurrentUser.FirstName;
        public string LastName => AuthVM.CurrentUser.LastName;
        public string MiddleName => AuthVM.CurrentUser.MiddleName;

        public static ObservableCollection<BaseTableVM> Pages { get; }
        public BaseTableVM SelectedPageItem { get; set; }

        #endregion

        #region Commands

        public DelegateCommand BackToAuthPage => new DelegateCommand((obj) =>
        {
            NavigationHandler.MenuNavigationService.Navigate(new AuthorizationView());
        });

        #endregion

        #region Constructors

        static MenuVM()
        {
            Pages = new ObservableCollection<BaseTableVM>
            {
                new OrderVM(),
                new EmployessVM(),
                new CustomersVM(),
                new JournalVM()
            };
        }

        public MenuVM()
        {
            SelectedPageItem = Pages.FirstOrDefault();
        }

        #endregion
    }
}
