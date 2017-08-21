using System;
using System.Diagnostics;

namespace Motor.ATP.Domain.Interface.UserAction
{
    [DebuggerStepThrough]
    public class DriverSelectedEventArgs : EventArgs
    {
        public DriverSelectedEventArgs(IDriverSelectableItem selectedItem, DriverSelectedType selectedType)
        {
            SelectedType = selectedType;
            SelectedItem = selectedItem;
        }

        public IDriverSelectableItem SelectedItem { private set; get; }

        public DriverSelectedType SelectedType { private set; get; }
    }

    public enum DriverSelectedType
    {
        Press,

        Relase,
    }
}