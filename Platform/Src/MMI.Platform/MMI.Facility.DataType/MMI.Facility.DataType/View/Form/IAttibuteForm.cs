using System.Collections.Generic;
using MMI.Facility.DataType.View.Control;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI.Interface;

namespace MMI.Facility.DataType.View.Form
{
    public interface IAttributeForm : IHelpForm
    {
        Dictionary<string, IChildAttributeControl> ChildAttributeControlDic { get; }

        void Select(string appName, IUIObject obj);

        void Select(string appName, string objKey);
    }
}
