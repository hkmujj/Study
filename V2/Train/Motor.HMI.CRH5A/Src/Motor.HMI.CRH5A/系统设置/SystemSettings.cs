using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH5A.底层共用;

namespace Motor.HMI.CRH5A.系统设置
{
    [GksDataType(DataType.isMMIObjectClass)]
     class SystemOneSettings : CRH5ABase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "系统设置1";
        }
        public override bool Initalize()
        {
            return Init();
        }
         #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void Paint(Graphics g)
        {
      
            DrawOn(g);
        }
        private void DrawOn(Graphics e)
        {
            int index = 0;
            for (index = 0; index < 1; index++)
            {
                e.DrawString(m_NameArr[index], FontsItems.Fonts_Regular( FontName .宋体 ,24f,false ) , SolidBrushsItems.YellowBrush1 ,
                    m_RectsList[index ], FontsItems.TheAlignment(FontRelated.居中));
                
               
                e.DrawRectangle(new Pen(Color.Green,5), Rectangle.Round(m_RectsList[index]));
             

            }
        }
        #endregion

        private bool Init()
        {
         
            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.SystemOneSettings );
            return true;

        }


      
        private List<RectangleF> m_RectsList;


        private string[] m_NameArr =
        { 
            "请输入密码\n\n" +
            "_ _ _ _ _" 
            
          };
    }

    [GksDataType(DataType.isMMIObjectClass)]
    internal class SystemTwoSettings : CRH5ABase
    {
        public override string GetInfo()
        {
            return "系统设置2";
        }

        public override bool Initalize()
        {
            return Init();
        }

        public override void Paint(Graphics g)
        {
            DrawOn(g);

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 11://上翻
                    if (m_SelectedIdx > 0)
                    {
                        m_SelectedIdx = (--m_SelectedIdx) % 5;
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 12://下翻
                    m_SelectedIdx = (++m_SelectedIdx) % 5;
                    ButtonsScreen.BtnState.Press();
                    break;
            }
        }
        private void DrawOn(Graphics e)
        {
            int index = 0;
            for (index = 0; index < m_NameArr.Length; index++)
            {
                e.DrawString(m_NameArr[index], FontsItems.Fonts_Regular( FontName .宋体 ,16f,false )   , SolidBrushsItems.WhiteBrush,
                   m_RectsList[index], FontsItems.TheAlignment(FontRelated.居中 ));

            }


            if (m_SelectedIdx != -1)
            {

                e.DrawRectangle(new Pen(Color.Red), m_RectsList[m_SelectedIdx].X - 40, m_RectsList[m_SelectedIdx].Y, m_RectsList[m_SelectedIdx].Width + 80, m_RectsList[m_SelectedIdx].Height);
            
            }


        }
        private bool Init()
        {
            
            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.SystemTwoSettings );
            return true;

        }

        public override bool OnMouseDown(Point nPoint)
        {
            for (int i = 0; i < m_RectsList.Count; i++)
            {
                if (m_RectsList[i].Contains(nPoint))
                {
                    m_SelectedIdx = i;
                    break;
                }
            }
            return true;
        }

        public override bool OnMouseUp(Point nPoint)
        {
            return true;
        }


        private int m_SelectedIdx = 0;

      
        private List<RectangleF> m_RectsList;


        private string[] m_NameArr ={ "监视器更新","Windows更新","故障更新",
                                   "系统信息","设备地址"};
    }
}
