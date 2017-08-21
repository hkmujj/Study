using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;

namespace Motor.HMI.CRH3C380B.Base.Fault
{
    public class FaultViewResume : FaultItemViewBase
    {
        private readonly string[] m_ButtomBtnContens;

        /// <summary>
        /// 故障概况中行数绘制起点
        /// </summary>
        private int m_FaultListFirstRowId;

        private bool m_LayoutChanged;

        public const int CountPerPage = 20;

        public override int BelongToView
        {
            get { return 4; }
        }

        public override FaultViewType ViewType
        {
            get { return FaultViewType.Resume; }
        }

        public override string[] ButtomBtnContens
        {
            get { return m_ButtomBtnContens; }
        }

        public override void NavigateToThis(bool needReset)
        {
            base.NavigateToThis(needReset);

            GotoNextNotConfirm();

            if (needReset)
            {
                ReselectFault();
            }
        }

        private void GotoNextNotConfirm()
        {
            if (CurrentSelectedFaultInfo != null)
            {
                while (CurrentSelectedFaultInfo.TheMsgFlag &&
                       FaultCollection.LastOrDefault() != CurrentSelectedFaultInfo)
                {
                    GoDown();
                }

                if (CurrentSelectedFaultInfo != null && (FaultCollection.LastOrDefault() == CurrentSelectedFaultInfo && CurrentSelectedFaultInfo.TheMsgFlag))
                {
                    ReselectFault();
                }
            }
        }

        private void ReselectFault()
        {
            CurrentSelectedFaultInfo = FaultCollection.FirstOrDefault();
            m_FaultListFirstRowId = 0;
            while (CurrentSelectedFaultInfo != null && (CurrentSelectedFaultInfo.TheMsgFlag &&
                                                        FaultCollection.LastOrDefault() != CurrentSelectedFaultInfo))
            {
                GoDown();
            }
            if (CurrentSelectedFaultInfo != null && (FaultCollection.LastOrDefault() == CurrentSelectedFaultInfo && CurrentSelectedFaultInfo.TheMsgFlag))
            {
                CurrentSelectedFaultInfo = FaultCollection.FirstOrDefault();
                m_FaultListFirstRowId = 0;
            }
        }

        public override void ResponseUser()
        {
            base.ResponseUser();

            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }
            switch (DMIButton.BtnUpList[0])
            {
                //报告
                case 6: 
                    if (CurrentViewId == 4)
                    {
                        OnNavigateToView(FaultViewType.Report);
                    }
                    break;

                //删除标记
                case 9: 
                    if (CurrentViewId == 4 && MsgHandler.CurrentMsgList.Count != 0)
                    {

                        if (MsgHandler.CurrentMsgList[CurrentSelectedFaultIndex].TheMsgFlag)
                        {
                            MsgHandler.CurrentMsgRest(MsgHandler.CurrentMsgList[CurrentSelectedFaultIndex].MsgLogicID);
                            append_postCmd(CmdType.SetBoolValue,
                                MsgHandler.CurrentMsgList[CurrentSelectedFaultIndex].MsgSendLogicID, 0,
                                0);
                        }
                    }
                    break;

                //更改布局
                case 12: 
                    m_LayoutChanged = !m_LayoutChanged;
                    break;
            }
        }

        public override void OnDraw(Graphics g)
        {
            g.DrawString("故障概况", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                RectsList[3], FontsItems.TheAlignment(FontRelated.靠左));
            g.DrawString(
                "第 " +
                CurrentSelectedFaultRow + "行共 " +
                (MsgHandler.CurrentMsgList.Count == 0
                    ? " "
                    : MsgHandler.CurrentMsgList.Count.ToString(CultureInfo.InvariantCulture)) + " 行",
                FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                RectsList[6], FontsItems.TheAlignment(FontRelated.靠右));
            if (MsgHandler.CurrentMsgList.Count != 0)
            {
                var rowNumb = MsgHandler.CurrentMsgList.Count - m_FaultListFirstRowId;
                var rowIndex = 0;
                for (var index = m_FaultListFirstRowId;
                    index < m_FaultListFirstRowId + (rowNumb > 20 ? 20 : rowNumb);
                    index++)
                {
                    #region :::::::::::::: 已读标记 :::::::::::::::::

                    if (MsgHandler.CurrentMsgList[index].TheMsgFlag)
                    {
                        g.DrawString("*", FontsItems.FontC11,
                            DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                            RectsList[8 + rowIndex*6], FontsItems.TheAlignment(FontRelated.靠左));
                    }

                    #endregion

                    #region ::::::::::::::: 车厢号 ::::::::::::::::::

                    g.DrawString(MsgHandler.CurrentMsgList[index].FullCarriageID, FontsItems.FontC11,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        RectsList[8 + rowIndex*6], FontsItems.TheAlignment(FontRelated.靠右));

                    #endregion

                    #region :::::::::: 故障类型或者代码 :::::::::::::

                    g.DrawString(m_LayoutChanged
                        ? MsgHandler.CurrentMsgList[index].MsgID
                        : MsgHandler.CurrentMsgList[index].FaultTypeName, FontsItems.FontC11,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        RectsList[9 + rowIndex*6], FontsItems.TheAlignment(FontRelated.靠右));

                    #endregion

                    #region :::::::::::::: 故障名称 :::::::::::::::::

                    g.DrawString(MsgHandler.CurrentMsgList[index].FaultName, FontsItems.FontC11,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        RectsList[10 + rowIndex*6], FontsItems.TheAlignment(FontRelated.靠左));

                    #endregion

                    #region :::::::::::::: 故障日期 :::::::::::::::::

                    if (!m_LayoutChanged)
                    {
                        g.DrawString(MsgHandler.CurrentMsgList[index].MsgReceiveTime.ToString("MM.dd."),
                            FontsItems.FontC11,
                            DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                            RectsList[11 + rowIndex*6], FontsItems.TheAlignment(FontRelated.靠右));
                    }

                    #endregion

                    #region :::::::::::::: 故障时间 :::::::::::::::::

                    g.DrawString(MsgHandler.CurrentMsgList[index].MsgReceiveTime.ToString("HH:mm:ss"),
                        FontsItems.FontC11,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        RectsList[12 + rowIndex*6], FontsItems.TheAlignment(FontRelated.靠右));

                    #endregion

                    rowIndex++;
                }
                g.DrawRectangle(DMITitle.NightMode ? PenItems.BlackPen : PenItems.WhiltPen,
                    Rectangle.Round(RectsList[7 + (CurrentSelectedFaultIndex > 19 ? 19 : CurrentSelectedFaultIndex)*6]));
            }
        }

        protected override void GoUp()
        {
            base.GoUp();

            if (m_FaultListFirstRowId > 0)
            {
                --m_FaultListFirstRowId;
            }
        }

        protected override void GoDown()
        {
            base.GoDown();

            if (CurrentSelectedFaultIndex > 19 && CurrentSelectedFaultIndex + 1 < FaultCollection.Count)
            {
                m_FaultListFirstRowId++;
            }
        }

        public FaultViewResume(DMIFault targetObj, IList<RectangleF> rectsList) : base(targetObj, rectsList)
        {
            m_ButtomBtnContens = new[] {"报告", "", "", "删除\n标记", "", "", "更改\n布局", "", "", "返回"};
        }
    }
}