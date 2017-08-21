using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.SoftSwitch.Detail;

namespace Engine.TPX21F.HXN5B.Model.ViewSource.DesignData
{
    public class DesignSoftSwitch
    {
        public static readonly DesignSoftSwitch Instance = new DesignSoftSwitch();


        public Lazy<ReadOnlyCollection<SoftSwitchItem>> AllItems { private set; get; }

        public DesignSoftSwitch()
        {
            AllItems = new Lazy<ReadOnlyCollection<SoftSwitchItem>>(() => new List<SoftSwitchItem>
            {
                new SoftSwitchItem(new SoftSwitchItemConfig("1 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("2 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("3 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("4 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("5 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("6 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("7 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("8 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("9 #牵引电机", "上", "下")),
                new SoftSwitchItem(new SoftSwitchItemConfig("10#牵引电机", "上", "下")),
            }.AsReadOnly());
        }

    }
}