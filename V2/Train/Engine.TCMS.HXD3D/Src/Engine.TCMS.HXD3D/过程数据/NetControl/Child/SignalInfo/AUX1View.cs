using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.Resource;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child.SignalInfo
{
    public class AUX1View : SingalInfoChild
    {
        private List<Label> m_ConstInfo;

        private List<Label> m_ValueInfo;

        public AUX1View(NetSiganlInfoView parent) : base(parent)
        {
        }

        /// <summary>初始化</summary>
        public override void Init()
        {
            SetItemConfigs(Parent.ContentConfigs.Where(w => w.GroupName == "AUX1"));

            InitalizeConstInfos();

            InitalizeValueInfo();
        }

        private void InitalizeValueInfo()
        {
            m_ValueInfo = new List<Label>();
            var x = 390;
            var y = 190;
            var w = 80;
            var h = 24;

            var keys = new[] {InfKeys.Inf原边电流, InfKeys.Inf电空制动电流, InfKeys.Inf主变压器温度};
            var sf = new[] {"0.00", "0.000", "0.0", };
            for (int i = 0; i < 3; ++i)
            {
                var index = i;
                m_ValueInfo.Add(new Label
                {
                    OutLineRectangle = new Rectangle(x, y + (h + 3)*i, w, h),
                    Brush = Brushes.White,
                    Font = CommonRes.Font14,
                    Format = { Alignment = StringAlignment.Far},
                    RefreshAction = o =>
                    {
                        var lb = (Label) o;
                        lb.Text = Parent.Parent.GetInFloatValue(keys[index]).ToString(sf[index]);
                    }
                });
            }
        }

        private void InitalizeConstInfos()
        {
            m_ConstInfo = new List<Label>();

            var x = 100;
            var y = 190;
            var w = 200;
            var h = 24;

            var names = new[] {"原边电流", "电空制动电流", "主变压器温度"};
            for (int i = 0; i < 3; ++i)
            {
                m_ConstInfo.Add(new Label
                {
                    OutLineRectangle = new Rectangle(x, y + (h + 3)*i, w, h),
                    Text = names[i],
                    Brush = Brushes.White,
                    Font = CommonRes.Font14,
                    Format = { Alignment = StringAlignment.Near},
                });
            }

            names = new[] {"0077(Hex)", "0000(Hex)", "2C3C(Hex)"};
            x = 230;
            for (int i = 0; i < 3; ++i)
            {
                m_ConstInfo.Add(new Label
                {
                    OutLineRectangle = new Rectangle(x, y + (h + 3)*i, w, h),
                    Text = names[i],
                    Brush = Brushes.White,
                    Font = CommonRes.Font14
                });
            }

            names = new[] { "A/bit", "A/bit", "℃/bit" };
            x = 400;
            for (int i = 0; i < 3; ++i)
            {
                m_ConstInfo.Add(new Label
                {
                    OutLineRectangle = new Rectangle(x, y + (h + 3) * i, w, h),
                    Text = names[i],
                    Brush = Brushes.White,
                    Font = CommonRes.Font14
                });
            }
        }

        /// <summary>绘图</summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            base.OnPaint(g);

            m_ConstInfo.ForEach(e => e.OnDraw(g));

            m_ValueInfo.ForEach(e => e.OnPaint(g));
        }
    }
}