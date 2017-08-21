using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    class State : baseClass
    {
        private StateButton[] m_StateButtonArray = new StateButton[5];
        private string[] m_LabelArray = new string[5] { "准备", "运行", "信息", "广播系统", "维护" };
        private int[] m_MessageArray = { 1 ,2,3,4,33};

        private Rectangle m_Back;
        private int m_SelectIndex;
        private int m_OldSelectIndex = -1;

        private List<StateButton> m_CurrentStateButtonList = new List<StateButton>();

        public override string GetInfo()
        {
            return "下部状态";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Back = new Rectangle(Common.m_InitialPosX, 558 + Common.m_InitialPosY, 800, 42);
            for (int i = 0; i < 5; i++)
            {
                m_StateButtonArray[i] = new StateButton(new Rectangle(18 + Common.m_InitialPosX + 152 * i, 558 + Common.m_InitialPosY, 164, 42), m_LabelArray[i], m_MessageArray[i]);
                m_StateButtonArray[i].MouseUpEvent += OnMouseUpHandler;
            }
            return true;
        }

        private void OnMouseUpHandler(int meaage)
        {
            append_postCmd(CmdType.ChangePage, meaage, 0, 0);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            m_SelectIndex = nParaB - 1;

            if (m_SelectIndex != m_OldSelectIndex)
            {
                if (m_SelectIndex < 5 && m_SelectIndex >= 0)
                {
                    m_CurrentStateButtonList.Clear();
                    for (int i = m_SelectIndex - 1; i >= 0; i--)
                    {
                        m_StateButtonArray[i].IsSelected = false;
                        m_CurrentStateButtonList.Add(m_StateButtonArray[i]);
                    }

                    for (int i = 4; i > m_SelectIndex; i--)
                    {
                        m_StateButtonArray[i].IsSelected = false;
                        m_CurrentStateButtonList.Add(m_StateButtonArray[i]);
                    }

                    m_CurrentStateButtonList.Add(m_StateButtonArray[m_SelectIndex]);
                    m_StateButtonArray[m_SelectIndex].IsSelected = true;

                    m_OldSelectIndex = m_SelectIndex;
                }
                else
                {
                    m_CurrentStateButtonList.Clear();
                    for (int i = 0; i < m_StateButtonArray.Length; i++)
                    {
                        m_StateButtonArray[i].IsSelected = false;
                        m_CurrentStateButtonList.Add(m_StateButtonArray[i]);
                    }
                    m_OldSelectIndex = m_SelectIndex;
                }
            }
        }





        private void GetVlaue()
        {

        }


        public override void paint(Graphics g)
        {
            GetVlaue();
            g.FillRectangle(Common.m_BlackBrush, m_Back);
            foreach (var v in m_CurrentStateButtonList)
            {
                v.Paint(g);
            }
        }


        public override bool mouseUp(Point point)
        {
            for (int i = 0; i < 5; i++)
            {
                m_StateButtonArray[i].MouseUp(point);
            }
            return true;
        }


        public override bool mouseDown(Point point)
        {
            return true;
        }
    }
}
