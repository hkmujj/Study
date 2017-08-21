using System.IO;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 主要数据视图
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainDataView : baseClass
    {
        private MainData _AData;
        private MainData _BData;

        private Point _linePointStart = new Point(410, Common.InitialPosY + 50);
        private Point _linePointEnd = new Point(410, Common.InitialPosY + 450);

        private string[] _bowStringArray = new string[] { "降下", "封锁", "升起" };
        private string[] _CCUStringArray = new string[] { "主控", "从控", "断开" };
        private string[] _trainModelArray = new string[] { "正常操作", "库内动车", "辅助测试", "定速模式" };
        private string[] _stateArray = new string[] { "正常", "故障" };
        private string[] _TMStateArray = new string[] { "ON", "OFF" };
        private Color[] _TMColorArray = new Color[] { Color.Black, Color.Red };

        private string[] _RDCStateArray = new string[] { "闭合", "断开" };

        private Image[] _imageArray;
        public override string GetInfo()
        {
            return "主要数据";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            _BData = new MainData("B", new Point(415, 100 + Common.InitialPosY));



            _AData = new MainData("A", new Point(25, 100 + Common.InitialPosY));

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

        private void RefreshDatas()
        {

            for (int i = 0; i < 3; i++)
            {
                RefreshPantograph(i);

                RefreshCCU(i);
            }

            for (int i = 0; i < 4; i++)
            {
                RefreshMainDisconnectionSwtich(i);

                RefreshModel(i);

                RefreshNodeId();
            }

            for (int i = 0; i < 2; i++)
            {
                RefreshMVB(i);

                RefreshWTB(i);

                RefreshTM(i);

                RefreshRDC(i);
            }
        }

        private void RefreshRDC(int i)
        {
            #region RDC

            if (BoolList[1033 + i])
            {
                _AData.RDC.RectTextList[0].Text = _RDCStateArray[i];
                _AData.RDC.RectTextList[0].BackgroundColor = _TMColorArray[i];
            }

            if (BoolList[1039 + i])
            {
                _BData.RDC.RectTextList[0].Text = _RDCStateArray[i];
                _BData.RDC.RectTextList[0].BackgroundColor = _TMColorArray[i];
            }

            #endregion
        }

        private void RefreshTM(int i)
        {
            #region TM

            if (BoolList[974 + 4*i])
            {
                _AData.TM.RectTextList[0].Text = _TMStateArray[i];
                _AData.TM.RectTextList[0].BackgroundColor = _TMColorArray[i];
            }

            if (BoolList[975 + 4*i])
            {
                _AData.TM.RectTextList[1].Text = _TMStateArray[i];
                _AData.TM.RectTextList[1].BackgroundColor = _TMColorArray[i];
            }

            if (BoolList[976 + 4*i])
            {
                _AData.TM.RectTextList[2].Text = _TMStateArray[i];
                _AData.TM.RectTextList[2].BackgroundColor = _TMColorArray[i];
            }

            if (BoolList[977 + 4*i])
            {
                _AData.TM.RectTextList[3].Text = _TMStateArray[i];
                _AData.TM.RectTextList[3].BackgroundColor = _TMColorArray[i];
            }

            if (BoolList[1002 + 4*i])
            {
                _BData.TM.RectTextList[0].Text = _TMStateArray[i];
                _BData.TM.RectTextList[0].BackgroundColor = _TMColorArray[i];
            }

            if (BoolList[1003 + 4*i])
            {
                _BData.TM.RectTextList[1].Text = _TMStateArray[i];
                _BData.TM.RectTextList[1].BackgroundColor = _TMColorArray[i];
            }

            if (BoolList[1004 + 4*i])
            {
                _BData.TM.RectTextList[2].Text = _TMStateArray[i];
                _BData.TM.RectTextList[2].BackgroundColor = _TMColorArray[i];
            }

            if (BoolList[1005 + 4*i])
            {
                _BData.TM.RectTextList[3].Text = _TMStateArray[i];
                _BData.TM.RectTextList[3].BackgroundColor = _TMColorArray[i];
            }

            #endregion
        }

        private void RefreshWTB(int i)
        {
            #region WTB

            if (BoolList[995 + i])
            {
                _AData.WTB.RectTextList[0].Text = _CCUStringArray[i];
            }

            if (BoolList[998 + i])
            {
                _AData.WTB.RectTextList[1].Text = _CCUStringArray[i];
            }

            if (BoolList[1023 + i])
            {
                _BData.WTB.RectTextList[0].Text = _CCUStringArray[i];
            }

            if (BoolList[1026 + i])
            {
                _BData.WTB.RectTextList[1].Text = _CCUStringArray[i];
            }

            #endregion
        }

        private void RefreshMVB(int i)
        {
            #region MVB

            if (BoolList[989 + i])
            {
                _AData.MVB.RectTextList[0].Text = _CCUStringArray[i];
            }
            if (BoolList[992 + i])
            {
                _AData.MVB.RectTextList[1].Text = _CCUStringArray[i];
            }

            if (BoolList[1017 + i])
            {
                _BData.MVB.RectTextList[0].Text = _CCUStringArray[i];
            }
            if (BoolList[1020 + i])
            {
                _BData.MVB.RectTextList[1].Text = _CCUStringArray[i];
            }

            #endregion
        }

        private void RefreshNodeId()
        {
            #region 设置节点编号

            if (FloatList[166] > 0)
            {
                _AData.Node.RectTextList[0].Text = FloatList[166].ToString();
            }
            if (FloatList[167] > 0)
            {
                _BData.Node.RectTextList[0].Text = FloatList[167].ToString();
            }

            #endregion
        }

        private void RefreshModel(int i)
        {
            #region 机车模式  这里存在问题

            if (BoolList[1029 + i])
            {
                _AData.Model.RectTextList[0].Text = _trainModelArray[i];
            }

            if (BoolList[1035 + i])
            {
                _BData.Model.RectTextList[0].Text = _trainModelArray[i];
            }

            #endregion
        }

        private void RefreshMainDisconnectionSwtich(int i)
        {
            #region 主断路器

            if (BoolList[826 + i])
            {
                _AData.Bow.RectTextList[1].ImagePicture = _imageArray[0 + i];
            }

            if (BoolList[830 + i])
            {
                _BData.Bow.RectTextList[1].ImagePicture = _imageArray[0 + i];
            }

            #endregion
        }

        private void RefreshCCU(int i)
        {
            #region 设置CCU

            if (BoolList[982 + i])
            {
                _AData.CC.RectTextList[0].Text = _CCUStringArray[i];
            }

            if (BoolList[985 + i])
            {
                _AData.CC.RectTextList[1].Text = _CCUStringArray[i];
            }

            if (BoolList[1010 + i])
            {
                _BData.CC.RectTextList[0].Text = _CCUStringArray[i];
            }

            if (BoolList[1013 + i])
            {
                _BData.CC.RectTextList[1].Text = _CCUStringArray[i];
            }

            #endregion
        }

        private void RefreshPantograph(int i)
        {
            #region 设置受电弓

            if (BoolList[835 + i])
            {
                _AData.Bow.RectTextList[0].Text = _bowStringArray[i];
            }

            if (BoolList[838 + i])
            {
                _BData.Bow.RectTextList[0].Text = _bowStringArray[i];
            }

            #endregion
        }

        public override void paint(Graphics dcGs)
        {
            RefreshDatas();

            _AData.OnDraw(dcGs);
            _BData.OnDraw(dcGs);
            dcGs.DrawLine(Common.PinkPen, _linePointStart, _linePointEnd);
        }
    }
}
