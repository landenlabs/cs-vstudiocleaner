using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VStudioCleaner_ns
{
    /// <summary>
    /// Author: Dennis Lang - 2009
    /// https://landenlabs.com/
    /// 
    /// ListView column sorter (uses  ordinal ignore case compare) for alpha comparison.
    /// </summary>
    public class ListViewColumnSorter : System.Collections.IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int columnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder orderOfSort;
        public enum SortDataType { eAuto, eAlpha, eNumeric, eDateTime };
        private SortDataType sortDataType;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter(SortDataType inSortDataType)
        {
            this.sortDataType = inSortDataType;

            // Initialize the column to '0'
            this.columnToSort = 0;

            // Initialize the sort order to 'none'
            this.orderOfSort = SortOrder.None;
        }

        public bool IsNumber(string str)
        {
            foreach (char c in str)
            {
                if (char.IsNumber(c) == false)
                    return false;
            }
            return true;
        }

        public int NumCompare(string strX, string strY)
        {
            int numX = int.Parse(strX);
            int numY = int.Parse(strY);
            int compareResult = numX - numY;
            return compareResult;
        }

        public int DateTimeCompare(string strX, string strY)
        {
            DateTime dtX, dtY;
            int result = 0;

            if (DateTime.TryParse(strX, out dtX) &&
                DateTime.TryParse(strY, out dtY))
            {
                result = (int)dtX.Subtract(dtY).TotalSeconds;
            }

            return result;
        }


        /// <summary>
        /// This method is inherited from the IComparer interface.  
        /// It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, 
        /// negative if 'x' is less than 'y' and positive if 'x' 
        /// is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult = 0;

            try
            {
                ListViewItem listviewX = (ListViewItem)x;
                ListViewItem listviewY = (ListViewItem)y;

                // Cast the objects to be compared to ListViewItem objects
                string strX = listviewX.SubItems[this.columnToSort].Text;
                string strY = listviewY.SubItems[this.columnToSort].Text;

                if (this.sortDataType == SortDataType.eAuto)
                {
                    compareResult = String.Compare(strX, strY, StringComparison.OrdinalIgnoreCase);
                    if (IsNumber(strX) && IsNumber(strY))
                    {
                        // Only column #2 can contain numeric strings.
                        // Try and sort as numbers.
                        try
                        {
                            Int32 numX = Int32.Parse(strX);
                            Int32 numY = Int32.Parse(strY);
                            if (numX != 0 && numY != 0)
                            {
                                compareResult = numX - numY;
                            }
                        }
                        catch { }
                    }
                }

                switch (this.sortDataType)
                {
                    case SortDataType.eAlpha:
                        // Compare the two items
                        compareResult = String.Compare(strX, strY, StringComparison.OrdinalIgnoreCase);
                        break;
                    case SortDataType.eNumeric:
                        compareResult = NumCompare(strX, strY);
                        break;
                    case SortDataType.eDateTime:
                        compareResult = DateTimeCompare(strX, strY);
                        break;
                }

                // Calculate correct return value based on object comparison
                if (this.orderOfSort == SortOrder.Ascending)
                {
                    // Ascending sort is selected, return normal result of compare operation
                    return compareResult;
                }
                else if (this.orderOfSort == SortOrder.Descending)
                {
                    // Descending sort is selected, return negative result of compare operation
                    return (-compareResult);
                }
                else
                {
                    // Return '0' to indicate they are equal
                    return 0;
                }
            }
            catch { }
            {
                // One or more of the inputs is null or conversion failed.
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                this.columnToSort = value;
            }
            get
            {
                return this.columnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                this.orderOfSort = value;
            }
            get
            {
                return this.orderOfSort;
            }
        }
    }
}
