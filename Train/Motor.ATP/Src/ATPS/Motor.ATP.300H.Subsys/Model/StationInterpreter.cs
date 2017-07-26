using System;
using System.Collections.Generic;
using System.IO;
using CommonUtil.Util;
using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP._300H.Subsys.Model
{
    public class StationInterpreter : IStationInterpreter
    {
        private readonly Dictionary<int, string> m_StationDictionary;

        public StationInterpreter(string file)
        {
            m_StationDictionary = new Dictionary<int, string> { { 1, "车站1" }, { 2, "车站2" } };

            if (!File.Exists(file))
            {
                AppLog.Error(string.Format("Can not found station config file ,{0}", file));
                return;
            }
        }

        public StationInterpreterResult Interpreter(float index)
        {
            int idx = int.MaxValue;
            try
            {
                idx = Convert.ToInt32(index);
            }
            catch (Exception e)
            {
                return new StationInterpreterResult(false, string.Empty,
                    string.Format("Can not convert station index to int,{0}", e));
            }
            if (m_StationDictionary.ContainsKey(idx))
            {
                return new StationInterpreterResult(true, m_StationDictionary[idx]);
            }
            return new StationInterpreterResult(false, string.Empty,
                string.Format("Can not found station of index={0}", idx));
        }
    }
}