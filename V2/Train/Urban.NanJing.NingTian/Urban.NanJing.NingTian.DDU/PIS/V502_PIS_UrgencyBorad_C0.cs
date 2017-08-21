#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图502-PIS-紧急广播-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.PIS
{
    /// <summary>
    /// 功能描述：视图502-PIS-紧急广播-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V502_PIS_UrgencyBorad_C0 : baseClass
    {
        #region 结构体

        /// <summary>
        /// 功能描述：紧急广播info
        /// 创建人：唐林
        /// 创建时间：2014-07-25
        /// </summary>
        public class UrgencyBorad
        {
            /// <summary>
            /// 读取或设置紧急广播编号属性
            /// </summary>
            public int ID { get; set; }

            /// <summary>
            /// 读取或设置广播描述属性
            /// </summary>
            public string Display { get; set; }
        }

        #endregion

        #region 枚举

        /// <summary>
        /// 自复按钮标志
        /// </summary>
        public enum ButtonFlag
        {
            /// <summary>
            /// 单次播放
            /// </summary>
            Once = 0,

            /// <summary>
            /// 循环播放
            /// </summary>
            Loop = 1,

            /// <summary>
            /// 停止
            /// </summary>
            Stop = 2,

            /// <summary>
            /// 上一页
            /// </summary>
            Last = 3,

            /// <summary>
            /// 下一页
            /// </summary>
            Next = 4,

            /// <summary>
            /// 返回
            /// </summary>
            Back = 5
        }

        #endregion

        #region 私有变量

        private List<Image> _resource_Image = new List<Image>(); //图片资源
        private List<Button> _btns = new List<Button>(); //按钮列表
        private List<Button> _btns_Borad = new List<Button>(); //紧急广播按钮列表
        private List<UrgencyBorad> _lists_Borad = new List<UrgencyBorad>();
        private int _readInfoId = 0;
        private bool isFirstSet = true;

        #endregion

        #region 属性

        /// <summary>
        /// 设置当前页面属性（用于翻页时按钮的创建）
        /// </summary>
        public int CurrentPage
        {
            set
            {
                if (_currentPage == value)
                    return;

                _currentPage = value;
                _btns_Borad.Clear();
                for (int i = 0;
                    i <
                    ((_lists_Borad.Count - _currentPage*9) > 9
                        ? 9
                        : (_lists_Borad.Count - _currentPage*9));
                    i++)
                {
                    Button btn = new Button(
                        _lists_Borad[_currentPage*9 + i].Display,
                        new RectangleF(20, 148 + 37*i, 774, 37),
                        _lists_Borad[_currentPage*9 + i].ID,
                        new ButtonStyle()
                        {
                            FontStyle =
                                new FontStyle_ES()
                                {
                                    Font = new Font("宋体", 10.5F),
                                    TextBrush = Brushs.BlackBrush,
                                    StringFormat = FontInfo.SF_LC
                                },
                            Background = _resource_Image[3],
                            DownImage = _resource_Image[4]
                        }
                        );
                    btn.ClickEvent += btn_Borad_ClickEvent;
                    _btns_Borad.Add(btn);
                }
            }
        }

        private int _currentPage = -1;

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
            LoadBroadInfo();

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            string[] _strs = new string[6] {"单次播放", "循环播放", "停止", "上一页", "下一页", "返回"};
            for (int i = 0; i < 6; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new RectangleF(18 + i*129.83f, 487, 128, 45),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("宋体", 13),
                            TextBrush = (SolidBrush) Brushes.Black,
                            StringFormat = FontInfo.SF_CC
                        },
                        Background = _resource_Image[1],
                        DownImage = _resource_Image[2]
                    }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            return true;
        }

        private void LoadBroadInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "紧急广播信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split('\t');
                for (int i = 0; i < split.Length; i++)
                {
                    _lists_Borad.Add(new UrgencyBorad() {ID = i, Display = " " + split[i]});
                }
            }
        }

        #endregion

        #region 鼠标事件

        public override bool mouseDown(Point nPoint)
        {
            _btns.ToList().ForEach(a => a.MouseDown(nPoint));
            _btns_Borad.ToList().ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ToList().ForEach(a => a.MouseUp(nPoint));
            _btns_Borad.ToList().ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 广播按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Borad_ClickEvent(object sender, ClickEventArgs<int> e)
        {
        }

        /// <summary>
        /// 下排功能按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch ((ButtonFlag) e.Message)
            {
                case ButtonFlag.Once: //单次播放
                    break;
                case ButtonFlag.Loop: //循环播放
                    break;
                case ButtonFlag.Stop: //停止
                    break;
                case ButtonFlag.Last: //上一页
                    if (_currentPage > 0)
                        CurrentPage = _currentPage - 1;
                    break;
                case ButtonFlag.Next: //下一页
                    if (_currentPage < _lists_Borad.Count/9)
                        CurrentPage = _currentPage + 1;
                    break;
                case ButtonFlag.Back: //返回
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int) ViewState.PIS, 0, 0);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 界面绘制

        public override void paint(Graphics g)
        {
            if (isFirstSet)
            {
                CurrentPage = 0;
                isFirstSet = false;
            }

            //上线框
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));
            g.DrawImage(_resource_Image[0], new Point(0, 108)); //背景

            _btns.ForEach(a => a.Paint(g));
            _btns_Borad.ForEach(a => a.Paint(g));

            base.paint(g);
        }

        #endregion
    }
}
