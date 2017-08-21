using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ButtomButtonView : baseClass
    {
        public event Func<int, bool> ButtonUpEvent;

        private List<IButtonBtnEventResponser> m_BtnEventResponsers;

        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> m_Rect;
        /// <summary>
        /// 单例
        /// </summary>
        public static ButtomButtonView Instance { private set; get; }

        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        private bool[] m_BValue;

        /// <summary>
        /// 接收模拟量
        /// </summary>
        private float[] m_FValue;
        /// <summary>
        /// 按键上的字符
        /// </summary>
        public ReadOnlyCollection<string> ButtonStr { set; get; }
        /// <summary>
        /// 标题按钮
        /// </summary>
        private List<HXD3Button> m_TitleButtons;

        public override bool init(ref int nErrorObjectIndex)
        {
            Instance = this;
            m_BtnEventResponsers = new List<IButtonBtnEventResponser>();
            ButtonStr = new ReadOnlyCollection<string>(new string[8]);
            m_TitleButtons = new List<HXD3Button>();
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];
            m_Rect = new List<Region>();
            for (var i = 0; i < 8; i++)
            {
                m_TitleButtons.Add(new HXD3Button(new Rectangle(i * 100, 550, 100, 50), new Rectangle(5 + i * 100, 555, 90, 40)));
                m_Rect.Add(new Region(m_TitleButtons[i].OutRectangle));
            }

            return true;
        }

        public void AddResponser(IButtonBtnEventResponser responser)
        {
            m_BtnEventResponsers.Add(responser);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                ResetEnvent();
                // ButtonUpEvent = null;
                switch (nParaB)
                {

                    case 215:
                        //m_TitleName = "密码设定画面";
                        ButtonStr = new ReadOnlyCollection<string>(Common.Str205);
                        break;
                    default:
                        //m_TitleName = "";
                        break;
                }
            }
        }
        private void DrawButton(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                m_TitleButtons[i].DrawRectButoonFillAndNoState(g);
                g.DrawString(ButtonStr[i], Common.Txt12FontB, Common.WhiteBrush,
                    m_TitleButtons[i].OutRectangle, Common.DrawFormat);
            }
        }

        private void ResetEnvent()
        {
            ButtonUpEvent = null;
        }
        public override void paint(Graphics g)
        {
            GetValue();

            DrawButton(g);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void GetValue()
        {
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
            }
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }

            if (NeedShowPower())
            {
                Common.Str201[1] = "列车供电";
                Common.Str203[1] = "列车供电";
                Common.Str204[1] = "列车供电";
            }
            else
            {
                Common.Str201[1] = "";
                Common.Str203[1] = "";
                Common.Str204[1] = "";
            }
        }

        private bool NeedShowPower()
        {
            return m_BValue[20] || m_BValue[21];
        }

        public override bool mouseUp(Point point)
        {
            var index = 0;
            for (; index < 8; index++)
            {
                if (m_Rect[index].IsVisible(point))
                    break;
            }
            if (index < 8)
            {
                OnButtonEvent(index + 1);
            }
            return true;
        }

        private void OnButtonEvent(int obj)
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            m_BtnEventResponsers.Any(responser => responser.Response(obj));
        }
    }
}