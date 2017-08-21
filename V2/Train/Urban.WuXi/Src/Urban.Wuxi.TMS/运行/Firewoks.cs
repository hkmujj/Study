using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.运行
{
    /// <summary>
    /// 烟火报警
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class Firewoks : TMSBaseClass
    {
        #region //列表，数组，变量的定义
        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] m_Imgs;
        /// <summary>
        /// 矩形集
        /// </summary>
        private RectangleF[] m_Rect;
        /// <summary>
        /// 按钮相关框
        /// </summary>
        private Rectangle[] m_BtnRectArrs;
        /// <summary>
        /// 按钮是否能按下
        /// </summary>
        private bool[] m_IsbuttonDown;
  
        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> m_Regions;
        /// <summary>
        /// bool逻辑号
        /// </summary>
        private List<int> m_BoolIds;


        #endregion
      

        #region :::::::::::::::::::::::::: 重载 :::::::::::::::::::::::::

        public override string GetInfo()
        {
            return "烟火报警";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            DrawOn(dcGs);
            DrawButton(dcGs);
        }

       /// <summary>
       /// 按钮按下发送逻辑值
       /// </summary>
       /// <param name="nPoint"></param>
       /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {   
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }
            if (index==7)
            {
                m_IsbuttonDown[7] = true;
            }
          
            if (index<7)//发送逻辑值
            {
                m_IsbuttonDown[index] = true;
                OnPost(CmdType.SetBoolValue, 1680 + index, 1, 0); 
            }
                   
            return base.mouseDown(nPoint);
        }
        /// <summary>
        /// 按钮弹起时不发送值
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < m_Regions.Count; ++index)
            {
                if (m_Regions[index].IsVisible(nPoint))
                    break;
            }
            if (index == 7)//返回运行界面
            {
                m_IsbuttonDown[7] = false;
                OnPost(CmdType.ChangePage, 11, 1, 0);
            }

            if (index < 7)
            {
                m_IsbuttonDown[index] = false;
                OnPost(CmdType.SetBoolValue, 1680 + index, 0, 0);
            }
                    

            return base.mouseUp(nPoint);
           
        }

        # endregion:::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        /// <summary>
        /// 画烟火报警界面各车门的显示状态
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawOn(Graphics e)
        {
            e.DrawImage(m_Imgs[0], m_Rect[0]);
            //烟火报警
            for (int i=0;i<6;i++)
            {
                if (BoolList[m_BoolIds[0 + i * 3]])

                    e.DrawImage(m_Imgs[4], m_Rect[i + 1]);

                else if (BoolList[m_BoolIds[1 + i * 3]])
                {
                    e.DrawImage(m_Imgs[1], m_Rect[i + 1]);

                }

                else if (BoolList[m_BoolIds[2 + i * 3]])
                {
                    e.DrawImage(m_Imgs[2], m_Rect[i + 1]);

                }
                else
                {
                    e.DrawImage(m_Imgs[3], m_Rect[i + 1]);
                }
            }
            
            //for (int i = 0; i < 3; i++)
            //{
            //    if (BoolList[_boolIds[0+i*3]])

            //        e.DrawImage(_imgs[4], _rect[i + 1]);

            //    else if (BoolList[_boolIds[1 + i * 3]])

            //    {
            //        e.DrawImage(_imgs[1], _rect[i + 1]);

            //    }

            //    else if (BoolList[_boolIds[2 + i * 3]])

            //    {
            //        e.DrawImage(_imgs[2], _rect[i + 1]);

            //    }
            //    else
            //    {
            //        e.DrawImage(_imgs[3], _rect[i + 1]);
            //    }
            //}
            ////烟火报警

            //for (int i = 0; i < 3; i++)
            //{
            //    if (BoolList[_boolIds[9 + i * 3]])

            //        e.DrawImage(_imgs[4], _rect[i + 4]);

            //    else if (BoolList[_boolIds[10 + i * 3]])

            //        e.DrawImage(_imgs[1], _rect[i + 4]);
            //    else if (BoolList[_boolIds[11 + i * 3]])

            //        e.DrawImage(_imgs[2], _rect[i + 4]);
            //    else
            //    {
            //        e.DrawImage(_imgs[3], _rect[i + 4]);
            //    }
            //}
            //按钮框
            for (int i = 0; i < 3; i++)
            {
            e.DrawRectangle(FormatStyle.m_WhitePen,m_BtnRectArrs[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                e.DrawRectangle(FormatStyle.m_WhitePen, m_BtnRectArrs[i+3]);
            }
            //整车重启
            e.DrawRectangle(FormatStyle.m_WhitePen,m_BtnRectArrs[6]);
            //返回
            e.DrawRectangle(FormatStyle.m_WhitePen,m_BtnRectArrs[7]);
            //烟火报警帮助
            e.DrawImage(m_Imgs[7],m_Rect[7]);
          
    }
       /// <summary>
       /// 画按钮框，按下和弹起，颜色的不同
       /// </summary>
       /// <param name="e">画图对象</param>
        private void DrawButton(Graphics e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (m_IsbuttonDown[i])
                {
                    e.DrawImage(m_Imgs[6], m_BtnRectArrs[i]);
                }
                else
                {
                    e.DrawImage(m_Imgs[5], m_BtnRectArrs[i]);
                }
            }

            for (int i = 0; i < 8; i++)
            {
               e.DrawString(FormatStyle.m_Str29[i],FormatStyle.m_Font12,FormatStyle.m_BlackBrush,m_BtnRectArrs[i].X+17,m_BtnRectArrs[i].Y+15); 
            }
            
        
        }
       
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            #region//对象列表初始化
            m_BtnRectArrs = new Rectangle[20];
            m_IsbuttonDown = new bool[8];
            m_Rect = new RectangleF[20];
            m_Imgs = new Image[UIObj.ParaList.Count];
            m_Regions = new List<Region>();
            m_BoolIds = new List<int>();
            #endregion

            #region  //从对象配置表取东西
            for (int i = 0; i < m_Imgs.Length; i++)
            {
                m_Imgs[i] = Image.FromFile(Path.Combine(RecPath , UIObj.ParaList[i]));
            }

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    m_BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            #endregion

            #region //列表，数组的赋值

            //整个门状态底片位置
            for (int i = 0; i < 1; i++)
            {
                m_Rect[i] = new RectangleF(55, 105, 650, 230);
            }
            //Tc1 M11 M21门状态的位置
            for (int i = 0; i < 3; i++)
            {
                m_Rect[i + 1] = new RectangleF(215 + i * 175, 142, 20, 20);
            }
            //M22,M21,TC2门状态的位置
            for (int i = 0; i < 3; i++)
            {
                m_Rect[i + 4] = new RectangleF(215 + i * 175, 255, 20, 20);
            }
            //烟火帮助
            m_Rect[7] = new RectangleF(155, 475, 200, 60);
            //TC1/M11/M21/M12/M21/TC2火灾重启按钮位置
            for (int i = 0; i < 3; i++)
            {
                m_BtnRectArrs[i] = new Rectangle(155 + i * 123, 372, 120, 40);
            }
            for (int i = 0; i < 3; i++)
            {
                m_BtnRectArrs[i + 3] = new Rectangle(155 + i * 123, 415, 120, 40);
            }
            //整车火灾重启

            m_BtnRectArrs[6] = new Rectangle(532, 400, 120, 40);
            //还回
            m_BtnRectArrs[7] = new Rectangle(558, 490, 80, 40);
            //按钮位置放在集合内
            for (int i = 0; i < 8; i++)
            {
                m_Regions.Add(new Region(m_BtnRectArrs[i]));
            }
        

           
            #endregion
           
        
           
        }
    }  
}
