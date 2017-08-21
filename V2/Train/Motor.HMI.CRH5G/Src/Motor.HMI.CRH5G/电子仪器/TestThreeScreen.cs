using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH5G.底层共用;
using Motor.HMI.CRH5G.底层共用.RectView;

namespace Motor.HMI.CRH5G.电子仪器
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class TestThreeScreen : ElectronicInstrumentBase
    {
        //2
        public override string GetInfo()
        {
            return "电子仪器2视图";
        }

        //6
        public override bool Initalize()
        {
            NameIndexCollection =
                GlobalParam.Instance.ProjectDetailConfig.ElectronicInstrumentConfig.PageTwoDataNameCollection.Select(
                    s => new Tuple<string, int>(s.Name, s.FindIndex(this))).ToList();
            UpdateUiObject(GlobalParam.Instance.ProjectDetailConfig.ElectronicInstrumentConfig.PageTwoDataNameCollection);

            NameArr = new[]
            {
                "1位轴温[℃]", "5位轴温[℃]",
                "2位轴温[℃]", "6位轴温[℃]", "3位轴温[℃]",
                "7位轴温[℃]", "4位轴温[℃]", "8位轴温[℃]",
            };

            RectsList = new List<RectangleF>();
            RectsList = Coordinate.RectangleFLists(ViewIDName.TestTwoScreen);
            TheRectStateList = new List<MarshallingChangableRectState>(180);

            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            Debug.Assert(appConfig != null, "appConfig != null");
            ReadFile(Path.Combine(appConfig.AppPaths.ConfigDirectory, "车辆换端信息.txt"));
            base.Initalize();
            return true;
        }

        protected override void ParseLine(int line, string content)
        {

            string[] strArr = content.Split(new[] {'\t'});
            if (strArr.Length >= 7 && strArr[0].Trim() == "TestTwoScreen")
            {
                TheRectStateList.Add(new MarshallingChangableRectState(
                    (!string.IsNullOrEmpty(strArr[1]) ? Convert.ToInt32(strArr[1]) : 1),
                    (!string.IsNullOrEmpty(strArr[6]) ? Convert.ToInt32(strArr[6]) : 0),
                    new[]
                    {
                        RectsList[!string.IsNullOrEmpty(strArr[2]) ? Convert.ToInt32(strArr[2]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[3]) ? Convert.ToInt32(strArr[3]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[4]) ? Convert.ToInt32(strArr[4]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[5]) ? Convert.ToInt32(strArr[5]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[7]) ? Convert.ToInt32(strArr[7]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[8]) ? Convert.ToInt32(strArr[8]) : 0]
                    }, string.IsNullOrEmpty(strArr[1]) ? 0 : Convert.ToInt32(strArr[1])
                    ));
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            base.Paint(g);
        }

        private void GetValue()
        {
            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress)
            {
                return;
            }
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 23:
                    ChangeTitleMarshingType();
                    ButtonsScreen.BtnState.Press();
                    break;
                //case 24:
                //    TheRectStateList.ForEach(f => f.ChangeIsCar16());
                //    ButtonsScreen.BtnState.Press();
                //    break;
            }
        }
    }
}
