using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3C.Fault
{
    //故障状态
    [GksDataType(DataType.isMMIObjectClass)]
    public partial class DefaultState : baseClass
    {
        public override string GetInfo()
        {
            return "故障状态";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther && nParaB == 210)
            {
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str204);
            }
            if (nParaA == 1)
            {
                if (nParaB == 201 || nParaB == 202 || nParaB == 203 || nParaB == 204 || nParaB == 205
                     || nParaB == 206 || nParaB == 207 || nParaB == 208 || nParaB == 211 || nParaB == 212
                     || nParaB == 214)
                {
                    m_CurrentView = "现存故障";
                }
                else if (nParaB == 210)
                {
                    m_CurrentView = "故障履历";
                }
                else
                    m_CurrentView = "";

                if (nParaB == 0)
                {
                    m_TheNoOverDefault.Clear();
                    m_TheAllDefault.Clear();
                    m_TheDefaultLevel1.Clear();
                    m_TheDefaultLevel2.Clear();
                    m_TheDefaultLevel3.Clear();
                    m_DefaultSort.Clear();
                }
            }

        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFaultInfos();

            //3
            nErrorObjectIndex = -1;

            m_Img = this.GetImages();

            InitData();
            return true;
        }

        private void LoadFaultInfos()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "故障信息.txt");

            var all = File.ReadAllLines(file, Encoding.Default);

            foreach (var cStr in all)
            {
                //4-1
                string[] split = cStr.Split(new Char[] { '\t' });
                string[] tmp = new string[5];
                int i = 0;
                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        if (i < 5)
                        {
                            tmp[i] = s;
                        }
                        i++;
                    }
                    if (i == 5)
                    {
                        m_FaultList.Add(int.Parse(tmp[0]), tmp);
                    }
                }
            }
        }


        public override void paint(Graphics g)
        {
            RefreshDate();
            GetValue();
            if (m_CurrentView == "现存故障" && m_IsDefaultOccur)
                DrawOnNoOverDefault(g);
            else if (m_CurrentView == "故障履历")
                DrawOnAllDefault(g);

            base.paint(g);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < 7; index++)
            {
                if (m_Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = false;
                    append_postCmd(CmdType.ChangePage, 209, 0, 0);
                    break;
                case 1:
                    if (m_ButtonIsShow[1])
                    {
                        m_ButtonIsDown[1] = false;
                        if (m_CurrentRow > 0)
                        {
                            m_CurrentRow--;
                        }
                    }
                    break;
                case 2:
                    if (m_ButtonIsShow[2])
                    {
                        m_ButtonIsDown[2] = false;
                        if (m_CurrentRow + 1 < m_TheAllDefault.Count)
                        {
                            m_CurrentRow++;
                        }
                    }
                    break;
                case 3:
                    if (m_ButtonIsShow[3])
                    {
                        m_ButtonIsDown[3] = false;
                        if (m_CurrentPage <= 0)
                        {
                            if (m_CurrentRow > 0)
                            {
                                m_CurrentRow -= 12;
                                if (m_CurrentRow < 0)
                                {
                                    m_CurrentRow = 0;
                                }
                            }
                            break;
                        }
                        m_CurrentPage--;
                    }
                    break;
                case 4:
                    if (m_ButtonIsShow[4])
                    {
                        m_ButtonIsDown[4] = false;
                        if (m_CurrentPage < m_Page)
                        {
                            m_CurrentPage++;
                        }
                    }
                    break;
                case 5:
                    m_ButtonIsDown[5] = false;
                    m_NeedSort = false;
                    break;

                case 6:
                    m_ButtonIsDown[6] = false;
                    m_NeedSort = true;
                    break;
                default:
                    return false;
            }
            return true;

        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < 7; index++)
            {
                if (m_Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    m_ButtonIsDown[0] = true;
                    break;
                case 1:
                    if (m_ButtonIsShow[1])
                    {
                        m_ButtonIsDown[1] = true;
                    }
                    break;
                case 2:
                    if (m_ButtonIsShow[2])
                    {
                        m_ButtonIsDown[2] = true;
                    }
                    break;
                case 3:
                    if (m_ButtonIsShow[3])
                    {
                        m_ButtonIsDown[3] = true;
                    }
                    break;
                case 4:
                    if (m_ButtonIsShow[4])
                    {
                        m_ButtonIsDown[4] = true;
                    }
                    break;
                case 5:
                    m_ButtonIsDown[5] = true;
                    break;

                case 6:
                    m_ButtonIsDown[6] = true;
                    break;
                default:
                    return false;
            }
            return true;
        }

        #region :::::::::::::::::::::::: 当前故障 ::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 框架线条,当前故障
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrameLinesD(Graphics e)
        {
            e.FillRectangle(Common.RedBrush, new Rectangle(m_PDrawPoint[0], new Size(300, 35)));
            e.DrawRectangle(Common.WhitePen1, new Rectangle(m_PDrawPoint[0], new Size(300, 35)));
            e.DrawString("故障信息", Common.Txt12FontB, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[0], new Size(300, 35)), Common.DrawFormat);

            e.DrawRectangle(Common.WhitePen1, new Rectangle(m_PDrawPoint[1], new Size(300, 340)));
        }

        /// <summary>
        /// 当前故障信息
        /// </summary>
        /// <param name="e"></param>
        private void DrawCurrentDefaultInfo(Graphics e)
        {
            int defCount = 0;
            if (m_TheNoOverDefault.Count < 12)
            {
                defCount = m_TheNoOverDefault.Count;
            }
            else
            {
                defCount = 12;
            }

            for (int i = 0; i < defCount; i++)
            {
                e.DrawImage(BackDefaultImage(m_TheNoOverDefault[i].m_PicType),
                    new Rectangle(m_PDrawPoint[1].X, m_PDrawPoint[1].Y + i * 25, 20, 20));
                e.DrawString(m_TheNoOverDefault[i].m_FaultName, Common.Txt12FontB, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[1].X + 20, m_PDrawPoint[1].Y + i * 25, 300, 28), Common.LeftFormat);
            }
        }

        /// <summary>
        /// 当前故障
        /// </summary>
        /// <param name="e"></param>
        private void DrawOnNoOverDefault(Graphics e)
        {
            DrawFrameLinesD(e);

            DrawCurrentDefaultInfo(e);

        }
        #endregion#

        #region :::::::::::::::::::::::: 故障履历 ::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 框架线条,故障履历
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrameLinesL(Graphics e)
        {
            for (int i = 0; i < 6; i++)
            {
                e.DrawRectangle(Common.WhitePen1, new Rectangle(m_PDrawPoint[2 + i], new Size(m_RectWrith[i], 368)));
                e.DrawString(m_RectStr[i], Common.Txt12FontB, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[2 + i], new Size(m_RectWrith[i], 28)), Common.DrawFormat);
            }
            e.DrawLine(Common.WhitePen1, new Point(m_PDrawPoint[2].X, m_PDrawPoint[2].Y + 28),
                new Point(m_PDrawPoint[2].X + 800, m_PDrawPoint[2].Y + 28));
        }

        /// <summary>
        /// 故障等级对应的图案
        /// </summary>
        /// <param name="imageNumb"></param>
        /// <returns></returns>
        private Image BackDefaultImage(string imageNumb)
        {
            switch (imageNumb)
            {
                case "1":
                    return m_Img[0];
                case "2":
                    return m_Img[1];
                case "3":
                    return m_Img[2];
                default:
                    return m_Img[0];
            }
        }

        /// <summary>
        /// 所有故障内容
        /// </summary>
        /// <param name="e"></param>
        private void DrawDefaultContentL(Graphics e, List<ItemInfo> defaultList)
        {
            if (defaultList.Count > 0)
            {
                int num = (defaultList.Count - 1) - m_CurrentRow;
                int num2 = num;

                for (int i = 0; num2 >= 0; i++)
                {
                    if (num - num2 >= m_CurrentPage * 12 && num - num2 < (m_CurrentPage + 1) * 12)
                    {
                        int tmpRowIndex = i % 12;
                        e.DrawString(defaultList[num2].m_Code, Common.Txt12FontB, Common.WhiteBrush,
                            new Rectangle(m_PDrawPoint[2].X, m_PDrawPoint[2].Y + 28 + tmpRowIndex * 28, 40, 28), Common.DrawFormat);
                        e.DrawImage(BackDefaultImage(defaultList[num2].m_PicType),
                            new Rectangle(m_PDrawPoint[3].X, m_PDrawPoint[3].Y + 28 + tmpRowIndex * 28, 20, 20));
                        e.DrawString(defaultList[num2].m_FaultName, Common.Txt12FontB, Common.WhiteBrush,
                            new Rectangle(m_PDrawPoint[3].X + 20, m_PDrawPoint[3].Y + 28 + tmpRowIndex * 28, 320, 28), Common.LeftFormat);
                        e.DrawString(defaultList[num2].m_StartTime, Common.Txt12FontB, Common.WhiteBrush,
                            new Rectangle(m_PDrawPoint[4].X, m_PDrawPoint[4].Y + 28 + tmpRowIndex * 28, 145, 28), Common.DrawFormat);
                        if (defaultList[num2].m_OverTime != null)
                        {
                            e.DrawString(defaultList[num2].m_OverTime, Common.Txt12FontB, Common.WhiteBrush,
                                new Rectangle(m_PDrawPoint[5].X, m_PDrawPoint[5].Y + 28 + tmpRowIndex * 28, 120, 28), Common.DrawFormat);
                        }
                        e.DrawString(defaultList[num2].m_WangYa + "  " + defaultList[num2].m_DianLiu + "  " + defaultList[num2].m_TrainState
                            + "   " + defaultList[num2].m_JiWei + "  " + defaultList[num2].m_TrainSpeed, Common.Txt10FontB, Common.WhiteBrush,
                            new Rectangle(m_PDrawPoint[6].X, m_PDrawPoint[6].Y + 28 + tmpRowIndex * 28, 250, 28), Common.DrawFormat);
                        e.DrawString(defaultList[num2].m_SolveName, Common.Txt12FontB, Common.WhiteBrush,
                            new Rectangle(m_PDrawPoint[7].X, m_PDrawPoint[7].Y + 28 + tmpRowIndex * 28, 45, 28), Common.DrawFormat);
                    }
                    num2--;
                }
            }

        }

        /// <summary>
        /// 画按钮相关图案
        /// </summary>
        /// <param name="e"></param>
        private void DrawPicButton(Graphics e)
        {
            //
            m_Page = m_TheAllDefault.Count / 13;

            if (m_TheAllDefault.Count == 0)
            {
                for (int i = 1; i < 4; i++)
                {
                    m_ButtonIsShow[i] = false;
                }
            }
            else
            {
                if (m_Page > m_CurrentPage)
                {
                    m_ButtonIsShow[4] = true;
                }
                else m_ButtonIsShow[4] = false;

                if (m_CurrentPage > 0)
                {
                    m_ButtonIsShow[3] = true;
                }
                else if (m_CurrentRow > 0)
                {
                    m_ButtonIsShow[3] = true;
                }
                else m_ButtonIsShow[3] = false;
            }

            ////
            //if (_theAllDefault.Count > 0)
            //{
            //    if (_theAllDefault.Count - (currentPage + 1) * 12 >= 0)
            //    {
            //        Row = 12;
            //    }
            //    else
            //    {
            //        Row = _theAllDefault.Count - (currentPage + 1) * 12 + 12;
            //    }
            //}
            //else
            //{
            //    Row = -1;
            //}

            m_Row = m_TheAllDefault.Count;

            if (m_Row >= 0)
            {
                if (m_CurrentRow < m_Row - 1)
                {
                    m_ButtonIsShow[2] = true;
                }
                else if (m_CurrentRow == m_Row)
                {
                    m_ButtonIsShow[2] = false;
                }
                else if (m_CurrentRow > m_Row)
                {
                    m_CurrentRow = -1;
                }

                if (m_CurrentRow > 0)
                {
                    m_ButtonIsShow[1] = true;
                }
                else
                    m_ButtonIsShow[1] = false;
            }

            ////
            //for (int i = 0; i < 5; i++)
            //{
            //    if (buttonIsShow[i])
            //    {
            //        if (i == 0)
            //        {
            //            faultButtons[i].DrawRectButoonFillAndNoState(e);
            //            e.DrawString(buttonStr[i], Common.Txt12FontB, Common.whiteBrush, rects[0], Common.drawFormat);
            //        }
            //        else
            //        {
            //            faultButtons[i].DrawPolygonButoonFillAndNoState(e);
            //            e.DrawString(buttonStr[i], Common.Txt12FontB, Common.whiteBrush, rects[5 + i], Common.drawFormat);
            //        }
            //    }
            //}

            //
            int num = 0;
            while (num < 5)
            {
                if (m_ButtonIsShow[num])
                {
                    if (num == 0)
                    {
                        m_FaultButtons[num].DrawRectButoonFillAndNoState(e);
                        e.DrawString(m_ButtonStr[num], Common.Txt12FontB, Common.WhiteBrush, m_Rects[0], Common.DrawFormat);
                    }
                    else
                    {
                        m_FaultButtons[num].DrawPolygonButoonFillAndNoState(e);
                        e.DrawString(m_ButtonStr[num], Common.Txt12FontB, Common.WhiteBrush, m_Rects[5 + num], Common.DrawFormat);
                    }
                }
                num++;
            }

            if (m_NeedSort)
                e.FillRectangle(Common.BlackBrush, m_Rects[4]);
            else
                e.FillRectangle(Common.BlackBrush, m_Rects[2]);

            for (num = 0; num < 2; num++)
            {
                m_FaultButtons[5 + num].DrawRectButoonFillAndNoState(e);
                e.DrawString(m_ButtonStr[5 + num], Common.Txt12FontB, Common.WhiteBrush, m_Rects[2 + num * 2], Common.DrawFormat);
            }
        }

        /// <summary>
        /// 故障履历
        /// </summary>
        /// <param name="e"></param>
        private void DrawOnAllDefault(Graphics e)
        {
            if (m_NeedSort)
                DrawDefaultContentL(e, m_DefaultSort);
            else
                DrawDefaultContentL(e, m_TheAllDefault);

            DrawPicButton(e);
            DrawFrameLinesL(e);
        }
        #endregion#
    }
}
