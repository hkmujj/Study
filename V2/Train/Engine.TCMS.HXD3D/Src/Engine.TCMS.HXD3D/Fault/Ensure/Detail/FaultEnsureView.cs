using System;
using System.Collections.Generic;
using CommonUtil.Controls;

namespace Engine.TCMS.HXD3D.Fault.Ensure.Detail
{
    public class FaultEnsureView: CommonInnerControlBase
    {
        public Action<FaultMsgHXD3D> EnsureItem;

        public Action ReturnAction;

        public FaultEnsureView(FaultEnsure targetObject,EnsureType ensureType)
        {
            EnsureType = ensureType;
            TargetObject = targetObject;
        }

        public FaultEnsure TargetObject { get; private set; }

        public EnsureType EnsureType { get; private set; }

        public virtual void NavigateToThis(FaultMsgHXD3D[] msgs)
        {
            
        }

        protected virtual void OnEnsureItem(FaultMsgHXD3D obj)
        {
            if (EnsureItem != null)
            {
                EnsureItem.Invoke(obj);
            }
        }

        protected virtual void OnReture()
        {
            //ReturnAction?.Invoke();
            if (ReturnAction !=null)
            {
                ReturnAction.Invoke();
            }
        }
    }
}