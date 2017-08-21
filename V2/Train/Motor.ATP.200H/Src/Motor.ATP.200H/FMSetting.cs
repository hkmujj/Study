using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class FMSetting : baseClass
    {
        private readonly RectText[] m_GText = new RectText[8];
        private Rectangle m_Rect;
        
        public override string GetInfo()
        {
            return "载频设置";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            nErrorObjectIndex = -1;
            return true;
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
            m_GText[0].SetText("上行");
            m_GText[1].SetText("下行");
            m_GText[2].SetText("");
            m_GText[3].SetText("");
            m_GText[4].SetText("");
            m_GText[5].SetText("");
            m_GText[6].SetText("");
            m_GText[7].SetImage(ImageResource.返回);
        }

        public override void paint(Graphics dcGs)
        {
            OnDraw(dcGs);
            ButtonEvent();
            base.paint(dcGs);
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common2D.DarkBlueBrush, m_Rect);
            g.FillRectangle(Common2D.LightBlueBrush, m_Rect.X, m_Rect.Y, m_Rect.Width, 60);
            g.DrawString("载频方式选择", Common2D.Font14B, Common2D.WhiteBrush, m_Rect.X + 30, m_Rect.Y + 25);
            g.DrawString("上行载频", Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 30, m_Rect.Y + 115);
            g.DrawString("下行载频", Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 30, m_Rect.Y + 180);

            Common2D.MeunTitle.SetText("载频");
            Common2D.MeunTitle.OnDraw(g);
            for (int i = 0; i < 8; i++)
            {
                m_GText[i].OnDraw(g);
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
                        case 0:
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            ConfirmModelView.m_TheModelSelect = ConfirmModel.上行载频;
                            break;
                        case 1:
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            ConfirmModelView.m_TheModelSelect = ConfirmModel.下行载频;
                            break;
                        case 7:
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;

                    }
                }
            }

        }
    }
}