using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace MMI.Facility.WPFInfrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataGridCommand
    {
        /// <summary>
        /// Command to execute on click or double click or mouse down or mouse up event.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(DataGridCommand),
            new PropertyMetadata(OnSetCommandCallback));

        /// <summary>
        /// Sets the <see cref="ICommand"/> to execute on the return key event.
        /// </summary>
        /// <param name="dataGrid"> dependency object to attach command</param>
        /// <param name="command">Command to attach</param>
        public static void SetCommand(DataGrid dataGrid, ICommand command)
        {
            if (dataGrid == null)
            {
                throw new ArgumentNullException("dataGrid");
            }

            dataGrid.SetValue(CommandProperty, command);
        }

        /// <summary>
        /// Retrieves the <see cref="ICommand"/> attached to the <see cref="TextBox"/>.
        /// </summary>
        /// <param name="dataGrid"> containing the Command dependency property</param>
        /// <returns>The value of the command attached</returns>
        public static ICommand GetCommand(DataGrid dataGrid)
        {
            if (dataGrid == null)
            {
                throw new ArgumentNullException("dataGrid");
            }

            return dataGrid.GetValue(CommandProperty) as ICommand;
        }

        private static readonly DependencyProperty DataGridCommandBehaviorProperty = DependencyProperty.RegisterAttached(
            "DataGridCommandBehavior",
            typeof (DataGridCommandBehavior),
            typeof (DataGridCommand),
            null);

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = dependencyObject as DataGrid;
            if (dataGrid != null)
            {
                var behavior = GetOrCreateBehavior(dataGrid);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static DataGridCommandBehavior GetOrCreateBehavior(DataGrid dataGrid)
        {
            var behavior = dataGrid.GetValue(DataGridCommandBehaviorProperty) as DataGridCommandBehavior;
            if (behavior == null)
            {
                behavior = new DataGridCommandBehavior(dataGrid);
                dataGrid.SetValue(DataGridCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
    }
}
