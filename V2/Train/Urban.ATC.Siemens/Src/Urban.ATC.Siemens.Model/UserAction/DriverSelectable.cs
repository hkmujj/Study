using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model.UserAction
{
    public class DriverSelectable : NotificationObject, IDriverSelectable
    {
        protected List<IDriverSelectableItem> m_DrvierSelectableItems;

        public IDriverInterface Parent { get; set; }

        public event EventHandler<DriverSelectedEventArgs> Selected;

        public ReadOnlyCollection<IDriverSelectableItem> SelectableItems { get; private set; }

        public IDriverSelectableItem this[UserActionType actionType]
        {
            get { return SelectableItems.FirstOrDefault(f => (UserActionType) f.UserActionType == actionType); }
        }

        public DriverSelectable(IEnumerable<IDriverSelectableItem> selectableItems)
        {
            m_DrvierSelectableItems = new List<IDriverSelectableItem>(selectableItems);
            SelectableItems = m_DrvierSelectableItems.AsReadOnly();
            foreach (var item in m_DrvierSelectableItems)
            {
                item.Parent = this;
                item.PropertyChanged += (sender, args) => RaisePropertyChanged(PropertySupport.ExtractPropertyName(() => SelectableItems) + "." + args.PropertyName);
            }
        }

        public void RaiseSelectedChanged(DriverSelectedEventArgs args)
        {
            if (Selected != null)
            {
                Selected(this, args);
            }
        }

        public void UpdateStates()
        {
            foreach (var item in SelectableItems)
            {
                item.UpdateStates();
            }
        }
    }
}