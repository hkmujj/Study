#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-10
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图708-检修-数据记录-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;
using ES.Facility.Common;
using ES.Facility.Common.Control.Common;
using ES.Facility.Common.Control;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图708-检修-数据记录-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-10
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V708_Check_DataNotes_C0 : baseClass
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
            return "检修试图-数据记录";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            String[] str = new String[] { "数据\n重置", "", "", "", "返回" };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    str[i],
                    new RectangleF(708, 140 + 75 * i, 90, 60),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]}
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
            }

            return true;
        }
        #endregion

        #region 鼠标消息
        public override bool mouseDown(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://数据重置按钮
                    break;
                case 4://返回按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.检修, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            this._btns.ForEach(a => a.Paint(dcGs));
            this.paint_Frame(dcGs);
            this.paint_DataValue(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制界面上的线框与标题等
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            String[] str1 = new String[] { "辅助能耗", "1单元\n压缩机工作时间", "牵引能耗", "2单元\n压缩机工作时间", "再生电量" };
            String[] str2 = new String[] { "Kw.h", "Min", "Kw.h", "Min", "Kw.h" };
            for (int i = 0; i < 5; i++)
            {
                dcGs.DrawString(str1[i], new Font("宋体", 10), Brushs.WhiteBrush, new RectangleF(0 + (i + 1) % 2 * 317, 173 + (i + 1) / 2 * 51, 170, 39), FontInfo.SF_RC);
                dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(170 + (i + 1) % 2 * 317, 173 + (i + 1) / 2 * 51, 111, 39));
                dcGs.DrawString(str2[i], new Font("宋体", 10), Brushs.WhiteBrush, new RectangleF(173 + 111 + (i + 1) % 2 * 317, 173 + (i + 1) / 2 * 51, 170, 39), FontInfo.SF_LB);
            }
        }

        /// <summary>
        /// 绘制数据（后续添加真实数据）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_DataValue(Graphics dcGs)
        {
            for (int i = 0; i < 5; i++)
            {
                dcGs.DrawString("0", new Font("宋体", 10), Brushs.WhiteBrush, new RectangleF(170 + (i + 1) % 2 * 317, 173 + (i + 1) / 2 * 51, 111, 39), FontInfo.SF_CC);
            }
        }
        #endregion
    }
}
