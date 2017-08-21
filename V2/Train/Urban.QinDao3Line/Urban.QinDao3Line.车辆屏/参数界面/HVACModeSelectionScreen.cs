using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI.参数界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HVACModeSelectionScreen:QingDaoBaseClass 
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "空调模式选择";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
            return true;
        }
        #endregion
        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = true;
                    switch (index)
                    {
                        case 0:       //上翻
                            if (m_SelectedIdx > 0)
                            {
                                m_SelectedIdx = (--m_SelectedIdx) % 7;
                            }
                            else
                            {
                                m_SelectedIdx = 6;
                            }
                            break;
                        case 1:       //下翻
                            m_SelectedIdx = (++m_SelectedIdx) % 7;
                            break;
                        case 2:       //确认
                            for (int k = 0; k < 7; k++)
                            {
                                if (k == m_SelectedIdx)
                                {
                                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[k], 1, 0);
                                }
                                else
                                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[k], 0, 0);
                            }
                            break;
                    }
                }
            }
            for (int i = 0; i < 7; i++)
            {
                if (m_RectsList[i].Contains(point))
                {
                    m_SelectedIdx = i;
                    break;
                }
            }
            return true;
        }
        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = false;
                    break;
                }
            }
            return true;
        }
        public override void paint(Graphics g)
        {
            Draw(g);
        }
        private void Draw(Graphics e)
        {
            if (m_SelectedIdx != -1 && m_SelectedIdx < 7)
            {
                e.FillRectangle(new SolidBrush(Color.White), m_RectsList[m_SelectedIdx]);
            }
            PaintState(e);
        }
        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        private void PaintState(Graphics e)
        {
            e.DrawString("空调模式选择", FontItems.Fonts_Regular(FontName.宋体, 16, false), SolidBrushsItems.BlackBrush,
                m_RectsList[7], FontItems.TheAlignment(FontRelated.居中));
            for (int i = 0; i < 7; i++)
            {
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[i].X, m_RectsList[i].Y, m_RectsList[i].Width,
                    m_RectsList[i].Height);
                e.DrawString(m_Str[i], FontItems.Fonts_Regular(FontName.宋体, 14, false), SolidBrushsItems.BlackBrush,
                m_RectsList[i], FontItems.TheAlignment(FontRelated.靠左));
            }

            //CONFIRM 按钮
            if (m_BtnIsDown[2])
            {
                e.DrawImage(m_ImgsList[3], m_RectsList[10]);
            }
            else
                e.DrawImage(m_ImgsList[2], m_RectsList[10]);
            e.DrawString("确认", FontItems.Fonts_Regular(FontName.宋体, 10, false), SolidBrushsItems.BlackBrush,
                m_RectsList[10], FontItems.TheAlignment(FontRelated.居中));
            //上翻 下翻
            for (int i = 0; i < 2; i++)
            {
                if (m_BtnIsDown[i])
                {
                    e.DrawImage(m_ImgsList[3], m_RectsList[8 + i]);
                }
                else
                    e.DrawImage(m_ImgsList[2], m_RectsList[8 + i]);
                e.DrawImage(m_ImgsList[i], m_RectsList[11 + i]);
            }
        }

        #endregion
        #endregion
        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        private void InitData()
        {
            m_ImgsList = new List<Image>();
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            if (Coordinate.RectangleFLists(ViewIDName.HVACModeSelectionScreen, ref m_RectsList))
            {
                for (int i = 0; i < 3; i++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[8 + i]));
                }
            }
        }
        #endregion
        #region:::::::::::::::init values:::::::::::::

        private int m_SelectedIdx = 0;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[3];
        private readonly string[] m_Str = { "自动模式", "菜单模式", "通风模式", "半冷模式", "全冷模式", "半制热模式","全制热模式" };

        #endregion
    }
}
