using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util.Extension;

namespace Motor.HMI.CRH3C380B.Base.系统.Decoupling
{
    public class ConfirmDecouplingContentView: DecouplingContentView
    {
        private readonly GraphicsPath m_TrianglePath;

        public ConfirmDecouplingContentView(DMIDecoupling sourceObj)
            : base(sourceObj)
        {
            AddTrainGroupView(TrainGroupType.TrainGroup1);
            AddTrainGroupView(TrainGroupType.TrainGroup2);

            ButtomBtnContentBuffer = new List<string> { "", "", "", "", "", "", "", "", "", "主页面", };

            var txt = new GDIRectText
            {
                          OutLineRectangle = new Rectangle(650, 400, 150, 90),
                          Text = "\t确认\r\n\t动车组1|2\r\n\t正在解编",
                          TextColor = Color.Black,
                          BkColor = Color.White,
                          TextFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                      };
            ConstControlCollection.Add(txt);
            var x1 = txt.OutLineRectangle.Right - 15;
            var x2 = txt.OutLineRectangle.Right;
            var y2 = txt.OutLineRectangle.GetMidPoint(Direction.Right).Y;
            var y1 = y2 - 10;
            var y3 = y2 + 10;
            m_TrianglePath = new GraphicsPath();
            m_TrianglePath.AddLine(new Point(x1, y1), new Point(x2, y2));
            m_TrianglePath.AddLine(new Point(x2, y2), new Point(x1, y3));
            m_TrianglePath.AddLine(new Point(x1, y3), new Point(x1, y1));

            var location = new Point(txt.Location.X + 10, txt.Location.Y + 5);
            // 确认键处红色方块
            ConstControlCollection.Add(new GDIRectText { OutLineRectangle = new Rectangle(location, new Size(10, 25)) , BkColor = Color.Red});

            var trainGroup1 = (TrainGroupView)ConstControlCollection.FindAll(f => f is TrainGroupView).OrderBy(m => ( (TrainGroupView) m ).RightEdgeLocation.X).First();

            // 车中间方块
            ConstControlCollection.Add(new GDIRectText { OutLineRectangle = new Rectangle(trainGroup1.RightEdgeLocation.X-3, trainGroup1.RightEdgeLocation.Y, 10, 25), BkColor = Color.Red });
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            g.FillPath(Brushes.Black, m_TrianglePath);
        }

        protected override void OnResponseNotBottomBtn(int btnIndex)
        {
            // E 键
            if (btnIndex == 5)
            {
                SourceObj.CurrentDecouplingState = DecouplingState.Decoupling;
            }
        }
    }
}