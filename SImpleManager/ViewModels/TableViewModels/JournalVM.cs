using SimpleManager.Commands;
using SimpleManager.Context;
using SimpleManager.Handlers;
using SimpleManager.Models;
using System;
using System.Collections.ObjectModel;

namespace SimpleManager.ViewModels.TableViewModels
{
    class JournalVM : BaseTableVM
    {
        #region Properties

        public ObservableCollection<Record> Records { get; } = new ObservableCollection<Record>(SimpleManagerContext.DataBase.Journal);

        #endregion

        #region Command methods

        private void ExportJournalTableToExcel(object obj)
        {
            ExporterToExcel.ExportDataToExcel(DisplayablePageName, SimpleManagerContext.DataBase.Journal.ToDataTable());
        }

        #endregion

        #region public methods

        public static void AddRecord(string action)
        {
            SimpleManagerContext.DataBase.Journal.Add(new Record() 
            {
                Employee = AuthVM.CurrentUser,
                Action = action,
                ActionDate = DateTime.Now 
            });
            SimpleManagerContext.DataBase.SaveChanges();
        }

        #endregion

        #region Constructor

        public JournalVM() : base("JournalView", "Журнал")
        {
            ExportTableToExcel = new DelegateCommand(ExportJournalTableToExcel);
        }

        #endregion
    }
}
