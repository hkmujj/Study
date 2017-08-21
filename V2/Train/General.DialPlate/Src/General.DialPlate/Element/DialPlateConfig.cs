using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Excel.Interface;
using General.DialPlate.Resources;

namespace General.DialPlate.Element
{
    public class DialPlateConfig
    {
        public static DialPlateConfig Instance { private set; get; }
        static DialPlateConfig()
        {
            Instance = new DialPlateConfig();
        }

        private DialPlateConfig()
        {

        }

        private List<IElementModel> m_ElementModels;
        public IEnumerable<IElementModel> ElementModels
        {
            get
            {
                return m_ElementModels;
            }
        }

        public void Initalize(string file)
        {
            //
            var dt = LoadElemeltModelConfig(file).Tables[0];

            BuildElements(dt);
        }

        private void BuildElements(DataTable dt)
        {
            m_ElementModels = new List<IElementModel>(dt.Rows.Count);
            
            foreach (DataRow row in dt.Rows)
            {
                var name = row[DialPlateResource.图片名称].ToString();
                var region = new RectangleF(Convert.ToSingle(row[DialPlateResource.顶点坐标X]),
                    Convert.ToSingle(row[DialPlateResource.顶点坐标Y]),
                    Convert.ToSingle(row[DialPlateResource.显示大小Width]),
                    Convert.ToSingle(row[DialPlateResource.显示大小Height]));

                var logicIndex = int.MaxValue;
                if (!Convert.IsDBNull(row[DialPlateResource.值的数据索引]))
                {
                    logicIndex = Convert.ToInt32(row[DialPlateResource.值的数据索引]);
                }

                var min = float.NaN;
                if (!Convert.IsDBNull(row[DialPlateResource.最小值]))
                {
                    min = Convert.ToSingle(row[DialPlateResource.最小值]);
                }

                var max = float.NaN;
                if (!Convert.IsDBNull(row[DialPlateResource.最大值]))
                {
                    max = Convert.ToSingle(row[DialPlateResource.最大值]);
                }

                var angle0 = float.NaN;
                if (!Convert.IsDBNull(row[DialPlateResource.初始角度]))
                {
                    angle0 = Convert.ToSingle(row[DialPlateResource.初始角度]);
                }

                var angle1 = float.NaN;
                if (!Convert.IsDBNull(row[DialPlateResource.最大角度]))
                {
                    angle1 = Convert.ToSingle(row[DialPlateResource.最大角度]);
                }

                var visless = true;
                if (dt.Columns.Contains(DialPlateResource.小于最小值时是否显示) && !Convert.IsDBNull(row[DialPlateResource.小于最小值时是否显示]))
                {
                    visless = Convert.ToBoolean(row[DialPlateResource.小于最小值时是否显示]);
                }

                var visLarge = true;
                if (dt.Columns.Contains(DialPlateResource.大于等于最大值时是否显示) && !Convert.IsDBNull(row[DialPlateResource.大于等于最大值时是否显示]))
                {
                    visLarge = Convert.ToBoolean(row[DialPlateResource.大于等于最大值时是否显示]);
                }

                var ele = new LineElementModel(name, region, logicIndex, min, max, angle0, angle1, visless, visLarge);

                m_ElementModels.Add(ele);
            }

        }

        private DataSet LoadElemeltModelConfig(string file)
        {
            
            var excelConfig = new ExcelReaderConfig()
            {
                File = file,
                SheetNames = new List<string>() {DialPlateResource.SheetName},
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig() {Name ="*"}
                }
            };

            return excelConfig.Adapter();
        }
    }
}