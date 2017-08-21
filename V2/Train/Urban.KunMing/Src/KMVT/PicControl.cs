using System.Collections.Generic;
using System.Drawing;
using ES.Facility.PublicModule.Memo;


namespace KMVT
{
    /// <summary>
    /// Vd热区信息
    /// </summary>
    public class VdAreaInfo
    {
        private RectangleF _vdHotAreaInfo = new RectangleF(0, 0, 0, 0);
        /// <summary>
        /// 热区信息
        /// </summary>
        public RectangleF VdHotAreaInfo 
        {
            get { return _vdHotAreaInfo; }
            set { _vdHotAreaInfo = value; }
        }

        /// <summary>
        /// 跳转控件
        /// </summary>
        public int VdJumpIndex { get; set; }

        private Dictionary<int, bool> _vdSendData = new Dictionary<int, bool>();
        /// <summary>
        /// 发送数据
        /// </summary>
        public Dictionary<int, bool> VdSendData
        {
            get { return _vdSendData; }
            set { _vdSendData = value; }
        }
    }

    /// <summary>
    /// Vd基础类
    /// </summary>
    public class VdBaseClass
    {
        #region ::::::::::::: fun :::::::::::::
        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="nScore">需要解析的文本</param>
        /// <returns>数据编号列表</returns>
        private static List<int> SplitST(string nScore)
        {
            var anyList = new List<int>();

            var tmpSouceStr = nScore;    //[发送的数据]
            var tmpStr = string.Empty;   //发送的数据
            var outBoolStr = string.Empty;   //发送的数据.Trim()

            if (StringHelper.findStringByKey(tmpSouceStr, "[", "]", ref tmpStr))
            {
                outBoolStr = tmpStr.Trim();
            }

            var sendPart = outBoolStr.Split(new[] { ' ', ',' });  //发送的数据解析成数组


            //判断是否解析成功
            foreach (var tmpstr in sendPart)
            {
                int tmpint;
                if (int.TryParse(tmpstr, out tmpint))
                    anyList.Add(tmpint);
            }

            return anyList;
        }

        /// <summary>
        /// 解析发送内容
        /// </summary>
        /// <param name="nScore"></param>
        /// <param name="nVDAI"></param>
        private void SplitTab(string nScore, ref VdAreaInfo nVDAI)
        {
            //根据正负存储数字量的值
            //正为1,即true
            //负为0,即false
            foreach (var tmpInt in SplitST(nScore))
            {
                nVDAI.VdSendData.Add(tmpInt, tmpInt >= 0);
            }
        }


        /// <summary>
        /// 获取控件编号和控件状态
        /// 控件编号_控件状态
        /// 唯一识别标记
        /// </summary>
        /// <returns></returns>
        public string GetNameAndStaus()
        {
            return _vdIndex + "_" + _vdStaus;
        }

        /// <summary>
        /// 参数个数
        /// </summary>
        /// <returns></returns>
        public static int GetUnitCount()
        {
            return 22;
        }
        #endregion

