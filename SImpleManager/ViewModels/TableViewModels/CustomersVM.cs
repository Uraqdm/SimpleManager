using SimpleManager.Commands;
using SimpleManager.Context;
using SimpleManager.Handlers;
using SimpleManager.Models;
using SimpleManager.Views.AddingViews;
using System.Collections.ObjectModel;
using System.Windows;

namespace SimpleManager.ViewModels.TableViewModels
{
    class CustomersVM : BaseTableVM
    {
        #region Properties

        public Customer SelectedCustomer { get; set; }
        public ObservableCollection<Customer> Customers => new ObservableCollection<Customer>(SimpleManagerContext.DataBase.Customers);

        #endregion

        #region Commands

        public DelegateCommand RemoveCustomer
        {
            get => new DelegateCommand((obj) =>
            {
                if (SelectedCustomer != null)
                {
                    SimpleManagerContext.DataBase.Customers.Remove(SelectedCustomer);
                    SimpleManagerContext.DataBase.SaveChanges();
                    JournalVM.AddRecord($"Удалил заказчика под номером {SelectedCustomer.CustomerId}");
                    NavigationHandler.NavigationService.Refresh();
                    MessageBox.Show($"Заказчик {SelectedCustomer} успешно удален.");
                }
                else MessageBox.Show("Выберите заказчика");
            }, (obj) => (int)AuthVM.CurrentUser.Role > 1);
        }
        public DelegateCommand AddCustomer
        {
            get => new DelegateCommand((obj) =>
            {
                NavigationHandler.NavigationService.Navigate(new AddCustomerView());
            });
        }
        public DelegateCommand ChangeCustomer => new((obj) =>
        {
            if(SelectedCustomer != null)
                NavigationHandler.NavigationService.Navigate(new AddCustomerView());
            else
                MessageBox.Show("Выберете заказчика");

        });

        #endregion

        #region Command methods

        private void ExportCustomersTableToExcel(object obj)
        {
            ExporterToExcel.ExportDataToExcel(DisplayablePageName, SimpleManagerContext.DataBase.Customers.ToDataTable());
        }

        #endregion

        #region Constructor

        public CustomersVM() : base("CustomersView", "Заказчики")
        {
            ExportTableToExcel = new DelegateCommand(ExportCustomersTableToExcel);
        }

        #endregion
    }
}
