using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Util;
using CommonUtil.Controls.Grid;


namespace CRH2MMI.Common.Models
{
    internal class GridLineConfigTest
    {
        public static void Test()
        {
            var data = new GridConfig()
            {
                GridType = GridType.GridLine,
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
                            new GridColumnConfig(){ ColumIndex = 0, InBoolList = new List<int>(){ 1,2,3}}
                        },
                        Colors = new List<Color>(){ Color.White, Color.FromArgb(1,2,3)}
                    }
                }

            };
            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");

            var v = DataSerialization.DeserializeFromXmlFile<GridConfig>("D:\\a.xml");
        }
    }
}
