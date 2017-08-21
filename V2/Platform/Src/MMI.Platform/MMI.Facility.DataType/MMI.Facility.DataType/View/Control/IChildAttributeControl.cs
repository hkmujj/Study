using System;
using MMI.Facility.DataType.View.EventArg;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.DataType.View.Control
{
    public interface IChildAttributeControl : IHelpControl
    {

        string AppName { get; }

        event EventHandler<AttributeValueDoubleClickEventArg> BoolObjectAttribeFormDoubelClick;

        event EventHandler<AttributeValueDoubleClickEventArg> FloatObjectattributeFormDoubleClick;

        /// <summary>
        /// 选中一个
        /// </summary>
        /// <param name="obj"></param>
        void Select(IUIObject obj);

        //void Initalize(IDataPackage dataPackage, string appName);

    }
}
