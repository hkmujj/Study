#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图501-PIS-跳站-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.PIS
{
    /// <summary>
    /// 功能描述：视图501-PIS-跳站-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V501_PIS_JumpStation_C0 : baseClass
    {
        #region 枚举
        /// <summary>
        /// 自复按钮
        /// </summary>
        public enum ButtonFlag
        {
            /// <summary>
            /// 确认按钮
            /// </summary>
            OK = 0,
            /// <summary>
            /// 挑站复位
            /// </summary>
            JumpStation = 1,
            /// <summary>
            /// 返回
            /// </summary>
            Back = 2,
            /// <summary>
            /// 向上选择
            /// </summary>
            Up = 3,
            /// <summary>
            /// 向下选择
            /// </summary>
            Down = 4
        }

        /// <summary>
        /// 站台设置
        /// </summary>
        public enum StationSet
        {
            /// <summary>
            /// 第一站
            /// </summary>
            Start = 0,
            /// <summary>
            /// 最后站
            /// </summary>
            Destination = 1
        }
        #endregion

        #region 稀有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源

        private List<Button> _btns_StationSetting = new List<Button>();//站点设置按钮列表
        private StationSet _currentStationSetting = StationSet.Start;//当前设置站点

        private List<Button> _btns = new List<Button>();//功能按钮
        private ListBox<Station> _listBox;

        private List<TextBox> _textBoxs = new List<TextBox>();//文本框列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行（站点设置）试图-站点设置";
        }


        /// <summary>
        /// 初始化函数：导入图片、创建控件
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

            string[] strs_2 = new string[] { "第一站", "最后站" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                     strs_2[i],
                     new RectangleF(627, 278 + 67 * i, 138, 62),
                     i,
                     new ButtonStyle()
                     {
                         FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                         Background = _resource_Image[1],
                         DownImage = _resource_Image[2],
                         DisableImage = _resource_Image[3]
                     },
                     false
                     );
                btn.ClickEvent += btn_StationSetting_ClickEvent;
                _btns_StationSetting.Add(btn);
            }
            _btns_StationSetting[0].IsReplication = false;

            //确定按钮
            Button btn_OK = new Button(
                     "确认",
                     new RectangleF(428, 416, 177, 60),
                     0,
                     new ButtonStyle()
                     {
                         FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                         Background = _resource_Image[1],
                         DownImage = _resource_Image[2],
                         DisableImage = _resource_Image[3]
                     }
                     );
            btn_OK.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_OK);

            //功能键
            string[] strs_3 = new string[] { "跳站复位", "返回" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                     strs_3[i],
                     new RectangleF(627, 277 + 67 * 2 + 64 * i, 138, 60),
                     i + 1,
                     new ButtonStyle()
                     {
                         FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                         Background = _resource_Image[1],
                         DownImage = _resource_Image[2],
                         DisableImage = _resource_Image[3]
                     }
                     );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            //上下选择站台按钮
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                     "",
                     new RectangleF(72, 389 + 67 * i, 64, 64),
                     i + 3,
                     new ButtonStyle()
                     {
                         FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                         Background = _resource_Image[4 + 0 + 3 * i],
                         DownImage = _resource_Image[4 + 1 + 3 * i],
                         DisableImage = _resource_Image[4 + 2 + 3 * i]
                     }
                     );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            //站台列表
            _listBox = new ListBox<Station>(new RectangleF(141, 120, 265, 400), V5_PIS_C0_MainView.CurrentListStation,
                new ListBoxHeader()
                {
                    Text = "编码",
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("黑体", 12),
                    DataFont = new Font("宋体", 13, FontStyle.Bold),
                    BackgroundBrush = Brushes.Transparent,
                    Height = 26,
                    Width = 40,
                    TProperty = "",
                    SF_Data = FontInfo.SF_CC,
                    SF_Header = FontInfo.SF_CC,
                    IsCount = true
                },
                new ListBoxHeader()
                {
                    Text = "车站名称",
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("黑体", 12),
                    DataFont = new Font("宋体", 13, FontStyle.Bold),
                    BackgroundBrush = Brushes.Transparent,
                    Height = 26,
                    Width = 223,
                    TProperty = "Name",
                    SF_Data = FontInfo.SF_CC,
                    SF_Header = FontInfo.SF_CC
                }
                ) { HideHeader = false, SelectedIndex = 0 };
            _listBox.RowCount = 15;
            _listBox.BackgroundBrush = Brushes.White;
            _listBox.GridBrush = Brushes.Black;

            //文本框（显示选择的站台）
            for (int i = 0; i < 2; i++)
            {
                TextBox textBox = new TextBox(
                    new RectangleF(428, 280 + 66 * i, 177, 60),
                    new TextBoxStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 15, FontStyle.Bold), StringFormat = FontInfo.SF_CC, TextBrush = (SolidBrush)Brushes.Black }
                    },
                    i);
                _textBoxs.Add(textBox);
            }

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns_StationSetting.ForEach(a => a.MouseDown(nPoint));
            _btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns_StationSetting.ForEach(a => a.MouseUp(nPoint));
            _btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 站台选择菜单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_StationSetting_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if ((int)_currentStationSetting == e.Message)
            {
                _btns_StationSetting.Find(a => a.ID == e.Message).IsReplication = false;
                return;
            }

            _btns_StationSetting.Find(a => a.ID == e.Message).IsReplication = false;
            _btns_StationSetting.Find(a => a.ID == (int)_currentStationSetting).IsReplication = true;

            switch ((StationSet)e.Message)
            {
                case StationSet.Start://起始站
                    _currentStationSetting = StationSet.Start;
                    break;
                case StationSet.Destination://终点站
                    _currentStationSetting = StationSet.Destination;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 自复按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch ((ButtonFlag)e.Message)
            {
                case ButtonFlag.Up://向上选择
                    _listBox.LastItem();
                    break;
                case ButtonFlag.Down://向下选择
                    _listBox.NextItem();
                    break;
                case ButtonFlag.OK://确认按钮
                    //向逻辑发生站台信息
                    break;
                case ButtonFlag.Back://返回
                    append_postCmd(CmdType.ChangePage, (int)ViewState.PIS, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            paint_Frame(g);

            _btns[(int)ButtonFlag.Up].Enable = !_listBox.IsFirsItem;
            _btns[(int)ButtonFlag.Down].Enable = !_listBox.IsFinalItem;

            _btns_StationSetting.ForEach(a => a.Paint(g));
            _btns.ForEach(a => a.Paint(g));

            _listBox.Paint(g);
            _listBox.Paint_PageInfo(g, new RectangleF(418, 495, 32, 23));

            ////根据选择的站台选择功能键获取到目前有效的TextBox，直接赋值其text属性
            TextBox tb = _textBoxs.Find(a => a.ID == _btns_StationSetting.FindIndex(b => b.IsReplication == false));
            if (tb != null)
                tb.Text = _listBox.SelectedItem.Name;
            _textBoxs.ForEach(a => a.Paint(g));

            base.paint(g);
        }

        /// <summary>
        /// 绘制线框
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));
            g.DrawImage(_resource_Image[0], new Point(0, 108));
        }
        #endregion
    }
}
