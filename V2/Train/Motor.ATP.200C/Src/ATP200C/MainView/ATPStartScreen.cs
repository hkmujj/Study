using System;
using System.Drawing;
using ATP200C.PublicComponents;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ATP200C.MainView
{
    /// <summary>
    /// 功能描述 GT_MainAeroB类 
    ///     重启准备界面
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ATPStartScreen : ATPBaseClass
    {
        #region  重载方法
        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "ATP启动界面";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2) return;
            _switchInTime = CurrentTime;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="dcGs">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString(_info, FontsItems.Font28_Def_B,
                SolidBrushsItems.WhiteBrush, _infoRect, FontsItems.TheAlignment(FontRelated.居中));
            dcGs.DrawRectangle(PenItems.WhitePen1, _infoRect);

            if (CurrentTime - _switchInTime > _timeSpan)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        #endregion

        private readonly string _info = "DMI CTCS\nV01-18";
        private readonly Rectangle _infoRect = new Rectangle(0, 0, 800, 600);
        private readonly TimeSpan _timeSpan = new TimeSpan(0, 0, 0, 3);
        private DateTime _switchInTime;
    }
}