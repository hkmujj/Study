using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Util;
using Excel.Interface;

namespace Mmi.Common.StationReader
{
    public class StationReader : IStationReader
    {
        private readonly List<StationModel> m_StationModelCollection;

        public ReadOnlyCollection<StationModel> StationModelCollection { get; private set; }

        public StationReader()
        {
            m_StationModelCollection = new List<StationModel>();
            StationModelCollection = new ReadOnlyCollection<StationModel>(m_StationModelCollection);
        }

        public bool Load(FileInfo file)
        {
            m_StationModelCollection.Clear();

            LogMgr.Info("Staion reader load file {0} startted! ", file);

            var rlt = LoadFile(file);

            LogMgr.Info("Loaded station {0} records : {1}", m_StationModelCollection.Count,
                string.Join("\r\n",
                    m_StationModelCollection.Select(
                        s => string.Format("CID: {0}, Name: {1}", s.CommunicationId, s.StationName))));

            LogMgr.Info("Staion reader load file {0} ended! ", file);

            return rlt;
        }

        private bool LoadFile(FileInfo file)
        {
            if (!file.Exists)
            {
                LogMgr.Error("{0} is not exsit or is not a file.", file);
                return false;
            }

            if (!file.Extension.Contains("xls"))
            {
                LogMgr.Error("StationReader only can parser .xls file,  can not parser {0}", file);

                return false;
            }

            var config = new ExcelReaderConfig()
            {
                File = file.FullName,
                SheetNames = new List<string>() {StationConfigKeys.SheetName},
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig(){ IsPrimaryKey = true, Name = StationConfigKeys.CommunicationID},
                    new ColoumnConfig(){ IsPrimaryKey = false, Name = StationConfigKeys.ShowID},
                    new ColoumnConfig(){ IsPrimaryKey = false, Name = StationConfigKeys.StationName},
                }
            };

            var dt = config.Adapter().Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                var cid = Convert.ToSingle(row[StationConfigKeys.CommunicationID]);
                var name = row[StationConfigKeys.StationName].ToString();
                var sid = row[StationConfigKeys.ShowID].ToString();
                m_StationModelCollection.Add(new StationModel(cid, name, sid));
            }

            return true;
        }
    }
}
