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
            SimpleManagerContext.DataBase.Customers.Add(model as Customer);
            SimpleManagerContext.DataBase.SaveChanges();
            JournalVM.AddRecord($"Добавил заказчика {model}");
            MessageBox.Show($"{model} успешно добавлен!");
            NavigationHandler.NavigationService.GoBack();
        },(obj) => NullOrEmpty());

        public AddCustomerVM()
        {
            model = new Customer();
        }
    }
}
