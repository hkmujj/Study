#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-26
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图706-检修-加速度测试-No.0-主界面
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
    /// 功能描述：视图706-检修-加速度测试-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V705_Check_AccelerateTest_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片列表
        private List<Button> _btns = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-加速度测试";
        }


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
                    new RectangleF(404 + (i % 3 * 128), 130 + (i / 3 * 59), 126, 57),
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
                new RectangleF(404 + (10 % 3 * 128), 130 + (10 / 3 * 59), 126 * 2 + 2, 57),
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
                    new RectangleF(404 + (i * 2 * 128), 385, 126, 57),
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

            return true;
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
                    break;
                case 11://确定按钮
                    break;
                case 12://取消按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.检修, 0, 0);
                    break;
                default://0-9数字按钮
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            _btns.ForEach(a => a.Paint(g));
            paint_Frame(g);

            base.paint(g);
        }

        /// <summary>
        /// 绘制背景与框架
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            //上线框
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));
            g.DrawImage(_resource_Image[4], new Point(0, 108));//背景

            //g.DrawString("起始速度：", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106,169,93,27), FontInfo.SF_RC);
            //g.DrawString("目标速度：", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 169+31, 93, 27), FontInfo.SF_RC);

            //g.DrawString("平均加速度", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 251, 86, 27), FontInfo.SF_RC);
            //g.DrawString("制动距离", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 251 + 31, 86, 27), FontInfo.SF_RC);
            //g.DrawString("平均减速度", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 251 + 31 + 31, 86, 27), FontInfo.SF_RC);
            //g.DrawString("减速度\n测试初始值", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 251 + 31 + 31 + 31, 86, 33), FontInfo.SF_RC);
        }
        #endregion
    }
}
