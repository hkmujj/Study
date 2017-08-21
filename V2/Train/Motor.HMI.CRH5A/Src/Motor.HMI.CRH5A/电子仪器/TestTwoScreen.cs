using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH5A.底层共用;
using Motor.HMI.CRH5A.底层共用.RectView;

namespace Motor.HMI.CRH5A.电子仪器
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class TestTwoScreen : ElectronicInstrumentBase
    {
        //2
        public override string GetInfo()
        {
            return "电子仪器3视图";
        }

        //6
        public override bool Initalize()
        {
            if (ScreenIdentification == ScreenIdentification.ScreenTd)
            {
                NameArr = new[]
            {
                "1轴压力[Bar]", "2轴压力[Bar]",
                "3轴压力[Bar]", "4轴压力[Bar]"
            };
            }
            else
            {
                NameArr = new[]
            {
                "1轴压力[Bar]", "2轴压力[Bar]",
                "3轴压力[Bar]", "4轴压力[Bar]"
            };
            }

            NameIndexCollection =
                GlobalParam.Instance.ProjectDetailConfig.ElectronicInstrumentConfig.PageThreeDataNameCollection.Select(
                    s => new Tuple<string, int>(s.Name, s.FindIndex(this))).ToList();

            UpdateUiObject(
                GlobalParam.Instance.ProjectDetailConfig.ElectronicInstrumentConfig.PageThreeDataNameCollection);
            RectsList = new List<RectangleF>();
            RectsList = Coordinate.RectangleFLists(ViewIDName.TestThreeScreen);
            TheRectStateList = new List<MarshallingChangableRectState>(180);

            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            Debug.Assert(appConfig != null, "appConfig != null");
            ReadFile(Path.Combine(appConfig.AppPaths.ConfigDirectory, "车辆换端信息.txt"));
            base.Initalize();
            return true;
        }

        protected override void ParseLine(int line, string content)
        {
            string[] strArr = content.Split(new[] { '\t' });
            if (strArr.Length >= 7 && strArr[0].Trim() == "TestThreeScreen")
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
            //g.FillRectangle(Brushes.Red, 0, 0, 800, 600);
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
                case 24:
                    TheRectStateList.ForEach(f => f.ChangeIsCar16());
                    ButtonsScreen.BtnState.Press();
                    break;
            }
        }

    }
}