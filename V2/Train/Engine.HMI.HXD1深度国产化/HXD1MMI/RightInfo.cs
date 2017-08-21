using System.IO;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 右边信息栏
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class RightInfo : baseClass
    {
        private List<RectText> _rightRectTextList = new List<RectText>();
        private MainViewRectText _speed;
        private MainViewRectText _force;
        private MainViewRectText _handle;

        private Image[] _imageArray;

        public override string GetInfo()
        {
            return "右边信息栏";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            for (int i = 0; i < 8; i++)
            {
                _rightRectTextList.Add(new RectText(new Rectangle(650 + 72 * (i % 2), Common.InitialPosY + 160 + (i / 2) * 80, 72, 80), "", 14, 1, Common.WhiteColor, Common.BlackColor, Common.WhiteColor, 1, true));
            }
            _speed = new MainViewRectText(new Point(650, 40 + Common.InitialPosY), "--", "km/h", "设定\n速度");
            _force = new MainViewRectText(new Point(650, 80 + Common.InitialPosY), "0", "kN", "实际\n力");
            _handle = new MainViewRectText(new Point(650, 120 + Common.InitialPosY), "大零位", "", "手柄\n级位");

            _imageArray = new Image[UIObj.ParaList.Count];

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , UIObj.ParaList[i]), FileMode.Open))
                {
                    _imageArray[i] = Image.FromStream(fs);
                }
            }


            return true;
        }

        private void GetValue()
        {
            #region 速度
            _speed.Value = ((int)FloatList[101]).ToString();
            #endregion

            #region 实际力
            bool _isDraught = BoolList[869];
            bool _isBrake = BoolList[870];
            if (_isDraught)
            {

                _force.Value = ((int)FloatList[102]).ToString();

                _force.SilidBrushColor = Color.Blue;
            }

            if (_isBrake)
            {
                _force.Value = ((int)FloatList[103]).ToString();
                _force.SilidBrushColor = Color.Red;
            }

            if (!(_isBrake || _isDraught))
            {
                _force.SilidBrushColor = Color.White;
                _force.Value = "0";
            }
            #endregion

            #region 手柄级位


            _handle.Value = (((float)((int)(FloatList[182] * 10))) / 10).ToString();


            #endregion

            #region 机车方向
            //if (!BoolList[844] && !BoolList[845] && !BoolList[846])
            //{
            //    _rightRectTextList[0].ImagePicture = _imageArray[26];
            //}

            for (int i = 0; i < 3; i++)
            {
                if (BoolList[844 + i])
                {
                    _rightRectTextList[0].ImagePicture = _imageArray[0 + i];
                }
            }
            #endregion

            #region 受电工

            if (!BoolList[835] && !BoolList[836] && !BoolList[837] && !BoolList[838] && !BoolList[839] && !BoolList[840])
            {
                _rightRectTextList[1].ImagePicture = _imageArray[27];
            }

            if (BoolList[837] && BoolList[840])  //1 2 升
            {
                _rightRectTextList[1].ImagePicture = _imageArray[3];
            }

            if (BoolList[835] && BoolList[839])//1降2隔
            {
                _rightRectTextList[1].ImagePicture = _imageArray[4];
            }

            if (BoolList[837] && BoolList[839])//1升2隔
            {
                _rightRectTextList[1].ImagePicture = _imageArray[5];
            }

            if (BoolList[837] && BoolList[838])//1升2降
            {
                _rightRectTextList[1].ImagePicture = _imageArray[6];
            }

            if (BoolList[836] && BoolList[838]) //1隔2降
            {
                _rightRectTextList[1].ImagePicture = _imageArray[7];
            }

            if (BoolList[836] && BoolList[840])//1 隔2升
            {
                _rightRectTextList[1].ImagePicture = _imageArray[8];
            }

            if (BoolList[835] && BoolList[840])   //1降2升
            {
                _rightRectTextList[1].ImagePicture = _imageArray[9];
            }

            if (BoolList[835] && BoolList[838])// 1 2 降
            {
                _rightRectTextList[1].ImagePicture = _imageArray[10];
            }

            if (BoolList[836] && BoolList[839])
            {
                _rightRectTextList[1].ImagePicture = _imageArray[11];
            }

            #endregion

            #region 警惕装置
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[847 + i])
                {
                    _rightRectTextList[2].ImagePicture = _imageArray[12 + i];
                }
            }
            #endregion

            #region 电机
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[866 + i])
                {
                    _rightRectTextList[3].ImagePicture = _imageArray[14 + i];
                }
            }
            #endregion

            #region 机车制动状态

            for (int i = 0; i < 3; i++)
            {
                if (BoolList[849 + i])
                {
                    _rightRectTextList[4].ImagePicture = _imageArray[16 + i];
                }
            }
            #endregion    

            #region 停放制动

            if (!BoolList[852] && !BoolList[853] && !BoolList[854] && !BoolList[855])
            {
                _rightRectTextList[5].ImagePicture = _imageArray[28];
            }

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[852 + i])
                {
                    _rightRectTextList[5].ImagePicture = _imageArray[22 + i];
                }
            }
            #endregion

            #region 空转


            if (!BoolList[859] && !BoolList[860] && !BoolList[861])
            {
                _rightRectTextList[7].ImagePicture = _imageArray[29];
            }

            for (int i = 0; i < 3; i++)
            {
                if (BoolList[859 + i])
                {
                    _rightRectTextList[7].ImagePicture = _imageArray[19 + i];
                }
            }
            #endregion

        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            foreach (var v in _rightRectTextList)
            {
                v.OnDraw(dcGs);
            }

            _speed.OnDraw(dcGs);
            _force.OnDraw(dcGs);
            _handle.OnDraw(dcGs);
        }
    }
}
