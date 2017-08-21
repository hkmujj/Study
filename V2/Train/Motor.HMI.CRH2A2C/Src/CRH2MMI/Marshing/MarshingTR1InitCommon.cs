using System.Drawing;
using CRH2MMI.Common.Util;

namespace CRH2MMI.Marshing
{
    internal class MarshingTR1InitCommon
    {
        private MarshingItemView view;
        public MarshingTR1InitCommon(MarshingItemView obj)
        {

            view = obj;
            obj.m_MarshingTexts = new RectTextInfo[9];

            for (int i = 0; i < 9; i++)
            {
                obj.m_MarshingTexts[i] = new RectTextInfo();
            }
            for (int i = 0; i < 5; i++)
            {
                obj.m_MarshingTexts[i].SetRectTextInfo(25 + 62.5f * i, 145, 30, 205, 0, "", 2, 5, 2, 14);
            }
            for (int i = 0; i < 3; i++)
            {
                obj.m_MarshingTexts[i + 5].SetRectTextInfo(608 + 62.5f * i, 145, 30, 205, 0, "", 2, 5, 2, 14);
            }
            obj.m_MarshingTexts[8].SetRectTextInfo(733, 358, 30, 167, 0, "", 2, 5, 2, 14);

            var marshingStr = new string[9];
            marshingStr[0] = " 联挂准备";
            marshingStr[1] = " 打开头罩锁";
            marshingStr[2] = " 打开头罩";
            marshingStr[3] = " 锁住头罩";
            marshingStr[4] = " 联挂准备就绪";
            marshingStr[5] = " 密接连杆退回";
            marshingStr[6] = " 总风管气压开关";
            marshingStr[7] = " 联挂完成";
            marshingStr[8] = " 密接连接不良";



            for (int i = 0; i < 9; i++)
            {
                obj.m_MarshingTexts[i].SetRectStr(marshingStr[i]);
            }
        }

        public void OnDraw(Graphics g)
        {
            
        }
    }
}