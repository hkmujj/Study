#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图5-PIS-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using ES.Facility.PublicModule.Memo;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using SS4B_TMS.Common;

namespace SS4B_TMS.KMHClear
{

    /// <summary>
    /// 功能描述：公里时清零
    /// 创建人：lih
    /// 创建时间：2015-8-25
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V5_AirConditioner_C0_Page1 : baseClass
    {

        #region 稀有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btns_Mode = new List<Button>();
        private Pen _blackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private List<Button> _btns_Down_TabView = new List<Button>();
        private int i, j = 0;
        private SolidBrush _RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));

        private SolidBrush _controlBrush = new SolidBrush(Color.FromArgb(147, 147, 147));

        private Font _digitFont = new Font("Arial", 18);

        private Font _chineseFont = new Font("宋体", 18);
        private Font _chineseFontB = new Font("宋体", 14);
        private Font _chineseFontA = new Font("宋体", 14, FontStyle.Bold);
        private Rectangle _pageRect = new Rectangle(710, 335, 56, 27);
        private SolidBrush _blackBrush = new SolidBrush(Color.Black);
        private Rectangle[] _frameRects;
        private Rectangle _frameSpeA = new Rectangle(12, 452, 128, 50);
        private String[] _frameStrs;
        private Int32[] _RectYs;
        private Int32[] _RectXs;
        private Rectangle[] _rect1;
        private Rectangle[] _rect2;
        private Rectangle[] _rectStr2;
        private Rectangle[] _rect3;
        private Rectangle[] _rectStr3;
        private String[] _modelStrs;
        private String _pageStr = "页1-2";

        private bool[] _rect1Flags;
        private bool[] _rect3Flags;
        private String[] _rect2Strs;

        private int[] rect1Count;

        private int indexFlag = 0;

        private ViewState _currentViewState;

        private int timeCount = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "空调-界面1";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V5_AirConditioner_C0_Page1";
        //}

        /// <summary>
        /// 初始化函数：导入图片、创建控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            
            //_frameRects = new Rectangle[3];
            //_frameRects[0] = new Rectangle(12, 262, 674, 84);
            //_frameRects[1] = new Rectangle(12, 350, 674, 45);
            //_frameRects[2] = new Rectangle(12, 400, 674, 45);
            //_frameStrs = new String[4] { "空调机组状态","客室温度","控制模式","空调模式选择"};

            //_RectXs = new int[6];
            //for (i = 0; i < _RectXs.Length; i++)
            //{
            //    _RectXs[i] = 177 + i * 86;
            //}

            //_RectYs = new int[4];
            //_RectYs[0] = 266;
            //_RectYs[1] = 306;
            //_RectYs[2] = 355;
            //_RectYs[3] = 404;

            //_rect1 = new Rectangle[12];
            //_rect1Flags = new bool[12];
            //rect1Count = new int[12];
            //for (i = 0; i < _RectXs.Length; i++)
            //{
            //    rect1Count[2 * i] = 0;
            //    rect1Count[2 * i+1] = 0;
            //    _rect1Flags[2 * i] = false;
            //    _rect1Flags[2 * i + 1] = false;
            //    _rect1[2 * i] = new Rectangle(_RectXs[i], _RectYs[0], 60, 36);
            //    _rect1[2 * i + 1] = new Rectangle(_RectXs[i], _RectYs[1], 60, 36);
            //}
            

            //_rect2 = new Rectangle[6];
            //_rectStr2 = new Rectangle[6];
            //_rect2Strs = new String[6] { "", "", "", "", "", "" };
            //for (i = 0; i < _rect2.Length; i++)
            //{
            //    _rect2[i] = new Rectangle(_RectXs[i], _RectYs[2], 58, 35);
            //    _rectStr2[i] = new Rectangle(_RectXs[i] + 1, _RectYs[2] + 1, 57, 34);
            //}

            //_rect3 = new Rectangle[6];
            //_rectStr3 = new Rectangle[6];
            //_modelStrs = new String[3] { "集控", "本控","?"};
            //for (i = 0; i < _rect3.Length; i++)
            //{
            //    _rect3[i] = new Rectangle(_RectXs[i], _RectYs[3], 58, 35);
            //    _rectStr3[i] = new Rectangle(_RectXs[i] + 1, _RectYs[3] + 1, 57, 34);
            //}

            ////
            //UIObj.ParaList.ForEach(a =>
            //{
            //    using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
            //    {
            //        this._resource_Image.Add(Image.FromStream(fs));
            //    }
            //});

            ////模式按钮列表
            //String[] strs_1 = new String[] { "自动", "手动", "通风", "停止" };
            //for (int i = 0; i < strs_1.Length; i++)
            //{
            //    Button btn = new Button(
            //        strs_1[i],
            //        new RectangleF(140+i*92, 452, 86, 50),
            //        (int)(ViewState.AirCondition_P1_Auto+i),
            //        new ButtonStyle()
            //        {
            //            FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
            //            Background = _resource_Image[0],
            //            DownImage = _resource_Image[1]
            //        },
            //        false
            //        );
            //    btn.ClickEvent += btn_Mode_ClickEvent;
            //    this._btns_Mode.Add(btn);
            //}
            //this._btns_Mode[0].IsReplication = false;
            //_currentViewState = ViewState.AirCondition_P1_Auto;
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + CommonStatus.SDBoolBaseNumber, 1, 0);

            //ButtonStyle bs1 = new ButtonStyle() { FontStyle = FontStyles.FS_Song_16_CC_B, Background = _resource_Image[2], DownImage = _resource_Image[2] };
            //Button btn1 = new Button(
            //     "",
            //     new RectangleF(713, 359, 57, 34),
            //     5000,
            //     bs1,
            //     false
            //     );
            //btn1.ClickEvent += btn1_ClickEvent;//up
            //this._btns_Down_TabView.Add(btn1);
            return true;
        }

        /// <summary>
        /// 课程开始时发生默认命令
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        //public override void setRunValue(int nParaA, int nParaB, float nParaC)
        //{
        //    if (2 == nParaA)
        //    {
        //        if (Common.CommonStatus.IsBlackScreen == true)
        //        {
        //            timeCount = 2;
        //            _currentViewState = ViewState.AirCondition_P1_Auto;
        //            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + CommonStatus.SDBoolBaseNumber, 1, 0);
                    
        //        }
        //    }
        //    if (timeCount>1 && timeCount < 10)
        //    {
        //        timeCount++;
        //    }
        //    else if (timeCount>10)
        //    {
        //        Common.CommonStatus.IsBlackScreen = false;
        //        timeCount = -1;
        //    }
        //}
        //public override bool canSetStringList(int nPara)
        //{
        //    if (nPara == 3)//线路信息
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

        /// <summary>
        /// 读取站点信息
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="cStr"></param>
        //public override void addStringByLine(int nIndex, string cStr)
        //{
        //}
        #endregion

        #region 鼠标事件
        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            for (int i = 0; i < this._btns_Mode.Count; i++)
            {
                if ((nPoint.X >= this._btns_Mode[i].Rect.X)
                    && (nPoint.X <= this._btns_Mode[i].Rect.X + this._btns_Mode[i].Rect.Width)
                    && (nPoint.Y >= this._btns_Mode[i].Rect.Y)
                    && (nPoint.Y <= this._btns_Mode[i].Rect.Y + this._btns_Mode[i].Rect.Height))
                {
                    _btns_Mode[i].MouseDown(nPoint);
                    _btns_Mode[i].IsReplication = false;
                    this._btns_Mode.FindAll(a => a.ID != _btns_Mode[i].ID).ForEach(b => b.IsReplication = true);
                    break;
                }
            }
           

            for (int i = 0; i < this._btns_Down_TabView.Count; i++)
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
            for (int i = 0; i < this._btns_Mode.Count; i++)
            {
                if ((nPoint.X >= this._btns_Mode[i].Rect.X)
                    && (nPoint.X <= this._btns_Mode[i].Rect.X + this._btns_Mode[i].Rect.Width)
                    && (nPoint.Y >= this._btns_Mode[i].Rect.Y)
                    && (nPoint.Y <= this._btns_Mode[i].Rect.Y + this._btns_Mode[i].Rect.Height))
                {
                    _btns_Mode[i].MouseUp(nPoint);
                    break;
                }
            }

            for (int i = 0; i < this._btns_Down_TabView.Count; i++)
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
        /// 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Mode_ClickEvent(object sender, ClickEventArgs<int> e)
        {
           ViewState temp;
           if(!Enum.TryParse(e.Message.ToString(),out temp))
           {
               return;
           }
           switch (temp)
           {
               case ViewState.AirCondition_P1_Auto:
                   _currentViewState = ViewState.AirCondition_P1_Auto;
                   break;
               case ViewState.AirCondition_P1_Manual:
                   _currentViewState = ViewState.AirCondition_P1_Manual;
                   break;
               case ViewState.AirCondition_P1_Air:
                   _currentViewState = ViewState.AirCondition_P1_Air;
                   break;
               case ViewState.AirCondition_P1_Stop:
                   _currentViewState = ViewState.AirCondition_P1_Stop;
                   break;
               default: break;
           }
           
        }

        /// <summary>
        /// btn1_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       void  btn1_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.AirConditionerPage2, 0, 0);
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            //this._btns_Mode.ToList().ForEach(a => a.Paint(dcGs));
            //this._btns_Down_TabView.ToList().ForEach(a => a.Paint(dcGs));

            //onSendData();

            //dcGs.DrawString(_pageStr, _chineseFontA, _blackBrush, _pageRect, FontInfo.SF_CC);

            //for (i = 0; i < _frameRects.Length; i++)
            //{
            //    dcGs.DrawRectangle(_blackLinePen, _frameRects[i]);
            //    dcGs.DrawString(_frameStrs[i], _chineseFontB, Brushs.BlackBrush, _frameRects[i], FontInfo.SF_LC);
            //}
            //dcGs.DrawString(_frameStrs[3], _chineseFontB, Brushs.BlackBrush, _frameSpeA, FontInfo.SF_LC);


            //for (i = 0; i < 6; i++)
            //{
            //    _rect1Flags[2 * i] = false;
            //    _rect1Flags[2 * i + 1] = false;
            //    for (j = 0; j < 8; j++)
            //    {
            //        if (j == 1 || j == 5)
            //        {
            //            if (BoolList[UIObj.InBoolList[i] + j])
            //            {
            //                _rect1Flags[2 * i] = true;
            //                paintAC(dcGs, 2 * i);
            //            }
            //            if (BoolList[UIObj.InBoolList[i] + j + 8])
            //            {
            //                _rect1Flags[2 * i + 1] = true;
            //                paintAC(dcGs, 2 * i + 1);
            //            }
            //        }
            //        else
            //        {
            //            if (BoolList[UIObj.InBoolList[i] + j])
            //            {
            //                _rect1Flags[2 * i] = true;
            //                dcGs.DrawImage(_resource_Image[3 + j], _rect1[2 * i]);
            //            }
            //            if (BoolList[UIObj.InBoolList[i] + j + 8])
            //            {
            //                _rect1Flags[2 * i + 1] = true;
            //                dcGs.DrawImage(_resource_Image[3 + j], _rect1[2 * i + 1]);
            //            }
            //        }
            //    }
            //}

            //for (i = 0; i < 6; i++)
            //{
            //    if (_rect1Flags[2 * i] == false)
            //    {
            //        dcGs.DrawImage(_resource_Image[11], _rect1[2 * i]);
            //    }
            //    if (_rect1Flags[2 * i + 1] == false)
            //    {
            //        dcGs.DrawImage(_resource_Image[11], _rect1[2 * i + 1]);
            //    }
            //}

            //for (i = 0; i < _rect2.Length; i++)
            //{
            //    dcGs.DrawRectangle(_blackLinePen, _rect2[i]);
            //    dcGs.FillRectangle(_RectBrush, _rectStr2[i]);

            //    _rect2Strs[i] = FloatList[UIObj.InFloatList[i]].ToString("0");
            //    dcGs.DrawString(_rect2Strs[i], _digitFont, Brushs.BlackBrush, _rect2[i], FontInfo.SF_CC);
            //}

            //for (i = 0; i < _rect3.Length; i++)
            //{

            //    if(BoolList[UIObj.InBoolList[i+6]+1])
            //    {
            //        dcGs.DrawRectangle(_blackLinePen, _rect3[i]);
            //        dcGs.FillRectangle(_RectBrush, _rectStr3[i]);
            //        dcGs.DrawString(_modelStrs[1], _chineseFont, Brushs.BlackBrush, _rect3[i], FontInfo.SF_CC);
            //    }
            //    else if(BoolList[UIObj.InBoolList[i+6]])
            //    {
            //        dcGs.DrawRectangle(_blackLinePen, _rect3[i]);
            //        dcGs.FillRectangle(_RectBrush, _rectStr3[i]);
            //        dcGs.DrawString(_modelStrs[0], _chineseFont, Brushs.BlackBrush, _rect3[i], FontInfo.SF_CC);
            //    }
            //    else
            //    {
            //        dcGs.DrawImage(_resource_Image[11], _rect3[i]);
            //    }
                
            //}
            //
            base.paint(dcGs);
        }

        /// <summary>
        /// paintAC
        /// </summary>
        /// <param name="dcGs"></param>
        /// <param name="cindex"></param>
        private void paintAC(Graphics dcGs,int cindex)
        {
            if (rect1Count[cindex] < 2)
            {
                rect1Count[cindex]++;
            }
            else if (rect1Count[cindex] >= 2 && rect1Count[cindex] <= 4)
            {
                dcGs.DrawImage(_resource_Image[3 + j], _rect1[cindex]);
                rect1Count[cindex]++;
            }
            else
            {
                rect1Count[cindex] = 0;
            }
        }

        /// <summary>
        /// onSendData
        /// </summary>
        private  void onSendData()
        {
            switch (_currentViewState)
            {
                case ViewState.AirCondition_P1_Auto:
                    indexFlag = 0;
                    break;
                case ViewState.AirCondition_P1_Manual:
                    indexFlag = 1;
                    break;
                case ViewState.AirCondition_P1_Air:
                    indexFlag = 2;
                    break;
                case ViewState.AirCondition_P1_Stop:
                    indexFlag = 3;
                    break;
                default: break;
            }

            for (i = 0; i < 4; i++)
            {
                if (i != indexFlag)
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[i] + CommonStatus.SDBoolBaseNumber, 0, 0);
                }
                else
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[i] + CommonStatus.SDBoolBaseNumber, 1, 0);
                }
            }
        }
        #endregion
    }
}
