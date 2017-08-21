#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-10
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图706-检修-加速度测试-No.0-主界面
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
    /// 功能描述：视图706-检修-加速度测试-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-10
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V706_Check_AccelerateTest_C0 : baseClass
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
                    new RectangleF(403 + (i % 3 * 102), 187 + (i / 3 * 48), 100, 44),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            Button btn_DEL = new Button(
                "DEL",
                new RectangleF(403 + (10 % 3 * 102), 187 + (10 / 3 * 48), 202, 44),
                10,
                new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                );
            btn_DEL.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_DEL);

            String[] str_ = new String[] { "确定", "取消" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    str_[i],
                    new RectangleF(403 + (i * 204), 393, 100, 44),
                    11 + i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
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
                    //append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.检修, 0, 0);
                    break;
                default://0-9数字按钮
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));
            paint_Frame(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制背景与框架
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[3], new Point(106,155));
            dcGs.DrawImage(_resource_Image[4], new Point(106,241));

            dcGs.DrawString("起始速度：", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106,169,93,27), FontInfo.SF_RC);
            dcGs.DrawString("目标速度：", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 169+31, 93, 27), FontInfo.SF_RC);

            dcGs.DrawString("平均加速度", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 251, 86, 27), FontInfo.SF_RC);
            dcGs.DrawString("制动距离", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 251 + 31, 86, 27), FontInfo.SF_RC);
            dcGs.DrawString("平均减速度", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 251 + 31 + 31, 86, 27), FontInfo.SF_RC);
            dcGs.DrawString("减速度\n测试初始值", new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(106, 251 + 31 + 31 + 31, 86, 33), FontInfo.SF_RC);
        }
        #endregion
    }
}
