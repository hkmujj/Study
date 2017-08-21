using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.隔离
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ConverterCutOff : HXD3BaseClass
    {
        // Fields
        private SolidBrush[] m_Brush1 = new SolidBrush[0];

        public static bool[] ButtonOldState = new bool[10];
        public List<ButtonState> Buttons = new List<ButtonState>();
        public bool[] ButtonState = new bool[10];
        private bool[] m_BValue;
        public static bool[] ChangeState = { true, true, true, true, true, true, true, true, true, true };
        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private bool[] m_OldbValue;
        private List<Region> m_Rect;
        private List<RectangleF> m_RectsList;
        private bool[] m_SetBValue;
        private int[] m_SetBValueNumb;
        public string[] Str1 = { "CI1", "CI2", "CI3", "APU1", "CI4", "CI5", "CI6", "APU2", "警惕", "轮缘" };

        // Methods
        private void ButtonCancle()
        {
            append_postCmd(CmdType.ChangePage, 1, 0, 0f);
        }

        private void ButtonColorChange(int index)
        {
            ChangeState[index] = false;
            if (!(m_BValue[index] || ButtonOldState[index]))
            {
                ButtonState[index] = true;
                ButtonOldState[index] = ButtonState[index];
            }
            else if (!(m_BValue[index] || !ButtonOldState[index]))
            {
                ButtonState[index] = false;
                ButtonOldState[index] = ButtonState[index];
            }
            else if (!(!m_BValue[index] || ButtonOldState[index]))
            {
                ButtonState[index] = false;
                ButtonOldState[index] = !ButtonState[index];
            }
            else if (m_BValue[index] && ButtonOldState[index])
            {
                ButtonState[index] = true;
                ButtonOldState[index] = !ButtonState[index];
            }
        }

        private void ButtonOk()
        {
            for (var i = 0; i < 10; i++)
            {
                if (m_BValue[i] && (m_BValue[i] != ButtonState[i]))
                {
                    append_postCmd(CmdType.SetBoolValue, m_SetBValueNumb[i], 0, 0f);
                }
                else if (!(m_BValue[i] || (m_BValue[i] == ButtonState[i])))
                {
                    append_postCmd(CmdType.SetBoolValue, m_SetBValueNumb[i], 1, 0f);
                }
                ChangeState[i] = true;
                ButtonOldState[i] = false;
            }
        }

        private void Draw(Graphics e)
        {
            int num;
            RectangleF ef;
            DrawState(e);
            for (num = 0; num < 10; num++)
            {
                ef = m_RectsList[0x29 + num];
                ef = m_RectsList[0x29 + num];
                ef = m_RectsList[0x29 + num];
                ef = m_RectsList[0x29 + num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                e.DrawString(Str1[num], FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush, m_RectsList[0x33 + num], FontsItems.TheAlignment(FontRelated.居中));
            }
            for (num = 0; num < 10; num++)
            {
                if (m_BValue[num] || ButtonState[num])
                {
                    ef = m_RectsList[0x3d];
                    ef = m_RectsList[0x3d];
                    ef = m_RectsList[0x3d];
                    ef = m_RectsList[0x3d];
                    e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
                    e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[0x3e]);
                    e.DrawString("确定", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush, m_RectsList[0x3d], FontsItems.TheAlignment(FontRelated.居中));
                }
            }
            ef = m_RectsList[0x3f];
            ef = m_RectsList[0x3f];
            ef = m_RectsList[0x3f];
            ef = m_RectsList[0x3f];
            e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            e.DrawString("取消", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush, m_RectsList[0x3f], FontsItems.TheAlignment(FontRelated.居中));
            ef = m_RectsList[0];
            ef = m_RectsList[0];
            ef = m_RectsList[0];
            ef = m_RectsList[0];
            e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
        }

        private void DrawState(Graphics e)
        {
            for (var i = 0; i < 10; i++)
            {
                if (m_BValue[i])
                {
                    e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[1 + i]);
                    if (m_BValue[20 + i])
                    {
                        e.DrawImage(m_ImgsList[3], m_RectsList[11 + i]);
                    }
                    e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[0x15 + i]);
                    e.DrawString("隔离", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush, m_RectsList[0x1f + i], FontsItems.TheAlignment(FontRelated.居中));
                    if (ChangeState[i])
                    {
                        e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[0x33 + i]);
                    }
                }
                else if (m_BValue[10 + i])
                {
                    e.FillRectangle(new SolidBrush(Color.Green), m_RectsList[1 + i]);
                    e.FillRectangle(new SolidBrush(Color.Red), m_RectsList[0x15 + i]);
                    e.DrawString("故障", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush, m_RectsList[0x1f + i], FontsItems.TheAlignment(FontRelated.居中));
                }
                else
                {
                    e.FillRectangle(new SolidBrush(Color.Green), m_RectsList[1 + i]);
                    e.FillRectangle(new SolidBrush(Color.Green), m_RectsList[0x15 + i]);
                    e.DrawString("正常", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.BlackBrush, m_RectsList[0x15 + i], FontsItems.TheAlignment(FontRelated.居中));
                }
                if (!(ChangeState[i] || !ButtonState[i]))
                {
                    e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[0x33 + i]);
                }
            }
        }

        public override string GetInfo()
        {
            return "隔离";
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < UIObj.InBoolList.Count; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
                m_OldbValue[num] = BoolOldList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < UIObj.InFloatList.Count; num++)
            {
                m_FValue[num] = FloatList[UIObj.InFloatList[num]];
            }
            for (num = 0; num < UIObj.OutBoolList.Count; num++)
            {
                m_SetBValue[num] = BoolList[UIObj.OutBoolList[num]];
                m_SetBValueNumb[num] = UIObj.OutBoolList[num];
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
            m_OldbValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];
            m_SetBValue = new bool[UIObj.OutBoolList.Count];
            m_SetBValueNumb = new int[UIObj.OutBoolList.Count];
            m_ImgsList = new List<Image>();
            for (num = 0; num < UIObj.ParaList.Count; num++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[num]));
            }
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.ConverterCutOff, ref m_RectsList))
            {
                m_HasPlan = true;
            }
            m_Rect = new List<Region>();
            for (num = 0; num < 10; num++)
            {
                m_Rect.Add(new Region(m_RectsList[0x33 + num]));
            }
            m_Rect.Add(new Region(m_RectsList[0x3e]));
            m_Rect.Add(new Region(m_RectsList[0x40]));
        }

        public override bool mouseUp(Point nPoint)
        {
            var num = 0;
            while (num < 12)
            {
                if (m_Rect[num].IsVisible(nPoint))
                {
                    break;
                }
                num++;
            }
            switch (num)
            {
                case 0:
                    ButtonColorChange(0);
                    break;

                case 1:
                    ButtonColorChange(1);
                    break;

                case 2:
                    ButtonColorChange(2);
                    break;

                case 3:
                    ButtonColorChange(3);
                    break;

                case 4:
                    ButtonColorChange(4);
                    break;

                case 5:
                    ButtonColorChange(5);
                    break;

                case 6:
                    ButtonColorChange(6);
                    break;

                case 7:
                    ButtonColorChange(7);
                    break;

                case 8:
                    ButtonColorChange(8);
                    break;

                case 9:
                    ButtonColorChange(9);
                    break;

                case 10:
                    ButtonOk();
                    break;

                case 11:
                    ButtonCancle();
                    break;
            }
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }
    }
}