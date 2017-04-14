using System;
using System.Data;

namespace MMI.Facility.WPFInfrastructure.Event.EventArg
{
    /// <summary>
    /// 
    /// </summary>
    public class DataGridMouseOperatorArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouseState"></param>
        /// <param name="dataTable"></param>
        /// <param name="selectedRow"></param>
        /// <param name="selectedColumn"></param>
        /// <param name="selectedValue"></param>
        public DataGridMouseOperatorArgs(MouseState mouseState, DataTable dataTable, DataRow selectedRow, DataColumn selectedColumn, object selectedValue)
        {
            SelectedValue = selectedValue;
            SelectedColumn = selectedColumn;
            SelectedRow = selectedRow;
            DataTable = dataTable;
            MouseState = mouseState;
        }

        /// <summary>
        /// 
        /// </summary>
        public MouseState MouseState { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DataTable DataTable { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DataRow SelectedRow { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DataColumn SelectedColumn { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public object SelectedValue { private set; get; }

    }
}
