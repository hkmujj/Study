using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 功能描述 MainAeroB类 
    ///     重启准备界面
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MMIStartScreen : baseClass
    {
        private int m_JiShuCount = 1;
        private int m_StepIndex;

        private readonly string[] m_TitleStrs = {"ATP 正在初始化","ATP 正在初始化.","ATP 正在初始化..",
            "ATP 正在初始化...","ATP 初始化完成!",
            "ATP 正在启动" ,"ATP 正在启动.","ATP 正在启动..", "ATP 正在启动...",
            "ATP 正在启动" ,"ATP 正在启动.","ATP 正在启动..", "ATP 正在启动...",
        };

        private readonly Font m_TitleFont = new Font("宋体", 36, FontStyle.Bold);


        #region  重载方法
        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "ATP启动界面";
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
        /// <param name="dcGs">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics dcGs)
        {
            base.paint(dcGs);
            GetValue();
            YanShi();
            OnDraw(dcGs);
        }
        #endregion

        #region  私有方法
        private void GetValue()
        {

        }

        private void OnDraw(Graphics g)
        {
            if (m_StepIndex == 14)
            {
                m_StepIndex = 0;
                TextInfo.ClearCurrentInforRecords();
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }

            if (m_StepIndex >= 0 && m_StepIndex < 13)
            {
                g.DrawString(m_TitleStrs[m_StepIndex], m_TitleFont, Brushes.White, 360, 280);
            }


            g.DrawString("CopyRight:  YunDa Technology 2013", Common2D.Font12B, Brushes.White, 420, 350);
        }

        private void YanShi()
        {
            m_JiShuCount++;
            if (m_JiShuCount == 5)
            {
                m_StepIndex++;
                m_JiShuCount = 0;
            }
        }
        #endregion
    }
}