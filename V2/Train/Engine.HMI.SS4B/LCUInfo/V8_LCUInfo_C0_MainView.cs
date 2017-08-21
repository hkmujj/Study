#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;

namespace SS4B_TMS.LCUInfo
{
    /// <summary>
    /// 功能描述：LCU信息
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V8_Maintenance_C0_MainView:baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private Pen _blackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font _chineseFont = new Font("宋体", 12);
        private Button[] _btns;//按钮列表
        private String[] _btnNames;
        private int i = 0;

        private Pen _whiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Pen _whiteBigLinePen = new Pen(new SolidBrush(Color.White), 2.0f);

        private Rectangle[] _frameRects;
        private String[] _frameStrs;
        private Rectangle[] _frameStrRects;

        private Point[] hPStarts;
        private Point[] hPEnds;

        private Point[] vPStarts;
        private Point[] vPEnds;

        private Rectangle[] fixedRects;
        private String[] fixedStrs;

        private Rectangle[] changeableRects;

        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-主界面";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V8_Maintenance_C0_MainView";
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            hPStarts = new Point[19];
            hPEnds = new Point[19];
            hPStarts[0] = new Point(12, 20);
            hPEnds[0] = new Point(752, 20);

            for (i = 1; i < 17; i++)
            {
                hPStarts[i] = new Point(12, 56 + 26 * (i - 1));
                hPEnds[i] = new Point(752, 56 + 26 * (i - 1));
            }

            for (i = 17; i < 19; i++)
            {
                hPStarts[i] = new Point(12, 486 + 40 * (i - 17));
                hPEnds[i] = new Point(752, 486 + 40 * (i - 17));
            }

            vPStarts = new Point[8];
            vPEnds = new Point[8];
            vPStarts[0] = new Point(12, 20);
            vPEnds[0] = new Point(12, 526);

            vPStarts[1] = new Point(92, 446);
            vPEnds[1] = new Point(92, 526);

            vPStarts[2] = new Point(262, 20);
            vPEnds[2] = new Point(262, 446);

            vPStarts[3] = new Point(322, 20);
            vPEnds[3] = new Point(322, 446);

            vPStarts[4] = new Point(382, 20);
            vPEnds[4] = new Point(382, 446);

            vPStarts[5] = new Point(632, 20);
            vPEnds[5] = new Point(632, 446);

            vPStarts[6] = new Point(692, 20);
            vPEnds[6] = new Point(692, 446);

            vPStarts[7] = new Point(752, 20);
            vPEnds[7] = new Point(752, 526);


            fixedStrs = new String[37]
            {
                "        名称",
                "功补隔离开关572QS","4位电机故障隔离开关49QS",
                "3位电机故障隔离开关39QS","2位电机故障隔离开关29QS","1位电机故障隔离开关19QS",
                "牵引风速1隔离开关573QS","一端入库转换开关20QP","二端入库转换开关50QP",
                "劈相机2隔离开关583QS","制动风速1隔离开关589QS","自起劈相机隔离开关591QS",
                "牵引风速2隔离开关574QS","主断隔离开关586QS","欠压隔离开关236QS",
                "A车故障","B车故障",
                "A车","B车",
                "         名称",
                "制动风机2隔离开关582QS","制动风机1隔离开关581QS","牵引风机2隔离开关576QS",
                "牵引风机1隔离开关575QS","劈相机隔离开关2位242QS","劈相机隔离开关1位242QS",
                "备用","备用","备用","备用","备用",
                "变压器风机隔离开关599QS","油泵隔离开关584QS","制动机风速2隔离开关","压缩机隔离开关579QS",
                "A车","B车"
            };

            fixedRects = new Rectangle[37];
            fixedRects[0] = new Rectangle(12, 20, 120, 36);
            for (i = 1; i < 15; i++)
            {
                fixedRects[i] = new Rectangle(12, 56 + 26 * (i - 1), 250, 26);
            }
            fixedRects[15] = new Rectangle(12, 446, 80, 40);
            fixedRects[16] = new Rectangle(12, 486, 80, 40);

            fixedRects[17] = new Rectangle(262, 20, 60, 36);
            fixedRects[18] = new Rectangle(322, 20, 60, 36);
            fixedRects[19] = new Rectangle(382, 20, 120, 36);

            for (i = 0; i < 15; i++)
            {
                fixedRects[20 + i] = new Rectangle(382, 56 + 26 * i, 250, 26);
            }

