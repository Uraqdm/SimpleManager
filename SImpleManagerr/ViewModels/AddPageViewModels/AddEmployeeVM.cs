using SimpleManager.Commands;
using SimpleManager.Context;
using SimpleManager.Handlers;
using SimpleManager.Models;
using SimpleManager.ViewModels.TableViewModels;
using SimpleManager.Views.TableViews;
using System.Collections.ObjectModel;
using System.Windows;

namespace SimpleManager.ViewModels.AddPageViewModels
{
    class AddEmployeeVM : AddPersonBaseVM
    {
        private Employee employee;
        public string Login
        {
            get
            {
                return employee.Login;
            }
            set
            {
                employee.Login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get
            {
                return employee.Password;
            }
            set
            {
                employee.Password = value;
                OnPropertyChanged();
            }
        }
        public Role Role
        {
            get
            {
                return employee.Role;
            }
            set
            {
                employee.Role = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Role> Roles => new ObservableCollection<Role> { Models.Role.Staff, Models.Role.Admin, Models.Role.Boss };

        public DelegateCommand AddEmployee => new DelegateCommand((obj) =>
        {
            SimpleManagerContext.DataBase.Employees.Add(employee);
            SimpleManagerContext.DataBase.SaveChanges();
            JournalVM.AddRecord($"Добавил сотрудника {model}");
            MailHandler.SendMessage(new System.Net.Mail.MailAddress(model.Email), "Проверка связи");
            MessageBox.Show($"{model} Успешно добавлен");
            NavigationHandler.NavigationService.Navigate(new EmployeeView());
            
        }, (obj) => NullOrEmpty());

        protected override bool NullOrEmpty()
        {
            if ((int)AuthVM.CurrentUser.Role > (int)employee.Role && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
            {
                return base.NullOrEmpty();
            }
            else return false;
        }

        public AddEmployeeVM()
        {
            model = new Employee();
            employee = model as Employee;
        }
    }
}
