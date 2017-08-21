#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图101-运行-No.0-故障
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图101-运行-No.0-故障
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V101_Run_C0_Fault : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btn_OK_Cancel = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "主界面框架";
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns>是否初始化成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            String[] str = new String[] { "确定", "返回" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    str[i],  
                    new RectangleF(716, 391 + 65 * i, 73, 47),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_105_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]}
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btn_OK_Cancel.Add(btn);
            }

            return true;
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// 组件鼠标按下事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            this._btn_OK_Cancel.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            this._btn_OK_Cancel.ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 按钮点击事件响应函数：处理确定按钮和返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://确定按钮
                    VC_C4_GetFault.CurrentFaults.Remove(VC_C4_GetFault.CurrentFault);
                    if (VC_C4_GetFault.CurrentFaults.Count == 0)
                    {
                        append_postCmd(CmdType.ChangePage, 1, 0, 0);

                        break;
                    }
                    break;
                case 1://返回按钮
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            this.paint_Frame(dcGs);
            this.paint_Fault(dcGs);
            this._btn_OK_Cancel.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制线框
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new Point(0, 96), new Point(800, 96));
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(32, 119, 651, 68));
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(32, 200, 651, 304));
        }

        /// <summary>
        /// 绘制当前故障信息
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Fault(Graphics dcGs)
        {
            dcGs.DrawString(VC_C4_GetFault.CurrentFault.Display, new Font("宋体", 11), Brushs.WhiteBrush, new Rectangle(32, 119, 651, 68), FontInfo.SF_LC);
            dcGs.DrawString(VC_C4_GetFault.CurrentFault.PointOut, new Font("宋体", 11), Brushs.WhiteBrush, new Rectangle(32, 200 + 20, 651, 304), FontInfo.SF_LT);
        }
        #endregion
    }
}
