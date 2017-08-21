#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-26
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图7-检修-No.0-主界面
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
    /// 功能描述：视图7-检修-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V7_Check_C0_MainView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-主界面";
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

            string[] str_Set = new string[] { "时间", "密码", "轮径", "加速度", "测试" };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    str_Set[i],
                    new RectangleF(2 + 112 * i, 160, 111, 62),
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

            string[] str_Data = new string[] { "数据记录" };
            for (int i = 0; i < 1; i++)
            {
                Button btn = new Button(
                    str_Data[i],
                    new RectangleF(2 + 112 * i, 160 + 116, 111, 62),
                    i + 5,
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

            string[] str_Select = new string[] { "端口数据", "版本", "I/O", "参数明细" };
            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button(
                    str_Select[i],
                    new RectangleF(2 + 112 * i, 160 + 116 * 2, 111, 62),
                    i + 6,
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
        /// 按钮点击事件响应函数：切换到相应视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message >= 7)//后续视图待开发
                return;

            append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, e.Message + 702, 0, 0);
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            paint_Frame(g);
            _btns.ForEach(a => a.Paint(g));

            base.paint(g);
        }

        /// <summary>
        /// 绘制线框与标题
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            //上线框
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));

            string[] str = new string[] { "设定:", "记录:", "查询:" };
            for (int i = 0; i < 3; i++)
            {
                g.DrawString(str[i], new Font("宋体", 20, FontStyle.Bold), Brushs.BlackBrush, new RectangleF(10, 108 + 116 * i + 10, 500, 48), FontInfo.SF_LC);
                g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 156 + 116 * i), new Point(800, 156 + 116 * i));
            }
        }
        #endregion
    }
}
