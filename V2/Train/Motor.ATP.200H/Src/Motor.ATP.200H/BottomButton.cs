using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 底部虚拟硬件按钮 调试用
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class BottomButton : baseClass
    {
        private readonly Button[] m_GButton = new Button[11];
        private readonly string[] m_ButtonText = { "调车\n1", "目视\n2", "启动\n3", "缓解\n4", "上行\n5", "下行\n6", "确定\n7", "8", "9", "0", "警惕/字母" };

        #region  重载方法
        public override string GetInfo()
        {
            return "底部虚拟硬件按钮";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            nErrorObjectIndex = -1;

            return true;
        }

        public override void paint(Graphics g)
        {
            RefreshData();

            OnDraw(g);

            OnButtonEvent();
        }
        #endregion

        #region 私有方法
        private void InitData()
        {
            for (int i = 0; i < 11; i++)
            {
                m_GButton[i] = new Button();
                m_GButton[i].SetButtonColor(192, 192, 192);
                m_GButton[i].SetButtonText(m_ButtonText[i]);
                m_GButton[i].SetButtonRect(2 + 85 * i, 605, 80, 55);
            }
        }

        private void RefreshData()
        { 
        }

        private void  OnDraw(Graphics g)
        {
            for (int index = 0; index < 11; index++)
            {
                m_GButton[index].OnDraw(g);
            }
        }

        private void OnButtonEvent()
        { 

        }
        #endregion

        #region 响应鼠标事件
        /// <summary>
        /// 响应鼠标按下事件
        /// </summary>
        /// <param name="nPoint">鼠标按下时的坐标点</param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            ButtonStatus.ClearManaulUp();

            for (int i = 0; i < 11; i++)
            {
                if (nPoint.X > m_GButton[i].RectPosition.X && nPoint.X <= m_GButton[i].RectPosition.Right &&
                    nPoint.Y > m_GButton[i].RectPosition.Y && nPoint.Y <= m_GButton[i].RectPosition.Bottom)
                {
                    m_GButton[i].OnButtonDown();
                    ButtonStatus.ManualIsBottomButtonDown[i] = true;
                }
            }
            return true;
        }

        /// <summary>
        /// 响应鼠标弹起事件
        /// </summary>
        /// <param name="nPoint">鼠标弹起时的坐标点</param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            ButtonStatus.ClearManaulDown();

            for (int i = 0; i < 11; i++)
            {
                if (nPoint.X > m_GButton[i].RectPosition.X && nPoint.X <= m_GButton[i].RectPosition.Right &&
                    nPoint.Y > m_GButton[i].RectPosition.Y && nPoint.Y <= m_GButton[i].RectPosition.Bottom)
                {
                    m_GButton[i].OnButtonUp();
                    ButtonStatus.ManualIsBottomButtonUp[i] = true;
                }
            }
            return true;
        }
        #endregion
    }
}
