using System;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Event.EventArg;
using MouseState = MMI.Facility.WPFInfrastructure.Event.EventArg.MouseState;

namespace MMI.Facility.WPFInfrastructure.Behaviors
{
    /// <summary>
    /// DataGrid 的行为
    /// </summary>
    public class DataGridCommandBehavior : CommandBehaviorBase<DataGrid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        public DataGridCommandBehavior(DataGrid dataGrid)
            : base(dataGrid)
        {
            if (dataGrid == null)
            {
                throw new ArgumentNullException("dataGrid");
            }

            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            dataGrid.MouseDoubleClick += OnDataGridOnMouseDoubleClick;
            dataGrid.MouseDown += OnDataGridOnMouseDown;
            dataGrid.MouseUp += OnDataGridOnMouseUp;

        }

        private void OnDataGridOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var cells = TargetObject.SelectedItems;
            if (cells.Count == 0)
            {
                return;
            }

            CommandParameter = new DataGridMouseOperatorArgs(MouseState.Up, TargetObject.Tag as DataTable, ((DataRowView)cells[0]).Row, null, TargetObject.SelectedValue);
            ExecuteCommand();
        }

        private void OnDataGridOnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            
        }

        private void OnDataGridOnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            
        }

        private DataGridRow GetSelectedRow(DataGrid dataGrid)
        {
            var selectcell = dataGrid.SelectedCells;
            if (!selectcell.Any())
            {
                return null;
            }

            //selectcell[0]
            return null;

        }
    }
}
