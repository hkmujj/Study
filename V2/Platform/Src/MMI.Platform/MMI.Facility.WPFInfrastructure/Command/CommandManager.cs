using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace MMI.Facility.WPFInfrastructure.Command
{
    /// <summary>
    /// 命令管理器
    /// </summary>
    public class CommandManager
    {
        private static Dictionary<DependencyObject, CommandBehavior> AllCommandBehaviors = new Dictionary<DependencyObject, CommandBehavior>();
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command", typeof(ICommand), typeof(CommandManager), new PropertyMetadata(default(ICommand), OnCommandChanged));
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.RegisterAttached(
            "EventName", typeof(string), typeof(CommandManager), new PropertyMetadata(default(string), OnEventNameChanged));
        public static readonly DependencyProperty CommandParamProperty = DependencyProperty.RegisterAttached(
            "CommandParam", typeof(object), typeof(CommandManager), new PropertyMetadata(default(object), OnCommandParamChanged));



        public static void SetCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void SetEventName(DependencyObject element, string value)
        {
            element.SetValue(EventNameProperty, value);
        }

        public static string GetEventName(DependencyObject element)
        {
            return (string)element.GetValue(EventNameProperty);
        }

        public static void SetCommandParam(DependencyObject element, object value)
        {
            element.SetValue(CommandParamProperty, value);
        }

        public static object GetCommandParam(DependencyObject element)
        {
            return (object)element.GetValue(CommandParamProperty);
        }
        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = GetOrCreatBehavior(d);
            behavior.Command = (ICommand)e.NewValue;
        }
        private static void OnCommandParamChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = GetOrCreatBehavior(d);
            behavior.CommandParam = e.NewValue;
        }
        private static void OnEventNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var behavior = GetOrCreatBehavior(d);
            var eventinfo = behavior.Target.GetType().GetEvent(e.NewValue as string);
            eventinfo.AddEventHandler(behavior.Target, GetDelagate(eventinfo));

        }

        private static Delegate GetDelagate(EventInfo eventInfo)
        {
            Delegate result = null;

            MethodInfo methodInfo = eventInfo.EventHandlerType.GetMethod("Invoke");
            ParameterInfo[] parameters = methodInfo.GetParameters();
            if (parameters.Length == 2)
            {
                Type currentType = typeof(CommandManager);
                Type argType = parameters[1].ParameterType;
                MethodInfo eventRaisedMethod =
                    currentType.GetMethod("OnEventRaised", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(argType);
                result = Delegate.CreateDelegate(eventInfo.EventHandlerType, eventRaisedMethod);
            }

            return result;

        }
        private static void OnEventRaised<T>(object sender, T arg) where T : EventArgs
        {
            DependencyObject dependencyObject = sender as DependencyObject;
            if (dependencyObject != null)
            {

                var be = GetOrCreatBehavior(dependencyObject);
                ICommand command = be.Command;
                if (command.CanExecute(be.CommandParam))
                {
                    command.Execute(be.CommandParam);
                }
            }
        }
        private static CommandBehavior GetOrCreatBehavior(DependencyObject d)
        {
            foreach (var behavior in AllCommandBehaviors)
            {
                if (behavior.Key.Equals(d))
                {
                    return behavior.Value;
                }
            }
            AllCommandBehaviors.Add(d, new CommandBehavior(d as UIElement));
            return AllCommandBehaviors[d];

        }
    }

    public class CommandBehavior
    {
        public CommandBehavior(UIElement target)
        {
            Target = target;
        }

        public ICommand Command { get; set; }
        public object CommandParam { get; set; }
        public UIElement Target { get; private set; }
    }
}
