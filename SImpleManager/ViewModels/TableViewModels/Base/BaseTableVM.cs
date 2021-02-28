using System.Windows;
using ClosedXML.Excel;
using System.IO;
using System;
using System.Data;
using SimpleManager.Commands;
using SimpleManager.Handlers;
using System.Collections.Generic;

namespace SimpleManager.ViewModels
{
    abstract class BaseTableVM : BaseVM
    {
        #region Properties

        public Uri PageUri { get; }
        public string PageName { get; }
        public string DisplayablePageName { get; }

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
