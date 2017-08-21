using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Engine.TCMS.HXD3.Model.ConfigModel
{
    public class FaultHandleDescription
    {
        public static readonly ReadOnlyCollection<FaultHandleItem> FaultHandleItems =
            new ReadOnlyCollection<FaultHandleItem>(new List<FaultHandleItem>()
            {
                new FaultHandleItem(1, "StringFaultHandleDescription01"),
                new FaultHandleItem(2, "StringFaultHandleDescription02"),
                new FaultHandleItem(3, "StringFaultHandleDescription03"),
                new FaultHandleItem(4, "StringFaultHandleDescription04"),
                new FaultHandleItem(5, "StringFaultHandleDescription05"),
                new FaultHandleItem(6, "StringFaultHandleDescription06"),
                new FaultHandleItem(7, "StringFaultHandleDescription07"),
                new FaultHandleItem(8, "StringFaultHandleDescription08"),
                new FaultHandleItem(9, "StringFaultHandleDescription09"),
                new FaultHandleItem(10, "StringFaultHandleDescription10"),
                new FaultHandleItem(11, "StringFaultHandleDescription11"),
                new FaultHandleItem(12, "StringFaultHandleDescription12"),
                new FaultHandleItem(13, "StringFaultHandleDescription13"),
            });
    }

    [DebuggerDisplay("Identify={Identify} DescriptionKey={DescriptionKey}")]
    public class FaultHandleItem
    {
        [DebuggerStepThrough]
        public FaultHandleItem(int identify, string descriptionKey)
        {
            Identify = identify;
            DescriptionKey = descriptionKey;
        }

        public int Identify { private set; get; }

        public string DescriptionKey { private set; get; }
    }
}