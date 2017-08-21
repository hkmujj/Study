using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ATP200C.MainView
{
    /// <summary>
    /// 功能描述 GT_MainAeroB类 
    ///    跳到黑屏界面
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-17
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ATPMoveToBlackScreen : ATPBaseClass
    {
        #region 私有字段
        /// <summary>
        /// 是否离开黑屏界面(true进入启动界面)
        /// </summary>
        private bool _isLeaveBlackView = false;
        #endregion

        #region  重载方法
        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "黑 屏 跳 转 状 态";
        }

        public override void paint(Graphics dcGs)
        {
            _isLeaveBlackView = BoolList[UIObj.InBoolList[0]];
            if (!_isLeaveBlackView)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        #endregion
    }
}