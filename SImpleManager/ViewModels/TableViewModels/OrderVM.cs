using SimpleManager.Commands;
using SimpleManager.Context;
using SimpleManager.Handlers;
using SimpleManager.Models;
using SimpleManager.Views.AddingViews;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace SimpleManager.ViewModels.TableViewModels
{
    class OrderVM : BaseTableVM
    {

        #region Properties

        public ObservableCollection<Order> Orders
        {
            get
            {
                if (AuthVM.CurrentUser.Role == Role.Staff)
                {
                    return new ObservableCollection<Order>(SimpleManagerContext.DataBase.Orders.Where(order => order.Employee.EmployeeId == AuthVM.CurrentUser.EmployeeId));
                }
                else return new ObservableCollection<Order>(SimpleManagerContext.DataBase.Orders);
            }
        }

        public static Order SelectedOrder { get; set; }

        #endregion

        #region Commands

        public DelegateCommand RemoveOrder => new DelegateCommand((obj) =>
        {
            if (SelectedOrder != null)
            {
                SimpleManagerContext.DataBase.Orders.Remove(SelectedOrder);
                SimpleManagerContext.DataBase.SaveChanges();
                JournalVM.AddRecord($"Удалил заказ номер {SelectedOrder.OrderId}");
                MessageBox.Show($"{SelectedOrder.OrderName} успешно удален!");
                NavigationHandler.NavigationService.Refresh();
            }
            else MessageBox.Show("Выберете элемент");
        }, (obj) => (int)AuthVM.CurrentUser.Role > 0);

        public DelegateCommand AddOrder => new DelegateCommand((obj) =>
        {
            SelectedOrder = null;
            NavigationHandler.NavigationService.Navigate(new AddOrderView());
        });

        public DelegateCommand ChangeSelectedOrder => new DelegateCommand(obj =>
        {
            if (SelectedOrder != null)
                NavigationHandler.NavigationService.Navigate(new AddOrderView());
            else MessageBox.Show("Выберете элемент");
        });

        #endregion

        #region Command methods

        private void ExportOrderTableToExcel(object obj)
        {
            ExportDataToExcel("Заказы", SimpleManagerContext.DataBase.Orders.ToDataTable());
        }

        #endregion

        #region Constructor

        public OrderVM() : base("OrderView", "Заказы")
        {
            ExportTableToExcel = new DelegateCommand(ExportOrderTableToExcel);
        }

        #endregion
    }
}
