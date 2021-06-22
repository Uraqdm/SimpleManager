using SimpleManager.Commands;
using SimpleManager.Context;
using SimpleManager.Handlers;
using SimpleManager.Models;
using SimpleManager.Views.AddingViews;
using System.Collections.ObjectModel;
using System.Windows;

namespace SimpleManager.ViewModels.TableViewModels
{
    class EmployessVM : BaseTableVM
    {
        #region Propetries

        public static Employee SelectedEmployee { get; set; }
        public static ObservableCollection<Employee> Employees => new ObservableCollection<Employee>(SimpleManagerContext.DataBase.Employees);

        #endregion

        #region Commands

        public DelegateCommand NavigateToAddEmployees => new DelegateCommand((obj) =>
        {
            NavigationHandler.NavigationService.Navigate(new AddEmployeeView());
        });

        public DelegateCommand RemoveEmployee => new DelegateCommand((obj) =>
        {
            if (AuthVM.CurrentUser.EmployeeId == SelectedEmployee.EmployeeId) MessageBox.Show("Вы не можете удалить себя");
            else if (AuthVM.CurrentUser.Role <= SelectedEmployee.Role) MessageBox.Show("У вас недостаточно прав для удаления этого сотрудника");
            else if (SelectedEmployee != null)
            {
                SimpleManagerContext.DataBase.Employees.Remove(SelectedEmployee);
                SimpleManagerContext.DataBase.SaveChanges();
                JournalVM.AddRecord($"Удалил сотрудника c номером {SelectedEmployee.EmployeeId}");
                NavigationHandler.NavigationService.Refresh();
                MessageBox.Show($"{SelectedEmployee.LastName} {SelectedEmployee.FirstName} {SelectedEmployee.MiddleName} удален!");
            }
            else MessageBox.Show("Выберете Сотрудника");
        }, (obj) => (int)AuthVM.CurrentUser.Role > 0);

        #endregion

        #region Command methods

        private void ExportEmployeesTableToExcel(object name)
        {
            ExporterToExcel.ExportDataToExcel(DisplayablePageName, SimpleManagerContext.DataBase.Employees.ToDataTable());
        }

        #endregion

        #region Constructor

        public EmployessVM() : base("EmployeeView", "Сотрудники")
        {
            ExportTableToExcel = new DelegateCommand(ExportEmployeesTableToExcel);
        }

        #endregion
    }
}
