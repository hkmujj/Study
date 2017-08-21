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
    class Bow : baseClass
    {
        private RectText m_Bow1;
        private RectText m_Bow2;
        private Image[] m_ImageArray;

        public override string GetInfo()
        {
            return "受电弓状态信息";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            m_ImageArray = new Image[UIObj.ParaList.Count];
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }
            m_Bow1 = new RectText(new Rectangle(300 + Common.m_InitialPosX, 125 + Common.m_InitialPosY, 50, 40), "", 1, 1, Color.White, Common.m_BackGroundColor, Color.White, 1, false);
            m_Bow2 = new RectText(new Rectangle(515 + Common.m_InitialPosX, 125 + Common.m_InitialPosY, 50, 40), "", 1, 1, Color.White, Common.m_BackGroundColor, Color.White, 1, false);
            return true;
        }

        private void GetValue()
        {
            #region 设置受电弓
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + 5 * i + j])
                    {
                        if (i == 0)
                        {
                            m_Bow1.ImagePicture = m_ImageArray[j];
                        }
                        else
                        {
                            m_Bow2.ImagePicture = m_ImageArray[j];
                        }
                    }
                }
            }
            #endregion
        }

        public override void paint(Graphics g)
        {
            GetValue();
            m_Bow1.OnDraw(g);
            m_Bow2.OnDraw(g);
        }
    }
}
