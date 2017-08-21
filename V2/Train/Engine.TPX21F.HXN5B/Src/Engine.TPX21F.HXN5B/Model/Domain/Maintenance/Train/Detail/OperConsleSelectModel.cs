using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train.Detail
{
    [Export]
    public class OperConsleSelectModel : NotificationObject
    {
        public Lazy<ReadOnlyCollection<OperConsleSelectItem>> OperConsleSelectItems { get; private set; }

        public OperConsleSelectModel()
        {
            OperConsleSelectItems =
                new Lazy<ReadOnlyCollection<OperConsleSelectItem>>(
                    () =>
                        GlobalParam.Instance.OperConsoleSelectItemConfigs.Value.Select(s => new OperConsleSelectItem(s))
                            .ToList()
                            .AsReadOnly());
        }
    }

    public class OperConsleSelectItem : NotificationObject, IPassableItem
    {
        
        private bool m_Passed;

        public OperConsleSelectItem(OperConsoleSelectItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
        }

        public OperConsoleSelectItemConfig ItemConfig { private set; get; }

        public bool Passed
        {
            get { return m_Passed; }
            set
            {
                if (value == m_Passed)
                {
                    return;
                }

                m_Passed = value;
                RaisePropertyChanged(() => Passed);
            }
        }

        IPassableItemConfig IPassableItem.ItemConfig { get { return ItemConfig; } }
    }
}