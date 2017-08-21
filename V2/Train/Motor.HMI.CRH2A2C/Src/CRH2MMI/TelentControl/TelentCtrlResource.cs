using System.Collections.Generic;
using System.Linq;
using CRH2MMI.Common.Global;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.TelentControl
{
    internal class TelentCtrlResource
    {
        private readonly Telecontr m_Telecontr;
        /// <summary>
        /// 输出bool的前面五对索引
        /// </summary>
        private readonly List<int> m_OutBoolIdx;

        private readonly Dictionary<TeleSendKeyModel, int> m_OutBoolDic;

        public TelentCtrlResource(Telecontr telecontr)
        {
            m_Telecontr = telecontr;

            m_OutBoolIdx = new List<int>()
            {
                0, 4, 8,  10,16,
                1, 5, 9,11,17,
                2, 6, 12,14,18,
                3, 7, 13,15,19,
            };

            m_OutBoolDic =
                GlobalInfo.CurrentCRH2Config.TeleControlConfig.TeleOutBoolConfigs.ToDictionary(
                    s => new TeleSendKeyModel()
                    {
                        Type = s.Type,
                        Unit = s.Unit,
                    },
                    s => GlobalResource.Instance.GetOutBoolIndex(s.OutBoolColoumNames.First()));
        }

        public void SetTelentState(TeleSendKeyModel keyModel, int value)
        {

            m_Telecontr.append_postCmd(CmdType.SetBoolValue, m_OutBoolDic[keyModel], value, 0);
            //远程控制切除 / 设定时选择单元1U
            m_Telecontr.append_postCmd(CmdType.SetBoolValue, m_Telecontr.GetOutBoolIndex(string.Format("远程控制切除 / 设定时选择单元{0}U", keyModel.Unit)), value, 0);
        }

        public void SetTelentState(int unitNo, int btnIdx)
        {
            var offset = 0;
            if (btnIdx >= 0 && btnIdx <= 4)
            {
                offset = m_OutBoolIdx[btnIdx + unitNo * 10];
            }
            else if (btnIdx >= 7 && btnIdx <= 11)
            {
                offset = m_OutBoolIdx[btnIdx - 7 + 5 + unitNo * 10];
            }
            // 无压切除
            else if (btnIdx == 5)
            {
                offset = 21 + unitNo;
            }
            // 有压切除
            else if (btnIdx == 6)
            {
                offset = 20 + unitNo;
            }
            // M 车切除
            else
            {
                offset = 24 + unitNo;
            }

            m_Telecontr.append_postCmd(CmdType.SetBoolValue, m_Telecontr.UIObj.OutBoolList[offset], 1, 0);
        }
    }

}
