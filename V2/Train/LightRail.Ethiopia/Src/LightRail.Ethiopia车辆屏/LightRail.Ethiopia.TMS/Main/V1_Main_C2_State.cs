#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.3-状态
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace LightRail.Ethiopia.TMS.Main
{
    /// <summary>
    /// 功能描述：视图1-运行-No.2-状态
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_Main_C2_State : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private Timer _timer = new Timer();
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "运行试图-状态栏";
        }

        /// <summary>
        /// 初始化函数：导入图片、初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //导入图片资源
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Images.Add(Image.FromStream(fs));
                }
            });

            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            return true;
        }

        /// <summary>
        /// 计时器事件响应函数：倒计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, EventArgs e)
        {
            _count = _count == 0 ? 0 : _count - 1;
        }

        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            paint_Frame(dcGs);
            paint_Level(dcGs);
            paint_Vigliant(dcGs);
            paint_Mode(dcGs);
            paint_Safe(dcGs);
            paint_ATPMode(dcGs);
            paint_Rescue(dcGs);
            paint_HouBeiMode(dcGs);

            base.paint(dcGs);
        }

        private Brush _countDownBrush = Brushes.CornflowerBlue;
        /// <summary>
        /// 运行界面-状态栏线框和不变状态的绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[9]])
            {
                dcGs.DrawImage(_resource_Images[17], new Point(0, 77));
                IsStartCountDown60 = false;
                IsStartCountDown60 = false;
                _count = 60;
                _countDownBrush = Brushes.Red;
                return;
            }
            if (_count < 6) _countDownBrush = Brushes.Red;
            else _countDownBrush = Brushes.CornflowerBlue;
            dcGs.DrawImage(_resource_Images[0], new Point(0, 77));
        }

        /// <summary>
        /// Main-级位绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Level(Graphics dcGs)
        {
            String[] strs = new[] { "P4", "P3", "P2", "P1", "NP", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "EB" };
            for (int i = 0; i < 13; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.FillRectangle(new SolidBrush(Color.Yellow), new RectangleF(12, 86 + 30.3F * i, 49, 30.3F));//24
                    dcGs.DrawString(
                        strs[i],
                        new Font("Verdana", 10, FontStyle.Bold),
                        new SolidBrush(Color.Black),
                        new RectangleF(12, 86 + 30.3F * i + 2, 49, 30.3F),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                    continue;
                }
                dcGs.DrawString(
                    strs[i],
                    new Font("Verdana", 10, FontStyle.Bold),
                    new SolidBrush(Color.Yellow),
                    new RectangleF(12, 86 + 30.3F * i + 2, 49, 30.3F),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }
        }

        /// <summary>
        /// 设置是否开始倒计时属性（用于处理开始倒计时命令）
        /// </summary>
        public Boolean IsResetCountDown
        {
            set
            {
                if (_isResetCountDown == value)
                {
                    if (value == true) _count = 60;
                    return;
                }

                if (value == true)
                {
                    _count = 60;
                    _isFlicker = true;
                }
                _isResetCountDown = value;
            }
        }
        private Boolean _isResetCountDown = false;//是否开始倒计时

        /// <summary>
        /// 读取或设置倒计时数属性
        /// </summary>
        public Int32 Count
        {
            get { return _count; }
            set
            {
                if (_count == value) return;

                _count = value;
                //_isFlicker = _count <= 6;
                if (_count == 0)
                {
                    //向逻辑发送Deadman事件
                    //append_postCmd(CmdType.SetBoolValue, 70);
                }
            }
        }
        private Int32 _count = 60;

        /// <summary>
        /// 警惕（反馈警惕信号）
        /// </summary>
        public Boolean IsVigilanted
        {
            set
            {
                if (_isVigilanted == value)
                    return;

                _isVigilanted = value;
                if(_isVigilanted)
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                else append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
            }
        }
        private Boolean _isVigilanted = false;

        private Boolean _isStartCountDown = false;//是否停止倒计时
        private Boolean _isFlicker = true;//是否闪烁

        public Boolean IsStartCountDown60
        {
            set
            {
                if (_isStartCountDown60 == value) return;
                _isStartCountDown60 = value;
                if (value) //开始60秒倒计时
                {
                    Count = 60;
                    _timer.Start();
                }
                else
                {
                    if (!_isStartCountDown15)
                    {
                        Count = 60;
                        _timer.Stop();
                    }
                }
            }
        }
        private Boolean _isStartCountDown60 = false;

        public Boolean IsStartCountDown15
        {
            set
            {
                if (_isStartCountDown15 == value) return;
                _isStartCountDown15 = value;
                if (value) //开始15秒倒计时
                {
                    Count = 15;
                    _timer.Start();
                }
                else
                {
                    if (!_isStartCountDown60)
                    {
                        Count = 60;
                        _timer.Stop();
                    }
                }
            }
        }
        private Boolean _isStartCountDown15 = false;

        /// <summary>
        /// Main-警惕倒计时
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Vigliant(Graphics dcGs)
        {
            IsStartCountDown60 = BoolList[UIObj.InBoolList[1]];
            IsStartCountDown15 = BoolList[UIObj.InBoolList[2]];


            if (Count <= 5)
            {
                _isFlicker = !_isFlicker;
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
            }
            else
            {
                _isFlicker = true;
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
            }

            if (_isFlicker)
                dcGs.DrawString(
                    _count.ToString(),
                    new Font("Verdana", 20, FontStyle.Bold),
                    _countDownBrush,
                    new RectangleF(279, 195, 64, 64),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );

            IsVigilanted = (_count == 0);
        }

        /// <summary>
        /// 绘制洗车那几种模式
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Mode(Graphics dcGs)
        {
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[3]+i])
                {
                    dcGs.DrawImage(
                        _resource_Images[1 + i],
                        new RectangleF(247, 112, 114, 50)
                        );
                }
            }
        }

        /// <summary>
        /// 绘制Safe几种
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Safe(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[4]])
            {
                dcGs.DrawImage(
                        _resource_Images[7],
                        new RectangleF(175, 198, 89, 72)
                        );
            }//无框Safe

            if (BoolList[UIObj.InBoolList[5]])
            {
                dcGs.DrawImage(
                        _resource_Images[8],
                        new RectangleF(72, 160, 89, 72)
                        );
            }//有框Safe

            if (BoolList[UIObj.InBoolList[6]])
            {
                dcGs.DrawImage(
                        _resource_Images[9],
                        new RectangleF(72, 160, 89, 72)
                        );
            }//制动缓解旁路
        }

        /// <summary>
        /// 绘制ATP那几种模式
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_ATPMode(Graphics dcGs)
        {
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[7] + i])
                {
                    dcGs.DrawImage(
                        _resource_Images[10 + i],
                        new RectangleF(83, 262, 77, 71)
                        );
                }
            }
        }

        /// <summary>
        /// 救援模式
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Rescue(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[10]])
            {
                dcGs.DrawImage(
                    _resource_Images[14],
                    new RectangleF(247+18, 112-10, 77, 71)
                    );
            }
        }

        private void paint_HouBeiMode(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[11]])
            {
                dcGs.DrawString(
                    "Back Up Mode",
                    new Font("Verdana", 10),
                    Brushes.Red,
                    new RectangleF(96,84,120,28),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }
        }

        #endregion
    }
}
