using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 功能描述 MainAeroB类 
    ///    跳到黑屏界面
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-17
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MoveToBlackScreen : baseClass
    {
        #region 私有字段
        /// <summary>
        /// 是否离开黑屏界面(true进入启动界面)
        /// </summary>
        private bool m_IsLeaveBlackView;
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

        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            return true;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="g">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics g)
        {
            GetValue();

            OnDraw(g);
        }
        #endregion

        #region  私有方法
        private void GetValue()
        {
            m_IsLeaveBlackView = BoolList[this.GetInboolIndex(InBoolKeys.InbATP屏亮屏标志)];
        }

        private void OnDraw(Graphics g)
        {
            if (!m_IsLeaveBlackView)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }
        #endregion
    }
}