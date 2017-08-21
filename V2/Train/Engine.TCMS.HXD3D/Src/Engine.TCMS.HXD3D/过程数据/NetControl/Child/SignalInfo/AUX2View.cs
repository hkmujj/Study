using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.Resource;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child.SignalInfo
{
    public class AUX2View : SingalInfoChild
    {
        private List<Label> m_ConstInfo;

        private List<Label> m_ValueInfo;

        public AUX2View(NetSiganlInfoView parent) : base(parent)
        {
        }

        /// <summary>初始化</summary>
        public override void Init()
        {
            SetItemConfigs(Parent.ContentConfigs.Where(w => w.GroupName == "AUX2"));

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

            var keys = new[] { InfKeys.Inf司控器电压, InfKeys.Inf主变压器温度1, InfKeys.Inf主变压器温度2 };
            var sf = new[] { "0.000", "0.0", "0.0", };
            for (int i = 0; i < 3; ++i)
            {
                var index = i;
                m_ValueInfo.Add(new Label
                {
                    OutLineRectangle = new Rectangle(x, y + (h + 3) * i, w, h),
                    Brush = Brushes.White,
                    Font = CommonRes.Font14,
                    Format = { Alignment = StringAlignment.Far },
                    RefreshAction = o =>
                    {
                        var lb = (Label)o;
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

            var names = new[] {"司控器", "主变压器温度1", "主变压器温度2"};
            for (int i = 0; i < 3; ++i)
            {
                m_ConstInfo.Add(new Label
                {
                    OutLineRectangle = new Rectangle(x, y + (h + 3)*i, w, h),
                    Text = names[i],
                    Brush = Brushes.White,
                    Font = CommonRes.Font14,
                    Format = {Alignment = StringAlignment.Near},
                });
            }

            names = new[] {"000C(Hex)", "121D(Hex)", "121F(Hex)"};
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

            names = new[] {"V/bit", "℃/bit", "℃/bit"};
            x = 400;
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

            names = new[] {"PG1    0000(Hex)", "PG2    0000(Hex)",};
            x = 510;
            for (int i = 0; i < 2; ++i)
            {
                m_ConstInfo.Add(new Label
                {
                    OutLineRectangle = new Rectangle(x, y + (h + 3)*i, w, h),
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