using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.系统.Decoupling
{
    public class PreparedDecouplingContentView : DecouplingContentView
    {
        private readonly GDIRectText m_SwitchTo0;

        public PreparedDecouplingContentView(DMIDecoupling sourceObj)
            : base(sourceObj)
        {

            AddTrainGroupView(TrainGroupType.TrainGroup1);
            AddTrainGroupView(TrainGroupType.TrainGroup2);

            ConstControlCollection.Add(new GDIRectText
            {
                                             Text = "- 如果有必要，将方向开关扳至\"0\"位，取消此过程\r\n等待直至\"司机室变更模式\"被激活并将方向开关扳回至\"前进\"位" ,
                                             OutLineRectangle = new Rectangle(30, 400, 600, 60)
                                         });

            ButtomBtnContentBuffer = new List<string> { "", "", "", "", "", "", "", "", "", "主页面", };

            m_SwitchTo0 = new GDIRectText { Text = "- 方向开关扳至\"0\"位", DrawFont = FontsItems.FontC22B, OutLineRectangle = new Rectangle(30, 200, 400, 30) };

            RefreshAction += OnPreparedRefreshAction;
        }


        private void OnPreparedRefreshAction(object o)
        {
            if (SourceObj.GetInBoolValue(InBoolKeys.Inb解编时_列车已就绪_可以解编))
            {
                m_SwitchTo0.Visible = false;
                ButtomBtnContentBuffer = new List<string> { "", "", "", "解编", "", "", "", "", "", "主页面", };
                UpdateBottomBtnContents();
            }
        }

        public override void Reset()
        {
            ButtomBtnContentBuffer = new List<string> { "", "", "", "", "", "", "", "", "", "主页面", };
            UpdateBottomBtnContents();
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            m_SwitchTo0.OnDraw(g);
        }

        protected override void OnResponseBottomBtn(BottomBtnType btnType)
        {
            if (!m_SwitchTo0.Visible && btnType == BottomBtnType.Btn04)
            {
                SourceObj.CurrentDecouplingState = DecouplingState.ConfirmDecoupling;
            }
        }
    }
}