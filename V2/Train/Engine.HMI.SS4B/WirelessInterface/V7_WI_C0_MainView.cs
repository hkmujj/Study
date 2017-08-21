#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-26
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图7-检修-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS;
using ES.JCTMS.Common.Control.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common;
using SS4B_TMS.Common;

namespace SS4B_TMS.WirelessInterface
{
    /// <summary>
    /// 功能描述：无线界面
    /// 创建人：lih
    /// 创建时间：2015-8-26
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V7_History_C0_MainView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
       
        private Pen _blackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private Pen _whiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Pen _whiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.6f);

        private Font _chineseFont = new Font("宋体", 14);
        private Font _chineseBigFont = new Font("宋体", 18);

        private Font _digitFont = new Font("Arial", 12);

        private ButtonStyle normalBS;
        private Rectangle flagRect;

        private int i = 0;

        private Int32 _currentPageIndex = 1;
        private Int32 _allPageCount = 1;
        private String _timeShowFormat = "yyyy-MM-dd HH:mm:ss";

        private Int32 _itemsCount = 0;

        private List<Button> _btns_Down_TabView = new List<Button>();//按钮列表

        private Rectangle[] upRects;
        private Rectangle[] centreRects;

        private Rectangle currentTerminalRect = new Rectangle(1, 1, 141, 36);
        private String curTStr = "当前端为";
        private String[] terminalStrs = new string[2] { "A端", "B端" };

        private Rectangle curTimeRect = new Rectangle(623, 1, 177, 36);
        private String currentTimeStr;

        private Rectangle[] upFixedRects;

        private Rectangle[] upFixedStrsRects;
        private String[] upFixedStrs;

        private Rectangle[] upFillRects;
        private String[] upChangedStrs;

        private Point[] upFixedLineStarts;
        private Point[] upFixedLineEnds;

        private Point[] centreLineStarts;
        private Point[] centreLineEnds;

        private int[] centrePXs;
        private int[] centrePYs;

        private Rectangle[] centreFixedRects;
        private String[] centreFixedStrs;

        private Rectangle[] centreFillRects;
        private String[] centreFillStrs;

        private Rectangle faultRect = new Rectangle(6, 492, 250, 40);
        private String faultStr = "";

        private bool bztsFlag = false;
        private Rectangle bztsRect = new Rectangle(300, 465, 200, 40);
        private String bztsStr = "正在通讯....,请等待!";

        private Boolean[] btnFlags;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "故障历史信息-主界面";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V7_History_C0_MainView";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    this._resource_Images.Add(Image.FromStream(fs));
                }
            });
            String[] _strs_Btn_TabView = new String[10] { "状态显示", "编组", "", "解除编组", "", "编组设置", "改端", "编组诊断", "", "返回" };
            btnFlags = new bool[10] { false, false, false, false, false, false, false, false, false,false };
            normalBS = new ButtonStyle() 
            { 
                FontStyle = FontStyles.FS_Song_14_CC_W,
                Background = _resource_Images[0], 
                DownImage = _resource_Images[1] 
            };

            ButtonStyle greenStyle = new ButtonStyle()
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = _resource_Images[2],
                DownImage = _resource_Images[3]
            };

            ButtonStyle redStyle = new ButtonStyle()
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = _resource_Images[4],
                DownImage = _resource_Images[5]
            };

            ButtonStyle yellowStyle = new ButtonStyle()
            {
                FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = _resource_Images[6],
                DownImage = _resource_Images[7]
            };

            for (i = 0; i < _strs_Btn_TabView.Length; i++)
            {
                Button btn = new Button(
                    _strs_Btn_TabView[i],
                    new Rectangle(125 + 68 * i, 539, 60, 60),
                    ((int)(ViewState.WI_BTN_ShowStatus + i)),
                    normalBS,
                    true
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns_Down_TabView.Add(btn);
            }
            _btns_Down_TabView[1].Style = greenStyle;
            _btns_Down_TabView[3].Style = redStyle;
            _btns_Down_TabView[5].Style = yellowStyle;


            upRects = new Rectangle[4];
            upRects[0] = new Rectangle(6, 38, 165, 97);
            upRects[1] = new Rectangle(216, 38, 165, 97);
            upRects[2] = new Rectangle(423, 38, 165, 97);
            upRects[3] = new Rectangle(623, 38, 171, 97);

            centreRects = new Rectangle[2];
            centreRects[0] = new Rectangle(6, 149, 787, 151);
            centreRects[1] = new Rectangle(6, 311, 787, 151);
            
            flagRect = new Rectangle(1, 539, 122, 58);//标识

            onIniteUpRects();
            onIniteCentre();

            //faultStr = "重联开关故障";
            return true;
        }

        /// <summary>
        /// onIniteUpRects
        /// </summary>
        private void onIniteUpRects()
        {
            upFixedRects = new Rectangle[9];
            upFixedRects[0] = new Rectangle(14, 52, 147, 25);
            upFixedRects[1] = new Rectangle(14, 91, 147, 25);

            upFixedRects[2] = new Rectangle(228, 44, 71, 42);
            upFixedRects[3] = new Rectangle(228, 89, 71, 42);

            upFixedRects[4] = new Rectangle(626, 47, 82, 26);
            upFixedRects[5] = new Rectangle(626, 77, 82, 26);
            upFixedRects[6] = new Rectangle(626, 107, 82, 26);
            upFixedRects[7] = new Rectangle(710, 47, 82, 26);
            upFixedRects[8] = new Rectangle(710, 77, 82, 26);

            upFixedLineStarts = new Point[4];
            upFixedLineEnds = new Point[4];

            upFixedLineStarts[0] = new Point(86, 52);
            upFixedLineEnds[0] = new Point(86, 77);

            upFixedLineStarts[1] = new Point(86, 91);
            upFixedLineEnds[1] = new Point(86, 116);

            upFixedLineStarts[2] = new Point(228, 65);
            upFixedLineEnds[2] = new Point(299, 65);

            upFixedLineStarts[3] = new Point(228, 110);
            upFixedLineEnds[3] = new Point(299, 110);

            upFillRects = new Rectangle[21];
            upFillRects[0] = new Rectangle(87, 53, 74, 24);
            upFillRects[1] = new Rectangle(87, 92, 74, 24);
            upFillRects[2] = new Rectangle(229, 66, 70, 20);
            upFillRects[3] = new Rectangle(229, 111, 70, 20);

            upFillRects[4] = new Rectangle(317, 72, 12, 13);
            upFillRects[5] = new Rectangle(331, 62, 12, 23);
            upFillRects[6] = new Rectangle(345, 52, 12, 33);
            upFillRects[7] = new Rectangle(359, 42, 12, 43);

            upFillRects[8] = new Rectangle(317, 118, 12, 13);
            upFillRects[9] = new Rectangle(331, 108, 12, 23);
            upFillRects[10] = new Rectangle(345, 98, 12, 33);
            upFillRects[11] = new Rectangle(359, 88, 12, 43);

            upFillRects[12] = new Rectangle(425, 63, 79, 20);
            upFillRects[13] = new Rectangle(508, 63, 79, 20);
            upFillRects[14] = new Rectangle(425, 106, 79, 20);
            upFillRects[15] = new Rectangle(508, 106, 79, 20);

            upFillRects[16] = new Rectangle(627, 48, 81, 25);
            upFillRects[17] = new Rectangle(627, 78, 81, 25);
            upFillRects[18] = new Rectangle(627, 108, 81, 25);
            upFillRects[19] = new Rectangle(711, 48, 81, 25);
            upFillRects[20] = new Rectangle(711, 78, 81, 25);

            upChangedStrs = new string[21]
            {
                "0","200","已脱网","已注册",
                "", "", "", "","", "", "", "",
                "A节","B节","A节","B节",
                "GDTE-A","GDTE-B","TAX箱通信",
                "固态盘状态","BCU通信"
            };

            upFixedStrsRects = new Rectangle[6];

            upFixedStrs = new String[6] { "OCE-A", "OCE-B", "LTE-A", "LTE-B", "800M电台", "400K电台" };

            upFixedStrsRects[0] = new Rectangle(14, 52, 72, 25);
            upFixedStrsRects[1] = new Rectangle(14, 91, 72, 25);

            upFixedStrsRects[2] = new Rectangle(228, 44, 70, 20);
            upFixedStrsRects[3] = new Rectangle(228, 89, 70, 20);

            upFixedStrsRects[4] = new Rectangle(423, 38, 165, 27);
            upFixedStrsRects[5] = new Rectangle(423, 81, 165, 27);

            
        }

        /// <summary>
        /// onIniteCentre
        /// </summary>
        private void onIniteCentre()
        {
            centreLineStarts = new Point[28];
            centreLineEnds = new Point[28];

            centrePXs=new int[9]{69,154,268,331,415,529,591,675,793};
            centrePYs=new int[8]{149,201,253,300,311,362,411,462};

            for (i = 0; i < 8; i++)
            {
                centreLineStarts[2 * i] = new Point(centrePXs[i], centrePYs[0]);
                centreLineEnds[2 * i] = new Point(centrePXs[i], centrePYs[3]);

                centreLineStarts[2 * i+1] = new Point(centrePXs[i], centrePYs[4]);
                centreLineEnds[2 * i+1] = new Point(centrePXs[i], centrePYs[7]);
           }

            for (i = 8; i < 11; i++)
            {
                centreLineStarts[2 * i] = new Point(centrePXs[3 * (i - 8)], centrePYs[1]);
                centreLineEnds[2 * i] = new Point(centrePXs[3 * (i - 8) + 2], centrePYs[1]);

                centreLineStarts[2 * i + 1] = new Point(centrePXs[3 * (i - 8)], centrePYs[2]);
                centreLineEnds[2 * i + 1] = new Point(centrePXs[3 * (i - 8) + 2], centrePYs[2]);
            }

            for (i = 11; i < 14; i++)
            {
                centreLineStarts[2 * i] = new Point(centrePXs[3 * (i-11)], centrePYs[5]);
                centreLineEnds[2 * i] = new Point(centrePXs[3 * (i - 11) + 2], centrePYs[5]);

                centreLineStarts[2 * i + 1] = new Point(centrePXs[3 * (i-11)], centrePYs[6]);
                centreLineEnds[2 * i + 1] = new Point(centrePXs[3 * (i-11) + 2], centrePYs[6]);
            }

            centreFixedRects = new Rectangle[24];

            centreFixedStrs=new String[24]
            {
                "本车信息","车型","车号","序号",
                "编组信息","编组状态","编组数量","主从设置",
                "主车信息","车型","车号","运行方向",
                "从1 信息","车型","车号","车间距",
                "从2 信息","车型","车号","车间距",
                "从3 信息","车型","车号","车间距"
            };

            centreFillStrs = new String[24]
            {
                "SS4B","188","",
                "未设","","未设",
                "","","未设",
                "","","",
                "","","",
                "","","",

                "LTE-A","LTE-B",
                "LTE-A","LTE-B",
                "LTE-A","LTE-B",

            };

            centreFixedRects = new Rectangle[24];

            centreFixedRects[0] = new Rectangle(6,            centrePYs[0], centrePXs[0] - 6,            centrePYs[3] - centrePYs[0]);
            centreFixedRects[1] = new Rectangle(centrePXs[0], centrePYs[0], centrePXs[1] - centrePXs[0], centrePYs[1] - centrePYs[0]);
            centreFixedRects[2] = new Rectangle(centrePXs[0], centrePYs[1], centrePXs[1] - centrePXs[0], centrePYs[2] - centrePYs[1]);
            centreFixedRects[3] = new Rectangle(centrePXs[0], centrePYs[2], centrePXs[1] - centrePXs[0], centrePYs[3] - centrePYs[2]);

            centreFixedRects[4] = new Rectangle(centrePXs[2], centrePYs[0], centrePXs[3] - centrePXs[2], centrePYs[3] - centrePYs[0]);
            centreFixedRects[5] = new Rectangle(centrePXs[3], centrePYs[0], centrePXs[4] - centrePXs[3], centrePYs[1] - centrePYs[0]);
            centreFixedRects[6] = new Rectangle(centrePXs[3], centrePYs[1], centrePXs[4] - centrePXs[3], centrePYs[2] - centrePYs[1]);
            centreFixedRects[7] = new Rectangle(centrePXs[3], centrePYs[2], centrePXs[4] - centrePXs[3], centrePYs[3] - centrePYs[2]);

            centreFixedRects[8] = new Rectangle(centrePXs[5], centrePYs[0], centrePXs[6] - centrePXs[5], centrePYs[3] - centrePYs[0]);
            centreFixedRects[9] = new Rectangle(centrePXs[6], centrePYs[0], centrePXs[7] - centrePXs[6], centrePYs[1] - centrePYs[0]);
            centreFixedRects[10] = new Rectangle(centrePXs[6], centrePYs[1], centrePXs[7] - centrePXs[6], centrePYs[2] - centrePYs[1]);
            centreFixedRects[11] = new Rectangle(centrePXs[6], centrePYs[2], centrePXs[7] - centrePXs[6], centrePYs[3] - centrePYs[2]);

            centreFixedRects[12] = new Rectangle(6,            centrePYs[4], centrePXs[0] - 6,            centrePYs[7] - centrePYs[4]);
            centreFixedRects[13] = new Rectangle(centrePXs[0], centrePYs[4], centrePXs[1] - centrePXs[0], centrePYs[5] - centrePYs[4]);
            centreFixedRects[14] = new Rectangle(centrePXs[0], centrePYs[5], centrePXs[1] - centrePXs[0], centrePYs[6] - centrePYs[5]);
            centreFixedRects[15] = new Rectangle(centrePXs[0], centrePYs[6], centrePXs[1] - centrePXs[0], centrePYs[7] - centrePYs[6]);

            centreFixedRects[16] = new Rectangle(centrePXs[2], centrePYs[4], centrePXs[3] - centrePXs[2], centrePYs[7] - centrePYs[4]);
            centreFixedRects[17] = new Rectangle(centrePXs[3], centrePYs[4], centrePXs[4] - centrePXs[3], centrePYs[5] - centrePYs[4]);
            centreFixedRects[18] = new Rectangle(centrePXs[3], centrePYs[5], centrePXs[4] - centrePXs[3], centrePYs[6] - centrePYs[5]);
            centreFixedRects[19] = new Rectangle(centrePXs[3], centrePYs[6], centrePXs[4] - centrePXs[3], centrePYs[7] - centrePYs[6]);

            centreFixedRects[20] = new Rectangle(centrePXs[5], centrePYs[4], centrePXs[6] - centrePXs[5], centrePYs[7] - centrePYs[4]);
            centreFixedRects[21] = new Rectangle(centrePXs[6], centrePYs[4], centrePXs[7] - centrePXs[6], centrePYs[5] - centrePYs[4]);
            centreFixedRects[22] = new Rectangle(centrePXs[6], centrePYs[5], centrePXs[7] - centrePXs[6], centrePYs[6] - centrePYs[5]);
            centreFixedRects[23] = new Rectangle(centrePXs[6], centrePYs[6], centrePXs[7] - centrePXs[6], centrePYs[7] - centrePYs[6]);

            centreFillRects = new Rectangle[24];
            centreFillRects[0] = new Rectangle(centrePXs[1] + 5, centrePYs[0] + 5, centrePXs[2] - centrePXs[1] - 8, centrePYs[1] - centrePYs[0] - 8);
            centreFillRects[1] = new Rectangle(centrePXs[1] + 5, centrePYs[1] + 5, centrePXs[2] - centrePXs[1] - 8, centrePYs[2] - centrePYs[1] - 8);
            centreFillRects[2] = new Rectangle(centrePXs[1] + 5, centrePYs[2] + 5, centrePXs[2] - centrePXs[1] - 8, centrePYs[3] - centrePYs[2] - 8);

            centreFillRects[3] = new Rectangle(centrePXs[4] + 5, centrePYs[0] + 5, centrePXs[5] - centrePXs[4] - 8, centrePYs[1] - centrePYs[0] - 8);
            centreFillRects[4] = new Rectangle(centrePXs[4] + 5, centrePYs[1] + 5, centrePXs[5] - centrePXs[4] - 8, centrePYs[2] - centrePYs[1] - 8);
            centreFillRects[5] = new Rectangle(centrePXs[4] + 5, centrePYs[2] + 5, centrePXs[5] - centrePXs[4] - 8, centrePYs[3] - centrePYs[2] - 8);

            centreFillRects[6] = new Rectangle(centrePXs[7] + 5, centrePYs[0] + 5, centrePXs[8] - centrePXs[7] - 8, centrePYs[1] - centrePYs[0] - 8);
            centreFillRects[7] = new Rectangle(centrePXs[7] + 5, centrePYs[1] + 5, centrePXs[8] - centrePXs[7] - 8, centrePYs[2] - centrePYs[1] - 8);
            centreFillRects[8] = new Rectangle(centrePXs[7] + 5, centrePYs[2] + 5, centrePXs[8] - centrePXs[7] - 8, centrePYs[3] - centrePYs[2] - 8);

            centreFillRects[9] = new Rectangle(centrePXs[1] + 5, centrePYs[4] + 5, centrePXs[2] - centrePXs[1] - 8, centrePYs[5] - centrePYs[4] - 8);
            centreFillRects[10] = new Rectangle(centrePXs[1] + 5, centrePYs[5] + 5, centrePXs[2] - centrePXs[1] - 8, centrePYs[6] - centrePYs[5] - 8);
            centreFillRects[11] = new Rectangle(centrePXs[1] + 5, centrePYs[6] + 5, centrePXs[2] - centrePXs[1] - 8, centrePYs[7] - centrePYs[6] - 8);

            centreFillRects[12] = new Rectangle(centrePXs[4] + 5, centrePYs[4] + 5, centrePXs[5] - centrePXs[4] - 8, centrePYs[5] - centrePYs[4] - 8);
            centreFillRects[13] = new Rectangle(centrePXs[4] + 5, centrePYs[5] + 5, centrePXs[5] - centrePXs[4] - 8, centrePYs[6] - centrePYs[5] - 8);
            centreFillRects[14] = new Rectangle(centrePXs[4] + 5, centrePYs[6] + 5, centrePXs[5] - centrePXs[4] - 8, centrePYs[7] - centrePYs[6] - 8);

            centreFillRects[15] = new Rectangle(centrePXs[7] + 5, centrePYs[4] + 5, centrePXs[8] - centrePXs[7] - 8, centrePYs[5] - centrePYs[4] - 8);
            centreFillRects[16] = new Rectangle(centrePXs[7] + 5, centrePYs[5] + 5, centrePXs[8] - centrePXs[7] - 8, centrePYs[6] - centrePYs[5] - 8);
            centreFillRects[17] = new Rectangle(centrePXs[7] + 5, centrePYs[6] + 5, centrePXs[8] - centrePXs[7] - 8, centrePYs[7] - centrePYs[6] - 8);

            centreFillRects[18] = new Rectangle(8, 425, centrePXs[0] - 8, 16);
            centreFillRects[19] = new Rectangle(8, 442, centrePXs[0] - 8, 18);

            centreFillRects[20] = new Rectangle(centrePXs[2] + 2, 425, centrePXs[0] - 8, 16);
            centreFillRects[21] = new Rectangle(centrePXs[2] + 2, 442, centrePXs[0] - 8, 18);

            centreFillRects[22] = new Rectangle(centrePXs[5] + 2, 425, centrePXs[0] - 8, 16);
            centreFillRects[23] = new Rectangle(centrePXs[5] + 2, 442, centrePXs[0] - 8, 18);
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            for (i = 0; i < _btns_Down_TabView.Count; i++)
            {
                if ((nPoint.X >= this._btns_Down_TabView[i].Rect.X)
                       && (nPoint.X <= this._btns_Down_TabView[i].Rect.X + this._btns_Down_TabView[i].Rect.Width)
                       && (nPoint.Y >= this._btns_Down_TabView[i].Rect.Y)
                       && (nPoint.Y <= this._btns_Down_TabView[i].Rect.Y + this._btns_Down_TabView[i].Rect.Height))
                {
                    _btns_Down_TabView[i].MouseDown(nPoint);
                    break;
                }
            }

            
            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            for (i = 0; i < _btns_Down_TabView.Count; i++)
            {
                if ((nPoint.X >= this._btns_Down_TabView[i].Rect.X)
                       && (nPoint.X <= this._btns_Down_TabView[i].Rect.X + this._btns_Down_TabView[i].Rect.Width)
                       && (nPoint.Y >= this._btns_Down_TabView[i].Rect.Y)
                       && (nPoint.Y <= this._btns_Down_TabView[i].Rect.Y + this._btns_Down_TabView[i].Rect.Height))
                {
                    _btns_Down_TabView[i].MouseUp(nPoint);
                    break;
                }
            }
          
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 按钮点击事件响应函数：切换到相应视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case (int)ViewState.WI_BTN_Back:
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage,(int)ViewState.MainInterface, 0, 0);
                    break;
                case (int)ViewState.WI_BTN_ShowStatus:
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.WI_BTN_ShowStatus, 0, 0);
                    break;
                case (int)ViewState.WI_BTN_ShowStatus+3:
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_RemoveBZ), 0, 0);
                    break;
                case (int)ViewState.WI_BTN_ShowStatus+5:
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_BZSetting), 0, 0);
                    break;
                case (int)ViewState.WI_BTN_ShowStatus + 6:
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_RemoveBZ), 0, 0);
                    break;
                case (int)ViewState.WI_BTN_ShowStatus + 7:
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_BZDiagnoseInfo), 0, 0);
                    break;
                default: break;
            }
            if (this._btns_Down_TabView.Find(a => a.ID == e.Message) != null)
            {
                this._btns_Down_TabView.Find(a => a.ID == e.Message).IsReplication = false;
            }
            this._btns_Down_TabView.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
          
                if (BoolList[UIObj.InBoolList[9]])
                {
                    btnFlags[9] = true;
                   // append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage,(int)(ViewState.MainInterface), 0, 0);
                }
                else if ((btnFlags[9]))
                {
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.MainInterface), 0, 0);
                    btnFlags[9] = false;
                }

                if(BoolList[UIObj.InBoolList[0]])
                {
                    btnFlags[0] = true;
                    //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_ShowStatus), 0, 0);
                }
                else if (btnFlags[0])
                {
                    btnFlags[0] = false;
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_ShowStatus), 0, 0);
                }


                if (BoolList[UIObj.InBoolList[1]])
                {
                    bztsFlag = true;
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.SetBoolValue, UIObj.OutBoolList[0] + CommonStatus.SDBoolBaseNumber, 1, 0);
                }
                if (bztsFlag == true && BoolList[UIObj.InBoolList[10]]==false)
                {
                    dcGs.DrawString(bztsStr, _chineseFont, Brushs.WhiteBrush, bztsRect, FontInfo.SF_CC);
                }

                if (BoolList[UIObj.InBoolList[3]])// BoolList[UIObj.InBoolList[6]])
                {
                    btnFlags[3] = true;
                    //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_RemoveBZ), 0, 0);
                }
                else if (btnFlags[3])
                {
                    btnFlags[3] = false;
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_RemoveBZ), 0, 0);
                }

                if (BoolList[UIObj.InBoolList[6]])
                {
                    btnFlags[6] = true;
                }
                else if (btnFlags[6])
                {
                    btnFlags[6] = false;
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_RemoveBZ), 0, 0);
                }


                if (BoolList[UIObj.InBoolList[5]])
                {
                    btnFlags[5] = true;
                    //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_BZSetting), 0, 0);
                }
                else if (btnFlags[5])
                {
                     btnFlags[5] = false;
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_BZSetting), 0, 0);

                }


                if (BoolList[UIObj.InBoolList[7]])
                {
                    btnFlags[7] = true;
                    //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_BZDiagnoseInfo), 0, 0);
                }
                else if (btnFlags[7])
                {
                    btnFlags[7] = false;
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.WI_BTN_BZDiagnoseInfo), 0, 0);

                }


            dcGs.DrawImage(this._resource_Images[8], flagRect);
            this._btns_Down_TabView.ForEach(a => a.Paint(dcGs));

            dcGs.DrawRectangles(_whiteLinePen, upRects);

            dcGs.DrawRectangles(_whiteBigLinePen, centreRects);

            dcGs.DrawString((curTStr + terminalStrs[0]), _chineseFont, Brushs.WhiteBrush, currentTerminalRect, FontInfo.SF_CC);

            currentTimeStr=System.DateTime.Now.ToString(_timeShowFormat);
            dcGs.DrawString(currentTimeStr, _digitFont, Brushs.WhiteBrush, curTimeRect, FontInfo.SF_CC);

            dcGs.DrawRectangles(_whiteLinePen, upFixedRects);

            for (i = 0; i < 4; i++)
            {
                dcGs.DrawLine(_whiteLinePen, upFixedLineStarts[i], upFixedLineEnds[i]);
            }

            for (i = 0; i < 28; i++)
            {
                dcGs.DrawLine(_whiteBigLinePen, centreLineStarts[i], centreLineEnds[i]);
            }

            paintUpFillRects(dcGs);
            paintCentreRects(dcGs);

            faultRect.Width = (faultStr.Length * 40 > 700) ? 700 : faultStr.Length * 40;
            dcGs.FillRectangle(Brushs.RedBrush, faultRect);
            dcGs.DrawString(faultStr, _chineseFont, Brushs.WhiteBrush, faultRect, FontInfo.SF_LC);
            
            base.paint(dcGs);
        }

        /// <summary>
        /// paintUpFillRects
        /// </summary>
        /// <param name="dcGs"></param>
        private void paintUpFillRects(Graphics dcGs)
        {
            dcGs.FillRectangle(Brushs.RedBrush, upFillRects[0]);
            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[1]);

            dcGs.FillRectangle(Brushs.RedBrush, upFillRects[2]);
            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[3]);

            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[4]);
            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[5]);
            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[6]);
            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[7]);

            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[8]);
            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[9]);
            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[10]);
            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[11]);

            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[12]);
            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[13]);

            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[14]);
            dcGs.FillRectangle(Brushs.WhiteBrush, upFillRects[15]);

            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[16]);
            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[17]);
            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[18]);
            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[19]);
            dcGs.FillRectangle(Brushs.GreenBrush, upFillRects[20]);

            for (i = 0; i < 21; i++)
            {
                dcGs.DrawString(upChangedStrs[i], _chineseFont, Brushs.BlackBrush, upFillRects[i], FontInfo.SF_CC);
            }

            for (i = 0; i < 6; i++)
            {
                dcGs.DrawString(upFixedStrs[i], _chineseFont, Brushs.WhiteBrush, upFixedStrsRects[i], FontInfo.SF_CC);
            }

        }

        /// <summary>
        /// paintCentreRects
        /// </summary>
        /// <param name="dcGs"></param>
        private void paintCentreRects(Graphics dcGs)
        {

            dcGs.FillRectangles(Brushs.WhiteBrush, centreFillRects);

            for (i = 0; i < 24; i++)
            {
                dcGs.DrawString(centreFillStrs[i], _chineseFont, Brushs.BlackBrush, centreFillRects[i], FontInfo.SF_CC);
            }


                for (i = 0; i < 24; i++)
                {
                    dcGs.DrawString(centreFixedStrs[i], _chineseFont, Brushs.WhiteBrush, centreFixedRects[i], FontInfo.SF_CC);
                }
        }

        #endregion
    }
}
