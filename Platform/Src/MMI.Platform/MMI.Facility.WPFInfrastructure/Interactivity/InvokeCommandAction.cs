using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MMI.Facility.WPFInfrastructure.Interactivity
{
    /// <summary>
    /// 
    /// </summary>
    public class InvokeCommandAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof (ICommand), typeof (InvokeCommandAction), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof (object), typeof (InvokeCommandAction), new PropertyMetadata(default(object)));

        private string m_CommandName;

        /// <summary>
        /// 
        /// </summary>
        public object CommandParameter
        {
            get { return (object) GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }


        /// <summary>
        /// 
        /// </summary>
        public string CommandName
        {
            get
            {
                ReadPreamble();
                return m_CommandName;
            }
            set
            {
                if (value != m_CommandName)
                {
                    WritePreamble();
                    m_CommandName = value;
                    WritePreamble();
                }
            }
        }

        protected override void Invoke(object parameter)
        {
            if (AssociatedObject != null)
            {
                var comand = ResolveCommand();
                var param = new CommandParameter()
                {
                    Sender = AssociatedObject,
                    Parameter = CommandParameter,
                    EventArgs = parameter as EventArgs,
                };
                if (comand != null && comand.CanExecute(param))
                {
                    comand.Execute(param);
                }
            }
        }

        private ICommand ResolveCommand()
        {
            ICommand rlt = null;

            if (Command != null)
            {
                rlt = Command;
            }
            else if (AssociatedObject != null)
            {
                var type = AssociatedObject.GetType();
                var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (
                    var info in
                        props.Where(
                            info =>
                                typeof (ICommand).IsAssignableFrom(info.PropertyType) &&
                                string.Equals(info.Name, CommandName, StringComparison.Ordinal)))
                {
                    rlt = (ICommand) info.GetValue(AssociatedObject, null);
                }
            }

            return rlt;
        }
    }
}