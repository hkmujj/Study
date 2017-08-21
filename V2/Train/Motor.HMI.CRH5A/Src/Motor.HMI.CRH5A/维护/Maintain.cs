using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5A.底层共用;

namespace Motor.HMI.CRH5A.维护
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MaintainOneScreen : CRH5ABase 
    {
         public override string GetInfo()
        {
            return "维护视图1";
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
                    if (m_SelectedIdx != 0)
                    {
                        m_SelectedIdx = (--m_SelectedIdx)%7;
                    }
                    else
                    {
                        append_postCmd(CmdType.ChangePage, 43, 0, 0);
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 12://下翻
                    if (m_SelectedIdx < 6)
                    {
                        m_SelectedIdx = (++m_SelectedIdx)%7;
                    }
                    else
                    {
                        append_postCmd(CmdType.ChangePage, 42, 0, 0);
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
            }
        }
        private void DrawOn(Graphics e)
        {
            int index = 0;
            for (index = 0; index < m_NameArr.Length; index++)
            {
                e.DrawString(m_NameArr[index], FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush ,
                   m_RectsList[index], FontsItems.TheAlignment(FontRelated.居中));

            }


            if (m_SelectedIdx != -1)
            {
                
                    e.DrawRectangle(new Pen(Color.Red, 4), m_RectsList[m_SelectedIdx].X - 40, m_RectsList[m_SelectedIdx].Y,
                        m_RectsList[m_SelectedIdx].Width + 80, m_RectsList[m_SelectedIdx].Height);
                
            }
          


        }
        private bool Init()
        {
           
            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.MaintainScreen);
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


        private int m_SelectedIdx;

       
        private List<RectangleF> m_RectsList;

        private string[] m_NameArr ={ "车钩","喷水系统","喷水系统",
                                   "CCT","制动器","照明","信号系统"};
      

    
    }
    [GksDataType(DataType.isMMIObjectClass)]
    class MaintainTwoScreen : CRH5ABase
    {
        public override string GetInfo()
        {
            return "维护视图2";
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
                    if (m_SelectedIdx != 0)
                    {
                        m_SelectedIdx = (--m_SelectedIdx) % 7;
                    }
                    else
                    {
                        append_postCmd(CmdType.ChangePage, 41, 0, 0);
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 12://下翻
                    if (m_SelectedIdx < 6)
                    {
                        m_SelectedIdx = (++m_SelectedIdx) % 7;
                    }
                    else
                    {
                        append_postCmd(CmdType.ChangePage, 43, 0, 0);
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
            }
        }
        private void DrawOn(Graphics e)
        {
            int index = 0;
            for (index = 0; index < m_NameArr.Length; index++)
            {
                e.DrawString(m_NameArr[index], FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush,
                   m_RectsList[index], FontsItems.TheAlignment(FontRelated.居中));

            }


            if (m_SelectedIdx != -1)
            {
                
                    e.DrawRectangle(new Pen(Color.Red, 4), m_RectsList[m_SelectedIdx].X - 40, m_RectsList[m_SelectedIdx].Y,
                        m_RectsList[m_SelectedIdx].Width + 80, m_RectsList[m_SelectedIdx].Height);
                
            }


        }
        private bool Init()
        {
            
            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.MaintainScreen);
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


        private int m_SelectedIdx;

      
        private List<RectangleF> m_RectsList;

        private string[] m_NameArr ={ "牵引","CC C","空调系统",
                                   "PIS","车门","卫生间","充电机"};



    }
    [GksDataType(DataType.isMMIObjectClass)]
    class MaintainThreeScreen : CRH5ABase
    {
        public override string GetInfo()
        {
            return "维护视图3";
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
                    if (m_SelectedIdx < 0)
                    {
                        m_SelectedIdx = (--m_SelectedIdx) % 1;
                    }
                    else
                    {
                        append_postCmd(CmdType.ChangePage, 41, 0, 0);
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 12://下翻
                    if (m_SelectedIdx < 0)
                    {
                        m_SelectedIdx = (++m_SelectedIdx) % 1;
                    }
                    else
                    {
                        append_postCmd(CmdType.ChangePage, 42, 0, 0);
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
            }
        }
        private void DrawOn(Graphics e)
        {
            int index = 0;
            for (index = 0; index < m_NameArr.Length; index++)
            {
                e.DrawString(m_NameArr[index], FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush,
                   m_RectsList[index], FontsItems.TheAlignment(FontRelated.居中));

            }


            if (m_SelectedIdx != -1)
            {

                e.DrawRectangle(new Pen(Color.Red, 4), m_RectsList[m_SelectedIdx].X - 40, m_RectsList[m_SelectedIdx].Y, m_RectsList[m_SelectedIdx].Width + 80, m_RectsList[m_SelectedIdx].Height);

            }


        }
        private bool Init()
        {
            

            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.MaintainScreen);
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

        private string[] m_NameArr ={ "HADS"
                                   };



    }
}
