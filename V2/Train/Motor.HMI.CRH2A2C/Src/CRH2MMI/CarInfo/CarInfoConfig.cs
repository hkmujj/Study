using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.View.Train;
using CommonUtil.Controls.Grid;

namespace CRH2MMI.CarInfo
{
    public class CarInfoConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlElement]
        public GridConfig[] Page1UpGrid { set; get; }

        [XmlElement]
        public GridConfig[] Page1DownGrid { set; get; }

        [XmlElement]
        public GridConfig[] Page2Grid { set; get; }

        public void RefreshGridRelation()
        {
            if (Page1UpGrid!=null)
            {
                foreach (var gridConfig in Page1UpGrid)
                {
                    gridConfig.RefreshRelation();
                }
            }

            if (Page1DownGrid != null)
            {
                foreach (var gridConfig in Page1DownGrid)
                {
                    gridConfig.RefreshRelation();
                }
            }

            if (Page2Grid != null)
            {
                foreach (var gridConfig in Page2Grid)
                {
                    gridConfig.RefreshRelation();
                }
            }
        }
    }

    static class CarInfoConfigTest
    {
        public static void Test()
        {
            var data = GetData();
            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");

        }

        private static CarInfoConfig GetData()
        {
            var data = new CarInfoConfig()
            {
                Page1UpGrid = new GridConfig[]{new GridConfig()
                {
                    AbsX = 1,
                    AbsY = 2,
                    Width = 3,
                    Height = 4,
                    Rows = new List<GridRowConfig>()
                    {
                        new GridRowConfig()
                        {
                            Name = "1",
                            ItemType = GridItemType.Text,
                            ColumnConfigs = new List<GridColumnConfig>()
                            {
                                new GridColumnConfig() {ColumIndex = 0, InBoolList = new List<int>() {1, 2, 3}}
                            }
                        }
                    }

                }},

                Page1DownGrid = new GridConfig[]{new GridConfig()
                {
                    AbsX = 1,
                    AbsY = 2,
                    Width = 3,
                    Height = 4,
                    Rows = new List<GridRowConfig>()
                    {
                        new GridRowConfig()
                        {
                            Name = "1",
                            ItemType = GridItemType.Text,
                            ColumnConfigs = new List<GridColumnConfig>()
                            {
                                new GridColumnConfig() {ColumIndex = 0, InBoolList = new List<int>() {1, 2, 3}}
                            }
                        }
                    }

                }}
            };
            return data;
        }
    }
}
