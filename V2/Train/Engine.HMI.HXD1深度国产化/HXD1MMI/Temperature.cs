using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 温度界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Temperature : baseClass
    {

        private List<RowMainData> _rowMainDataList = new List<RowMainData>();
        public override string GetInfo()
        {
            return "温度界面";
        }

        private void GetValue()
        {
            _rowMainDataList[0].RectTextList[0].Text = FloatList[UIObj.InFloatList[0] + 0].ToString()+"°C";
            _rowMainDataList[0].RectTextList[1].Text = FloatList[UIObj.InFloatList[0] + 2].ToString() + "°C";

            _rowMainDataList[1].RectTextList[0].Text = FloatList[UIObj.InFloatList[0] + 1].ToString() + "°C";
            _rowMainDataList[1].RectTextList[1].Text = FloatList[UIObj.InFloatList[0] + 3].ToString() + "°C";

            _rowMainDataList[2].RectTextList[0].Text = FloatList[UIObj.InFloatList[0] + 4].ToString() + "°C";
            _rowMainDataList[2].RectTextList[1].Text = FloatList[UIObj.InFloatList[0] + 5].ToString() + "°C";

            _rowMainDataList[3].RectTextList[0].Text = FloatList[UIObj.InFloatList[0] + 6].ToString() + "°C";
            _rowMainDataList[3].RectTextList[1].Text = FloatList[UIObj.InFloatList[0] + 7].ToString() + "°C";

            _rowMainDataList[4].RectTextList[0].Text = FloatList[UIObj.InFloatList[0] + 8].ToString() + "°C";
            _rowMainDataList[4].RectTextList[1].Text = FloatList[UIObj.InFloatList[0] + 9].ToString() + "°C";

            _rowMainDataList[5].RectTextList[0].Text = FloatList[UIObj.InFloatList[0] + 10].ToString() + "bar";
            _rowMainDataList[5].RectTextList[1].Text = FloatList[UIObj.InFloatList[0] + 11].ToString() + "bar";

            _rowMainDataList[6].RectTextList[0].Text = FloatList[UIObj.InFloatList[0] + 12].ToString() + "V";
            _rowMainDataList[6].RectTextList[1].Text = FloatList[UIObj.InFloatList[0] + 13].ToString() + "V";

            if (BoolList[1041])
                _rowMainDataList[7].RectTextList[0].Text = "正常";
            if(BoolList[1042])
                _rowMainDataList[7].RectTextList[0].Text = "异常";
            
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            _rowMainDataList.Add(new RowMainData(new Point(50 + Common.InitialPosX, 90 + Common.InitialPosY), "电机1温度", new string[] { "", "" }, "电机3温度", 230, 230));
            _rowMainDataList.Add(new RowMainData(new Point(50 + Common.InitialPosX, 90 + 35 + Common.InitialPosY), "电机2温度", new string[] { "", "" }, "电机4温度", 230, 230));
            _rowMainDataList.Add(new RowMainData(new Point(50 + Common.InitialPosX, 90 + 70 + Common.InitialPosY), "主变流器1冷却水温度", new string[] { "", "" }, "主变流器2冷却水温度", 230, 230));
            _rowMainDataList.Add(new RowMainData(new Point(50 + Common.InitialPosX, 90 + 105 + Common.InitialPosY), "主变流器1柜体温度", new string[] { "", "" }, "主变流器2柜体温度", 230, 230));
            _rowMainDataList.Add(new RowMainData(new Point(50 + Common.InitialPosX, 90 + 140 + Common.InitialPosY), "主变流器1冷却轴温度", new string[] { "", "" }, "主变流器2冷却轴温度", 230, 230));
            _rowMainDataList.Add(new RowMainData(new Point(50 + Common.InitialPosX, 90 + 175 + Common.InitialPosY), "主变流器1冷却水压力", new string[] { "", "" }, "主变流器2冷却水压力", 230, 230));
            _rowMainDataList.Add(new RowMainData(new Point(50 + Common.InitialPosX, 90 + 210 + Common.InitialPosY), "主变流器1中间直流电压", new string[] { "", "" }, "主变流器2中间直流电压", 230, 230));
            _rowMainDataList.Add(new RowMainData(new Point(50 + Common.InitialPosX, 90 + 245 + Common.InitialPosY), "油流状态", new string[] { "" }, "", 230, 230));
            
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            foreach (var v in _rowMainDataList)
            {
                v.OnDraw(dcGs);
            }
        }
    }
}
