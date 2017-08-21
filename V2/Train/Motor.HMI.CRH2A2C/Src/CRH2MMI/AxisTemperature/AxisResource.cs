using System.Collections.Generic;
using System.Linq;
using CRH2MMI.Common.Global;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.AxisTemperature
{
    internal class AxisResource
    {
        private readonly Axis m_Axis;


        private Dictionary<AxisTemperatureSendKeyModel, int> m_OutBoolIndexDic;

        public AxisResource(Axis axis)
        {
            m_Axis = axis;
            m_OutBoolIndexDic = GlobalInfo.CurrentCRH2Config.AxisTemperatureConfig
                .AxisTemperatureSendModels.ToDictionary(
                    s => new AxisTemperatureSendKeyModel()
                    {
                        CarNo = s.CarNo,
                        AxisTemperaturedLocation = s.AxisTemperaturedLocation,
                        Type = s.Type,
                    },
                    s => GlobalResource.Instance.GetOutBoolIndex(s.OutBoolColoumNames.First()));
        }


        /// <summary>
        /// 
        /// </summary>
        public void SetAxisTemperaturState(AxisTemperatureSendKeyModel keyModel, int setValue)
        {
            m_Axis.append_postCmd(CmdType.SetBoolValue, m_OutBoolIndexDic[keyModel], setValue, 0);
        }
    }
}
