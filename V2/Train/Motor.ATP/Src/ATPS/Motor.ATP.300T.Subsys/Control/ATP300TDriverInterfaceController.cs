using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtil.Util;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Interface.UserAction.UpdateStateParam;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300T.Subsys.Control
{
    public class ATP300TDriverInterfaceController : DriverInterfaceController
    {
        private readonly Queue<IInfomationItem> m_InfomationItemQueue;

        private readonly Queue<IDriverInterface> m_DriverInterfaceQueue;

        public ATP300TDriverInterfaceController(ATPDomain atp, IDriverInterfaceFactory factory,
            IDriverInterfaceView interfaceView)
            : base(atp)
        {
            m_InfomationItemQueue = new Queue<IInfomationItem>();
            m_DriverInterfaceQueue = new Queue<IDriverInterface>();
            DriverInterfaceFactory = factory;
            DriverInterfaceView = interfaceView;
        }

        protected override void OnInfomationBegin(IInfomationItem infomationItem)
        {
            if (infomationItem.Content.IsOnlyTextInfo())
            {
                return;
            }

            if (infomationItem.Content.NextControlType != ControlType.Unknown)
            {
                Parent.ControlModel.NextControlType = infomationItem.Content.NextControlType;
                Parent.ControlModel.NextControlTypeVisible = true;

                if (CurrentInterface.Id != DriverInterfaceKey.Root)
                {
                    m_InfomationItemQueue.Enqueue(infomationItem);

                    return;
                }
            }

            CacheDriverInterfaceIfNeed(CurrentInterface);

            UpdateInterfaceByResponseType(infomationItem);
        }

        private void UpdateInterfaceByResponseType(IInfomationItem infomationItem)
        {
            var ep = new UpdateDriverInterfaceEventParam(this)
            {
                InfomationItem = infomationItem,
                State = InformationState.Begin
            };
            switch (infomationItem.Content.ResponseType)
            {
                case InfomationResponseType.System:
                    break;
                case InfomationResponseType.Ok:
                    UpdateDriverInterface(infomationItem.Content.ShowType.HasPopupView()
                        ? DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认有弹出框)
                        : DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认无弹出框), ep);
                    break;
                case InfomationResponseType.OkCancel:
                    UpdateDriverInterface(infomationItem.Content.ShowType.HasPopupView()
                        ? DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消有弹出框)
                        : DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消无弹出框), ep);
                    break;
                case InfomationResponseType.OkDelteCancel:
                    UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认删除取消), ep);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnInfomationEnsured(IInfomationItem infomationItem)
        {
            if (CurrentInterface.CurrentInfomationItem == infomationItem)
            {
                CurrentInterface.CurrentInfomationItem = null;
                if (!UpdateByCache())
                {
                    UpdateDriverInterface(DriverInterfaceKey.Root);
                }
            }

            ResetNextControlTypeIfNeed(infomationItem);
        }

        private void ResetNextControlTypeIfNeed(IInfomationItem infomationItem)
        {
            if (infomationItem.Content.IsOnlyTextInfo())
            {
                return;
            }

            if (infomationItem.Content.NextControlType != ControlType.Unknown)
            {
                Parent.ControlModel.NextControlType = ControlType.Unknown;
                Parent.ControlModel.NextControlTypeVisible = false;
            }
        }

        protected override void OnInfomationEnd(IInfomationItem infomationItem)
        {
            ResetNextControlTypeIfNeed(infomationItem);
        }

        protected override bool OnCurrentInterfaceChanging(IDriverInterfaceController sender, IUpdateDriverInterfaceParam updateParam,
            IDriverInterface old, IDriverInterface current)
        {
            if (old != null && old.CurrentInfomationItem != null)
            {
                CacheDriverInterfaceIfNeed(current);
                return false;
            }

            return base.OnCurrentInterfaceChanging(sender, updateParam, old, current);
        }

        private bool CacheDriverInterfaceIfNeed(IDriverInterface current)
        {
            if (m_DriverInterfaceQueue.Any())
            {
                // 防止一个事件一值确认不通过
                if (current.Id != m_DriverInterfaceQueue.Peek().Id && current.Id.ToString() != DriverInterfaceKeys.Root)
                {
                    m_DriverInterfaceQueue.Enqueue(current);

                    return true;
                }
            }
            else
            {
                m_DriverInterfaceQueue.Enqueue(current);

                return true;
            }

            return false;
        }

        protected override void OnCurrentInterfaceChanged(IDriverInterfaceController sender, IUpdateDriverInterfaceParam updateParam, IDriverInterface old,
            IDriverInterface current)
        {
            base.OnCurrentInterfaceChanged(sender, updateParam, old, current);

            UpdateByCache();
        }

        private bool UpdateByCache()
        {
            if (m_InfomationItemQueue.Any())
            {
                UpdateInterfaceByResponseType(m_InfomationItemQueue.Dequeue());
                return true;
            }

            if (m_DriverInterfaceQueue.Any())
            {
                UpdateDriverInterface(m_DriverInterfaceQueue.Dequeue(),
                    new UpdateDriverInterfaceParam(m_DriverInterfaceQueue));
                return true;
            }

            return false;
        }

        protected override void ClearWhenCourseStoped()
        {
            m_InfomationItemQueue.Clear();
            m_DriverInterfaceQueue.Clear();
            CurrentInterface.CurrentInfomationItem = null;
            base.ClearWhenCourseStoped();
        }
    }
}