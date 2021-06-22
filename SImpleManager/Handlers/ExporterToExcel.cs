using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;

namespace SimpleManager.Handlers
{
    public class ExporterToExcel
    {
        /// <summary>
        /// Exports DataTable to Excel table in ".xlsx" format.
        /// </summary>
        /// <param name="name">Name of saving file</param>
        /// <param name="table">Table which will be exported</param>
        /// <returns>Returns true if export was succes. Else returns false.</returns>
        public static bool ExportDataToExcel(string name, DataTable table)
        {
            XLWorkbook WB = new XLWorkbook();
            WB.Worksheets.Add(table, name);
            try
            {
                var desctopPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                WB.SaveAs($"{desctopPath}/{name}.xlsx");
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
