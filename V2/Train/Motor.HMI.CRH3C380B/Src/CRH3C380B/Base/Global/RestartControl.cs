using System.Drawing;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.Global
{
    public class RestartControl : IViewControl
    {
        private int m_RestartIndex;
        private bool m_IsRestarting;


        public void Initalize(CRH3C380BBase source)
        {
            const string indexName = "蓄电池状态";

            m_RestartIndex = source.GetInBoolIndex(indexName);

            source.UpdateUiObject(CommunicationDataType.InBool, new[] {indexName});
        }

        public void Refresh(CRH3C380BBase source, Graphics graphics)
        {
            if (source.BoolList[m_RestartIndex] && !m_IsRestarting)
            {
                source.append_postCmd(CmdType.ChangePage, 320, 0, 0);
                m_IsRestarting = true;
            }
        }

        public void Reset()
        {
            m_IsRestarting = false;
        }
    }
}