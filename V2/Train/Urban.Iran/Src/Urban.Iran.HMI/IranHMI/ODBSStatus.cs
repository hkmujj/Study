using System;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ODBSStatus : HMIBase
    {
        private Rectangle m_BmpRect;
        private Bitmap m_BmpODBS;
        private Rectangle m_CurrRect;

        private Label m_PercentValueLabel;
        private Label m_PercentLabel;

        public override string GetInfo()
        {
            return "ODBSStatus";
        }

        protected override bool Initalize()
        {
            m_CurrRect = new Rectangle(373, 0, 40, 0);
            m_BmpRect = new Rectangle(358, 116, 99, 349);
            m_BmpODBS = new Bitmap(RecPath + "\\frame/odbs.jpg");
            //82
            m_PercentValueLabel = new Label()
            {
                OutLineRectangle = new Rectangle(362, 444, 40, 24),
                Brush = GdiCommon.MediumGreyBrush,
                Font = GdiCommon.Txt12Font,
                RefreshAction = o =>
                {
                    var valueLable = (Label) o;
                    var value = Math.Min(100f, Math.Max(FloatList[UIObj.InFloatList[1]], 0f));
                    valueLable.Text = value.ToString("#0");
                    valueLable.Tag = value;
                }
            };
            m_PercentValueLabel.Format.Alignment = StringAlignment.Far;

            m_PercentLabel = new Label()
            {
                OutLineRectangle =
                    new Rectangle(m_PercentValueLabel.OutLineRectangle.Right, m_PercentValueLabel.Location.Y, 30, 24),
                Font = GdiCommon.Txt12Font,
                Brush = GdiCommon.GreyBrush,
                Text = "%"
            };
            m_PercentLabel.Format.Alignment = StringAlignment.Near;


            UIObj.InFloatList.Add(GlobleParam.Instance.FindInFloatIndex("ODBS status 百分比"));

            append_postCmd(CmdType.SetInFloatValue, UIObj.InFloatList[1], 0, UIObj.InFloatList[1]);

            return true;
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 12;

            g.DrawImage(m_BmpODBS, m_BmpRect);

            m_PercentLabel.OnDraw(g);

            m_PercentValueLabel .OnPaint(g);

            var value = (float)m_PercentValueLabel.Tag;

            m_CurrRect.Height = (int) ((308*value)/100);
            m_CurrRect.Y = 430 - m_CurrRect.Height;
            g.FillRectangle(GdiCommon.OceanBlueBrush, m_CurrRect);
        }
    }
}