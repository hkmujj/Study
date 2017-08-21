using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.数据输入
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DataInOthers : HXD3BaseClass
    {
        // Fields
        private readonly Image[] m_BaseImage = new Image[2];

        private readonly SolidBrush[] m_Brushs1 = new SolidBrush[2];
        private readonly SolidBrush[] m_Brushs2 = new SolidBrush[2];
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[11];
        private bool[] m_BValue;
        private List<Image> m_ImgsList;
        private readonly string[] m_Names = { "压缩机", "APU1模式选择", "空电联合模式选择" };
        private readonly List<ButtonState> m_OtherBtns = new List<ButtonState>();
        private bool[] m_OutbValue;
        private readonly List<Rectangle> m_Rects = new List<Rectangle>();
        private readonly List<RectangleF> m_RectsBtns = new List<RectangleF>();
        private readonly List<RectangleF> m_RectsImgs = new List<RectangleF>();
        private List<RectangleF> m_RectsList = new List<RectangleF>();
        private int[] m_SendID;
        private readonly string[] m_Str = { "间歇式", "延时式", "正常", "25Hz", "50Hz", "投入", "隔离", "680kPa双泵", "750kPa双泵", "单泵", "双泵" };
        private readonly string[] m_Title = { "模式选择", "双泵启动方式", "单APU" };

        // Methods
        public void Draw(Graphics e)
        {
            DrawAdd(e);
            PaintValue(e);
            PaintButtonsState(e);
        }

        private void DrawAdd(Graphics e)
        {
            e.DrawRectangle(CommonRes.WhitePen, m_Rects[0]);
            for (var i = 0; i < 3; i++)
            {
                e.DrawString(m_Title[i], CommonRes.Font14, CommonRes.WhiteBrush, m_Rects[2 + i], CommonRes.MFormat);
            }
        }

        public override string GetInfo()
        {
            return "其他设置";
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < m_BValue.Length; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < m_OutbValue.Length; num++)
            {
                m_OutbValue[num] = BoolList[UIObj.OutBoolList[num]];
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            int num;
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_OutbValue = new bool[UIObj.OutBoolList.Count];
            m_SendID = new int[UIObj.OutBoolList.Count];
            for (num = 0; num < m_SendID.Length; num++)
            {
                m_SendID[num] = UIObj.OutBoolList[num];
            }
            m_ImgsList = new List<Image>();
            foreach (var str in UIObj.ParaList)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + str));
            }
            m_Rects.Add(new Rectangle(0xa2, 0x81, 0x1df, 0xab));
            m_Rects.Add(new Rectangle(0xb7, 0xa6, 0x4b, 0x15));
            m_Rects.Add(new Rectangle(0x110, 0x9a, 0x58, 0x19));
            m_Rects.Add(new Rectangle(0x108, 0xc9, 0x63, 0x19));
            m_Rects.Add(new Rectangle(0x124, 0xf8, 0x49, 0x19));
            m_BaseImage[0] = m_ImgsList[1];
            m_BaseImage[1] = m_ImgsList[0];
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.DataInOthers, ref m_RectsList))
            {
                for (num = 0; num < 7; num++)
                {
                    m_RectsBtns.Add(m_RectsList[3 + num]);
                }
                for (num = 0; num < 4; num++)
                {
                    m_RectsBtns.Add(m_RectsList[0x11 + num]);
                }
                for (num = 0; num < 7; num++)
                {
                    m_RectsImgs.Add(m_RectsList[10 + num]);
                }
                for (num = 0; num < 4; num++)
                {
                    m_RectsImgs.Add(m_RectsList[0x15 + num]);
                }
                foreach (var ef in m_RectsBtns)
                {
                    m_BtnArea.Add(new Region(ef));
                }
                InitTheButtons();
            }
            m_Brushs1[0] = SolidBrushsItems.BlackBrush;
            m_Brushs1[1] = SolidBrushsItems.WhiteBrush;
            m_Brushs2[0] = SolidBrushsItems.YellowBrush1;
            m_Brushs2[1] = SolidBrushsItems.BlackBrush;
            SetBtnStatus(true, 0);
            SetBtnStatus(true, 7);
            SetBtnStatus(true, 9);
        }

        private void InitTheButtons()
        {
            for (var i = 0; i < 11; i++)
            {
                m_OtherBtns.Add(new ButtonState(m_RectsBtns[i], m_BaseImage, m_RectsImgs[i], m_Str[i], true));
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            var index = 0;
            while (index < 11)
            {
                if (m_BtnArea[index].IsVisible(nPoint))
                {
                    break;
                }
                index++;
            }
            switch (index)
            {
                case 0:
                case 7:
                case 9:
                    if (!m_BtnIsDown[index])
                    {
                        SetBtnStatus(true, index);
                    }
                    break;

                case 1:
                case 8:
                case 10:
                    if (!m_BtnIsDown[index])
                    {
                        SetBtnStatus(false, index);
                    }
                    break;

                case 2:
                    {
                        if (!m_OtherBtns[index].IsLocked)
                        {
                            m_OtherBtns[index].BtnStateChange(true);
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 1, 0f);
                            m_BtnIsDown[index] = true;
                            m_OtherBtns[index+1].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index+1], 0, 0f);
                            m_BtnIsDown[index+1] = false;
                            m_OtherBtns[index + 2].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index + 2], 0, 0f);
                            m_BtnIsDown[index + 2] = false;
                            break;
                        }
                        else
                        {
                            m_OtherBtns[index].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 0, 0f);
                            m_BtnIsDown[index] = false;
                            break;
                        }
                        
                    }
                case 3:
                    {
                        if (!m_OtherBtns[index].IsLocked)
                        {
                            m_OtherBtns[index].BtnStateChange(true);
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 1, 0f);
                            m_BtnIsDown[index] = true;
                            m_OtherBtns[index + 1].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index + 1], 0, 0f);
                            m_BtnIsDown[index + 1] = false;
                            m_OtherBtns[index -1].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index -1], 0, 0f);
                            m_BtnIsDown[index -1] = false;
                            break;
                        }
                        else
                        {
                            m_OtherBtns[index].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 0, 0f);
                            m_BtnIsDown[index] = false;
                            break;
                        }
                    }
                case 4:
                    {
                        if (!m_OtherBtns[index].IsLocked)
                        {
                            m_OtherBtns[index].BtnStateChange(true);
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 1, 0f);
                            m_BtnIsDown[index] = true;
                            m_OtherBtns[index - 1].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index - 1], 0, 0f);
                            m_BtnIsDown[index - 1] = false;
                            m_OtherBtns[index - 2].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index - 2], 0, 0f);
                            m_BtnIsDown[index - 2] = false;
                            break;
                        }
                        else
                        {
                            m_OtherBtns[index].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 0, 0f);
                            m_BtnIsDown[index] = false;
                            break;
                        }
                    }
                case 5:
                    {
                        if (!m_OtherBtns[index].IsLocked)
                        {
                            m_OtherBtns[index].BtnStateChange(true);
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 1, 0f);
                            m_BtnIsDown[index] = true;
                            m_OtherBtns[index + 1].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index + 1], 0, 0f);
                            m_BtnIsDown[index + 1] = false;
                            break;
                        }
                        else
                        {
                            m_OtherBtns[index].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 0, 0f);
                            m_BtnIsDown[index] = false;
                            break;
                        }
                    }
                case 6:
                    {
                        if (!m_OtherBtns[index].IsLocked)
                        {
                            m_OtherBtns[index].BtnStateChange(true);
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 1, 0f);
                            m_BtnIsDown[index] = true;
                            m_OtherBtns[index - 1].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index - 1], 0, 0f);
                            m_BtnIsDown[index - 1] = false;

                            break;
                        }
                        else
                        {
                            m_OtherBtns[index].BtnRecover();
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 0, 0f);
                            m_BtnIsDown[index] = false;
                            break;
                        }
                    }
                   
            }
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            for (var i = 0; i < 11; i++)
            {
                if (m_BtnArea[i].IsVisible(nPoint))
                {
                    break;
                }
            }
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }

        private void PaintButtonsState(Graphics e)
        {
            for (var i = 0; i < 11; i++)
            {
                m_OtherBtns[i].DrawTheBtn(e, m_BtnIsDown[i], m_BtnIsDown[i] ? m_Brushs2 : m_Brushs1);
            }
        }

        private void PaintValue(Graphics e)
        {
            e.DrawString(m_Names[0], FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_Rects[1], CommonRes.LeftFormat);
            for (var i = 0; i < 2; i++)
            {
                e.DrawString(m_Names[i + 1], FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[i + 1], FontsItems.TheAlignment(FontRelated.居中));
            }
            var ef = m_RectsList[1];
            ef = m_RectsList[1];
            ef = m_RectsList[1];
            ef = m_RectsList[1];
            e.DrawRectangle(new Pen(Color.White), ef.X - 8f, ef.Y - 25f, ef.Width + 449f, ef.Height + 50f);
            ef = m_RectsList[2];
            ef = m_RectsList[2];
            ef = m_RectsList[2];
            ef = m_RectsList[2];
            e.DrawRectangle(new Pen(Color.White), ef.X - 8f, ef.Y - 25f, ef.Width + 324f, ef.Height + 50f);
        }

        private void SetBtnStatus(bool flg, int index)
        {
            if (index >= 0)
            {
                m_OtherBtns[index].BtnStateChange(true);
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 1, 0f);
                m_BtnIsDown[index] = true;
                if (flg)
                {
                    m_OtherBtns[index + 1].BtnRecover();
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index + 1], 0, 0f);
                    m_BtnIsDown[index + 1] = false;
                }
                else
                {
                    m_OtherBtns[index - 1].BtnRecover();
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index - 1], 0, 0f);
                    m_BtnIsDown[index - 1] = false;
                }
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                for (var i = 0; i < 7; i++)
                {
                    if (!BoolList[UIObj.OutBoolList[i]])
                    {
                        m_OtherBtns[i].BtnRecover();
                    }
                }
            }
        }
    }
}