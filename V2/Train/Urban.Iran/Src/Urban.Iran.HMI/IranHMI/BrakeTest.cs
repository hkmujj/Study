using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BrakeTest : HMIBase
    {
        private Bitmap[] m_BmpArr;
        private Rectangle[] m_RectArr;
        private FjButtonEx m_BtnActiveT;
        private readonly List<string> m_StrIndexName = new List<string>()
                                                       {
                                                           "2011_OK", "2011_Feault", "2011_InProgress", "2012_OK", "2012_Feault", "2012_InProgress", "2013_OK", "2013_Feault", "2013_InProgress", "2015_OK", "2015_Feault", "2015_InProgress", "2016_OK", "2016_Feault", "2016_InProgress" 
                                                       };

        private Dictionary<int, int> m_Index;

        private void BtnActiveTMouseDown(FjButtonEx btnSender, Point pt)
        {

        }

        public override string GetInfo()
        {
            return "BrakeTest";
        }

        protected override bool Initalize()
        {
            m_BmpArr = new[]
                     {
                         new Bitmap(RecPath + "\\frame/btOk.jpg"),
                         new Bitmap(RecPath + "\\frame/btFaulty.jpg"),
                         new Bitmap(RecPath + "\\frame/btProgress.jpg"),
                         new Bitmap(RecPath + "\\frame/btNone.jpg")
                     };

            m_RectArr = new[]
                      {
                          new Rectangle(103, 194, 116, 50),
                          new Rectangle(221, 194, 116, 50),
                          new Rectangle(339, 194, 116, 50),
                          new Rectangle(457, 194, 116, 50),
                          new Rectangle(575, 194, 116, 50),
                          new Rectangle(100, 402, 697, 64)
                      };
            m_BtnActiveT = new FjButtonEx(1, "Activate Test", GlobleRect.m_LegendbtnRect);
            m_BtnActiveT.MouseDown += BtnActiveTMouseDown;

            UpdateUiobject(CommunicationIndexType.InBool, m_StrIndexName);

            return true;
        }

        public override void paint(Graphics g)
        {
            if (m_Index == null)
            {
                m_Index = m_StrIndexName.ToDictionary(s => (int)m_StrIndexName.IndexOf(s), s => Convert.ToInt32(GlobleParam.Instance.InBoolDictionary[s]));
            }
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[m_Index[i * 3]])
                {
                    g.DrawImage(m_BmpArr[1], m_RectArr[i]);
                }
                else if (BoolList[m_Index[i * 3 + 1]])
                {
                    g.DrawImage(m_BmpArr[0], m_RectArr[i]);
                }
                else if (BoolList[m_Index[i * 3 + 2]])
                {
                    g.DrawImage(m_BmpArr[2], m_RectArr[i]);
                }
                else
                {
                    g.DrawImage(m_BmpArr[3], m_RectArr[i]);
                }

            }
            m_BtnActiveT.OnDraw(g);
            g.DrawRectangle(GdiCommon.DarkGreyPen, m_RectArr[5]);
            g.DrawString("WSP test not allowed", GdiCommon.Txt14Font, GdiCommon.MediumGreyBrush, m_RectArr[5], GdiCommon.LeftFormat);
        }
       
        public override bool mouseDown(Point point)
        {
            if (m_BtnActiveT.IsVisible(point))
                m_BtnActiveT.OnMouseDown(point);
            return false;
        }
    }
}