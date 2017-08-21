using System.Collections.Generic;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统.Decoupling
{
    public class PrepareingDecouplingContentView : DecouplingContentView
    {
        public PrepareingDecouplingContentView(DMIDecoupling sourceObj)
            : base(sourceObj)
        {
            AddTrainGroupView(TrainGroupType.TrainGroup1);
            AddTrainGroupView(TrainGroupType.TrainGroup2);

            ButtomBtnContentBuffer = new List<string> { "", "", "", "", "", "", "", "使列车就绪", "", "主页面", };
        }

        protected override void OnResponseBottomBtn(BottomBtnType btnType)
        {
            if (btnType == BottomBtnType.Btn08)
            {
                SourceObj.CurrentDecouplingState = DecouplingState.Prepared;
            }
        }
    }
}