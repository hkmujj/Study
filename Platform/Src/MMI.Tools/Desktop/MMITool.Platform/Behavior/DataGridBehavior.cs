using System;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Interactivity;

namespace RailwaySimulation.Platform.Behavior
{
    internal class DataGridBehavior : Behavior<DataGrid>
    {
        public DataGridBehavior()
        {
            
        }

        protected override void OnAttached()
        {
            var dataGrid =
                this.AssociatedObject;
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
            var cells = AssociatedObject.SelectedItems;
            if (cells.Count == 0)
            {
                return;
            }

            //CommandParameter = new DataGridMouseOperatorArgs(MouseState.Up, TargetObject.Tag as DataTable, ((DataRowView)cells[0]).Row, null, TargetObject.SelectedValue);
            //ExecuteCommand();
        }

        private void OnDataGridOnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {

        }

        private void OnDataGridOnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {

        }
    }
}
