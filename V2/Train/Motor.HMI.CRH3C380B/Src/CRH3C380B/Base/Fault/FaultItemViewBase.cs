using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using MsgReceiveMod;

namespace Motor.HMI.CRH3C380B.Base.Fault
{
    public abstract class FaultItemViewBase
    {
        protected readonly IList<RectangleF> RectsList;
        protected readonly DMIFault TargetObj;

        /// <summary>
        /// 导航请求
        /// </summary>
        public event Action<FaultViewType> NavigateToView;

        /// <summary>
        /// 所属视图
        /// </summary>
        public abstract int BelongToView { get; }

        protected MsgHandlerFault0<FaultInfo> MsgHandler
        {
            get { return TargetObj.MsgHandler; }
        }

        protected List<FaultInfo> FaultCollection { get { return MsgHandler.CurrentMsgList; } }

        protected DMITitle DMITitle
        {
            get { return TargetObj.DMITitle; }
        }

        protected DMIButton DMIButton
        {
            get { return TargetObj.DMIButton; }
        }

        protected int CurrentViewId
        {
            get { return TargetObj.CurrentViewId; }
        }

        protected int LastViewIdNotFault
        {
            get { return TargetObj.LastViewIdNotFault; }
        }

        /// <summary>
        /// 故障内容    
        /// </summary>
        public FaultInfo CurrentSelectedFaultInfo { set; get; }

        protected string CurrentSelectedFaultRow
        {
            get
            {
                if (CurrentSelectedFaultInfo != null)
                {
                    return (FaultCollection.IndexOf(CurrentSelectedFaultInfo) + 1).ToString();
                }
                return " ";
            }
        }

        protected int CurrentSelectedFaultIndex
        {
            get
            {
                if (CurrentSelectedFaultInfo != null)
                {
                    return FaultCollection.IndexOf(CurrentSelectedFaultInfo);
                }
                return -1;
            }
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        public abstract FaultViewType ViewType { get; }

        /// <summary>
        /// 按键内容
        /// </summary>
        public abstract string[] ButtomBtnContens { get; }

        public virtual void NavigateToThis(bool needReset)
        {
            DMITitle.UpdateBtnContent(ButtomBtnContens);
        }

        public virtual void OnFaultChanged()
        {
            if (!FaultCollection.Contains(CurrentSelectedFaultInfo))
            {
                CurrentSelectedFaultInfo = FaultCollection.FirstOrDefault();
            }
        }

        /// <summary>
        /// 响应用户按键
        /// </summary>
        public virtual void ResponseUser()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }
            switch (DMIButton.BtnUpList[0])
            {
                // 返回
                case 0:
                case 15:
                    if (CurrentViewId == 4)
                    {
                        append_postCmd(CmdType.ChangePage,
                            LastViewIdNotFault != 0 ? LastViewIdNotFault : (DMITitle.CurrentMMIName == MMIType.右侧MMI屏 ? 11 : 21), 0,
                            0);

                    }
                    else if (CurrentViewId == 41 || CurrentViewId == 42 || CurrentViewId == 43)
                    {
                        OnNavigateToView(FaultViewType.Resume);
                    }
                    break;
                case 3: //光标向上
                    GoUp();
                    break;
                case 4: //光标向下
                    GoDown();
                    break;
                case 21:
                    OnNavigateToView(FaultViewType.Resume);
                    break;
                //V>0
                case 22: 
                    OnNavigateToView(FaultViewType.VLargerThan0);
                    break;
                    //V=0
                case 23:
                    OnNavigateToView(FaultViewType.VEq0);
                    break;
            }

        }

        protected virtual void GoDown()
        {
            if (CurrentSelectedFaultIndex == -1)
            {
                CurrentSelectedFaultInfo = FaultCollection.FirstOrDefault();
            }
            else
            {
                if (CurrentSelectedFaultIndex < FaultCollection.Count-1)
                {
                    CurrentSelectedFaultInfo = FaultCollection[FaultCollection.IndexOf(CurrentSelectedFaultInfo) + 1];
                }
            }
        }

        protected virtual void GoUp()
        {
            if (CurrentSelectedFaultIndex > 0)
            {
                CurrentSelectedFaultInfo = FaultCollection[FaultCollection.IndexOf(CurrentSelectedFaultInfo) -1];
            }
        }

        public abstract void OnDraw(Graphics g);

        protected FaultItemViewBase(DMIFault targetObj, IList<RectangleF> rectsList)
        {
            TargetObj = targetObj;
            RectsList = rectsList;

        }

        protected virtual void OnNavigateToView(FaultViewType obj)
        {
            var handler = NavigateToView;
            if (handler != null)
            {
                handler(obj);
            }
        }

        protected void append_postCmd(CmdType cmdType, int paraA, int paraB, float paraC)
        {
            TargetObj.append_postCmd(cmdType, paraA, paraB, paraC);
        }

    }
}