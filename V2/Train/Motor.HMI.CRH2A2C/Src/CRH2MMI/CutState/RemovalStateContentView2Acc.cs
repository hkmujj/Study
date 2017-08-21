using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Global;
using MMI.Facility.Interface;

namespace CRH2MMI.CutState
{
    class RemovalStateContentView2Acc : RemovalStateContentViewBase
    {
        public override void Initalize(baseClass viewClass)
        {
            CreateGridAndTitle(viewClass);

            ModifyTitle();

            GlobalEvent.Instance.ReversalChanged += (sender, args) => StateGrid.Reverse();
        }

        private void ModifyTitle()
        {
            var all = m_StateTitles.FindAll(f => f.Text.Contains("受电弓切除"));


            var it = all.OrderBy(o => o.OutLineRectangle.Left).First();

            m_StateTitles.Add(new GDIRectText()
                              {
                                  BkColor = it.BkColor,
                                  TextColor = it.TextColor,
                                  TextFormat = it.TextFormat,
                                  Text = "受电弓切除",
                                  DrawFont = it.DrawFont,
                                  OutLineRectangle = new Rectangle(it.Location.Translate(0, -it.Size.Height), it.Size)
                              });

            all.ForEach(e =>
            {
                e.Text = e.Text.Replace("受电弓切除", "");
                e.TextFormat = new StringFormat(e.TextFormat) { FormatFlags = StringFormatFlags.DirectionRightToLeft };
            });

        }
    }
}
