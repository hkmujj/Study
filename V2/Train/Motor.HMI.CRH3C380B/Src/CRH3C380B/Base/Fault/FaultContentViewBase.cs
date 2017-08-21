using System;
using System.Collections.Generic;
using System.Drawing;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.Fault
{
    public abstract class FaultContentViewBase : FaultItemViewBase
    {
        // ReSharper disable once InconsistentNaming
        protected bool m_LayoutChanged;

        public abstract Font ContentFont { get; }

        public override void OnDraw(Graphics g)
        {
            g.DrawImage(DMITitle.NightMode ? TitleImages.faultTitle_1_1 : TitleImages.faultTitle_1_0, RectsList[1]);

            var currentMsg = CurrentSelectedFaultInfo;
            if (currentMsg == null)
            {
                return;
            }

            g.DrawString(GetCurrentMsgContent(),
                ContentFont,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                RectsList[0], FontsItems.TheAlignment(FontRelated.靠左上));

            #region ::::::::::::::: 车厢号 ::::::::::::::::::

            g.DrawString(currentMsg.FullCarriageID, FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                RectsList[2], FontsItems.TheAlignment(FontRelated.靠左));

            #endregion

            #region :::::::::: 故障类型或者代码 :::::::::::::

            g.DrawString(m_LayoutChanged
                ? currentMsg.MsgID
                : currentMsg.FaultTypeName, FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                RectsList[2], FontsItems.TheAlignment(FontRelated.靠右));

            #endregion

            #region :::::::::::::: 故障名称 :::::::::::::::::

            g.DrawString(currentMsg.FaultName, FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                RectsList[3], FontsItems.TheAlignment(FontRelated.靠左));

            #endregion

            #region :::::::::::::: 计时器 :::::::::::::::::::

            #endregion

            #region ::::::::::::::: 名称 ::::::::::::::::::::

            g.DrawString(GetCurrentTitle(),
                FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                RectsList[5], FontsItems.TheAlignment(FontRelated.居中));

            #endregion
        }

        private string GetCurrentTitle()
        {
            switch (ViewType)
            {
                case FaultViewType.VLargerThan0:
                    return "V > 0";
                case FaultViewType.VEq0:
                    return "V = 0";
                case FaultViewType.Report:
                    return "报告";
                default:
                    throw new ArgumentOutOfRangeException();
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
                // 故障概况
                case 7:
                    OnNavigateToView(FaultViewType.Resume);
                    break;
            }

        }

        protected abstract string GetCurrentMsgContent();

        protected FaultContentViewBase(DMIFault targetObj, IList<RectangleF> rectsList) : base(targetObj, rectsList)
        {
        }
    }
}