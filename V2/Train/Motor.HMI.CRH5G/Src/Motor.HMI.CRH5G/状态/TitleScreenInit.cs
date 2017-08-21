using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Motor.HMI.CRH5G.Resource;
using Motor.HMI.CRH5G.Resource.Images;
using Motor.HMI.CRH5G.底层共用;
using Motor.HMI.CRH5G.底层共用.RectView;

namespace Motor.HMI.CRH5G.Staus
{
    public partial class TitleScreen : CRH5GBase
    {
        public Action<TitleScreen, Graphics, int, RectangleF> DrawBottonButtonContentAction;
        public new Car Car { get { return base.Car; } }
        public MarshallingType CurrentSelectedMarshallingType { set; get; }

        private BottomButtonVisitor m_BottomButtonVisitor;

        /// <summary>
        /// 当按返回需要变到主页面的页面。
        /// </summary>
        public List<int> ChangedMainViewsWhenPressReturn { get; private set; }

        /// <summary>
        /// 车辆是否换端
        /// </summary>
        public bool TrainInside;


        /// <summary>
        /// 长短编组切换
        /// true为16车
        /// false为8车
        /// </summary>
        public bool ChangeLength { get; private set; }
        public bool OldChangeLength { get; private set; }
        public int ViewId = -100;
        private Timer m_ClearSendDatatTimer;
       // private int[] m_TnIDinBoolList;

        private string[] LogicName = new[]
        {
            OutBoolKeys.OutB启动自动制动实验,
            OutBoolKeys.OutB停止自动制动实验,
            OutBoolKeys.OutB启动导向制动实验,
            OutBoolKeys.OutB停止导向制动实验,
            OutBoolKeys.OutB启动备用制动实验,
            OutBoolKeys.OutB停止备用制动实验,
        };
        private readonly string[] m_BtnStr1 = { "1", "2", "3", "其它", "", "", "", "", "", "" };
        private readonly string[] m_BtnStr2 = { "4", "5", "6", "其它", "", "", "", "", "", "" };
        private readonly string[] m_BtnStr3 = { "7", "8", "9", "其它", "", "", "", "", "", "" };
        private readonly string[] m_BtnStr4 = { "10", "11", "", "其它", "", "", "", "", "", "" };
        private readonly string[] m_BtnStr5 = { "1", "2", "", "", "", "", "", "", "", "" };
        private readonly string[] m_BtnStr6 = { "1", "2", "3", "", "", "", "", "", "", "" };
        private readonly string[] m_BtnStr7 = { "自动", "导向的", "备用", "启动", "停止", "", "", "", "", "" };
        private readonly string[] m_BtnStr8 = { "1", "2", "3", "4", "5", "", "", "", "", "" };
        private readonly string[] m_BtnStr9 = { "", "", "", "", "", "", "", "", "", "" };
        private readonly string[] m_BtnStr10 = { "1", "", "", "", "", "", "←", "→", "", "" };
        private readonly string[] m_BtnStr11 = { "1", "2", "3", "", "", "", "", "", "", "" };
        public static string[] BtnStr12 = { "", "", "", "", "", "", "", "", "", "" };
        private readonly string[] m_BtnStr13 = { "1", "2", "3", "4", "5", "确认", "取消", "", "", "" };
        public string[] BtnStr14 = { "上一页", "下一页", "", "排序按时间", "排序按车号", "排序按设备", "", "", "", "" };
        public readonly string[] m_BtnStr15 = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};

        public List<Image> ImgsList { private set; get; }

        private List<RectangleF> m_RectsList;

        //6
        public override bool Initalize()
        {
            return Init();
            
        }

        private bool Init()
        {
            ChangedMainViewsWhenPressReturn = Enumerable.Range(1, 11).ToList();
            ChangedMainViewsWhenPressReturn.Add(40);


            ImgsList =new List<Image>()
            {
                ImagesResouce.SingleToDouble,
                ImagesResouce.DoubleToSingle
            };

            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.TitleScreen);

           
            m_ClearSendDatatTimer = new Timer(ClearSendData);
            return true;
        }
    }
}
