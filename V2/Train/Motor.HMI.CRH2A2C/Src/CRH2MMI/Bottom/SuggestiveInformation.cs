using System;
using System.Drawing;
using CommonUtil.Controls;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.Bottom
{
    /// <summary>
    /// 提示信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class SuggestiveInformation : CRH2BaseClass
    {
        private GDIRectText m_UpText;
        private GDIRectText m_DownText;

        private static readonly Color DefaultTextColor = Color.Aqua;

        public override bool Init()
        {
            var location = new Point(0, 540);
            var size = new Size(800, 30);
            var format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            m_UpText = new GDIRectText()
                       {
                           Tag = InformationClearType.Manual,
                           TextColor = DefaultTextColor,
                           BkColor = Color.Black,
                           OutLineRectangle = new Rectangle(location, size),
                           DrawFont = CRH2Resource.Font12,
                           TextFormat = format
                       };
            location.Offset(0, size.Height);
            m_DownText = new GDIRectText()
                         {
                             Tag = InformationClearType.Manual,
                             TextColor = DefaultTextColor,
                             BkColor = Color.Black,
                             OutLineRectangle = new Rectangle(location, size),
                             DrawFont = CRH2Resource.Font12,
                             TextFormat = format
                         };
            GlobalEvent.Instance.RestartEvent += (sender, args) => ClearInformation();

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                AutoClear();
            }
        }

        public override void paint(Graphics g)
        {
            m_UpText.OnDraw(g);
            m_DownText.OnDraw(g);
        }

        public void UpdateInformation(InformationModel informationModel)
        {
            GDIRectText txt;
            switch (informationModel.Location)
            {
                case InformationLocation.Up:
                    txt = m_UpText;
                    break;
                case InformationLocation.Down:
                    txt = m_DownText;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SetTextState(txt, informationModel.InformationType);
            txt.Tag = informationModel.ClearType;
            txt.Text = informationModel.Information;
        }

        public void ClearInformation()
        {
            ClearText(m_UpText);
            ClearText(m_DownText);
        }

        private void AutoClear()
        {
            AutoClear(m_DownText);
            AutoClear(m_UpText);
        }

        private void AutoClear(GDIRectText txt)
        {
            if (txt.Tag is InformationClearType)
            {
                if ((InformationClearType)(txt.Tag) == InformationClearType.Auto)
                {
                    ClearText(txt);
                }
            }
        }

        public void ClearInformation(InformationLocation location)
        {
            switch (location)
            {
                case InformationLocation.Up:
                    ClearText(m_UpText);
                    break;
                case InformationLocation.Down:
                    ClearText(m_DownText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("location");
            }
        }

        private void ClearText(GDIRectText txt)
        {
            txt.Text = string.Empty;
            txt.TextColor = DefaultTextColor;
            txt.BkColor = Color.Black;
        }

        private void SetTextState(GDIRectText txt, InformationType type)
        {
            switch (type)
            {
                case InformationType.Info:
                    txt.TextColor = DefaultTextColor;
                    txt.BkColor = Color.Black;
                    break;
                case InformationType.Error:
                    txt.TextColor = Color.White;
                    txt.BkColor = Color.Red;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }
    }
}
