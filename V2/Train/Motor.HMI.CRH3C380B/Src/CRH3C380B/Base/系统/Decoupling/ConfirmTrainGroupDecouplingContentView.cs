using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.Restart;

namespace Motor.HMI.CRH3C380B.Base.系统.Decoupling
{
    public class ConfirmTrainGroupDecouplingContentView: DecouplingContentView
    {
        private readonly ReconfigViewUnstartPage m_Reconfig;

        public ConfirmTrainGroupDecouplingContentView(DMIDecoupling sourceObj)
            : base(sourceObj)
        {
            AddTrainGroupView(TrainGroupType.TrainGroup1);

            ButtomBtnContentBuffer = new List<string> { "", "", "", "", "", "", "", "", "", "", };

            m_Reconfig = new ReconfigViewUnstartPage(sourceObj);
            m_Reconfig.Init();
            m_Reconfig.ContentTitle = "已更新的列车配置:";
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            m_Reconfig.OnDraw(g);
        }

        public override void Reset()
        {
            base.Reset();

            SourceObj.DMITitle.TitleName = "确认列车配置";
        }

        protected override void OnResponseNotBottomBtn(int btnIndex)
        {
            // E 键
            if (btnIndex == 5)
            {
                // 主页面
                SourceObj.append_postCmd(CmdType.ChangePage, 11, 0, 0);
            }
        }
    }
}