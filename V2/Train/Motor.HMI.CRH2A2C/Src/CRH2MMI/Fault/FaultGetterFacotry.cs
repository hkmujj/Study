using System;

namespace CRH2MMI.Fault
{
    partial class FaultMgr
    {
        internal class FaultGetterFacotry
        {
            private readonly FaultMgr m_FaultMgr;

            public FaultGetterFacotry(FaultMgr faultMgr)
            {
                m_FaultMgr = faultMgr;
            }

            public IFaultGetter CreateGetter(FaultGetterType type)
            {
                IFaultGetter faultGetter = null;
                switch (type)
                {
                    case FaultGetterType.All:
                        faultGetter = new AllFaultGetter();
                        break;
                    case FaultGetterType.NotDel:
                        faultGetter = new ActiveFaultGetter();
                        break;
                    case FaultGetterType.ForView:
                        faultGetter = new ViewFaultGetter();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("type");
                }
                m_FaultMgr.m_FaultGetterList.Add(faultGetter);
                m_FaultMgr.FaultChangedEvent += faultGetter.OnFaultChangedEvent;
                return faultGetter;
            }


        }
    }

    enum FaultGetterType
    {
        All,

        NotDel,

        ForView,
    }
}
