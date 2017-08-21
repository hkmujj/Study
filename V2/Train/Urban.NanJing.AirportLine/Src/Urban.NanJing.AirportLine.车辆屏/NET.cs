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
    public class Net : baseClass
    {
        private Image[] m_ImageArray;
        private TrainEquipment m_Train;

        public override string GetInfo()
        {
            return "网络";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Train = new TrainEquipment();

            m_ImageArray = new Image[UIObj.ParaList.Count];
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }
            return true;
        }

      
        public override void paint(Graphics g)
        {
            m_Train.OnDraw(g);
            g.DrawImage(m_ImageArray[0], new Rectangle(30, 230, 650, 305));

            #region 第一排
            ///第一排
            int i = 0;
            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[1], new Rectangle(80, 234, m_ImageArray[1].Width, m_ImageArray[1].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[2], new Rectangle(125, 233, m_ImageArray[2].Width, m_ImageArray[2].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[3], new Rectangle(189, 233, m_ImageArray[3].Width, m_ImageArray[3].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[4], new Rectangle(265, 233, m_ImageArray[4].Width, m_ImageArray[4].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[5], new Rectangle(309, 233, m_ImageArray[5].Width, m_ImageArray[5].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[6], new Rectangle(355, 233, m_ImageArray[6].Width, m_ImageArray[6].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[7], new Rectangle(401, 233, m_ImageArray[7].Width, m_ImageArray[7].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[8], new Rectangle(465, 233, m_ImageArray[8].Width, m_ImageArray[8].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[9], new Rectangle(543, 233, m_ImageArray[9].Width, m_ImageArray[9].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[10], new Rectangle(585, 233, m_ImageArray[10].Width, m_ImageArray[10].Height));
            }
            #endregion

            #region 第一列
            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(45, 257, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(39, 281, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(39, 306, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(39, 332, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(39, 358, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(37, 383, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(36, 433, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(36, 455, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(36, 478, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }
            #endregion

            #region 第二列
            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(105, 281, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(108, 306, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(106, 332, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(107, 358, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(108, 383, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(112, 433, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(112, 455, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(112, 478, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }
            #endregion

            #region 第三列
            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 281, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 306, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 345, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 368, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 391, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 412, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 434, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 458, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 480, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(188, 503, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }
            #endregion

            #region 第四列
            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 281, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 306, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 345, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 368, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 391, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 412, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 434, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 458, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 480, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(277, 503, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }
            #endregion

            #region 第五列
            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 281, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 306, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 345, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 368, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 391, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 412, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 434, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 458, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 480, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(374, 503, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }
            #endregion

            #region  第六列
            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 281, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 306, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 345, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 368, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 391, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 412, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 434, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 458, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 480, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(460, 503, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }
            #endregion 

            #region 第七列
            

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(548, 281, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(548, 306, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(548, 358, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(546, 383, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(542, 433, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(542, 455, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(542, 478, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }
           
            #endregion

            #region 第八列
            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(623, 257, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(613, 281, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(613, 306, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(613, 332, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(613, 358, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(613, 383, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(618, 433, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(618, 455, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }

            if (BoolList[987 + 2 * i++])
            {
                g.DrawImage(m_ImageArray[i], new Rectangle(618, 478, m_ImageArray[i].Width, m_ImageArray[i].Height));
            }
            #endregion
        }
    }

}