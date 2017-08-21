using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Config;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Appraise
{
    /// <summary>
    /// 评价
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Appraise : CRH3C380BBase
    {
        private AppraiseResource m_Resource;



        public override bool Initalize()
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
                append_postCmd(CmdType.SetBoolValue, AppraiseOutbollListIndex(index), value, 0);
            }
        }

        /// <summary>
        /// 根据视图ID索引输出参数索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int AppraiseOutbollListIndex(int index)
        {
            var returnIndex = -1;
            switch (index)
            {
                case 130:
                    returnIndex = GetOutBoolIndex(OutBoolKeys.Oub给评价接口处于门界面);
                    break;
            }

            return returnIndex;
        }
    }
}