        #region ::::::::::::: init :::::::::::::
        /// <summary>
        /// 从配置信息初始化该对象
        /// </summary>
        /// <param name="nRefStres"></param>
        /// <param name="nRootPath"></param>
        /// <returns></returns>
        public bool InitVdObjectByStrings(ref string[] nRefStres, string nRootPath)
        {
            int tmpInt;
            float tmpFloat;
            var rectF = new RectangleF(0, 0, 0, 0);

            #region 0 控件类型
            if (int.TryParse(nRefStres[0], out tmpInt))
            {
                _typeInfo = tmpInt;
            }
            else return false;
            #endregion

            #region 1 控件顶点 X
            if (float.TryParse(nRefStres[1], out tmpFloat))
            {
                _vdLocPoint.X = tmpFloat;
            }
            else return false;
            #endregion

            #region 2 控件顶点 Y
            if (float.TryParse(nRefStres[2], out tmpFloat))
            {
                _vdLocPoint.Y = tmpFloat;
            }
            else return false;
            #endregion

            #region 3 控件编号
            if (int.TryParse(nRefStres[3], out tmpInt))
            {
                _vdIndex = tmpInt;
            }
            else return false;
            #endregion

            #region 4 控件状态
            if (int.TryParse(nRefStres[4], out tmpInt))
            {
                _vdStaus = tmpInt;
            }
            else return false;
            #endregion

            #region 5 控件图片信息
            if (System.IO.File.Exists(nRootPath + "\\" + nRefStres[5]))
            {
                _picImage = Image.FromFile(nRootPath + "\\" + nRefStres[5]);
                _picName = nRefStres[5].Trim();
            }
            else return false;
            #endregion

            #region 6 7 8 9 前向热区
            if (float.TryParse(nRefStres[6], out tmpFloat))
            {
                rectF.X = tmpFloat;
            }
            else return false;

            if (float.TryParse(nRefStres[7], out tmpFloat))
            {
                rectF.Y = tmpFloat;
            }
            else return false;

            if (float.TryParse(nRefStres[8], out tmpFloat))
            {
                rectF.Width = tmpFloat;
            }
            else return false;

            if (float.TryParse(nRefStres[9], out tmpFloat))
            {
                rectF.Height = tmpFloat;
            }
            else return false;
            //
            _vdHotAreaInfoF.VdHotAreaInfo = rectF;
            #endregion

            #region 10 前向热区跳转到的控件编号
            if (int.TryParse(nRefStres[10], out tmpInt))
            {
                _vdHotAreaInfoF.VdJumpIndex = tmpInt;
            }
            else return false;
            #endregion

            #region 11 解析发送数据
            SplitTab(nRefStres[11], ref _vdHotAreaInfoF);
            #endregion

            #region 12 面板序号
            if (int.TryParse(nRefStres[12], out tmpInt))
            {
                _menuID = tmpInt;
            }
            else return false;
            #endregion

            #region 13 14 15 16 后向热区
            if (float.TryParse(nRefStres[13], out tmpFloat))
            {
                rectF.X = tmpFloat;
            }
            else return false;

            if (float.TryParse(nRefStres[14], out tmpFloat))
            {
                rectF.Y = tmpFloat;
            }
            else return false;

            if (float.TryParse(nRefStres[15], out tmpFloat))
            {
                rectF.Width = tmpFloat;
            }
            else return false;

            if (float.TryParse(nRefStres[16], out tmpFloat))
            {
                rectF.Height = tmpFloat;
            }
            else return false;
            //
            _vdHotAreaInfoB.VdHotAreaInfo = rectF;
            #endregion

            #region 17 后向热区跳转到的控件编号
            if (int.TryParse(nRefStres[17], out tmpInt))
            {
                _vdHotAreaInfoB.VdJumpIndex = tmpInt;
            }
            else return false;
            #endregion

            #region 18 解析收到的数据
            SplitTab(nRefStres[18], ref _vdHotAreaInfoB);
            #endregion

            #region 19 跳转到的状态号 | 倒计时
            if (int.TryParse(nRefStres[19], out tmpInt))
            {
                _vdJumpToIndex = tmpInt;
                _vdJumpTime = 5;
            }
            else return false;
            #endregion

            #region 20 类型2控件 收跳闸数据
            _catchTrip = SplitST(nRefStres[20]);
            #endregion

            #region 21 根据逻辑传回的值变化 改变状态
            if (int.TryParse(nRefStres[21], out tmpInt))
            {
                _tripJumpToIndex = tmpInt;
            }
            else return false;
            #endregion

            return true;

        }

        #endregion

        #region ::::::::::::: var :::::::::::::
        /// <summary>
        /// 发送量
        /// </summary>
        private List<int> _sendList = new List<int>();

        /// <summary>
        /// 在队列中的编号
        /// </summary>
        private int _listIndex;
        /// <summary>
        /// 在队列中的编号
        /// </summary>
        public int ListIndex { get { return _listIndex; } set { _listIndex = value; } }

