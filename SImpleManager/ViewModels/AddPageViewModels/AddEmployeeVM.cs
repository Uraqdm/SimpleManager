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
        #region properties

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

        public ObservableCollection<Role> Roles { get; } = new ObservableCollection<Role> { Models.Role.Staff, Models.Role.Admin, Models.Role.Boss };

        #endregion

        #region Commands

        public DelegateCommand AddEmployee => new DelegateCommand((obj) =>
        {
            SimpleManagerContext.DataBase.Employees.Add(employee);
            SimpleManagerContext.DataBase.SaveChanges();
            JournalVM.AddRecord($"Добавил сотрудника {model}");
            MessageBox.Show($"{model} Успешно добавлен");
            NavigationHandler.NavigationService.Navigate(new EmployeeView());
            
        }, (obj) => NullOrEmpty());

        #endregion

        protected override bool NullOrEmpty()
        {
            if ((int)AuthVM.CurrentUser.Role > (int)employee.Role && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
            {
                return base.NullOrEmpty();
            }
            else return false;
        }

        #region Ctor

        public AddEmployeeVM()
        {
            model = new Employee();
            employee = model as Employee;
        }

        #endregion 
    }
}
