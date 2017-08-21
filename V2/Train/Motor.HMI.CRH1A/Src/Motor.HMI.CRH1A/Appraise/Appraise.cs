using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.Appraise
{
    /// <summary>
    /// 评价
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Appraise : CRH1BaseClass
    {
        private AppraiseResource m_Resource;



        public override bool Initialize()
        {
            GlobalViewNavigate.Instance.NavigeteTo += GlobalViewNavigateOnNavigeteTo;
            GlobalViewNavigate.Instance.NavigeteFrom += GlobalViewNavigateOnNavigeteFrom;
            m_Resource = new AppraiseResource();
            return true;
        }

        private void GlobalViewNavigateOnNavigeteFrom(ViewConfig viewConfig)
        {
            SetState(viewConfig, 0);
        }

        private void GlobalViewNavigateOnNavigeteTo(ViewConfig viewConfig)
        {
            SetState(viewConfig, 1);
        }

        private void SetState(ViewConfig viewConfig, int value)
        {
            var index = m_Resource.GetIndexOf(viewConfig);

            if (-1 != index)
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[index], value, 0);
            }
        }
    }
}
