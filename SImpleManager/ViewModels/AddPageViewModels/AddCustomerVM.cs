using SimpleManager.Commands;
using SimpleManager.Context;
using SimpleManager.Handlers;
using SimpleManager.Models;
using SimpleManager.ViewModels.TableViewModels;
using System.Windows;

namespace SimpleManager.ViewModels.AddPageViewModels
{
    class AddCustomerVM: AddPersonBaseVM
    {
        public DelegateCommand SaveCustomer => new DelegateCommand((obj) =>
        {
            if(CustomersVM.SelectedCustomer != null)
            {
                SimpleManagerContext.DataBase.Entry(CustomersVM.SelectedCustomer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                JournalVM.AddRecord($"Изменил заказчика {CustomersVM.SelectedCustomer}");
            }
            else
            {
                SimpleManagerContext.DataBase.Customers.Add(model as Customer);
                JournalVM.AddRecord($"Добавил заказчика {model}");
            }
            
            SimpleManagerContext.DataBase.SaveChanges();
            
            MessageBox.Show($"Изменения сохранены");
            CustomersVM.SelectedCustomer = null;
            NavigationHandler.NavigationService.GoBack();
        },(obj) => NullOrEmpty());

        public AddCustomerVM()
        {
            model = CustomersVM.SelectedCustomer ?? new Customer();
        }
    }
}
