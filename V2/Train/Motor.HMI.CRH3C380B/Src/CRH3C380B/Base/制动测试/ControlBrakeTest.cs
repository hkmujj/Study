using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH3C380B.Base.制动测试.TestItems;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.制动测试
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ControlBrakeTest : CRH3C380BBase
    {
        /// <summary>
        /// 所有坐标
        /// </summary>
        private List<RectangleF> m_RectsList;


        /// <summary>
        /// 计时器
        /// </summary>
        public TheTimeCounter m_TheTimeCounter;


        private BrakeTestControl m_CurrentBrakeTestControl;

                
        private void UpdateCurrentBrakeTest(BrakeTestControl value)
        {
            if (m_CurrentBrakeTestControl != value)
            {
                m_CurrentBrakeTestControl = value;
                if (m_CurrentBrakeTestControl != null)
                {
                    m_CurrentBrakeTestControl.ResetStates();
                }
            }
        }

        /// <summary>
        /// 制动试验时间
        /// </summary>
        public static readonly string[] BrakeTestTime = {"", "", "", "", "", ""};

        private Dictionary<BrakeTestType, BrakeTestControl> m_BrakeTestControlDictionary;

        //2
        public override string GetInfo()
        {
            return "控件-制动试验";
        }

        //6
        public override bool Initalize()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakeTest, ref m_RectsList))
            {
            }

            m_TheTimeCounter = new TheTimeCounter();

            m_BrakeTestControlDictionary = new Dictionary<BrakeTestType, BrakeTestControl>
            {
                {
                    BrakeTestType.DirectBrake,
                    new TestOfDirectBrake(BrakeTestType.DirectBrake, this)
                    {
                        OutLineRectangle = Rectangle.Ceiling(m_RectsList[23])
                    }
                },
                {
                    BrakeTestType.EmergencyBrake,
                    new TestOfEmergencyBrake(BrakeTestType.EmergencyBrake, this)
                    {
                        OutLineRectangle = Rectangle.Ceiling(m_RectsList[23])
                    }
                },
                {
                    BrakeTestType.MrpContinuity,
                    new TestOfMrpContinuity(BrakeTestType.MrpContinuity, this)
                    {
                        OutLineRectangle = Rectangle.Ceiling(m_RectsList[24])
                    }
                },
                {
                    BrakeTestType.BpLeakage,
                    new TestOfBpLeakage(BrakeTestType.BpLeakage, this)
                    {
                        OutLineRectangle = Rectangle.Ceiling(m_RectsList[23])
                    }
                },
                {
                    BrakeTestType.IndirectBrake,
                    new TestOfIndirectBrake(BrakeTestType.IndirectBrake, this)
                    {
                        OutLineRectangle = Rectangle.Ceiling(m_RectsList[23])
                    }
                },
                {
                    BrakeTestType.BpContinuity,
                    new TestOfBpContinuity(BrakeTestType.BpContinuity, this)
                    {
                        OutLineRectangle = Rectangle.Ceiling(m_RectsList[25])
                    }
                },
            };

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2)
            {
                return;
            }
            UpdateCurrentBrakeTest(null);
            var bt = (BrakeTestType) nParaB;
            if (m_BrakeTestControlDictionary.ContainsKey(bt))
            {
                UpdateCurrentBrakeTest(m_BrakeTestControlDictionary[bt]);
            }
        }

        public override void Paint(Graphics g)
        {
            if (m_CurrentBrakeTestControl != null)
            {
                m_CurrentBrakeTestControl.ResponseBtnEvent();

                m_CurrentBrakeTestControl.OnPaint(g);
            }
        }
    }
}
