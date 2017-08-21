using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ConfirmModelView : baseClass
    {
        private readonly RectText[] m_GText = new RectText[8];

        private Rectangle m_Rect;

        public static ConfirmModel m_TheModelSelect = ConfirmModel.无;

        private int[] m_OutBoolIndex;

        public override string GetInfo()
        {
            return "确认模式选择";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            m_OutBoolIndex = GetOutBoolIndexs().ToArray();

            InitData();
            
            return true;
        }

        private IEnumerable<int> GetOutBoolIndexs()
        {
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub调车模式);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub退出调车模式);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub目视模式);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub上行载频);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub下行载频);
            yield return this.GetOutBoolIndex(OutBoolKeys.OubCTCS0);
            yield return this.GetOutBoolIndex(OutBoolKeys.OubCTCS2);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub启动);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub缓解);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub警惕);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub目视请求确认标志);
        }

        public void InitData()
        {
            m_Rect = new Rectangle(Common2D.RectB[0].Right + 5, Common2D.rectposition.Y + 45, 280, 328);


            for (int i = 0; i < 8; i++)
            {
                m_GText[i] = new RectText();
                m_GText[i].SetBkColor(0, 0, 0);
                m_GText[i].SetTextColor(255, 255, 255);
                m_GText[i].SetTextStyle(16, FormatStyle.Center, true, "宋体");
                m_GText[i].SetTextRect(Common2D.RectF[i].X + 2, Common2D.RectF[i].Y + 2, Common2D.RectF[i].Width - 4,
                    Common2D.RectF[i].Height - 4);
            }
            m_GText[0].SetText("");
            m_GText[1].SetText("");
            m_GText[2].SetText("");
            m_GText[3].SetText("");
            m_GText[4].SetText("");
            m_GText[5].SetImage(ImageResource.确定);
            m_GText[6].SetText("");
            m_GText[7].SetImage(ImageResource.取消);
        }

        public void UpdateValue()
        {

        }

        public override void paint(Graphics g)
        {
            UpdateValue();
            OnDraw(g);
            ButtonEvent();
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common2D.DarkBlueBrush, m_Rect);
            g.FillRectangle(Common2D.LightBlueBrush, m_Rect.X, m_Rect.Y, m_Rect.Width, 60);
            g.DrawString(Title(m_TheModelSelect, true), Common2D.Font14, Common2D.WhiteBrush, m_Rect.X + 30, m_Rect.Y + 25);
            g.DrawString(Title(m_TheModelSelect, false), Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 115);

            Common2D.MeunTitle.SetText(m_TheModelSelect == ConfirmModel.退出调车 ? "调车" : m_TheModelSelect.ToString());
            Common2D.MeunTitle.OnDraw(g);
            for (int i = 0; i < 8; i++)
            {
                m_GText[i].OnDraw(g);
            }
        }

        private string Title(ConfirmModel modelName, bool isTitle)
        {
            switch (modelName)
            {
                case ConfirmModel.调车:
                    return isTitle ? "调车模式确认" : "    请您确认是否需要\n\n进入调车模式?";
                case ConfirmModel.退出调车:
                    return isTitle ? "退出调车模式确认" : "    请您确认是否需要\n\n退出调车模式?";
                case ConfirmModel.目视:
                    return isTitle ? "目视模式确认" : "    请您确认是否需要\n\n进入目视行车模式?";
                case ConfirmModel.上行载频:
                    return isTitle ? "上行载频确认" : "    请您确认是否需要\n\n进入上行载频方式?";
                case ConfirmModel.下行载频:
                    return isTitle ? "下行载频确认" : "    请您确认是否需要\n\n进入下行载频方式?";
                case ConfirmModel.CTCS0级:
                    return isTitle ? "转换到CTCS0级确认" : "    请您确认是否需要\n\n转入到CTCS0级?";
                case ConfirmModel.CTCS2级:
                    return isTitle ? "转换到CTCS2级确认" : "    请您确认是否需要\n\n转入到CTCS2级?";
                case ConfirmModel.启动:
                    return isTitle ? "启动模式确认" : "    请您确认是否需要\n\n启动设备?";
                case ConfirmModel.缓解:
                    return isTitle ? "缓解模式确认" : "    请您确认是否需要\n\n缓解制动?";
                default:
                    return "";
            }

        }

        public void ButtonEvent()
        {
            for (int i = 0; i < 8; i++)
            {
                if (ButtonStatus.m_IsRightButtonUp[i])
                {
                    switch (i)
                    {
                        case 5:
                            //这边注意，在逻辑表里面会将其瞬间置0，不需要考虑什么时候置0的问题
                            append_postCmd(CmdType.SetBoolValue, m_OutBoolIndex[(int)m_TheModelSelect], 1, 0);
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        case 7:
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                    }
                }
                else if (ButtonStatus.m_IsBottomButtonUp[i])
                {
                    if (i == 6)
                    {
                        append_postCmd(CmdType.SetBoolValue, m_OutBoolIndex[(int)m_TheModelSelect], 1, 0);
                        append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    }
                }
            }

        }
    }
}