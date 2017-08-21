using System.Collections.Generic;
using System.Drawing;

namespace Urban.QingDao3Line.MMI
{     
       public class GeneralData:NewQBaseclass
       {
           private static readonly List<Rectangle> m_Rects=new List<Rectangle>();
           public static List<Rectangle> Rects 
           {
               get { return m_Rects; }
               set { Rects = value; } 
           }
           public override bool init(ref int nErrorObjectIndex)
           {
               RectData();
               return true;
           }
           public void RectData()
           {
 
               //上排左侧四个  /0-3
               for (int i=0;i<4;i++)
               {
                   m_Rects.Add(new Rectangle(22,149+i*23,164,23));
               }
               //下排左侧四个 /4-7
               for (int i=0;i<4;i++)
               {
                   m_Rects.Add(new Rectangle(22, 293 + i*23, 164, 23));
               }
               //上排右侧矩形框  /8-32
               for (int i=0;i<5;i++)
               {
                   for (int j=0;j<5;j++)
                   {
                       m_Rects.Add(new Rectangle(186+j*111,126+i*23,111,23));
                   }
               }
               //下排右侧矩形框   /33-57
               for (int i = 0; i < 5; i++)
               {
                   for (int j = 0; j < 5; j++)
                   {
                       m_Rects.Add(new Rectangle(186 + j * 111, 270 + i * 23, 111, 23));
                   }
               }
               //58-59   /已消耗能量和已再生能量
               for (int i=0;i<2;i++)
               {
                   m_Rects.Add(new Rectangle(120+i*300,445,210,60));
               }
               //total 60
               m_Rects.Add(new Rectangle(50,465,57,23));
               //Energy Consumed 61
               m_Rects.Add(new Rectangle(153, 411, 117, 19));
               //EnergyRegenerated 62
               m_Rects.Add(new Rectangle(457, 411, 127, 19));
               //速度 63
               m_Rects.Add(new Rectangle(664, 443, 80, 94));
                //64-73上排右侧矩形框里面
               for (int i = 0; i < 2; i++)
               {
                   for (int j = 0; j < 5; j++)
                   {
                       m_Rects.Add(new Rectangle(232 + j * 110, 149 + i * 22, 24, 24));
                   }
               }
               //74-83下排右侧矩形框里面
               for (int i = 0; i < 2; i++)
               {
                   for (int j = 0; j < 5; j++)
                   {
                       m_Rects.Add(new Rectangle(232 + j * 110, 293 + i * 22, 24, 24));
                   }
               }
               //速度值显示  /84
               m_Rects.Add(new Rectangle(660, 440, 80, 80));
               //维护：基本信息  /85
               m_Rects.Add(new Rectangle(350, 90, 150, 30));
           }
       }
}
