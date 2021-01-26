using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SimpleManager.Handlers
{
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, typeof(string));
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
