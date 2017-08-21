using System;
using System.Drawing;
using CommonUtil.Controls;

using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.停放制动;
using Motor.HMI.CRH3C380B.Base.底层共用;

namespace Motor.HMI.CRH3C380B.Base.制动测试
{
    public abstract class BrakeTestControl : CommonInnerControlBase
    {
        public BrakeTestType BrakeTestType { private set; get; }

        public abstract string TitleName { get; }

        /// <summary>
        /// 步骤编号，-1为初始值
        /// </summary>
        protected int m_Step = -1;

        /// <summary>
        /// 当前测试界面文字显示 标题
        /// </summary>
        protected string m_Content0;

        /// <summary>
        /// 当前测试界面文字显示 内容
        /// </summary>
        protected string m_Content1;

        /// <summary>
        /// 按键内容
        /// </summary>
        public string[] BtnContens { protected set; get; }

        protected ControlBrakeTest m_SrcObj;

        public DMIButton DMIButton
        {
            get { return m_SrcObj.DMIButton; }
        }

        public DMITitle DMITitle
        {
            get { return m_SrcObj.DMITitle; }
        }

        public DMIParkingBrakes DmiParkingBrakes
        {
            get { return m_SrcObj.DmiParkingBrakes; }
        }

        protected TheTimeCounter m_TheTimeCounter { get { return m_SrcObj.m_TheTimeCounter; } }

        protected BrakeTestControl(BrakeTestType brakeTestType, ControlBrakeTest srcObj)
        {
            BrakeTestType = brakeTestType;
            m_SrcObj = srcObj;
            BtnContens = new[] { "开始试验", "", "", "", "", "", "", "制动试验", "", "制动状态", };
        }

        public sealed override void OnDraw(Graphics g)
        {
            UpdateContents();

            g.FillRectangle(SolidBrushsItems.WhiteBrush, OutLineRectangle);
            g.DrawString(m_Content0 + m_Content1, FontsItems.FontC14, SolidBrushsItems.BlackBrush,
                OutLineRectangle, FontsItems.TheAlignment(FontRelated.靠左上));
        }

        public virtual void ResetStates()
        {
            InitCommonValue();
        }

        public virtual void ResponseBtnEvent()
        {
            ResponseReturnBtn();
        }

        protected virtual void UpdateContents()
        {

        }

        protected bool ResponseReturnBtn()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 6:
                        if (m_Step == -1)
                        {
                            ResetValuesWhenStartTest();
                        }
                        break;
                    case 13:
                        m_SrcObj.append_postCmd(CmdType.ChangePage, 250, 0, 0);
                        return true;
                    case 15:
                        m_SrcObj.append_postCmd(CmdType.ChangePage, 21, 0, 0);
                        return true;
                }
                return false;
            }
            return true;
        }

        private void ResetValuesWhenStartTest()
        {
            m_Step = 0;
            DMITitle.BtnContentList[0].BtnStr = "";
            ResetWhenStartTest();
        }

        protected virtual void ResetWhenStartTest()
        {

        }

        private void InitCommonValue()
        {
            m_Step = -1;
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = BtnContens[i];
            }
            m_TheTimeCounter.ResetCounter();
        }

        protected string TestOverTime()
        {
            return string.Format("{0}.{1}.{2} {3}:{4}", CurrentTime.Year, CurrentTime.Month.ToString().PadLeft(2, '0'),
                CurrentTime.Day.ToString().PadLeft(2, '0'), CurrentTime.Hour, CurrentTime.Minute);
        }

        protected int GetInBoolIndex(string name)
        {
            return m_SrcObj.GetInBoolIndex(name);
        }
        protected bool GetInBoolValue(string name)
        {
            return m_SrcObj.GetInBoolValue(name);
        }
        protected int GetInFloatIndex(string name)
        {
            return m_SrcObj.GetInFloatIndex(name);
        }
        protected float GetInFloatValue(string name)
        {
            return m_SrcObj.GetInFloatValue(name);
        }
        protected int GetOutBoolIndex(string name)
        {
            return m_SrcObj.GetOutBoolIndex(name);
        }
        protected bool GetOutBoolValue(string name)
        {
            return m_SrcObj.GetOutBoolValue(name);
        }
        protected int GetOutFloatIndex(string name)
        {
            return m_SrcObj.GetOutFloatIndex(name);
        }
        protected float GetOutFloatValue(string name)
        {
            return m_SrcObj.GetInBoolIndex(name);
        }

        public void append_postCmd(CmdType nCT, int nParaA, int nParaB, float nParaC)
        {
            m_SrcObj.append_postCmd(nCT, nParaA, nParaB, nParaC);
        }

        /// <summary>
        /// 制动试验时间
        /// </summary>
        public string[] BrakeTestTime { get { return ControlBrakeTest.BrakeTestTime; } }

        protected DateTime CurrentTime { get { return m_SrcObj.CurrentTime; } }

        protected BrakeTestResult BrakeTestResult { get; set; }
    }
}