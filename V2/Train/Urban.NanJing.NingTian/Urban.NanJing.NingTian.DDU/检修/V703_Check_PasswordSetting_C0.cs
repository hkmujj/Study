#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-26
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图703-检修-密码设置-No.0-主界面
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
    /// 功能描述：视图703-检修-密码设置-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V703_Check_PasswordSetting_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片列表
        private List<Button> _btns = new List<Button>();//按钮列表
        private List<TextBox> _textBoxs = new List<TextBox>();//文本输入框列表
        private string _newPassword = string.Empty;//新密码
        private string _surePassword = string.Empty;//确认密码
        private string _password = string.Empty;//密码
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-密码设置";
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

            for (int i = 0; i < 2; i++)
            {
                TextBox textBox = new TextBox(
                    new RectangleF(194, 254 + 55 * i, 105, 40),
                    new TextBoxStyle(){ FontStyle=FontStyles.FS__Song_105_LC_W, Background=_resource_Image[2], BackgroundFocus=_resource_Image[2]},
                    i
                    );
                if (i == 0) textBox.IsFocus = true;
                _textBoxs.Add(textBox);
            }

            return true;
        }
        #endregion

        #region 鼠标消息
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
        /// 按钮响应鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 10://DEL按钮
                    if (_password.Length == 0)
                        break;
                    _password = _password.Substring(0, _password.Length - 1);
                    break;
                case 11://确定按钮
                    break;
                case 12://取消按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.检修, 0, 0);
                    break;
                default://0-9数字按钮
                    _password += ((e.Message + 1) % 10).ToString();
                    if (_password.Length == 6)
                    {
                        TextBox textBox = _textBoxs.Find(a => a.IsFocus);
                        textBox.Text = "******";
                        textBox.IsFocus = false;
                        if (textBox.ID == 0)
                        {
                            _textBoxs[1].IsFocus = true;
                            _newPassword = _password;
                        }
                        else _surePassword = _password;
                        _password = string.Empty;
                    }
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            _btns.ForEach(a => a.Paint(g));
            //this._textBoxs.ForEach(a => a.Paint(g));
            paint_Frame(g);
            paint_Password(g);

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
        /// 绘制密码（******）
        /// </summary>
        /// <param name="g"></param>
        public void paint_Password(Graphics g)
        {
            string password = string.Empty;
            for (int i = 0; i < _password.Length; i++)
            {
                password += "*";
            }
            TextBox textBox = _textBoxs.Find(a => a.IsFocus);
            if (textBox != null) _textBoxs.Find(a => a.IsFocus).Text = password;
        }
        #endregion
    }
}
