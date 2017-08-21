using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.系统.Decoupling
{
    public class DecouplingDecouplingContentView : DecouplingContentView
    {
        public DecouplingDecouplingContentView(DMIDecoupling sourceObj) : base(sourceObj)
        {
            ButtomBtnContentBuffer = new List<string> {"", "", "", "", "", "", "", "", "", "主页面",};

            AddTrainGroupView(TrainGroupType.TrainGroup1);
            AddTrainGroupView(TrainGroupType.TrainGroup2);

            ConstControlCollection.Add(new GDIRectText
            {
                Text = "- 如果有必要，将方向开关扳至\"0\"位，取消此过程\r\n等待直至\"司机室变更模式\"被激活并将方向开关扳回至\"前进\"位",
                OutLineRectangle = new Rectangle(30, 400, 600, 60)
            });

            ConstControlCollection.Add(new GDIRectText
            {
                Text = "正在解编中…",
                BackColorVisible = true,
                BkColor = Color.White,
                TextColor = Color.Black,
                DrawFont = FontsItems.FontE18,
                NeedDarwOutline = false,
                Bold = true,
                TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                OutLineRectangle = new Rectangle(20, 150, 760, 40)
            });
            ConstControlCollection.Add(new DoubleOutLineTextControl
            {
                Text = "正在配置中，请等待60S 根据ATP提示的线路信息惰行或减速",
                BackColorVisible = true,
                BkColor = Color.White,
                TextColor = Color.Black,
                NeedDarwOutline = false,
                InnerRectangleVisible = true,
                Bold = true,
                OutLinePen = Pens.Black,
                Padding = new Padding(6, 2, 6, 2),
                TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                OutLineRectangle = new Rectangle(52, 530, 690, 40)
            });

            RefreshAction += OnDecouplingRefreshAction;
        }

        private void OnDecouplingRefreshAction(object o)
        {
            if (SourceObj.GetInBoolValue(InBoolKeys.Inb解编成功))
            {
                SourceObj.CurrentDecouplingState = DecouplingState.ConfirmTrainGroup;
            }
        }
    }
}