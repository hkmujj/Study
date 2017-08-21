using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Constant;
using Engine.TAX2.SS7C.View.Common;
using Microsoft.Practices.Prism.Regions;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ModifyTimeReturnActionResponser : ReturnActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateBack();
            RegionManager.RequestNavigate(RegionNames.ContentModifyTime, typeof(NullView).FullName);
        }
    }

    [Export]
    public class ModifyTimeCaretLeftActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.OtherViewModel.Controller.ModifyTimeController.ModifyTimeCaretLeft();
        }
    }

    [Export]
    public class ModifyTimeCaretRightActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.OtherViewModel.Controller.ModifyTimeController.ModifyTimeCaretRight();
        }
    }

    [Export]
    public class ModifyTimeActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.OtherViewModel.Controller.ModifyTimeController.ModifyTime();
        }
    }

    [Export]
    public class ModifyTimeOkActionResponser : ModifyTimeReturnActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.OtherViewModel.Controller.ModifyTimeController.ModifyTimeOk();

            base.ResponseClick();
        }
    }
}