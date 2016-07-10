using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogComponent
{
    public partial class LogFileViewer
    {
        private void CustomSortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            try
            {
                int columnIndex = e.Column.Index;
                sortedByTime = false;

                // For signal strength, frequency and noise...
                if (columnIndex == (int)Index.Signal ||
                    columnIndex == (int)Index.Freq ||
                    columnIndex == (int)Index.Noise)
                {
                    decimal a = decimal.Parse(e.CellValue1.ToString());
                    decimal b = decimal.Parse(e.CellValue2.ToString());

                    e.SortResult = a.CompareTo(b);
                }
                // For event numbers and durations...
                else if (columnIndex == (int)Index.Event ||
                         columnIndex == (int)Index.Duration)
                {
                    double a = double.Parse(e.CellValue1.ToString());
                    double b = double.Parse(e.CellValue2.ToString());

                    e.SortResult = a.CompareTo(b);
                }
                // For timestamps...
                else if (columnIndex == (int)Index.Time)
                {
                    DateTime a = DateTime.ParseExact(e.CellValue1.ToString(), "HH:mm:ss", CultureInfo.CurrentCulture);
                    DateTime b = DateTime.ParseExact(e.CellValue2.ToString(), "HH:mm:ss", CultureInfo.CurrentCulture);

                    e.SortResult = a.CompareTo(b);

                    if (e.SortResult > 0)
                    {
                        sortedByTime = true;
                    }
                }
                else if (columnIndex == (int)Index.Image ||
                         columnIndex == (int)Index.Class)
                {
                    string a = (e.CellValue1 == null ? "" : e.CellValue1.ToString());
                    string b = (e.CellValue2 == null ? "" : e.CellValue2.ToString());

                    e.SortResult = a.CompareTo(b);
                }
            }
            catch
            { 
            }

            e.Handled = true;
        }
     }

        //public class RowComparer : System.Collections.IComparer
        //{
        //    private static int sortOrderModifier = 1;

        //    public RowComparer(SortOrder sortOrder)
        //    {
        //        if (sortOrder == SortOrder.Descending)
        //        {
        //            sortOrderModifier = -1;
        //        }
        //        else if (sortOrder == SortOrder.Ascending)
        //        {
        //            sortOrderModifier = 1;
        //        }
        //    }

        //    public int Compare(object x, object y)
        //    {
        //        DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
        //        DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

        //        // Try to sort based on the Last Name column. 
        //        int CompareResult = System.String.Compare(
        //            DataGridViewRow1.Cells[1].Value.ToString(),
        //            DataGridViewRow2.Cells[1].Value.ToString());

        //        // If the Last Names are equal, sort based on the First Name. 
        //        if (CompareResult == 0)
        //        {
        //            CompareResult = System.String.Compare(
        //                DataGridViewRow1.Cells[0].Value.ToString(),
        //                DataGridViewRow2.Cells[0].Value.ToString());
        //        }
        //        return CompareResult * sortOrderModifier;
        //    }
        //}
   
}
