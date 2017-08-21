using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Service;

namespace Urban.NanJing.NingTian.DDU.Flash.Model
{
    public class NingTianConfig : IService
    {
        public IReadOnlyDictionary<int, string> StationDictionary { private set; get; }

        public void Initalize(string appDirectory)
        {
            LoadStations(appDirectory);
        }

        private void LoadStations(string appDirectory)
        {
            var file = Path.Combine(appDirectory, "Flash\\车站信息.txt");
            var dic = new Dictionary<int, string>();
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                var split = cStr.Split('\t');
                var tmp = new string[2];
                var i = 0;
                foreach (var s in split)
                {
                    if (s.Trim() != "")
                    {
                        if (i < 2)
                        {
                            tmp[i] = s;
                        }
                        i++;
                    }
                    if (i == 2)
                    {
                        dic.Add(int.Parse(tmp[0]), tmp[1]);
                        break;
                    }
                }
            }

            StationDictionary = dic.AsReadOnlyDictionary();
        }
    }
}