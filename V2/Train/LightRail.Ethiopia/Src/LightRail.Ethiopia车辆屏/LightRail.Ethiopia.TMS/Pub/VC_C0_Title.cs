#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-第一个组件-标题
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;
using LightRail.Ethiopia.TMS.PIS;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace LightRail.Ethiopia.TMS.Pub
{
    /// <summary>
    /// 控件点击事件代理
    /// </summary>
    /// <param name="sender">事件发生对象</param>
    /// <param name="e">传输参数</param>
    public delegate void EventHandle_BooleanEvent(Object sender, ClickEventArgs<Boolean> e);

    /// <summary>
    /// 功能描述：公共组件-第一个组件-标题
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C0_Title : baseClass
    {
        #region 私有变量
        private Int32 readTxtID = 0;                                     //读取文本标志
        private Button _btn_Fault;                                        //故障按钮
        private List<Image> _resource_Image = new List<Image>();               //图片资源
        private SortedDictionary<Int32, String> _stationList = new SortedDictionary<int, string>();//车站列表
        private List<Button> _btns = new List<Button>();
        private ViewState _currentViewState;                      //当前视图
        #endregion

        public static event EventHandle_BooleanEvent EventHandle_ConfirmFault
        {
            add { _eventHandle_ConfirmFault += value; }
            remove { _eventHandle_ConfirmFault -= value; }
        }
        private static EventHandle_BooleanEvent _eventHandle_ConfirmFault;

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-标题栏";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                String b = a;
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {

                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            //帮助按钮
            Button btn_Help = new Button(
                "",
                new RectangleF(666, 0, 67, 72),
                0,
                new ButtonStyle()
                {
                    FontStyle = FontStyles.FS_Song_11_CC_B,
                    Background = _resource_Image[1],
                    DownImage = _resource_Image[1]
                }
                );
            btn_Help.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_Help);

            //故障按钮
            Button btn_Falut = new Button(
                "",
                new RectangleF(736, 0, 64, 72),
                1,
                new ButtonStyle()
                {
                    FontStyle = FontStyles.FS_Song_11_CC_B,
                    Background = _resource_Image[2],
                    DownImage = _resource_Image[2]
                }
                );
            btn_Falut.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_Falut);

            LoadStationInfo();

            return true;
        }

        private void LoadStationInfo()
        {
            string[] tmpFile0 = File.ReadAllLines(
               Path.Combine(AppConfig.AppPaths.ConfigDirectory, "车站信息.txt"),
               System.Text.Encoding.Default
               );
            foreach (string str0 in tmpFile0)
            {
                String[] split = str0.Split(new char[] { ':' });

                if (split.Length != 2)
                    continue;
                _stationList.Add(Convert.ToInt32(split[0]), split[1]);
            }

            ////车站信息.txt
            //var file = Path.Combine(AppConfig.AppPaths.ConfigDirectory, "车站信息.txt");
            //var all = File.ReadAllLines(file, Encoding.Default);
            //foreach (var cStr in all)
            //{
            //    String[] split = cStr.Split(new char[] {'\t'});
            //    int i;
            //    String[] tmp;
            //    tmp = new String[2];
            //    i = 0;
            //    foreach (string s in split)
            //    {
            //        if (s.Trim() != "")
            //        {
            //            if (i < 2)
            //                tmp[i] = s;
            //            i++;
            //        }
            //        if (i == 2)
            //        {
            //            _stationList.Add(int.Parse(tmp[0]), tmp[1]);
            //            return;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 获取当期视图值
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _currentViewState = (ViewState)nParaB;
            }
        }

        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        public static Boolean IsShowHelpView = false;
        /// <summary>
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://帮助按钮
                    switch (_currentViewState)
                    {
                        case ViewState.Main:
                            IsShowHelpView = !IsShowHelpView;
                            break;
                    }
                    break;
                case 1://故障按钮
                    //if (VC_C4_GetFault.CurrentFault == null)
                    //    return;

                    //append_postCmd(CmdType.ChangePage, 101, 0, 0);
                    if(_eventHandle_ConfirmFault!=null)
                        _eventHandle_ConfirmFault(this,new ClickEventArgs<Boolean>(true));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 绘制界面
        /// <summary>
        /// 界面绘制函数
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            paint_Frame(dcGs);
            paint_Up_SystemInfo(dcGs);
            _btns.ForEach(a => a.Paint(dcGs));

            switch (V6_PIS_MianView.CurrentTcmsPis)
            {
                case V6_PIS_MianView.TcmsPis.AUTO:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                    break;
                case V6_PIS_MianView.TcmsPis.MANUAL:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                    break;
            }

            base.paint(dcGs);
        }

        /// <summary>
        /// 北京绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new PointF(0, 0));
        }

        /// <summary>
        /// 绘制：系统信息（主界面上部分）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Up_SystemInfo(Graphics dcGs)
        {
            //NetVoltage-电网电压
            dcGs.DrawString(
                "NetVoltage：" + FloatList[UIObj.InFloatList[0]].ToString("0") + "V",
                new Font("Verdana", 10),
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new Rectangle(0, 0, 212, 35),
                new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                );
            //BatteryVoltage-蓄电池电压
            dcGs.DrawString(
                "BatteryVoltage：" + FloatList[UIObj.InFloatList[1]].ToString("0") + "V",
                new Font("Verdana", 10),
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new Rectangle(0, 37, 212, 35),
                new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                );
            //CurrentStation-当前站
            dcGs.DrawString(
                "CurrentStation：" + getStation((Int32)FloatList[UIObj.InFloatList[2]]),
                new Font("Verdana", 10),
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new Rectangle(214, 0, 324, 35),
                new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                );
            //NextStation-下一站
            dcGs.DrawString(
                "NextStation：" + getStation((Int32)FloatList[UIObj.InFloatList[3]]),
                new Font("Verdana", 10),
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new Rectangle(214, 37, 324, 35),
                new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                );
            //日期
            dcGs.DrawString(
                DateTime.Now.ToString("yyyy-MM-dd"),
                new Font("Verdana", 10),
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new Rectangle(540, 0, 124, 35),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
            //时间
            dcGs.DrawString(
                DateTime.Now.ToLongTimeString(),
                new Font("Verdana", 10),
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new Rectangle(540, 37, 124, 35),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        /// <summary>
        /// 获取站台信息
        /// </summary>
        /// <param name="stationNum"></param>
        /// <returns></returns>
        private String getStation(int stationNum)
        {
            String stationName;
            _stationList.TryGetValue(stationNum, out stationName);

            return stationName;
        }
        #endregion
    }
}
