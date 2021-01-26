using System.Windows;
using ClosedXML.Excel;
using System.IO;
using System;
using System.Data;
using SimpleManager.Commands;
using SimpleManager.Handlers;

namespace SimpleManager.ViewModels
{
    abstract class BaseTableVM : BaseVM
    {
        #region Properties

        public Uri PageUri { get; }
        public string PageName { get; }
        public string DisplayablePageName { get; set; }

        #endregion

        #region Commands

        public DelegateCommand ExportTableToExcel { get; protected set; }

        public DelegateCommand Refresh => new DelegateCommand((obj) =>
        {
            NavigationHandler.NavigationService.Refresh();
        });

        public DelegateCommand Cancel => new DelegateCommand((obj) =>
        {
            NavigationHandler.NavigationService.GoBack();
        });

        #endregion

        #region Protected methods

        protected void ExportDataToExcel(string name, DataTable table)
        {
            XLWorkbook WB = new XLWorkbook();
            WB.Worksheets.Add(table, name);
            try
            {
                var desctopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                WB.SaveAs($"{desctopPath}/{name}.xlsx");
            }
            catch (IOException)
            {
                MessageBox.Show("Не удается сохранить файл. Закройте файл и попробуйте снова.");
            }
        }

        #endregion

        #region Constructor

        public BaseTableVM(string pageName, string displayeblePageName)
        {
            PageUri = new Uri($"../TableViews/{pageName}.xaml", UriKind.RelativeOrAbsolute);
            PageName = pageName;
            DisplayablePageName = displayeblePageName;
        }

        #endregion
    }
}
