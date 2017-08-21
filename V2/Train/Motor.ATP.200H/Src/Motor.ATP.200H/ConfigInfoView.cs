using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class ConfigInfoView:baseClass
    {
        private RectText m_GText;
        
        #region  重载方法
        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "信息确认界面";
        }

        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
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
            RefreshData();
            OnDraw(dcGs);
            ButtonDownEvent();
        }
        #endregion

        private void  InitData()
        {         
                m_GText = new RectText();
                m_GText.SetBkColor(0, 0, 0);
                m_GText.SetTextColor(255, 255, 255);
                m_GText.SetTextStyle(16, FormatStyle.Center, true, "宋体");
                m_GText.SetTextRect(Common2D.RectF[7].X + 2, Common2D.RectF[7].Y + 2, Common2D.RectF[7].Width - 4,
                Common2D.RectF[7].Height - 4);
                m_GText.SetText("确认");
        }

        private void RefreshData()
        {
 
        }

        private void OnDraw(Graphics g)
        {
            //G_Text.OnDraw();
        }

        private void ButtonDownEvent()
        { 
        }
    }
}
