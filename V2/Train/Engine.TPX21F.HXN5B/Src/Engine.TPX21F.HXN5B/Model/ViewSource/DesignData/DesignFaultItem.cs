using System;
using System.Collections.Generic;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Fault.Detail;

namespace Engine.TPX21F.HXN5B.Model.ViewSource.DesignData
{
    public class DesignFaultItem
    {
        public static readonly List<FaultItem> Items = new List<FaultItem>()
        {
            new FaultItem(new NotifyInfoConfig("conten1 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten2 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten3 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten4 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten5 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten6 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten7 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten8 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten9 "), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten10"), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten11"), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten12"), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten13"), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten14"), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten15"), DateTime.Now, 0, 0, 0,0 ),
            new FaultItem(new NotifyInfoConfig("conten16"), DateTime.Now, 0, 0, 0,0 ),
        };
    }
}