using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class CloseCouplerHatchesViewBase : CommonInnerControlBase, ICloseCouplerHatchesView
    {
        private Action<Graphics> m_Graphics;
        protected List<CommonInnerControlBase> ConstInfoCollection;
        private bool m_BlBefore;
        private bool m_BlAfter;
        protected List<SelectableCloseCouplerHatchesViewItemDecorator> CloseCouplerHatchesViewItemCollection;
        public event Action<ICouplerHatchesStateItem> CloseCouplerHatchesStateChanged;
        public event Action<ICloseCouplerHatchesView> CurrentSelectedCloseCouplerHatchesUnitChanged;
        protected CRH3C380BBase BaseClass;
        private readonly int[] m_Index = new int[4];
        private readonly bool[] m_BEntry = {false, false, false, false};

        protected CloseCouplerHatchesViewBase(CRH3C380BBase baseClass)
        {
            BaseClass = baseClass;
            CloseCouplerHatchesViewItemCollection = new List<SelectableCloseCouplerHatchesViewItemDecorator>();
            ConstInfoCollection = new List<CommonInnerControlBase>
            {
                new CouplerGuide()
            };
            for (int i = 0; i < 4; i++)
            {
                m_Index[i] =
                    baseClass.GetOutBoolIndex(string.Format("请求动车组{0}{1}车钩关闭", i >= 2 ? "2" : "1",
                        (i + 1)%2 == 1 ? "前" : "后"));

            }

            InitalizeGrid();
        }

        private void InitalizeGrid()
        {
            var tgv = new TrainGroupView(new Point(55, 400)) {Tag = 1};
            var tgv1 = new TrainGroupView(new Point(tgv.RightEdgeLocation.X, 400)) {Tag = 2, Visible = false};
            ConstInfoCollection.Add(tgv);
            ConstInfoCollection.Add(tgv1);
            ConstInfoCollection.Add(new GDIRectText
            {
                Text = "在关闭钩罩前，请首先确认关闭动作不会产生损害！",
                TextColor = Color.White,
                OutLineRectangle = new Rectangle(tgv.LeftEdgeLocation.X, tgv.LeftEdgeLocation.Y - 150, 500, 20)
            });
        }

        public void Reset(bool bPara)
        {
            ConstInfoCollection[ConstInfoCollection.FindIndex(f => f is TrainGroupView && (int) f.Tag == 2)].Visible =
                bPara;
        }

        private void ClearSendValue()
        {
            foreach (var t in m_Index)
            {
                BaseClass.append_postCmd(CmdType.SetBoolValue, t, 0, 0);
            }
        }

        public override void OnDraw(Graphics g)
        {
            ClearSendValue();
            RespondBtnEvent();
            if (m_Graphics != null)
            {
                m_Graphics(g);
            }

            ConstInfoCollection.ForEach(e => e.OnDraw(g));

            CloseCouplerHatchesViewItemCollection.ForEach(e => e.OnPaint(g));
        }

        protected void RefreshItemView(CouplerHatchesViewItem item)
        {
            var indexName = item.Tag as string[];
            if (indexName == null || indexName.Length < 3)
            {
                AppLog.Debug("Can not RefreshItemView, the indexName.Legth must >= 3");
                return;
            }

            item.State = CouplerHatchesState.Close;
            int[] boolvalues = {0};
            bool[] blList = new bool[3];
            blList[0] = BaseClass.GetInBoolValue(indexName[0]);
            blList[1] = BaseClass.GetInBoolValue(indexName[1]);
            blList[2] = BaseClass.GetInBoolValue(indexName[2]);
            BitArray br = new BitArray(blList);
            br.CopyTo(boolvalues, 0);
            if (boolvalues[0] != 0 && boolvalues[0] != 4 && boolvalues[0] != 6)
            {
                item.State = (CouplerHatchesState) boolvalues[0];
            }
            if (item.OutLineRectangle.X < 80 && boolvalues[0] == 1)
            {
                BaseClass.DMITitle.BtnContentList[0].BtnStr = "关闭前车钩罩";
            }
            else if (item.OutLineRectangle.X < 80 && boolvalues[0] != 1)
            {
                BaseClass.DMITitle.BtnContentList[0].BtnStr = "";
            }
            if (item.OutLineRectangle.X > 80 && item.OutLineRectangle.X < 250 && boolvalues[0] == 1)
            {
                BaseClass.DMITitle.BtnContentList[2].BtnStr = "关闭后车钩罩";
            }
            else
            {
                BaseClass.DMITitle.BtnContentList[2].BtnStr = "";
            }
            if (item.OutLineRectangle.X > 250 && boolvalues[0] == 1)
            {
                BaseClass.DMITitle.BtnContentList[4].BtnStr = "关闭后车钩罩";
            }
            else
            {
                BaseClass.DMITitle.BtnContentList[4].BtnStr = "";
            }
        }

        private void SetBool(int index)
        {
            for (int i = 0; i < m_BEntry.Length; i++)
            {
                m_BEntry[i] = false;
            }

            if (index >= 0 && index <= 3)
            {
                m_BEntry[index] = true;
            }

        }

        private void RespondBtnEvent()
        {
            if (BaseClass.DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (BaseClass.DMIButton.BtnUpList[0])
            {
                case 0:
                    break;
                case 5:
                    DrawAffirmGraphics(string.Empty);
                    for (var i = 0; i < 4; i++)
                    {
                        if (m_BEntry[i])
                        {
                            BaseClass.append_postCmd(CmdType.SetBoolValue, m_Index[i], 1, 0);
                            break;
                        }
                    }

                    SetBool(-1);
                    break;
                case 6:
                    if (BaseClass.DMITitle.BtnContentList[0].BtnStr != string.Empty)
                    {
                        DrawAffirmGraphics(string.Format("确认：\n{0}", BaseClass.DMITitle.BtnContentList[0].BtnStr));
                        m_BlBefore = true;
                        m_BlAfter = false;
                        SetBool(0);
                    }
                    break;
                case 8:
                    if (BaseClass.DMITitle.BtnContentList[2].BtnStr != string.Empty)
                    {
                        DrawAffirmGraphics(string.Format("确认：\n{0}", BaseClass.DMITitle.BtnContentList[2].BtnStr));
                        m_BlBefore = false;
                        m_BlAfter = true;
                        SetBool(1);
                    }
                    break;
                case 10:
                    if (BaseClass.DMITitle.BtnContentList[4].BtnStr != string.Empty)
                    {
                        DrawAffirmGraphics(string.Format("确认：\n{0}", BaseClass.DMITitle.BtnContentList[4].BtnStr));
                        m_BlBefore = false;
                        m_BlAfter = true;
                        SetBool(3);
                    }
                    break;
                case 15: //主页面
                    BaseClass.append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        /// <summary>
        /// 绘制确认的框以及文字
        /// </summary>
        /// <param name="drawString">需要的文字</param>
        private void DrawAffirmGraphics(string drawString)
        {

            if (drawString == string.Empty && (m_BlBefore || m_BlAfter))
            {
                m_Graphics = null;
                if (m_BlBefore)
                {
                    BaseClass.DMITitle.BtnContentList[0].BtnStr = string.Empty;
                }
                if (m_BlAfter)
                {
                    BaseClass.DMITitle.BtnContentList[2].BtnStr = string.Empty;
                }
                return;
            }

            var location = new Point(660, 360);
            var sizeItem = new Size(120, 60);
            m_Graphics = g => g.FillRectangle(Brushes.White, location.X, location.Y, sizeItem.Width, sizeItem.Height);
            m_Graphics += g => g.DrawString(drawString, FontsItems.FontC12B, Brushes.Black, location);
        }

        public ReadOnlyCollection<ICouplerHatchesStateItem> AllCloseCouplerHatchesStateItemCollections
        {
            get { return CloseCouplerHatchesViewItemCollection.Cast<ICouplerHatchesStateItem>().ToList().AsReadOnly(); }
        }

        public ICouplerHatchesStateItem CurrentSelectedCloseCouplerHatchesUnit { get; private set; }


        public void ChangeAirconditionState(CouplerHatchesUnit unit, CouplerHatchesState state)
        {

        }

        public void Select(Direction direction)
        {

        }
    }
}
