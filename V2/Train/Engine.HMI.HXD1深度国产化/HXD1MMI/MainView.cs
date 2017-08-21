using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 主要视图
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainView : baseClass
    {
        private PressureDial _pressureDial;
        private CurrentDial _currentDial;
        private ForceDial _forceDial1;
        private ForceDial _forceDial2;
        private ForceDial _forceDial3;
        private ForceDial _forceDial4;

        private Image[] _imageArray;

        private bool _isDraught;
        private bool _isBrake;

        public override string GetInfo()
        {
            return "主页面";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            _pressureDial = new PressureDial(new Rectangle(70, 40 + Common.InitialPosY, 50, 395), 1, 32, "原边电压", Common.WhiteColor, 5);
            _currentDial = new CurrentDial(new Rectangle(210, 40 + Common.InitialPosY, 50, 350), 20, 32, "原边电流", Common.WhiteColor, 10);
            _currentDial.FillColor = Color.Yellow;

            _forceDial1 = new ForceDial(new Rectangle(385, 40 + Common.InitialPosY, 50, 395), 10, 22, "A-1", Common.WhiteColor, 5);
            _forceDial2 = new ForceDial(new Rectangle(445, 40 + Common.InitialPosY, 50, 395), 10, 22, "A-2", Common.WhiteColor, 5, false);
            _forceDial3 = new ForceDial(new Rectangle(510, 40 + Common.InitialPosY, 50, 395), 10, 22, "B-1", Common.WhiteColor, 5, false);
            _forceDial4 = new ForceDial(new Rectangle(570, 40 + Common.InitialPosY, 50, 395), 10, 22, "B-1", Common.WhiteColor, 5, false);

            _imageArray = new Image[UIObj.ParaList.Count];

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, UIObj.ParaList[i]), FileMode.Open))
                {
                    _imageArray[i] = Image.FromStream(fs);
                }
            }

            return true;
        }

        private void GetVlaue()
        {
            #region 牵引 制动工况 控制颜色
            _isDraught = BoolList[UIObj.InBoolList[0]];
            _isBrake = BoolList[UIObj.InBoolList[1]];


            if (FloatList[104] >= 0)
                _pressureDial.Value = FloatList[104];
            if (FloatList[105] >= 0)
                _currentDial.Value = FloatList[105];

            if (_isDraught)
            {
                _forceDial1.FillColor = Color.Blue;
                _forceDial2.FillColor = Color.Blue;
                _forceDial3.FillColor = Color.Blue;
                _forceDial4.FillColor = Color.Blue;

                _forceDial1.TriangleColor = Color.Blue;
                _forceDial2.TriangleColor = Color.Blue;
                _forceDial3.TriangleColor = Color.Blue;
                _forceDial4.TriangleColor = Color.Blue;

                _forceDial1.Value = FloatList[107];
                _forceDial1.DesireValue = FloatList[106];

                _forceDial2.Value = FloatList[111];
                _forceDial2.DesireValue = FloatList[110];

                _forceDial3.Value = FloatList[115];
                _forceDial3.DesireValue = FloatList[114];

                _forceDial4.Value = FloatList[119];
                _forceDial4.DesireValue = FloatList[118];
            }


            if (_isBrake)
            {
                _forceDial1.FillColor = Color.Red;
                _forceDial2.FillColor = Color.Red;
                _forceDial3.FillColor = Color.Red;
                _forceDial4.FillColor = Color.Red;

                _forceDial1.TriangleColor = Color.Red;
                _forceDial2.TriangleColor = Color.Red;
                _forceDial3.TriangleColor = Color.Red;
                _forceDial4.TriangleColor = Color.Red;

                _forceDial1.Value = FloatList[109];
                _forceDial1.DesireValue = FloatList[108];

                _forceDial2.Value = FloatList[113];
                _forceDial2.DesireValue = FloatList[112];

                _forceDial3.Value = FloatList[117];
                _forceDial3.DesireValue = FloatList[116];

                _forceDial4.Value = FloatList[121];
                _forceDial4.DesireValue = FloatList[120];
            }

            if (!(_isBrake || _isDraught))
            {
                //_forceDial1.Value = 0;
                //_forceDial1.DesireValue = 0;

                //_forceDial2.Value = 0;
                //_forceDial2.DesireValue = 0;

                //_forceDial3.Value = 0;
                //_forceDial3.DesireValue = 0;

                //_forceDial4.Value = 0;
                //_forceDial4.DesireValue = 0;
                //判断是否存在制动力
                var tmp1 = FloatList[109] > 0;
                var tmp2 = FloatList[108] > 0;
                var tmp3 = FloatList[113] > 0;
                var tmp4 = FloatList[112] > 0;
                var tmp5 = FloatList[117] > 0;
                var tmp6 = FloatList[116] > 0;
                var tmp7 = FloatList[121] > 0;
                var tmp8 = FloatList[120] > 0;
                //设置显示值
                _forceDial1.Value = tmp1 ? FloatList[109] : FloatList[107];
                _forceDial1.DesireValue = tmp2 ? FloatList[108] : FloatList[106];

                _forceDial2.Value = tmp3 ? FloatList[113] : FloatList[111];
                _forceDial2.DesireValue = tmp4 ? FloatList[112] : FloatList[110];

                _forceDial3.Value = tmp5 ? FloatList[117] : FloatList[115];
                _forceDial3.DesireValue = tmp6 ? FloatList[116] : FloatList[114];

                _forceDial4.Value = tmp7 ? FloatList[121] : FloatList[119];
                _forceDial4.DesireValue = tmp8 ? FloatList[120] : FloatList[118];

                //设置显示颜色
                _forceDial1.FillColor = tmp1 ? Color.Red : Color.Blue;
                _forceDial2.FillColor = tmp3 ? Color.Red : Color.Blue;
                _forceDial3.FillColor = tmp5 ? Color.Red : Color.Blue;
                _forceDial4.FillColor = tmp7 ? Color.Red : Color.Blue;

                _forceDial1.TriangleColor = Color.White;
                _forceDial2.TriangleColor = Color.White;
                _forceDial3.TriangleColor = Color.White;
                _forceDial4.TriangleColor = Color.White;
            }
            #endregion

            for (int i = 0; i < 4; i++)
            {
                #region 主断路器
                if (BoolList[826 + i])
                {
                    _currentDial._1ARectText.ImagePicture = _imageArray[0 + i];
                }

                if (BoolList[830 + i])
                {
                    _currentDial._1BRectText.ImagePicture = _imageArray[0 + i];
                }
                #endregion
            }

        }

        public override void paint(Graphics dcGs)
        {
            GetVlaue();

            _pressureDial.OnDraw(dcGs);
            _currentDial.OnDraw(dcGs);
            _forceDial1.OnDraw(dcGs);
            _forceDial2.OnDraw(dcGs);
            _forceDial3.OnDraw(dcGs);
            _forceDial4.OnDraw(dcGs);

        }

    }
}
