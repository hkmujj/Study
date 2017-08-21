using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class HX_Unalert:baseClass
    {
        private bool m_IsShow;//该界面是否显示
        private HxRectText m_NoText;
        private bool m_Status1;
        private bool m_Status2;
        public  int m_Number = 20;

        public override string GetInfo()
        {
            return "无人警惕";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            InitData();
          
            nErrorObjectIndex = -1;

            return true;
        }

        public void InitData()
        {
            m_NoText = new HxRectText();
            m_NoText.SetBkColor(255, 255, 0);
            m_NoText.SetTextColor(255, 0, 0);
            m_NoText.SetTextRect(200, 150, 350, 240);
            m_NoText.SetTextStyle(58, FormatStyle.Center, true, "宋体");
        
        }

        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
            base.paint(g);
        }

        public void DrawOn(Graphics g)
        {
            if (DateTime.Now.Second % 2 == 0)
            {
                m_Status1 = false;
            }
            else
            {
               m_Status1=true;
            }


            if (m_Status2!=m_Status1)
            {
                m_Status2=m_Status1;
                
                  m_NoText.SetText(m_Number.ToString());
                   if (m_Number > 0)
                    {
                     m_Number--;
                    }
            }

            if (m_IsShow)
            {
                m_NoText.OnDraw(g);
                g.DrawString("警惕", new Font("宋体",30), Brushes.Red, 325, 180);
            }
            else
            {
                m_Number = 20;
            }
           
                 
        }

        public void GetValue()
        {
            m_IsShow = BoolList[UIObj.InBoolList[0]];

            
        }

    }
}
