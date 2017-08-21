using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ATP200C.MainView
{
    /// <summary>
    /// 功能描述 跳转界面类 
    /// 
    /// 创建人：袁 凯
    /// 创建时间：2013-10-29
    /// 修改人：俞超梁
    /// 修改时间：2015-01-28
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ViewJumped : ATPBaseClass
    {
        /// <summary>
        /// 重启标志
        /// </summary>
        private bool _isReStarBlackScreen;

        /// <summary>
        /// 黑屏标志
        /// </summary>
        private bool _isBlackScreen;

        #region  重载方法

        public override string GetInfo()
        {
            return "界面跳转";
        }

        public override void paint(Graphics dcGs)
        {
            //if (CurrentViewId != 10)
            //{
            //    dcGs.FillRectangle(SolidBrushsItems.BKGBrush, _bkRect);
            //}

            _isReStarBlackScreen = BoolList[UIObj.InBoolList[0]];
            _isBlackScreen = BoolList[UIObj.InBoolList[1]];

            if (!_isReStarBlackScreen)
            {
                append_postCmd(CmdType.ChangePage, 10, 0, 0);
            }

             if (_isBlackScreen)
            {
                append_postCmd(CmdType.ChangePage, 14, 0, 0);
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2)
            {
                return;
            }
            CurrentViewId = nParaB;
        }

        public static int CurrentViewId = 0;
        private Rectangle _bkRect = new Rectangle(0, 0, 800, 600);

        #endregion
    }
}

