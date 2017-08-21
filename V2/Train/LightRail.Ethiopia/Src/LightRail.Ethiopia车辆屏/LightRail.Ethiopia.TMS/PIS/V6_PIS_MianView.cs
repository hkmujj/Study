#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.1-切换视图按钮
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace LightRail.Ethiopia.TMS.PIS
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V6_PIS_MianView : baseClass
    {
        public enum UpDown
        {
            UP = 0,
            DOWN = 1
        }

        public enum TcmsPis
        {
            AUTO = 0,
            MANUAL = 1
        }

        public enum EW_SN
        {
            EW = 0,
            SN = 1
        }

        #region 私有变量
        private ViewState _currentViewState;                      //当前视图
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        public static UpDown _currentUpDown = UpDown.UP;//当前上下行
        public static TcmsPis CurrentTcmsPis = TcmsPis.AUTO;
        private EW_SN _currentEWSN = EW_SN.EW;
        public static List<List<String>> _list_Stations = new List<List<String>>();//站点列表
        private Int32 _readInfoId = 0;//读取文本标志
        public static Int32 S_T_State = 0;
        public static Int32 StartingStation = 0;
        public static Int32 TerminalStation = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共试图-视图切换按钮";
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            String[] _strs_Btn_TabView = new String[5] { "up/down", "Starting station", "Terminal station", "Emergency Broadcast", "TCMS OR PIS" };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    _strs_Btn_TabView[i],
                    new RectangleF(100, 181 + 10 + 68 * i, 232, 45),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    }
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(550, 114 - 30 + 19 + 45 * i, 45, 38),
                    i + 5,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[2 + 2 * i],
                        DownImage = _resource_Image[3 + 2 * i]
                    }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            LoadLineInfo();
            return true;
        }

        private Boolean findStringByKey(String str, string startKey, String endKey, ref string temStr)
        {
            if (!str.Contains(startKey) || !str.Contains(endKey)) return false;

            Int32 startIndex = 0;
            Int32 endIndex = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (startKey == str[i].ToString())
                {
                    startIndex = i;
                }

                if (endKey == str[i].ToString())
                {
                    endIndex = i;
                }
            }

            if (startKey == endKey) return false;

            temStr = str.Substring(startIndex, endIndex - startIndex + 1);
            return true;
        }

        private void LoadLineInfo()
        {
                        var file = Path.Combine(AppConfig.AppPaths.ConfigDirectory, "线路信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split('\t');
                if (split.Length == 2)
                {
                    string tmpStations = split[1];
                    string tmpStr = string.Empty;
                    string outTmp = string.Empty;
                    if (findStringByKey(tmpStations, "[", "]", ref tmpStr))
                    {
                        outTmp = tmpStr.Trim();
                    }

                    if (outTmp != string.Empty)
                    {
                        _list_Stations.Add((outTmp.Split('-')).ToList());
                    }
                }
            }
        }

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

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)_btns[e.Message].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)_btns[e.Message].Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            switch (e.Message)
            {
                case 0:
                    _currentUpDown = _currentUpDown == UpDown.UP ? UpDown.DOWN : UpDown.UP;
                    break;
                case 1:
                    S_T_State = 0;
                    append_postCmd(CmdType.ChangePage, 601, 0, 0);
                    break;
                case 2:
                    S_T_State = 1;
                    append_postCmd(CmdType.ChangePage, 602, 0, 0);
                    break;
                case 3:
                    break;
                case 4:
                    int flag = (int) CurrentTcmsPis;
                    flag = (flag + 1)%2;
                    CurrentTcmsPis = (TcmsPis) flag;
                    break;
                case 5:
                    _currentEWSN = EW_SN.EW;
                    break;
                case 6:
                    _currentEWSN = EW_SN.SN;
                    break;
            }
        }

        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));
            paint_LineSelect(dcGs);
            paint_UpDown(dcGs);
            paint_Station(dcGs);
            paint_TCMSOrPIS(dcGs);
            paint_EWSN(dcGs);

            base.paint(dcGs);
        }
        #endregion

        /// <summary>
        /// 绘制线路选择
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_LineSelect(Graphics dcGs)
        {
            dcGs.DrawString(
                "Line Select",
                new Font("Verdana", 9, FontStyle.Bold),
                new SolidBrush(Color.Yellow),
                new RectangleF(100, 114, 232, 45),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        /// <summary>
        /// 绘制上下行
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_UpDown(Graphics dcGs)
        {
            dcGs.DrawString(
                _currentUpDown.ToString(),
                new Font("Verdana", 9, FontStyle.Bold),
                new SolidBrush(Color.Yellow),
                new RectangleF(382, 181 + 10 + 68 * 0, 232, 45),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        /// <summary>
        /// 绘制站台（起始站和终点站）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Station(Graphics dcGs)
        {
            dcGs.DrawString(
                _list_Stations[(int)_currentUpDown][StartingStation],
                new Font("Verdana", 9, FontStyle.Bold),
                new SolidBrush(Color.Yellow),
                new RectangleF(382, 181 + 10 + 68 * 1, 232, 45),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                _list_Stations[(int)_currentUpDown][TerminalStation],
                new Font("Verdana", 9, FontStyle.Bold),
                new SolidBrush(Color.Yellow),
                new RectangleF(382, 181 + 10 + 68 * 2, 232, 45),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        /// <summary>
        /// 绘制TCMS PIS
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TCMSOrPIS(Graphics dcGs)
        {
            dcGs.DrawString(
                CurrentTcmsPis.ToString(),
                new Font("Verdana", 9, FontStyle.Bold),
                new SolidBrush(Color.Yellow),
                new RectangleF(382, 181 + 10 + 68 * 4, 232, 45),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        private void paint_EWSN(Graphics dcGs)
        {
            dcGs.DrawRectangle(
                new Pen(new SolidBrush(Color.Yellow), 1),
                new Rectangle(382 + 93, 114, 45, 45)
                );
            dcGs.DrawString(
                _currentEWSN.ToString(),
                new Font("Verdana", 10, FontStyle.Bold),
                new SolidBrush(Color.Yellow),
                new RectangleF(382 + 93, 114, 45, 45),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }
    }
}
