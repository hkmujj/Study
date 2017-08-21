#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.1-右侧菜单按钮(旁路与故障按钮)
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

namespace Urban.NanJing.NingTian.DDU.运行
{
    /// <summary>
    /// 功能描述：视图1-运行-No.1-右侧菜单按钮(旁路与故障按钮)
    /// 创建人：唐林
    /// 创建时间：2014-7-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class V1_Run_C1_RightButton : baseClass
    {
        #region 私有变量
        private List<Button> _btns = new List<Button>();//按钮列表
        private List<Image> resource_Images = new List<Image>();//图片资源
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "主界面右侧按钮";
        }

        /// <summary>
        /// 初始化函数：导入图片、创建组件控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //导入图片（二进制）
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    resource_Images.Add(Image.FromStream(fs));
                }
            });

            //盘路与故障按钮
            string[] strs = new string[] { "旁路", "故障" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new Rectangle(681, 308 + i * 91, 113, 62),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_20_CC_B, Background = resource_Images[0], DownImage = resource_Images[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
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
            _btns.ForEach(a => a.MouseDown(nPoint));
            return true;
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));
            return true;
        }

        /// <summary>
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //按钮弹起事件
            switch (e.Message)
            {
                case 0://盘路
                    append_postCmd(CmdType.ChangePage, 101, 0, 0);
                    break;
                case 1://故障
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制：按钮绘制
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            _btns.ForEach(a => a.Paint(g));

            base.paint(g);
        }
        #endregion
    }
}
