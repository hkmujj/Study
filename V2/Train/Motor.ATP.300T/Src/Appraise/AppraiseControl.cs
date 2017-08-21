using System;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.Interface.Data;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.主屏;
using Motor.ATP._300T.共用;

namespace Motor.ATP._300T.Appraise
{
    public class AppraiseControl
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        private readonly IStringFloatConverter m_StringFloatConverter;

        /// <summary>
        /// 音量大小
        /// </summary>
        public int Voloumn { set; get; }

        public AppraiseControl(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
            Voloumn = 10;
            m_StringFloatConverter = new StringFloatConverter();
        }

        public void Notify()
        {
            NotifyTrainNo();

            NotifyDriverNo();

            NotifyTrainGroup();

            NotifyVoloumn();
        }

        public void ResetNotifies()
        {
            NotifyInputDriverID(0);
        }

        public void NotifyInputDriverID()
        {
            NotifyInputDriverID(1);
        }

        private void NotifyInputDriverID(int isInput )
        {
            //27
            m_ATPMainScreen.append_postCmd(CmdType.SetBoolValue, m_ATPMainScreen.GetOutBoolIndex(OutBoolKeys.OubATP进入司机号输入界面), isInput, 0);
        }

        private void NotifyVoloumn()
        {
            //12
            m_ATPMainScreen.append_postCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.OufATP音量大小), 0, Voloumn);
        }

        private void NotifyTrainGroup()
        {
            //11
            var group = 1;
            try
            {
                group = Convert.ToInt32(m_ATPMainScreen.OpenFunBtnCtcs300T.TrainData);
            }
            catch
            {
                AppLog.Error(string.Format("Can not convert {0} to train group data.", m_ATPMainScreen.OpenFunBtnCtcs300T.TrainData));
            }
            m_ATPMainScreen.append_postCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf列车数据), 0, group);

        }

        private void NotifyDriverNo()
        {
            // 7 8
            var data = new char[8];
            m_ATPMainScreen.OpenFunBtnCtcs300T.DriverId.Take(8).ToArray().CopyTo(data, 0);
            var send = m_StringFloatConverter.ConvertCharArray(data);

            m_ATPMainScreen.append_postCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf给评价的司机号0),0, send[0]);
            m_ATPMainScreen.append_postCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf给评价的司机号1),0, send[1]);
        }

        private void NotifyTrainNo()
        {
            // 9 10
            var data = new char[8];
            m_ATPMainScreen.OpenFunBtnCtcs300T.TrainId.Take(8).ToArray().CopyTo(data, 0);
            var send = m_StringFloatConverter.ConvertCharArray(data);

            m_ATPMainScreen.append_postCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf给评价的车次号0), 0, send[0]);
            m_ATPMainScreen.append_postCmd(CmdType.SetFloatValue, m_ATPMainScreen.GetOutFloatIndex(OutFloatKeys.Ouf给评价的车次号1), 0, send[1]);
        }
    }
}