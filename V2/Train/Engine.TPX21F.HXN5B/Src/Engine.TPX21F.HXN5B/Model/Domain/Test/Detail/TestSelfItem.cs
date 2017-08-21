using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Test.Detail
{
    public class TestSelfItemFactory
    {
        public static TestSelfItem CreateItem(SelfTestItemConfig config)
        {
            return new TestSelfItem(config);
        }
    }

    [DebuggerDisplay("GroupName={ItemConfig.GroupName}, Content={ItemConfig.Content}")]
    public class TestSelfItem : NotificationObject
    {
        [DebuggerStepThrough]
        public TestSelfItem(SelfTestItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
            ShowingContentCollection = new ObservableCollection<string>(Enumerable.Repeat(string.Empty, 15));
        }

        public SelfTestItemConfig ItemConfig { get; private set; }

        public ObservableCollection<string> ShowingContentCollection{ get; private set; }

    }
}