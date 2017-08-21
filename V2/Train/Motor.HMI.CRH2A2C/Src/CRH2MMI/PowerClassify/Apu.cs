using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    class Apu : GDIRectText, IUnit, IPowerUnit
    {

        private readonly GDIRectText m_Text;

        /// <summary>
        /// 默认大小
        /// </summary>
        public static readonly Size DefaultSize = new Size(40, 30);

        /// <summary>
        /// 导线 
        /// </summary>
        public List<StraightWire> StraightWires { set; get; }

        /// <summary>
        /// 车厢号
        /// </summary>
        public int CarNo { get; set; }

        public int UnitNo { get; set; }

        /// <summary>
        /// 实际的轮廓
        /// </summary>
        public Rectangle ActureOutLine{ get; private set; }


        public Apu()
        {
            TextVisible = true;
            StraightWires = new List<StraightWire>();
            m_Text = new GDIRectText()
            {
                Text = "APU",
                NeedDarwOutline = true
            };
            //IsNomarl = true;
            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            m_OutLineRectangle = new Rectangle(Location, DefaultSize);

            if (StraightWires.Any())
            {
                var mat = new Matrix();
                mat.Translate(Location.X -m_Text.OutLineRectangle.X ,
                    Location.Y - m_Text.OutLineRectangle.Y);
                foreach (var sw in StraightWires)
                {
                    sw.StartPoint = mat.TransformPoint(sw.StartPoint);
                    sw.EndPoint = mat.TransformPoint(sw.EndPoint);
                }
            }
            m_Text.OutLineRectangle = new Rectangle(Location, DefaultSize);
            ActureOutLine = new Rectangle(Location, DefaultSize);

        }

        /// <summary>
        /// 刷新状态
        /// </summary>
        public void RefreshState()
        {
            //m_Text.TextColor = Color.White;
            //m_Text.BkColor = Color.Black;
            //if (IsNomarl && StraightWires != null)
            //{
            //    var wir = StraightWires.FindAll(f => f.IsPowerOn).ToList();
            //    if (wir.Any())
            //    {
            //        //m_Text.BkColor = wir.First().PowerOnColor;
            //        m_Text.TextColor = Color.Black;
            //        var pon = wir.First();
            //        StraightWires.ForEach(e =>
            //        {
            //            //e.PowerOnColor = pon.PowerOnColor;
            //            e.IsPowerOn = true;
            //        });
            //    }
            //}
        }

        public bool TextVisible { get; set; }

        public override void OnDraw(Graphics g)
        {

            if (TextVisible)
            {
                m_Text.OnDraw(g);
            }

            if (StraightWires != null)
            {
                StraightWires.ForEach(e => e.OnDraw(g));
            }

        }

        public override void Reverse()
        {
            var mat = MatrixHelper.CreateTurnMatrix(Location.X + DefaultSize.Width / 2, TurnOrientation.Horizontal);
            StraightWires.ForEach(e =>
            {
                e.StartPoint = mat.TransformPoint(e.StartPoint);
                e.EndPoint = mat.TransformPoint(e.EndPoint);
            });
        }

        public TrainUnit Unit { get; set; }
        public bool IsPowerOn { set; get; }
        public Color Color { get; set; }

        public PowerFrom PowerFrom { get; set; }

        public void RefreshByState(PowerFrom powerFrom)
        {
            PowerFrom = powerFrom;
            var color = PowerFromColorAdaptor.GetColor(powerFrom);
            Color = color;
            m_Text.TextColor = Color.White;
            m_Text.BkColor = Color.Black;
            if (powerFrom != PowerFrom.Null)
            {
                m_Text.BkColor = color;
                m_Text.TextColor = Color.Black;
            }

            if (StraightWires != null)
            {
                StraightWires.ForEach(e => e.RefreshByState(powerFrom));
            }
        }
    }
}
