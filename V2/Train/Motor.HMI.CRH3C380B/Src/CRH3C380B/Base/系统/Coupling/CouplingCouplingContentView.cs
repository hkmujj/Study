using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统.Coupling
{
    public class CouplingCouplingContentView : CouplingContentView
    {
        public CouplingCouplingContentView(DMICoupling sourceobj)
            : base(sourceobj)
        {
            AddTrainGroupView(TrainGroupType.TrainGroup1);
            AddTrainGroupView(TrainGroupType.TrainGroup2);

            ConstControlCollection.Add(new GDIRectText
            {
                Text = " - 如果必要，将方向开关扳至\"0\"位，取消此过程\r\n等待直至\"司机室变更模式\"被激活，并将方向开关扳回至\"前进\"位",
                OutLineRectangle = new Rectangle(40, 300, 600, 70),
                Visible = true
            });
            ConstControlCollection.Add(new GDIRectText
            {
                Text = "自动速度控制联挂已响应！",
                OutLineRectangle = new Rectangle(40, 150, 380, 70),
                DrawFont = new Font("Arial", 20),
                Visible = true
            });
            ConstControlCollection.Add(new GDIRectText
            {
                Text = "V",
                OutLineRectangle = new Rectangle(40, 50, 20, 20),
                DrawFont = new Font("Arial", 20),
                Visible = true
            });
            ConstControlCollection.Add(new GDIRectText
            {
                Text = "实际",
                OutLineRectangle = new Rectangle(60, 55, 35, 15),
                DrawFont = new Font("Arial", 8),
                Visible = true
            });
            ConstControlCollection.Add(new GDIRectText
            {
                Text = "",
                OutLineRectangle = new Rectangle(40, 100, 120, 50),
                BackColorVisible = true,
                BkColor = Color.White,
                DrawFont = new Font("Arial", 8),
                TextFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                },
                Visible = true
            });
            var tgv = (TrainGroupView) ConstControlCollection.First(f => f is TrainGroupView && f.Tag.Equals("动车组 1"));
            var tgv1 = (TrainGroupView) ConstControlCollection.First(f => f is TrainGroupView && f.Tag.Equals("动车组 2"));
            var size = new Size(26, 26);
            var matrix = new Matrix();

            ActiveControlCollection.Add(new CouplerHatchesViewItem(CouplerHatchesUnit.Unit1, CouplerHatchesState.Close)
            {
                RefreshAction = o => RefreshItemView((CouplerHatchesViewItem) o),
                OutLineRectangle = new Rectangle(tgv.LeftEdgeLocation.Translate(-size.Width/2, 0), size),
                Tag =
                    new[]
                    {
                        "动车组1前车钩状态第0位",
                        "动车组1前车钩状态第1位",
                        "动车组1前车钩状态第2位"
                    },
            });
            ActiveControlCollection.Add(new CouplerHatchesViewItem(CouplerHatchesUnit.Unit1, CouplerHatchesState.Close)
            {
                RefreshAction = o => RefreshItemView((CouplerHatchesViewItem) o),
                OutLineRectangle = new Rectangle(tgv.RightEdgeLocation.Translate(-size.Width/2, 0), size),
                Tag =
                    new[]
                    {
                        "动车组1后车钩状态第0位",
                        "动车组1后车钩状态第1位",
                        "动车组1后车钩状态第2位"
                    },
                Visible = true
            });
            ActiveControlCollection.Add(new CouplerHatchesViewItem(CouplerHatchesUnit.Unit1, CouplerHatchesState.Close)
            {
                RefreshAction = o => RefreshItemView((CouplerHatchesViewItem) o),
                OutLineRectangle = new Rectangle(tgv1.LeftEdgeLocation.Translate(-size.Width/2, 0), size),
                Tag =
                    new[]
                    {
                        "动车组2前车钩状态第0位",
                        "动车组2前车钩状态第1位",
                        "动车组2前车钩状态第2位"
                    },
                Visible = true
            });
            ActiveControlCollection.Add(new CouplerHatchesViewItem(CouplerHatchesUnit.Unit1, CouplerHatchesState.Close)
            {
                RefreshAction = o => RefreshItemView((CouplerHatchesViewItem) o),
                OutLineRectangle = new Rectangle(tgv1.RightEdgeLocation.Translate(-size.Width/2, 0), size),
                Tag =
                    new[]
                    {
                        "动车组2后车钩状态第0位",
                        "动车组2后车钩状态第1位",
                        "动车组2后车钩状态第2位"
                    },
                Visible = true
            });

        }


        public override void Reset()
        {
            BottomBtnContentBuffer = new List<string> {"", "", "", "", "", "", "", "", "", "",};

            base.Reset();
        }

        protected override void OnResponseUser(BottomBtnType btnType, bool isDown)
        {
            switch (btnType)
            {
                case BottomBtnType.Btn01:
                    if (BottomBtnContentBuffer[(int) btnType].ToString(CultureInfo.InvariantCulture) != string.Empty)
                    {
                        SourceObj.append_postCmd(CmdType.SetBoolValue, SourceObj.GetOutBoolIndex("请求动车组1前车钩打开"),
                            isDown ? 1 : 0, 0);
                    }
                    break;
                case BottomBtnType.Btn02:
                    break;
                case BottomBtnType.Btn03:
                    if (BottomBtnContentBuffer[(int) btnType].ToString(CultureInfo.InvariantCulture) != string.Empty)
                    {
                        SourceObj.append_postCmd(CmdType.SetBoolValue, SourceObj.GetOutBoolIndex("请求动车组1后车钩打开"),
                            isDown ? 1 : 0, 0);
                    }
                    break;
                case BottomBtnType.Btn04:
                    break;
                case BottomBtnType.Btn05:
                    break;
                case BottomBtnType.Btn06:
                    break;
                case BottomBtnType.Btn07:
                    break;
                case BottomBtnType.Btn08:
                    if (BottomBtnContentBuffer[(int) btnType].ToString(CultureInfo.InvariantCulture) != string.Empty)
                    {
                        SourceObj.append_postCmd(CmdType.SetBoolValue, SourceObj.GetOutBoolIndex("列车就绪"), isDown ? 1 : 0,
                            0);
                    }
                    break;
                case BottomBtnType.Btn09:
                    break;
                case BottomBtnType.Btn10:
                    SourceObj.append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }
    }
}