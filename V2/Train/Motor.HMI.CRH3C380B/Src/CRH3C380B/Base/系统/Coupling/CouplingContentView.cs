using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统.Coupling
{
    public abstract class CouplingContentView : CommonInnerControlBase
    {
        protected List<CommonInnerControlBase> ConstControlCollection;

        protected List<CouplerHatchesViewItem> ActiveControlCollection;

        protected List<string> BottomBtnContentBuffer;

        protected DMICoupling SourceObj;

        private Dictionary<string, int> m_InIndexDictionary;
        private readonly IList<string> m_List = new List<string>
        {
                                         "动车组1前车钩状态第0位",
                                         "动车组1前车钩状态第1位",
                                         "动车组1前车钩状态第2位",
                                         "动车组1后车钩状态第0位",
                                         "动车组1后车钩状态第1位",
                                         "动车组1后车钩状态第2位",
                                     };
        protected CouplingContentView(DMICoupling sourceobj)
        {
            SourceObj = sourceobj;

            ConstControlCollection = new List<CommonInnerControlBase> { new CouplerGuide() };
            ActiveControlCollection = new List<CouplerHatchesViewItem>();
        }

        protected void AddTrainGroupView(TrainGroupType type)
        {
            var location = new Point(45, 400);
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
            var tgv = new TrainGroupView(location)
                      {
                          Visible = type == TrainGroupType.TrainGroup1,
                          Tag = "动车组 " + (type == TrainGroupType.TrainGroup1 ? "1" : "2")
                      };
            const int txtHeight = 40;
            ConstControlCollection.Add(new GDIRectText
            {
                Text = "动车组 " + (type == TrainGroupType.TrainGroup1 ? "1" : "2"),
                OutLineRectangle =
                    new Rectangle(tgv.OutLineRectangle.X, tgv.OutLineRectangle.Y - txtHeight, TrainGroupView.DefaultWidth, txtHeight),
                TextFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
                Visible = type == TrainGroupType.TrainGroup1
            });
            ConstControlCollection.Add(tgv);
        }

        public virtual void Reset()
        {
            SourceObj.DMITitle.TitleName = "系统;编组联挂";

            SourceObj.DMITitle.UpdateBtnContent(BottomBtnContentBuffer);
        }

        private void ClearSendData()
        {
            SourceObj.append_postCmd(CmdType.SetBoolValue, SourceObj.GetOutBoolIndex("请求动车组1前车钩打开"), 0, 0);
            SourceObj.append_postCmd(CmdType.SetBoolValue, SourceObj.GetOutBoolIndex("请求动车组1后车钩打开"), 0, 0);
        }
        public override void OnDraw(Graphics g)
        {
            ClearSendData();
            if (SourceObj.DMIButton.BtnDownList.Count != 0)
            {
                OnResponseUser((BottomBtnType)(SourceObj.DMIButton.BtnDownList[0] - 6), true);
            }

            //if (SourceObj.DMIButton.BtnUpList.Count != 0)
            //{
            //    OnResponseUser((BottomBtnType)(SourceObj.DMIButton.BtnUpList[0] - 6), false);
            //}

            ConstControlCollection.ForEach(e => e.OnDraw(g));

            ActiveControlCollection.ForEach(e => e.OnPaint(g));
        }

        public void DrawSpeed(Graphics g)
        {
            if (ConstControlCollection[9].Visible)
            {
                g.DrawString(SourceObj.GetInFloatValue("EVC显示列车速度").ToString("0"), FontsItems.FontC12B, SolidBrushsItems.BlackBrush,
                         new RectangleF(40, 100, 120, 50),
                         new StringFormat
                         {
                             Alignment = StringAlignment.Center,
                             LineAlignment = StringAlignment.Center
                         });
            }

        }
        /// <summary>
        /// 刷新设置动车组 2 是否显示 
        /// </summary>
        /// <param name="bPara">true 显示 flase 不显示</param>
        public void RefreshVisible(bool bPara)
        {
            ConstControlCollection[3].Visible = bPara;
            ConstControlCollection[4].Visible = bPara;
            ActiveControlCollection[2].Visible = bPara;
            ActiveControlCollection[3].Visible = bPara;
        }

        /// <summary>
        /// 设置准备就绪状态
        /// </summary>
        /// <param name="bPara">true 显示 flase 不显示</param>
        public virtual void PrepareReady(bool bPara)
        {
            ConstControlCollection[9].Visible = bPara;
            ConstControlCollection[8].Visible = bPara;
            ConstControlCollection[7].Visible = bPara;
            ConstControlCollection[6].Visible = bPara;
        }
        /// <summary>
        /// 刷新标题栏显示文字
        /// </summary>
        public void RefreshTitleName()
        {
            if (m_InIndexDictionary == null)
            {
                m_InIndexDictionary = m_List.ToDictionary(s => s, s => SourceObj.GetInBoolIndex(s));
            }
            for (int i = 0; i < BottomBtnContentBuffer.Count; i++)
            {
                BottomBtnContentBuffer[i] = string.Empty;
            }
            if (!ConstControlCollection[6].Visible)
            {
                int[] boolvalues = { 0 };
                int[] boolvalues1 = { 0 };
                bool[] bools = {
                               SourceObj.BoolList[m_InIndexDictionary[m_List[0]]], SourceObj.BoolList[m_InIndexDictionary[m_List[1]]],
                               SourceObj.BoolList[m_InIndexDictionary[m_List[2]]]
                           };
                bool[] bools1 = {
                                SourceObj.BoolList[m_InIndexDictionary[m_List[3]]], SourceObj.BoolList[m_InIndexDictionary[m_List[4]]],
                                SourceObj.BoolList[m_InIndexDictionary[m_List[5]]]
                            };
                BitArray br = new BitArray(bools);
                BitArray br1 = new BitArray(bools1);
                br.CopyTo(boolvalues, 0);
                br1.CopyTo(boolvalues1, 0);
                if (boolvalues[0] == 1 || boolvalues1[0] == 1)
                {
                    BottomBtnContentBuffer[7] = "列车\n就绪";
                }
                else
                {
                    BottomBtnContentBuffer[7] = string.Empty;
                    BottomBtnContentBuffer[0] = (boolvalues[0] == 0 || boolvalues[0] == 4 || boolvalues[0] == 6) ? "打开前车钩罩" : string.Empty;
                    BottomBtnContentBuffer[2] = (boolvalues1[0] == 0 || boolvalues1[0] == 4 || boolvalues1[0] == 6) ? "打开后车钩罩" : string.Empty;
                }
            }
            BottomBtnContentBuffer[9] = "主页面";
            SourceObj.DMITitle.UpdateBtnContent(BottomBtnContentBuffer);
        }
        protected virtual void OnResponseUser(BottomBtnType btnType, bool isDown)
        {

        }

        protected void RefreshItemView(CouplerHatchesViewItem item)
        {
            var indexName = item.Tag as string[];
            if (indexName == null || indexName.Length < 3)
            {
                AppLog.Debug(string.Format("Can not RefreshItemView, the indexName.Legth must >= 3"));
                return;
            }
            item.State = CouplerHatchesState.Close;
            int[] boolvalues = { 0 };
            bool[] blList = new bool[3];
            blList[0] = SourceObj.GetInBoolValue(indexName[0]);
            blList[1] = SourceObj.GetInBoolValue(indexName[1]);
            blList[2] = SourceObj.GetInBoolValue(indexName[2]);
            BitArray br = new BitArray(blList);
            br.CopyTo(boolvalues, 0);
            if (boolvalues[0] == 4 || boolvalues[0] == 6)
            {
                return;
            }
            item.State = (CouplerHatchesState)boolvalues[0];
        }
    }
}