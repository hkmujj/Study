using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Annotations;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.ViewModels;

namespace Subway.ShenZhenLine11.Config
{
    public class StationManager
    {
        private Dictionary<int, string> m_Stations;
        public const string FileName = "StationInfos.txt";
        static StationManager()
        {
            Instance = new StationManager();
        }
        public static StationManager Instance { get; private set; }

        public Dictionary<int, string> Stations
        {
            get
            {
                if (m_Stations == null)
                {
                    Load();
                }
                return m_Stations;
            }
            private set { m_Stations = value; }
        }

        public void Load()
        {
            //var path = Path.Combine(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory, FileName);
            Stations = new Dictionary<int, string>();

            Stations = ServiceLocator.Current.GetInstance<BorderCastViewModel>()
                .AllStation.ToDictionary(t => t.Index, t => t.Name);

            //var allLine = File.ReadAllLines(path, Encoding.Default);
            //Stations =
            //    allLine.Where(w => !w.StartsWith(";;;;"))
            //        .Select(s => s.Split(';'))
            //        .Where(slip => slip.Length == 2)
            //        .ToDictionary(t => t[0].ToInt(), t => t[1]);

        }

        public string GetStation(int index)
        {
            return Stations.ContainsKey(index) ? Stations[index] : string.Empty;
        }
    }
}