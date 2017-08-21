using System;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Resource;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.Appraise
{
    /// <summary>
    /// 评价
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Appraise : CRH2BaseClass
    {
        private AppraiseResource m_Resource;
        public override bool Init()
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

        public override void NavigateTo(ViewConfig toThis)
        {
            switch (ScreenId)
            {
                case ScreenId.CRH2:

                    append_postCmd(CmdType.SetFloatValue,
                        IndexDescriptionConfig.OutFloatDescriptionDictionary[OufKeys.给评价的1屏当前界面], 0, (float)toThis);

                    break;
                case ScreenId.CRH2_2:

                    append_postCmd(CmdType.SetFloatValue,
                        IndexDescriptionConfig.OutFloatDescriptionDictionary[OufKeys.给评价的2屏当前界面], 0, (float)toThis);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetState(ViewConfig viewConfig, int value)
        {
            var index = m_Resource.GetIndexOf(viewConfig);
            if (-1 != index)
            {
                append_postCmd(CmdType.SetBoolValue, index, value, 0);
            }
        }
    }
}
