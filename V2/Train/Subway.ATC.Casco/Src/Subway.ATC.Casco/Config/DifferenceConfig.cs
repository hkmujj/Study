using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util.Extension;
using Excel.Interface;
using Subway.ATC.Casco.Resource;

namespace Subway.ATC.Casco.Config
{
    public class DifferenceConfig
    {
        public IReadOnlyDictionary<string, string> ContentDictionary { private set; get; }

        public void Initalize(string configDirectory, UsedEnv usedEnv)
        {
            var config = new ExcelReaderConfig()
            {
                File = Path.Combine(configDirectory, DifferenceResource.FileName),
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig()
                    {
                        IsPrimaryKey = true,
                        Name = DifferenceResource.ColName,
                    },
                    new ColoumnConfig()
                    {
                        IsPrimaryKey = false,
                        Name = DifferenceResource.ColWuxi,
                    },
                    new ColoumnConfig()
                    {
                        IsPrimaryKey = false,
                        Name = DifferenceResource.ColNingTian,
                    },new ColoumnConfig()
                    {
                        IsPrimaryKey = false,
                        Name = DifferenceResource.ColXiaMen,
                    }
                },
                SheetNames = new List<string>() { "sheet1" }
            };
            var dt = config.Adapter().Tables[0];
            string col;
            switch (usedEnv)
            {
                case UsedEnv.WuxiSubway1:
                    col = DifferenceResource.ColWuxi;
                    break;
                case UsedEnv.NanjingNingTian:
                    col = DifferenceResource.ColNingTian;
                    break;
                case UsedEnv.XiaMen:
                    col = DifferenceResource.ColXiaMen;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("usedEnv", usedEnv, null);
            }
            var dic = dt.Rows.Cast<DataRow>()
                .ToDictionary(row => row[DifferenceResource.ColName].ToString(), row => row[col].ToString());

            ContentDictionary = dic.AsReadOnlyDictionary();
        }
    }
}