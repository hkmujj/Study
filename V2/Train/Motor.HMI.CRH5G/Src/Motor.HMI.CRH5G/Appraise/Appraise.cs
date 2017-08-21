using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.Config;
using Motor.HMI.CRH5G.Resource;
using Motor.HMI.CRH5G.底层共用;

namespace Motor.HMI.CRH5G.Appraise
{
    /// <summary>
    /// 评价
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Appraise : CRH5GBase
    {
        private AppraiseResource m_Resource;

        private string[] logicName = new[]
        {
            OutBoolKeys.OutB屏处于高压系统显示页面TD屏1页面,
            OutBoolKeys.OutBTD屏处于第2页面,
            OutBoolKeys.OutBTD屏处于第3页面,
            OutBoolKeys.OutBTD屏处于第4页面,
            OutBoolKeys.OutBTD屏处于第5页面,
            OutBoolKeys.OutBTD屏处于第6页面,
            OutBoolKeys.OutBTD屏处于第7页面,
            OutBoolKeys.OutBTD屏处于第8页面,
            OutBoolKeys.OutBTD屏处于第9页面,
            OutBoolKeys.OutBTD屏处于第10页面,
            OutBoolKeys.OutBTD屏处于软件切除界面,
            OutBoolKeys.OutBTS屏处于仪器查询页面3个表盘,
            OutBoolKeys.OutBTS屏处于电子仪器第1页面,
            OutBoolKeys.OutBTS屏处于电子仪器第2页面,
            OutBoolKeys.OutBTS屏处于电子仪器第3页面,
            OutBoolKeys.OutBTD屏处于故障历史页面,
        };

        private List<int> Indexs;
        
        public override bool Initalize()
        {
            Indexs = logicName.Select(GetOutBoolIndex).ToList();
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
            var index = m_Resource.GetIndexOf(viewConfig, ScreenIdentification);

            if (-1 != index)
            {
                append_postCmd(CmdType.SetBoolValue, Indexs[AppraiseOutbollListIndex(index)], value, 0);
            }
        }
        /// <summary>
        /// 根据视图ID索引输出参数索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private static int AppraiseOutbollListIndex(int index)
        {
            var returnIndex = -1;
            switch (index)
            {
                case 1:
                    returnIndex = 0;
                    break;
                case 2:
                    returnIndex = 1;
                    break;
                case 3:
                    returnIndex = 2;
                    break;
                case 4:
                    returnIndex = 3;
                    break;
                case 5:
                    returnIndex = 4;
                    break;
                case 6:
                    returnIndex = 5;
                    break;
                case 7:
                    returnIndex = 6;
                    break;
                case 8:
                    returnIndex = 7;
                    break;
                case 9:
                    returnIndex = 8;
                    break;
                case 10:
                    returnIndex = 9;
                    break;
                case 21:
                    returnIndex = 12;
                    break;
                case 30:
                    returnIndex = 13;
                    break;
                case 31:
                    returnIndex = 14;
                    break;
                case 32:
                    returnIndex = 15;
                    break;
                case 40:
                    returnIndex = 11;
                    break;
                case 102:
                    returnIndex = 16;
                    break;
            }
            return returnIndex;
        }
    }
}