        /// <summary>
        /// 控件类型
        /// </summary>
        private int _typeInfo;
        /// <summary>
        /// 控件类型
        /// </summary>
        public int TypeInfo { get { return _typeInfo; } set { _typeInfo = value; } }

        /// <summary>
        /// 控件顶点坐标
        /// </summary>
        private PointF _vdLocPoint = new PointF(0, 0);
        /// <summary>
        /// 控件顶点坐标
        /// </summary>
        public PointF VdLocPoint { get { return _vdLocPoint; } set { _vdLocPoint = value; } }

        /// <summary>
        /// 控件编号
        /// </summary>
        private int _vdIndex;
        /// <summary>
        /// 控件编号
        /// </summary>
        public int VdIndex { get { return _vdIndex; } set { _vdIndex = value; } }

        /// <summary>
        /// 控件状态
        /// </summary>
        private int _vdStaus;
        /// <summary>
        /// 控件状态
        /// </summary>
        public int VdStaus { get { return _vdStaus; } set { _vdStaus = value; } }

        /// <summary>
        /// 控件图片名称
        /// </summary>
        private string _picName = "";
        /// <summary>
        /// 控件图片名称
        /// </summary>
        public string PicName { get { return _picName; } set { _picName = value; } }

        /// <summary>
        /// 控件图片对象
        /// </summary>
        private Image _picImage;
        /// <summary>
        /// 控件图片对象
        /// </summary>
        public Image PicImage { get { return _picImage; } set { _picImage = value; } }

        /// <summary>
        /// 前向热区
        /// </summary>
        private VdAreaInfo _vdHotAreaInfoF = new VdAreaInfo();
        /// <summary>
        /// 前向热区
        /// </summary>
        public VdAreaInfo VdHotAreaInfoF
        {
            get { return _vdHotAreaInfoF; }
            set { _vdHotAreaInfoF = value; }
        }

        /// <summary>
        /// 后向热区
        /// </summary>
        private VdAreaInfo _vdHotAreaInfoB = new VdAreaInfo();
        /// <summary>
        /// 后向热区
        /// </summary>
        public VdAreaInfo VdHotAreaInfoB
        {
            get { return _vdHotAreaInfoB; }
            set { _vdHotAreaInfoB = value; }
        }

        /// <summary>
        /// 跳闸信息
        /// </summary>
        private List<int> _catchTrip = new List<int>();
        /// <summary>
        /// 跳闸信息
        /// </summary>
        public List<int> CatchTrip
        {
            get { return _catchTrip; }
            set { _catchTrip = value; }
        }

        /// <summary>
        /// 跳闸跳转到的状态
        /// </summary>
        private int _tripJumpToIndex;
        /// <summary>
        /// 跳闸跳转到的状态
        /// </summary>
        public int TripJumpToIndex { get { return _tripJumpToIndex; } set { _tripJumpToIndex = value; } }

        /// <summary>
        /// 是否停顿
        /// </summary>
        private bool _vdIsSleep;
        /// <summary>
        /// 是否停顿
        /// </summary>
        public bool VdIsSleep { get { return _vdIsSleep; } set { _vdIsSleep = value; } }

        /// <summary>
        /// 跳转倒计时
        /// </summary>
        private int _vdJumpTime;
        /// <summary>
        /// 跳转倒计时
        /// </summary>
        public int VdJumpTime { get { return _vdJumpTime; } set { _vdJumpTime = value; } }

        /// <summary>
        /// 需要跳转到的状态
        /// </summary>
        private int _vdJumpToIndex;
        /// <summary>
        /// 需要跳转到的状态
        /// </summary>
        public int VdJumpToIndex { get { return _vdJumpToIndex; } set { _vdJumpToIndex = value; } }

        /// <summary>
        /// 面板序号
        /// </summary>
        private int _menuID;
        /// <summary>
        /// 面板序号
        /// </summary>
        public int MenuID { get { return _menuID; } set { _menuID = value; } }
        #endregion
    }
}
