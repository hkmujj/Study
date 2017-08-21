#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-24
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图4-辅助-No.0-页面1
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.IO;
using System.Collections.Generic;
using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using System.Drawing;
using System.Linq;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using ES.JCTMS.Common;
using SS4B_TMS.Common;

namespace SS4B_TMS.TimeSetting
{
    /// <summary>
    /// 功能描述：时间设置
    /// 创建人：lih
    /// 创建时间：2015-8-24
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V4_Assist_C0_Page1 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private List<Button> _btns_Down_TabView = new List<Button>();//按钮列表
        private Pen _blackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private Pen _whiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);

        private int i, j = 0;
        private SolidBrush _RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font _digitFont = new Font("Arial", 14);
        private Font _chineseFont = new Font("宋体", 14);
        private Font _chineseFontA = new Font("宋体", 14, FontStyle.Bold);
        private Rectangle _pageRect = new Rectangle(710, 335, 56, 27);
        private SolidBrush _blackBrush = new SolidBrush(Color.Black);
        private Rectangle[] _frameRects;
        private Rectangle[] _frameNameRects;
        private String[] _frameStr;
        private String _pageStr = "页1-2";

        private List<Button> offsideBtns = new List<Button>();
        private ButtonStyle normalBS;
        private Rectangle flagRect;


        private String dateString = "0000-00-00";
        private String timeString = "00:00:00";
        private Rectangle dateRect;
        private Rectangle timeRect;

        private Rectangle dateFillRect;
        private Rectangle timeFillRect;

        private Button confirmBtn;

        private String[] rectStrs;
        private Rectangle[] rects;


        private int[] pointX;
        private int[] pointY;

        private int cursorIndex = 0;
        private String[] _strs_Btn_TabView;

        private string[] inputStrs;

        private bool[] btnFlags;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "辅助-页面1";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V4_Assist_C0_Page1";
        //}

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    this._resource_Images.Add(Image.FromStream(fs));
                }
            });

            _strs_Btn_TabView = new String[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            normalBS = new ButtonStyle() { FontStyle = FontStyles.FS_Song_14_CC_WAndB, Background = _resource_Images[0], DownImage = _resource_Images[1] };


            for (i = 0; i < _strs_Btn_TabView.Length; i++)
            {
                Button btn = new Button(
                    _strs_Btn_TabView[i],
                    new Rectangle(125 + 68 * i, 539, 60, 60),
                    ((int)(ViewState.SI_Digit_One + i)),
                    normalBS,
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns_Down_TabView.Add(btn);
            }
            flagRect = new Rectangle(1, 539, 122, 58);//标识

            ButtonStyle comonStyle=new ButtonStyle(){FontStyle = FontStyles.FS_Song_14_CC_WAndB, Background = _resource_Images[3], DownImage = _resource_Images[1] };
            String[] btnStrs = new String[5] { "返回", "上移", "下移", "左移", "右移" };
            for (i = 0; i < btnStrs.Length; i++)
            {
                Button btn=new Button(
                    btnStrs[i],
                    new Rectangle(738, 104+66*i, 58, 60),
                    ((int)(ViewState.SI_Btn_Back + i)),
                    comonStyle,
                    true
                    );
                btn.ClickEvent += offsideBtn_ClickEvent;
                offsideBtns.Add(btn);
            }

            ButtonStyle confirmStyle = new ButtonStyle() { FontStyle = FontStyles.FS_Song_14_CC_WAndB,
                Background = _resource_Images[4], 
                DownImage = _resource_Images[1] };
            confirmBtn = new Button(
                "确认",
                new RectangleF(738, 433, 58, 95),
                (int)ViewState.SI_Btn_Confirm,
                confirmStyle,
                true
                );
            confirmBtn.ClickEvent += offsideBtn_ClickEvent;

            rectStrs = new String[3] { "时间设置", "日期:", "时间:" };
            rects = new Rectangle[3];
            rects[0] = new Rectangle(362, 72, 125, 30);
            rects[1] = new Rectangle(129, 197, 60, 30);
            rects[2] = new Rectangle(129, 260, 60, 30);

            dateRect = new Rectangle(312, 197, 255, 40);
            timeRect = new Rectangle(312, 260, 255, 40);

            dateFillRect=new Rectangle(313,198,254,39);
            timeFillRect=new Rectangle(313,261,254,39);

            pointX = new int[14]
            {
               392,402,412,421,441,451,471,481,
               402,412,430,441,461,471
            };
            inputStrs = new String[14] 
            { "0", "0", "0", "0",
              "0", "0", "0", "0", 
              "0", "0", "0", "0",
              "0", "0"
            };
            pointY = new int[2] { 224, 287 };

            btnFlags = new bool[16]
            {
                false,false,false,false,false,false,false,false,
                false,false,false,false,false,false,false,false
            };

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
            for (int i = 0; i < this._btns_Down_TabView.Count; i++)
            {
                if ((nPoint.X >= this._btns_Down_TabView[i].Rect.X)
                    && (nPoint.X <= this._btns_Down_TabView[i].Rect.X + this._btns_Down_TabView[i].Rect.Width)
                    && (nPoint.Y >= this._btns_Down_TabView[i].Rect.Y)
                    && (nPoint.Y <= this._btns_Down_TabView[i].Rect.Y + this._btns_Down_TabView[i].Rect.Height))
                {
                    this._btns_Down_TabView[i].MouseDown(nPoint);
                    break;
                }
            }

            for (int i = 0; i < this.offsideBtns.Count; i++)
            {
                if ((nPoint.X >= this.offsideBtns[i].Rect.X)
                    && (nPoint.X <= this.offsideBtns[i].Rect.X + this.offsideBtns[i].Rect.Width)
                    && (nPoint.Y >= this.offsideBtns[i].Rect.Y)
                    && (nPoint.Y <= this.offsideBtns[i].Rect.Y + this.offsideBtns[i].Rect.Height))
                {
                    this.offsideBtns[i].MouseDown(nPoint);
                    break;
                }
            }

       
            if ((nPoint.X >= confirmBtn.Rect.X)
                 && (nPoint.X <= confirmBtn.Rect.X + confirmBtn.Rect.Width)
                 && (nPoint.Y >= confirmBtn.Rect.Y)
                 && (nPoint.Y <= confirmBtn.Rect.Y + confirmBtn.Rect.Height))
            {
                confirmBtn.MouseDown(nPoint);
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
            for (int i = 0; i < this._btns_Down_TabView.Count; i++)
            {
                if ((nPoint.X >= this._btns_Down_TabView[i].Rect.X)
                    && (nPoint.X <= this._btns_Down_TabView[i].Rect.X + this._btns_Down_TabView[i].Rect.Width)
                    && (nPoint.Y >= this._btns_Down_TabView[i].Rect.Y)
                    && (nPoint.Y <= this._btns_Down_TabView[i].Rect.Y + this._btns_Down_TabView[i].Rect.Height))
                {
                    this._btns_Down_TabView[i].MouseUp(nPoint);
                    break;
                }
            }

            for (int i = 0; i < this.offsideBtns.Count; i++)
            {
                if ((nPoint.X >= this.offsideBtns[i].Rect.X)
                    && (nPoint.X <= this.offsideBtns[i].Rect.X + this.offsideBtns[i].Rect.Width)
                    && (nPoint.Y >= this.offsideBtns[i].Rect.Y)
                    && (nPoint.Y <= this.offsideBtns[i].Rect.Y + this.offsideBtns[i].Rect.Height))
                {
                    this.offsideBtns[i].MouseUp(nPoint);
                    break;
                }
            }

            if ((nPoint.X >= confirmBtn.Rect.X)
             && (nPoint.X <= confirmBtn.Rect.X + confirmBtn.Rect.Width)
             && (nPoint.Y >= confirmBtn.Rect.Y)
             && (nPoint.Y <= confirmBtn.Rect.Y + confirmBtn.Rect.Height))
            {
                confirmBtn.MouseUp(nPoint);
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// offsideBtn_ClickEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void offsideBtn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case (int)ViewState.SI_Btn_Back:
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)Common.CommonStatus.CurrentViewState, 0, 0);
                    break;
                case (int)ViewState.SI_Btn_Confirm:
                     //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)Common.CommonStatus.CurrentViewState, 0, 0);
                    break;
                case (int)ViewState.SI_Btn_ShitUp:
                    if (cursorIndex >= 8)
                    {
                        cursorIndex = 0;
                    }
                    break;
                case (int)ViewState.SI_Btn_ShitDown:
                    if (cursorIndex < 8)
                    {
                        cursorIndex = 8;
                    }
                    break;
                case (int)ViewState.SI_Btn_ShitLeft:
                    if (cursorIndex < 8 && cursorIndex > 0)
                    {
                        cursorIndex--;
                    }
                    if (cursorIndex > 8 && cursorIndex < 14)
                    {
                        cursorIndex--;
                    }
                    break;

                case (int) ViewState.SI_Btn_ShitRight:
                    if (cursorIndex >= 0 && cursorIndex < 7)
                    {
                        cursorIndex++;
                    }
                    if (cursorIndex >= 8 && cursorIndex <= 12)
                    {
                        cursorIndex++;
                    }
                    break;


                default: break;
            }
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (this._btns_Down_TabView.Find(a => a.ID == e.Message) != null)
            {
                this._btns_Down_TabView.Find(a => a.ID == e.Message).IsReplication = false;
            }

            inputStrs[cursorIndex] = _strs_Btn_TabView[e.Message - (int)ViewState.SI_Digit_One];
            if (cursorIndex < 13)
            {
                cursorIndex++;
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
            dcGs.DrawImage(this._resource_Images[2], flagRect);
            this._btns_Down_TabView.ToList().ForEach(a => a.Paint(dcGs));
            this.offsideBtns.ToList().ForEach(a => a.Paint(dcGs));
            confirmBtn.Paint(dcGs);

            for (i = 0; i < 3; i++)
            {
                dcGs.DrawString(rectStrs[i], _chineseFont, Brushs.WhiteBrush, rects[i], FontInfo.SF_CC);
            }

            //for (i = 0; i < 10; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[i]])
            //    {
                    
            //    }
            //}



            if (BoolList[UIObj.InBoolList[10]] )//|| BoolList[UIObj.InBoolList[11]])
            {
                btnFlags[10] = true;
               // append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.MainInterface), 0, 0);
            }
            else if (btnFlags[10])
            {
                btnFlags[10] = false;
                append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)(ViewState.MainInterface), 0, 0);
            }


            if (BoolList[UIObj.InBoolList[12]])
            {
                btnFlags[12] = true;
            }
            else if (btnFlags[12])
            {
                btnFlags[12] = false;
                if (cursorIndex >= 8)
                {
                    cursorIndex = 0;
                }
            }

            if(BoolList[UIObj.InBoolList[13]])
            {
                btnFlags[13] = true;
            }
            else if (btnFlags[13])
            {
                btnFlags[13] = false;
                if (cursorIndex < 8)
                {
                    cursorIndex = 8;
                }
            }

            if (BoolList[UIObj.InBoolList[14]])
            {
                btnFlags[14] = true;
            }
            else if (btnFlags[14])
            {
                btnFlags[14] = false;
                if (cursorIndex < 8 && cursorIndex > 0)
                {
                    cursorIndex--;
                }
                if (cursorIndex > 8 && cursorIndex < 14)
                {
                    cursorIndex--;
                }
            }

            if (BoolList[UIObj.InBoolList[15]])
            {
                btnFlags[15] = true;
            }
            else if (btnFlags[15])
            {
                btnFlags[15] = false;
                if (cursorIndex >= 0 && cursorIndex <= 7)
                {
                    cursorIndex++;
                }
                if (cursorIndex >= 8 && cursorIndex <= 12)
                {
                    cursorIndex++;
                }
            }

            for (i = 0; i < 10;i++ )
            {
                if (BoolList[UIObj.InBoolList[i]])
                {
                    btnFlags[i] = true;
                }
                else if (btnFlags[i])
                {
                    btnFlags[i] = false;
                    inputStrs[cursorIndex] = _strs_Btn_TabView[i];
                    if (cursorIndex < 13)
                    {
                        cursorIndex++;
                    }
                }
                //if (BoolList[UIObj.InBoolList[i]])
                //{
                //    inputStrs[cursorIndex] = _strs_Btn_TabView[i];
                //    if (cursorIndex < 13)
                //    {
                //        cursorIndex++;
                //    }
                   
                //}
            }

            dateString=String.Format("{0}{1}{2}{3}-{4}{5}-{6}{7}",
                inputStrs[0],inputStrs[1],inputStrs[2],inputStrs[3],
                inputStrs[4],inputStrs[5],
                inputStrs[6],inputStrs[7]);

            timeString=String.Format("{0}{1}:{2}{3}:{4}{5}",
                inputStrs[8], inputStrs[9],
                inputStrs[10], inputStrs[11],
                inputStrs[12], inputStrs[13]);


            dcGs.FillRectangle(Brushs.WhiteBrush, dateRect);
            dcGs.FillRectangle(Brushs.WhiteBrush, timeRect);

            if (cursorIndex < 8)
            {
                dcGs.FillRectangle(Brushs.BlueBrush, dateFillRect);
                dcGs.DrawString(dateString, _chineseFont, Brushs.WhiteBrush, dateRect, FontInfo.SF_CC);


                dcGs.DrawLine(_whiteLinePen, pointX[cursorIndex], pointY[0], pointX[cursorIndex] + 6, pointY[0]);

                dcGs.DrawString(timeString, _chineseFont, Brushs.BlackBrush, timeRect, FontInfo.SF_CC);

            }

            if (cursorIndex >= 8)
            {

                dcGs.FillRectangle(Brushs.BlueBrush, timeFillRect);

                dcGs.DrawString(timeString, _chineseFont, Brushs.WhiteBrush, timeRect, FontInfo.SF_CC);

                dcGs.DrawLine(_whiteLinePen, pointX[cursorIndex], pointY[1], pointX[cursorIndex] + 6, pointY[1]);

                dcGs.DrawString(dateString, _chineseFont, Brushs.BlackBrush, dateRect, FontInfo.SF_CC);
            }

          

            //dcGs.DrawString(dateString, _chineseFont, Brushs.BlackBrush, dateRect, FontInfo.SF_CC);
            //dcGs.DrawString(timeString, _chineseFont, Brushs.BlackBrush, timeRect, FontInfo.SF_CC);

            base.paint(dcGs);
        }
        #endregion
    }
}