            fixedRects[35] = new Rectangle(632, 20, 60, 36);
            fixedRects[36] = new Rectangle(692, 20, 60, 36);

            changeableRects = new Rectangle[58];

            for (i = 0; i < 14; i++)
            {
                changeableRects[2 * i] = new Rectangle(283, 60+i*26, 18, 18);
                changeableRects[2 * i + 1] = new Rectangle(343, 60 + i * 26, 18, 18);
            }
            for (i = 0; i < 15; i++)
            {
                changeableRects[28 + 2 * i] = new Rectangle(653, 60 + i * 26, 18, 18);
                changeableRects[2 * i + 29] = new Rectangle(713, 60 + i * 26, 18, 18);
            }


                return true;
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
            //判断点击的位置是否属于该button集合
            for (i = 0; i < _btns.Length; i++)
            {
                if ((nPoint.X >= this._btns[i].Rect.X)
                       && (nPoint.X <= this._btns[i].Rect.X + this._btns[i].Rect.Width)
                       && (nPoint.Y >= this._btns[i].Rect.Y)
                       && (nPoint.Y <= this._btns[i].Rect.Y + this._btns[i].Rect.Height))
                {
                    _btns[i].MouseDown(nPoint);
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
            for (i = 0; i < _btns.Length; i++)
            {
                if ((nPoint.X >= this._btns[i].Rect.X)
                       && (nPoint.X <= this._btns[i].Rect.X + this._btns[i].Rect.Width)
                       && (nPoint.Y >= this._btns[i].Rect.Y)
                       && (nPoint.Y <= this._btns[i].Rect.Y + this._btns[i].Rect.Height))
                {
                    _btns[i].MouseUp(nPoint);
                    break;
                }
            }
            return base.mouseUp(nPoint);
        }


        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V8_Maintenance_C0_MainView_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ViewState _tempViewState;
            if(!Enum.TryParse(e.Message.ToString(),out _tempViewState))
            {
                return;
            }
            if((ViewState.MT_TrainInfo <= _tempViewState)
                && (_tempViewState <= ViewState.MT_AdjustBrightness))
            {
                append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, e.Message, 0, 0);
                CommonStatus.CurrentViewState = _tempViewState;
                CommonStatus.CurrentInterfaceName = CommonStatus.InterfaceNameDicts[CommonStatus.CurrentViewState];
            }
      
        }

        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {

            for (i = 0; i < 19; i++)
            {
                dcGs.DrawLine(_whiteLinePen, hPStarts[i], hPEnds[i]);
            }
            for (i = 0; i < 8; i++)
            {
                dcGs.DrawLine(_whiteLinePen, vPStarts[i], vPEnds[i]);
            }

            for (i = 0; i < 37; i++)
            {
                dcGs.DrawString(fixedStrs[i], _chineseFont, Brushs.WhiteBrush, fixedRects[i], FontInfo.SF_LC);
            }

                onPaintChange(dcGs);
            



                base.paint(dcGs);
        }

        /// <summary>
        /// onPaintChange
        /// </summary>
        /// <param name="dcGs"></param>
        private void onPaintChange(Graphics dcGs)
        {
            for (i = 0; i < 14; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, changeableRects[2*i]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, changeableRects[2 * i]);
                }
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, changeableRects[2 * i + 1]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, changeableRects[2 * i + 1]);
                }
            }

            for (i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, changeableRects[28+2 * i]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, changeableRects[28 + 2 * i]);
                }
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, changeableRects[28 + 2 * i + 1]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, changeableRects[28 + 2 * i + 1]);
                }
            }

            for (i = 0; i < 5; i++)
            {
                dcGs.FillEllipse(Brushs.GreenBrush, changeableRects[40 + 2 * i]);
                dcGs.FillEllipse(Brushs.GreenBrush, changeableRects[40 + 2 * i + 1]);
            }

            for (i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i+6])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, changeableRects[50 + 2 * i]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, changeableRects[50 + 2 * i]);
                }
                if (BoolList[UIObj.InBoolList[3] + i + 6])
                {
                    dcGs.FillEllipse(Brushs.RedBrush, changeableRects[50 + 2 * i + 1]);
                }
                else
                {
                    dcGs.FillEllipse(Brushs.GreenBrush, changeableRects[50 + 2 * i + 1]);
                }
            }

              
        }
        #endregion

    }
}
