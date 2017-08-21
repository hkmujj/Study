using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Subway.XiaMenLine1.Interface
{
    public interface IStationsMgr : IPaging<IStation>, IInfo<int, IStation>
    {
        string GetStationName(int logic);
        string GetStationNames(int Number);
    }

    public class StationsMgr : IStationsMgr
    {
        public StationsMgr()
        {
            CurrentData = new List<IStation>();
            AllData = new Dictionary<int, IStation>();
        }

        public int MaxPage { get; protected set; }
        public int CurrentPage { get; protected set; }
        public int PageNum { get; protected set; }
        public IList<IStation> CurrentData { get; private set; }

        /// <summary>
        /// 首页
        /// </summary>
        public void FristPage()
        {
            
        }

        public void NextPage()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
            }
        }

        public void LastPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        public IList<IStation> GetCurrent()
        {
            return CurrentData;
        }

        public string Name { get; private set; }
        public Dictionary<int, IStation> AllData { get; private set; }
        public void Load(string file)
        {
            if (File.Exists(file))
            {
                Name = file;
                var allLine = File.ReadAllLines(file, Encoding.Default);
                foreach (var tmp in from line in allLine.Skip(1) select line.Split(';') into slip where slip.Length == 3 select new Station(int.Parse(slip[0]), int.Parse(slip[1]), slip[2]))
                {
                    AllData.Add(tmp.LogicNum, tmp);
                }
            }
            else
            {
                throw new FileNotFoundException(file);
            }
        }

        public string GetStationName(int logic)
        {
            return AllData.ContainsKey(logic) ? AllData[logic].StationName : "";
        }

        public string GetStationNames(int Number)
        {
            var tmp = AllData.FirstOrDefault(f => f.Value.Number == Number).Value;
            return tmp!=null ? tmp.StationName : string.Empty;
        }
    }
}