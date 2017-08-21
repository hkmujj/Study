using System;
using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Resource;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    public abstract class StateInterfaceBase : NotificationObject, IStateInterface
    {
        private BtnItem m_BtnF8;
        private BtnItem m_BtnF7;
        private BtnItem m_BtnF6;
        private BtnItem m_BtnF5;
        private BtnItem m_BtnF4;
        private BtnItem m_BtnF3;
        private BtnItem m_BtnF2;
        private BtnItem m_BtnF1;
        private string m_Title;

        public string Title
        {
            get { return m_Title; }
            protected set
            {
                if (value == m_Title)
                {
                    return;
                }
                m_Title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public abstract StateInterfaceKey Id { get; }

        public BtnItem BtnF1
        {
            get { return m_BtnF1; }
            protected set
            {
                if (Equals(value, m_BtnF1))
                {
                    return;
                }
                m_BtnF1 = value;
                RaisePropertyChanged(() => BtnF1);
            }
        }

        public BtnItem BtnF2
        {
            get { return m_BtnF2; }
            protected set
            {
                if (Equals(value, m_BtnF2))
                {
                    return;
                }
                m_BtnF2 = value;
                RaisePropertyChanged(() => BtnF2);
            }
        }

        public BtnItem BtnF3
        {
            get { return m_BtnF3; }
            protected set
            {
                if (Equals(value, m_BtnF3))
                {
                    return;
                }
                m_BtnF3 = value;
                RaisePropertyChanged(() => BtnF3);
            }
        }

        public BtnItem BtnF4
        {
            get { return m_BtnF4; }
            protected set
            {
                if (Equals(value, m_BtnF4))
                {
                    return;
                }
                m_BtnF4 = value;
                RaisePropertyChanged(() => BtnF4);
            }
        }

        public BtnItem BtnF5
        {
            get { return m_BtnF5; }
            protected set
            {
                if (Equals(value, m_BtnF5))
                {
                    return;
                }
                m_BtnF5 = value;
                RaisePropertyChanged(() => BtnF5);
            }
        }

        public BtnItem BtnF6
        {
            get { return m_BtnF6; }
            protected set
            {
                if (Equals(value, m_BtnF6))
                {
                    return;
                }
                m_BtnF6 = value;
                RaisePropertyChanged(() => BtnF6);
            }
        }

        public BtnItem BtnF7
        {
            get { return m_BtnF7; }
            protected set
            {
                if (Equals(value, m_BtnF7))
                {
                    return;
                }
                m_BtnF7 = value;
                RaisePropertyChanged(() => BtnF7);
            }
        }

        public BtnItem BtnF8
        {
            get { return m_BtnF8; }
            protected set
            {
                if (Equals(value, m_BtnF8))
                {
                    return;
                }
                m_BtnF8 = value;
                RaisePropertyChanged(() => BtnF8);
            }
        }

        [Import]
        protected IEventAggregator EventAggregator { private set; get; }

        protected void SetF1OK(StateInterfaceKey changeToAfterOk, Action okAction = null)
        {
            var act = ServiceLocator.Current.GetInstance<OkActionResponser>();
            act.ChangToAfterOk = changeToAfterOk;
            if (okAction != null)
            {
                act.OkAction += okAction;
            }
            BtnF1 = new BtnItem(ResourceKeys.OK, act);
        }

        protected void SetF2Cancel(StateInterfaceKey changeToAfterCancel, Action cancelAction = null)
        {
            var act = ServiceLocator.Current.GetInstance<CancelActionResponser>();
            act.ChangToAfterCancel= changeToAfterCancel;
            if (cancelAction != null)
            {
                act.CancelAction += cancelAction;
            }
            BtnF2 = new BtnItem(ResourceKeys.Cancel, act);
        }

        protected void SetF8Return()
        {
            SetF8Return<ReturnToRootActionResponser>();
        }

        protected void SetF8Return<T>() where T : IBtnActionResponser
        {
            BtnF8 = new BtnItem(ResourceKeys.Return, GetActionResponser<T>());
        }

        protected T GetActionResponser<T>() where T : IBtnActionResponser
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        protected CompositePresentationEvent<T> GetEvent<T>()
        {
            return EventAggregator.GetEvent<CompositePresentationEvent<T>>();
        }


        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => Title);
            RaisePropertyChanged(() => BtnF1);
            RaisePropertyChanged(() => BtnF2);
            RaisePropertyChanged(() => BtnF3);
            RaisePropertyChanged(() => BtnF4);
            RaisePropertyChanged(() => BtnF5);
            RaisePropertyChanged(() => BtnF6);
            RaisePropertyChanged(() => BtnF7);
            RaisePropertyChanged(() => BtnF8);

            if (BtnF1 != null)
            {
                BtnF1.RaiseResourceChanged();
            }
            if (BtnF2 != null)
            {
                BtnF2.RaiseResourceChanged();
            }
            if (BtnF3 != null)
            {
                BtnF3.RaiseResourceChanged();
            }
            if (BtnF4 != null)
            {
                BtnF4.RaiseResourceChanged();
            }
            if (BtnF5 != null)
            {
                BtnF5.RaiseResourceChanged();
            }
            if (BtnF6 != null)
            {
                BtnF6.RaiseResourceChanged();
            }
            if (BtnF7 != null)
            {
                BtnF7.RaiseResourceChanged();
            }
            if (BtnF8 != null)
            {
                BtnF8.RaiseResourceChanged();
            }
        }
    }
}