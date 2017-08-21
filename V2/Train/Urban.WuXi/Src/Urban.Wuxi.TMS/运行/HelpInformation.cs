using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.Wuxi.TMS.运行
{
    /// <summary>
    /// 
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class HelpInformation : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "提示信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadNotifyInfo();

            InitData();
            return true;
        }

        private void LoadNotifyInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "消息通知.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                String[] split = cStr.Split(new char[] { '\t' });
                if (split.Length == 3 && split[0] == "msg")
                {
                    switch (split[1])
                    {
                        case "1":
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                        case "5":
                            break;
                    }
                }
            }
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics g)
        {
            DrawOn(g);
            m_Collection.ForEach(f => f.OnPaint(g));
        }
        /// <summary>
        /// 鼠标按下的状态
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < m_Rect.Count; ++index)
            {
                if (m_Rect[index].IsVisible(point))
                {
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = true;
                    break;
                case 1:
                    m_ButtonIsDown[1] = true;
                    break;
                case 2:
                    m_ButtonIsDown[2] = true;
                    break;
                case 3:
                    m_ButtonIsDown[3] = true;
                    break;
                case 4:
                    m_ButtonIsDown[4] = true;
                    break;
                case 5:
                    m_ButtonIsDown[5] = true;
                    break;
                case 6:
                    m_ButtonIsDown[6] = true;
                    break;
                default:
                    break;
            }
            return base.mouseDown(point);
        }
        /// <summary>
        /// 鼠标弹起的状态
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < m_Rect.Count; ++index)
            {
                if (m_Rect[index].IsVisible(point))
                {
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = false;
                    m_MenuID = 0;
                    break;
                case 1:
                    m_ButtonIsDown[1] = false;
                    m_MenuID = 1;
                    break;
                case 2:
                    m_ButtonIsDown[2] = false;
                    m_MenuID = 2;
                    break;
                case 3:
                    m_ButtonIsDown[3] = false;
                    m_MenuID = 3;
                    break;
                case 4:
                    m_ButtonIsDown[4] = false;
                    m_MenuID = 4;
                    break;
                case 5:
                    m_ButtonIsDown[5] = false;
                    break;
                case 6:
                    m_ButtonIsDown[6] = false;
                    OnPost(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
            return base.mouseUp(point);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrame(Graphics g)
        {


            g.DrawString(FormatStyle.m_Str21[m_MenuID], FormatStyle.m_Font14B,
                FormatStyle.m_WhiteBrush, m_Rects[9], m_DrawFormat);

        }

        /// <summary>
        /// 填充数值
        /// </summary>
        /// <param name="g"></param>
        private void DrawValue(Graphics g)
        {
            for (int i = 0; i < 5; i++)
            {
                if (m_HelpInfoDic[(HelpInfoMenu)i].Any(a=>BoolList[a]))
                {
                    g.DrawImage(m_ButtonIsDown[i] ? m_Images[3] : m_Images[2], m_Rects[i]);
                }
                else
                {
                    g.DrawImage(m_ButtonIsDown[i] ? m_Images[1] : m_Images[0], m_Rects[i]);
                }
                
            }
            for (int i = 5; i < 7; i++)
            {
                g.DrawImage(m_ButtonIsDown[i] ? m_Images[1] : m_Images[0], m_Rects[i]);
            }

            for (int i = 0; i < 7; i++)
            {
                g.DrawString(FormatStyle.m_Str20[i], FormatStyle.m_Font12B,
                    FormatStyle.m_BlackBrush, m_Rects[i], m_DrawFormat);
            }
        }


        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        private void DrawOn(Graphics g)
        {
            DrawFrame(g);
            DrawValue(g);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            m_DrawFormat.LineAlignment = (StringAlignment)1;
            m_DrawFormat.Alignment = (StringAlignment)1;

            m_RightFormat.LineAlignment = (StringAlignment)1;
            m_RightFormat.Alignment = (StringAlignment)2;

            m_LeftFormat.LineAlignment = (StringAlignment)0;
            m_LeftFormat.Alignment = (StringAlignment)1;

            m_PDrawPoint = new PointF[200];

            m_Rects = new RectangleF[200];

            m_Images = new Image[30];

            m_ButtonIsDown = new bool[30];

            m_Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: _rects :::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 7; i++)
            {
                m_Rects[i] = new RectangleF(730, 107 + i * 64, 60, 50);
            }
            m_Rects[7] = new RectangleF(1, 95, 710, 450);
            m_Rects[8] = new RectangleF(720, 95, 80, 450);

            m_Rects[9] = new RectangleF(0, 180, 710, 25);

            //menu1
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    m_Rects[10 + i * 6 + j] = new RectangleF(50 + i * 335, 250 + j * 30, 285, 30);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                m_Rects[22 + i] = new RectangleF(210, 250 + i * 30, 300, 30);
            }

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }

            #endregion

            #region :::::::::::::::::::::::::::::::: _rect ::::::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 7; i++)
            {
                m_Rect.Add(new Region(m_Rects[i]));
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::
            m_PDrawPoint[0] = new Point(705, 95);
            m_PDrawPoint[1] = new Point(705, 550);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            m_Collection = new List<CommonInnerControlBase>();
            m_HelpInfoDic = new Dictionary<HelpInfoMenu, List<int>>();
            LoadFile();
            #endregion
        }

        private void LoadFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\提示信息.txt");
            var allLine = File.ReadAllLines(file, Encoding.Default);
            var strF = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            var size = new Size(286, 30);
            var loc1 = new Point(50, 250);
            var loc2 = new Point(200, 250);
            var loc3 = new Point(385, 250);
            foreach (var line in allLine.Skip(1))
            {
                var slip = line.Split('\t');
                if (slip.Length == 5 && Convert.ToInt32(slip[0]) != -1)
                {
                    var menu = Convert.ToInt32(slip[0]);
                    var isRight = false;
                    if (!slip[4].Equals("-"))
                    {
                        isRight = Convert.ToInt32(slip[4]) == 1;
                    }
                    var tmploc = menu == 3 ? loc2 : (isRight ? loc3 : loc1);
                    if (m_HelpInfoDic.ContainsKey((HelpInfoMenu)menu))
                    {
                        m_HelpInfoDic[(HelpInfoMenu)menu].Add(Convert.ToInt32(slip[1]));
                    }
                    else
                    {
                        var lis = new List<int> { Convert.ToInt32(slip[1]) };
                        m_HelpInfoDic.Add((HelpInfoMenu)menu, lis);
                    }
                    m_Collection.Add(new GDIRectText()
                    {
                        Text = slip[2],
                        TextColor = FormatStyle.m_WhiteBrush.Color,
                        TextFormat = strF,
                        NeedDarwOutline = true,
                        BkColor = Color.Red,
                        Tag = new int[] { menu, Convert.ToInt32(slip[1]) },
                        RefreshAction = o => RefreshItemBkVisible((GDIRectText)o),
                        OutLineRectangle = new Rectangle(tmploc.X, tmploc.Y + size.Height * Convert.ToInt32(slip[3]), size.Width, size.Height)
                    });
                }
            }
        }

        private void RefreshItemBkVisible(GDIRectText item)
        {
            var name = (int[])item.Tag;
            item.BackColorVisible = BoolList[name[1]];
            item.Visible = name[0] == m_MenuID;
        }

        public static Dictionary<HelpInfoMenu, List<int>> m_HelpInfoDic;
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        private readonly StringFormat m_DrawFormat = new StringFormat();

        private readonly StringFormat m_RightFormat = new StringFormat();

        private readonly StringFormat m_LeftFormat = new StringFormat();
        private List<CommonInnerControlBase> m_Collection;
        /// <summary>
        /// 菜单页码
        /// </summary>
        public static int m_MenuID = 0;


        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] m_Images;

        /// <summary>
        /// 键是否按下
        /// </summary>
        private bool[] m_ButtonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> m_Rect;


        #endregion#

        #endregion
    }
}
