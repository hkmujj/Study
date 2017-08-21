#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-26
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图704-检修-轮径设置-No.0-主界面
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
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.检修
{
    /// <summary>
    /// 功能描述：视图704-检修-轮径设置-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V704_Check_WheelRadius_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片列表
        private List<Button> _btns = new List<Button>();//按钮列表
        private List<Button> _btns_Train = new List<Button>();//车厢按钮列表
        private string _newWheelRadius = string.Empty;//新轮径
        private string _oldWheelRadius = "802";//旧轮径
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-轮径设置";
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

            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button(
                    ((i + 1) % 10).ToString(),
                    new RectangleF(380 + (i % 3 * 128), 165 + (i / 3 * 59), 126, 57),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 15, FontStyle.Bold), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            Button btn_DEL = new Button(
                "DEL",
                new RectangleF(380 + (10 % 3 * 128), 165 + (10 / 3 * 59), 126 * 2 + 2, 57),
                10,
                new ButtonStyle()
                {
                    FontStyle = new FontStyle_ES() { Font = new Font("宋体", 15, FontStyle.Bold), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                    Background = _resource_Image[2],
                    DownImage = _resource_Image[3]
                }
                );
            btn_DEL.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_DEL);

            string[] str_ = new string[] { "确定", "取消" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    str_[i],
                    new RectangleF(380 + (i * 2 * 128), 420, 126, 57),
                    11 + i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 15, FontStyle.Bold), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            //String[] str_Train = new String[] { "Tc1", "Mp1", "M1", "M2", "Mp2", "Tc2" };
            //for (int i = 0; i < 6; i++)
            //{
            //    Button btn_Train = new Button(
            //        str_Train[i],
            //        new RectangleF(97 + (i % 3 * 100), 187 + (i / 3 * 48), 98, 44),
            //        i,
            //        new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] },
            //        false
            //        );
            //    if (i == 0) btn_Train.IsReplication = false;
            //    btn_Train.ClickEvent += btn_Train_ClickEvent;
            //    this._btns_Train.Add(btn_Train);
            //}

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));
            //this._btns_Train.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));
            //this._btns_Train.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 数字模块按钮响应点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 10://DEL按钮
                    if (_newWheelRadius.Length == 0)
                        break;
                    _newWheelRadius = _newWheelRadius.Substring(0, _newWheelRadius.Length - 1);
                    break;
                case 11://确定按钮
                    break;
                case 12://取消按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.检修, 0, 0);
                    break;
                default://0-9数字按钮
                    if (_newWheelRadius.Length == 3)
                        break;
                    _newWheelRadius += ((e.Message + 1) % 10).ToString();
                    break;
            }
        }

        /// <summary>
        /// 车辆选择按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Train_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _btns_Train.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
            _newWheelRadius = string.Empty;
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            paint_Frame(g);
            _btns.ForEach(a => a.Paint(g));
            //this._btns_Train.ForEach(a => a.Paint(g));
            paint_SetValue(g);

            base.paint(g);
        }

        /// <summary>
        /// 绘制界面上的线框与标题等
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            //上线框
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));
            g.DrawImage(_resource_Image[4], new Point(0, 108));//背景
        }

        /// <summary>
        /// 绘制轮径输入值
        /// </summary>
        /// <param name="g"></param>
        private void paint_SetValue(Graphics g)
        {
            //String[] str = new String[] { "原轮径：", "新轮径：" };
            //String[] str_ = new String[] { this._oldWheelRadius, this._newWheelRadius };
            //for (int i = 0; i < 2; i++)
            //{
            //    g.DrawString(str[i], new Font("宋体", 10.5f), Brushs.WhiteBrush, new RectangleF(128 + 128 * i, 378, 100, 36), FontInfo.SF_LC);
            //    g.FillRectangle(Brushs.WhiteBrush, new Rectangle(174 + 126 * i, 377, 56, 36));
            //    g.DrawString(str_[i], new Font("宋体", 11f), Brushs.BlackBrush, new RectangleF(174 + 126 * i, 377, 56, 36), FontInfo.SF_CC);
            //}

        }
        #endregion
    }
}
