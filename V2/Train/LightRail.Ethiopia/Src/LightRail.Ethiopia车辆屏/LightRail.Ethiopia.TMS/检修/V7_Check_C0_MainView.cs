
#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-10
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图7-检修-No.0-主界面
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
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace LightRail.Ethiopia.TMS.检修
{
    /// <summary>
    /// 功能描述：视图7-检修-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-10
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

            String[] str_Set = new String[] { "时间", "密码", "轮径", "车号", "加速度测试", "测试" };
            for (int i = 0; i < 6; i++)
            {
                Button btn = new Button(
                    str_Set[i],
                    new RectangleF(23 + 96 * i, 169, 89, 50),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            String[] str_Data = new String[] { "数据记录", "USB", "维护指南" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    str_Data[i],
                    new RectangleF(23 + 96 * i, 169 + 135, 89, 50),
                    i + 6,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            String[] str_Select = new String[] { "端口数据", "版本", "I/O" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    str_Select[i],
                    new RectangleF(23 + 96 * i, 169 + 135 + 135, 89, 50),
                    i + 9,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
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
        public override void paint(Graphics dcGs)
        {
            paint_Frame(dcGs);
            _btns.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制线框与标题
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            String[] str = new String[] { "设 定:", "记录与下载:", "查 询:" };
            for (int i = 0; i < 3; i++)
            {
                dcGs.DrawString(str[i], new Font("宋体", 15), Brushs.WhiteBrush, new RectangleF(24, 122 + 135 * i, 500, 35), FontInfo.SF_LC);
                dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new Point(20, 154 + 135 * i), new Point(784, 154 + 135 * i));
            }
        }
        #endregion
    }
}
