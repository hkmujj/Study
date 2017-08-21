using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统.Decoupling
{
    public abstract class DecouplingContentView : CommonInnerControlBase
    {
        protected DMIDecoupling SourceObj;

        protected List<CommonInnerControlBase> ConstControlCollection;

        protected List<string> ButtomBtnContentBuffer;

        protected DecouplingContentView(DMIDecoupling sourceObj)
        {
            SourceObj = sourceObj;
            ConstControlCollection = new List<CommonInnerControlBase>();
            RefreshAction += o => ResponseUser();
        }

        protected void AddTrainGroupView(TrainGroupType type)
        {
            var location = new Point(25, 280);
            switch (type)
            {
                case TrainGroupType.TrainGroup1:
                    break;
                case TrainGroupType.TrainGroup2:
                    location.Offset(TrainGroupView.DefaultWidth + 2, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            var tgv = new TrainGroupView(location);
            const int txtHeight = 40;
            ConstControlCollection.Add(new GDIRectText
            {
                Text = "动车组 " + (type == TrainGroupType.TrainGroup1 ? "1" : "2"),
                OutLineRectangle =
                                                 new Rectangle(tgv.OutLineRectangle.X, tgv.OutLineRectangle.Y - txtHeight, TrainGroupView.DefaultWidth, txtHeight),
                TextFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
            });
            ConstControlCollection.Add(tgv);
        }

        public virtual void Reset()
        {
            SourceObj.DMITitle.TitleName = "系统;解编";

            UpdateBottomBtnContents();
        }

        protected void UpdateBottomBtnContents()
        {
            SourceObj.DMITitle.UpdateBtnContent(ButtomBtnContentBuffer);
        }

        public override void OnDraw(Graphics g)
        {
            ConstControlCollection.ForEach(e => e.OnDraw(g));
        }

        private void ResponseUser()
        {
            if (SourceObj.DMIButton.BtnUpList.Count != 0)
            {
                OnResponseBottomBtn((BottomBtnType)(SourceObj.DMIButton.BtnUpList[0] - 6));

                if (SourceObj.DMIButton.BtnUpList[0] - 6 > (int)BottomBtnType.Btn10 || SourceObj.DMIButton.BtnUpList[0] - 6 < (int)BottomBtnType.Btn01)
                {
                    OnResponseNotBottomBtn(SourceObj.DMIButton.BtnUpList[0]);
                }
            }
        }

        protected virtual void OnResponseBottomBtn(BottomBtnType btnType)
        {

        }

        protected virtual void OnResponseNotBottomBtn(int btnIndex)
        {

        }

    }
}