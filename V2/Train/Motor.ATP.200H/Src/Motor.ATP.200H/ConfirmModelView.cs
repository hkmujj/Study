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

        public static ConfirmModel m_TheModelSelect = ConfirmModel.��;

        private int[] m_OutBoolIndex;

        public override string GetInfo()
        {
            return "ȷ��ģʽѡ��";
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
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub����ģʽ);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub�˳�����ģʽ);
            yield return this.GetOutBoolIndex(OutBoolKeys.OubĿ��ģʽ);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub������Ƶ);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub������Ƶ);
            yield return this.GetOutBoolIndex(OutBoolKeys.OubCTCS0);
            yield return this.GetOutBoolIndex(OutBoolKeys.OubCTCS2);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub����);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub����);
            yield return this.GetOutBoolIndex(OutBoolKeys.Oub����);
            yield return this.GetOutBoolIndex(OutBoolKeys.OubĿ������ȷ�ϱ�־);
        }

        public void InitData()
        {
            m_Rect = new Rectangle(Common2D.RectB[0].Right + 5, Common2D.rectposition.Y + 45, 280, 328);


            for (int i = 0; i < 8; i++)
            {
                m_GText[i] = new RectText();
                m_GText[i].SetBkColor(0, 0, 0);
                m_GText[i].SetTextColor(255, 255, 255);
                m_GText[i].SetTextStyle(16, FormatStyle.Center, true, "����");
                m_GText[i].SetTextRect(Common2D.RectF[i].X + 2, Common2D.RectF[i].Y + 2, Common2D.RectF[i].Width - 4,
                    Common2D.RectF[i].Height - 4);
            }
            m_GText[0].SetText("");
            m_GText[1].SetText("");
            m_GText[2].SetText("");
            m_GText[3].SetText("");
            m_GText[4].SetText("");
            m_GText[5].SetImage(ImageResource.ȷ��);
            m_GText[6].SetText("");
            m_GText[7].SetImage(ImageResource.ȡ��);
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

            Common2D.MeunTitle.SetText(m_TheModelSelect == ConfirmModel.�˳����� ? "����" : m_TheModelSelect.ToString());
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
                case ConfirmModel.����:
                    return isTitle ? "����ģʽȷ��" : "    ����ȷ���Ƿ���Ҫ\n\n�������ģʽ?";
                case ConfirmModel.�˳�����:
                    return isTitle ? "�˳�����ģʽȷ��" : "    ����ȷ���Ƿ���Ҫ\n\n�˳�����ģʽ?";
                case ConfirmModel.Ŀ��:
                    return isTitle ? "Ŀ��ģʽȷ��" : "    ����ȷ���Ƿ���Ҫ\n\n����Ŀ���г�ģʽ?";
                case ConfirmModel.������Ƶ:
                    return isTitle ? "������Ƶȷ��" : "    ����ȷ���Ƿ���Ҫ\n\n����������Ƶ��ʽ?";
                case ConfirmModel.������Ƶ:
                    return isTitle ? "������Ƶȷ��" : "    ����ȷ���Ƿ���Ҫ\n\n����������Ƶ��ʽ?";
                case ConfirmModel.CTCS0��:
                    return isTitle ? "ת����CTCS0��ȷ��" : "    ����ȷ���Ƿ���Ҫ\n\nת�뵽CTCS0��?";
                case ConfirmModel.CTCS2��:
                    return isTitle ? "ת����CTCS2��ȷ��" : "    ����ȷ���Ƿ���Ҫ\n\nת�뵽CTCS2��?";
                case ConfirmModel.����:
                    return isTitle ? "����ģʽȷ��" : "    ����ȷ���Ƿ���Ҫ\n\n�����豸?";
                case ConfirmModel.����:
                    return isTitle ? "����ģʽȷ��" : "    ����ȷ���Ƿ���Ҫ\n\n�����ƶ�?";
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
                            //���ע�⣬���߼�������Ὣ��˲����0������Ҫ����ʲôʱ����0������
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