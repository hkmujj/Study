using System;
using System.Linq;
using MMI.Facility.DataType.Data.Station;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Station;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class StationNameProviderService : IStationNameProviderService
    {
        private IReadOnlyDictionary<int, IStationInfo> m_StationDictionary;

        /// <summary>
        /// key : 车站索引 
        /// </summary>
        public IReadOnlyDictionary<int, IStationInfo> StationDictionary
        {
            get { return m_StationDictionary; }
            private set
            {
                if (Equals(value, m_StationDictionary))
                {
                    return;
                }

                m_StationDictionary = value;
                OnStationDictionaryChanged();
            }
        }

        public void UpdateStaionDictionay(string rawData)
        {
            var stations = rawData.Split(';').Select(s =>
            {
                var info = s.Split(',');
                try
                {
                    return new StationInfo(Convert.ToInt32(info[0]), info[1]);
                }
                catch (Exception e)
                {
                    SysLog.Error("Can not parser station info , where string ={0}, {1}", s, e);
                    return null;
                }
            }).ToList();

            StationDictionary =
                stations.Where(w => w != null).Distinct(StationInfo.Comparer)
                    .ToDictionary(kvp => kvp.Index, kvp => (IStationInfo) kvp)
                    .AsReadOnlyDictionary();

            SysLog.Info("Parser stations sucessed., count = {0}", stations.Count);
        }

        public event EventHandler StationDictionaryChanged;

        protected virtual void OnStationDictionaryChanged()
        {
            if (StationDictionaryChanged != null)
            {
                StationDictionaryChanged(this, EventArgs.Empty);
            }
        }
    }
}