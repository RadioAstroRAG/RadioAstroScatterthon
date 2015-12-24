using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogComponent
{
    public partial class LogFileViewer
    {

        private bool PartiallyResetGridsRowsAndColumns(DataGridView grid, int numberOfExtraColumns, bool showSignal, bool showNoise, bool showFreq)
        {
            bool partialReset = false;

            grid.Rows.Clear();
            grid.RowHeadersWidth = 25;

            int currentColumns = grid.Columns.Count - 1;

            // See how many (if any) columns we need to add...
            if (numberOfExtraColumns != currentColumns)
            {
                grid.Columns.Clear();

                for (int colNumber = 0; colNumber < numberOfExtraColumns; colNumber++)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                    // Names will include: Time, Event, Freq, etc...
                    column.Name = Enum.GetName(typeof(Index), colNumber);

                    switch(colNumber)
                    {
                        case (int)Index.Time:
                            column.Width = 60;
                            column.ReadOnly = true;
                            break;
                        case (int)Index.Event:
                            column.Width = 40;
                            column.ReadOnly = true;
                            break;
                        case (int)Index.Signal:
                            column.Visible = showSignal;
                            column.Width = 50;
                            break;
                        case (int)Index.Noise:
                            column.Visible = showNoise;
                            column.Width = 50;
                            break;
                        case (int)Index.Freq:
                            column.Visible = showFreq;
                            column.Width = 50;
                            break;
                        case (int)Index.Duration:
                            column.Width = 50;
                            break;
                        case (int)Index.Class:
                            column.Width = 140;
                            column.ReadOnly = true;
                            break;
                        default:
                            column.Width = 70;
                            break;
                    }

                    grid.Columns.Add(column);
                }

                if (!grid.Columns.Contains("Class"))
                {
                    // Now add a column for classification of meteors...
                    DataGridViewTextBoxColumn classification = new DataGridViewTextBoxColumn();
                    classification.Name = "Class";
                    classification.Width = 140;
                    grid.Columns.Add(classification);
                }


                if (!grid.Columns.Contains("Image"))
                {
                    // And a column for holding references to image files...
                    DataGridViewTextBoxColumn imageColumn = new DataGridViewTextBoxColumn();
                    imageColumn.Name = "Image";
                    grid.Columns.Add(imageColumn);
                }
                else if (grid.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in grid.Rows)
                    {
                        if (row.Cells[(int)Index.Image].Value != null)
                        {
                            // We've found a cell in the "Image" column, which is not empty...
                            // So we must have populated this before.
                            partialReset = true;
                            break;
                        }
                    }
                }

                //grid.Columns["Image"].Visible = false;
                grid.Columns["Image"].ReadOnly = true;
            }
            return partialReset;
        }

        private void PopulateGridViewWithData(DataGridView destGrid, string[] lines, int numberOfExtraColumns)
        {
            // Start afresh...
            destGrid.Rows.Clear();

            foreach (string line in lines)
            {
                string[] parts = line.Split(new char[] { ',' });

                // Should be at least 5 parts to each row: Time, event, etc (see Index enum)...
                if (parts.Length > 5)
                {
                    DataGridViewRow newRow = new DataGridViewRow();

                    int rowIndex = destGrid.Rows.Add(newRow);

                    for (int linePartNumber = 0; linePartNumber < parts.Length; linePartNumber++)
                    {
                        int nextCell = linePartNumber;

                        string valueToAssign = parts[linePartNumber];

                        if (!string.IsNullOrEmpty(valueToAssign))
                        {
                            destGrid.Rows[rowIndex].Cells[nextCell].Value = valueToAssign;

                            if (nextCell == (int)Index.Image)
                            {
                                // Colour the row which has an image associated with it...
                                destGrid.Rows[rowIndex].DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                            }
                        }
                    }
                }
            }
        }

        private void PopulateGridViewWithData(DataGridView destGrid, DataGridViewRow[] rows, int numberOfExtraColumns)
        {
            if (rows.Length == 0)
            {
                return;
            }

            // Start afresh...
            destGrid.Rows.Clear();

            int numberOfCells = rows[0].Cells.Count;

            foreach (DataGridViewRow row in rows)
            {
                DataGridViewRow newRow = new DataGridViewRow();

                int index = destGrid.Rows.Add(newRow);

                for (int cellIndex = 0; cellIndex < numberOfCells; cellIndex++)
                {
                    destGrid.Rows[index].Cells[cellIndex].Value = row.Cells[cellIndex].Value;
                }                
            }
        }

        private void PrepareBackup(DataGridView sourceGrid)
        {
            if (sourceGrid.Rows.Count == 0)
            {
                return;
            }

            int numberOfCells = sourceGrid.Columns.Count;

            int selectedRowsCount = sourceGrid.SelectedRows.Count;            

            int[] lastSelection = new int[selectedRowsCount];

            for (int i = 0; i < selectedRowsCount; i++)
            {
                // Save the row selection for when last action needs to be undone.
                lastSelection[i] = sourceGrid.SelectedRows[i].Index;
            }

            DataGridViewRow[] backup = CopyOfDataGridRows(sourceGrid);

            btnUndo.Enabled = true;

            // Save it on the history stack...
            KeyValuePair<DataGridViewRow[], int[]> undo = 
                                                    new KeyValuePair<DataGridViewRow[], int[]>(backup, lastSelection);
            
            undoHistory.Push(undo);
        }

        private DataGridViewRow[] CopyOfDataGridRows(DataGridView sourceGrid)
        {
            if (sourceGrid.Rows.Count == 0)
            {
                return null;
            }

            int numberOfCells = sourceGrid.Columns.Count;
            int originalRowCount = sourceGrid.Rows.Count;

            DataGridViewRow[] backup = new DataGridViewRow[originalRowCount];

            for (int i = 0; i < originalRowCount; i++)
            {
                DataGridViewRow row = sourceGrid.Rows[i];

                DataGridViewRow clone = row.Clone() as DataGridViewRow;

                for (int cellIndex = 0; cellIndex < numberOfCells; cellIndex++)
                {
                    clone.Cells[cellIndex].Value = row.Cells[cellIndex].Value;
                }

                backup[i] = clone;
            }
            return backup;
        }

        //private void UndoLastAction(DataGridViewRow[] sourceRows, DataGridView destinationGrid)
        private void UndoLastAction(DataGridView destinationGrid)
        {
            KeyValuePair<DataGridViewRow[], int[]> undo = undoHistory.Pop();

            DataGridViewRow[] sourceRows = undo.Key;
            int[] lastSelection = undo.Value;

            int originalRowCount = sourceRows.Length;

            PopulateGridViewWithData(destinationGrid, sourceRows, 0);

            int currentRowsCount = destinationGrid.Rows.Count;

            if (originalRowCount != currentRowsCount)
            {
                destinationGrid.AllowUserToAddRows = false;
            }

            destinationGrid.Rows.Clear();

            destinationGrid.AllowUserToAddRows = true;

            // Copy the saved state into the data grid view...
            for (int rowIndex = 0; rowIndex < originalRowCount; rowIndex++ )
            {
                destinationGrid.Rows.Insert(rowIndex, sourceRows[rowIndex]);
            }

            int lastSelectedIndex = 0;

            foreach (int rowIndex in lastSelection)
            {
                destinationGrid.Rows[rowIndex].Selected = true;
                lastSelectedIndex = rowIndex;
            }
                        
            //destinationGrid.CurrentCell = destinationGrid.Rows[lastSelectedIndex].Cells[0];
            // destinationGrid.CurrentCell = null;
            
            lastSelection = null;
            sourceRows = null;

            if (undoHistory.Count == 0)
            {                
                btnUndo.Enabled = false;
            }
        }
    }
}
