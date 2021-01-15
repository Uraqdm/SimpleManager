using SimpleManager.Commands;
using SimpleManager.Context;
using SimpleManager.Handlers;
using SimpleManager.Models;
using SimpleManager.ViewModels;
using SimpleManager.ViewModels.TableViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using SimpleManager.Views.AddingViews;
using SimpleManager.Views.TableViews;

namespace MyManager.ViewModels.AddPageViewModels
{
    class AddOrderVM: BaseVM
    {
        private readonly Order model;
        public ObservableCollection<Employee> Employees { get; private set; }
        public ObservableCollection<Customer> Customers => new ObservableCollection<Customer>(SimpleManagerContext.DataBase.Customers);

        public string OrderName
        {
            get
            {
                return model.OrderName;
            }
            set
            {
                model.OrderName = value;
                OnPropertyChanged();
            }
        }
        public string OrderDescription
        {
            get
            {
                return model.OrderDescription;
            }
            set
            {
                model.OrderDescription = value;
                OnPropertyChanged();
            }
        }
        public Customer Customer
        {
            get
            {
                return model.Customer;
            }
            set
            {
                model.Customer = value;
                OnPropertyChanged();
            }
        }
        public Employee Employee
        {
            get
            {
                return model.Employee;
            }
            set
            {
                model.Employee = value;
                OnPropertyChanged();
            }
        }
        public decimal OrderPrice
        {
            get
            {
                return model.OrderPrice;
            }
            set
            {
                model.OrderPrice = value;
                OnPropertyChanged();
            }
        }
        public DateTime DeadLine
        {
            get
            {
                return model.DeadLine;
            }
            set
            {
                model.DeadLine = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand AddOrder => new DelegateCommand((obj) =>
        {
            SimpleManagerContext.DataBase.Orders.Add(model);
            JournalVM.AddRecord($"Добавил заказаз номер {model.OrderId}");
            MessageBox.Show($"Заказ под названием {model} успешно добавлен");
            NavigationHandler.NavigationService.Navigate(new OrderView());
        }, (obj) => !string.IsNullOrEmpty(OrderName) && model.Customer != null && model.Employee != null && model.OrderPrice >= 0 && model.OrderDate < DeadLine);
        
        public DelegateCommand AddNewCustomer => new DelegateCommand((obj) => 
        {
            NavigationHandler.NavigationService.Navigate(new AddCustomerView());
        });

        public AddOrderVM()
        {
            model = new Order();
            model.OrderDate = DateTime.Now;
            model.Status = Status.OnProcess;
            DeadLine = DateTime.Now.AddDays(1);
            Employees = AuthVM.CurrentUser.Role == Role.Staff
                ? new ObservableCollection<Employee> { AuthVM.CurrentUser }
                : new ObservableCollection<Employee>(SimpleManagerContext.DataBase.Employees);
        }
    }
}
