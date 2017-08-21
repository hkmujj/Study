#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.1-右侧菜单按钮
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
    /// 功能描述：视图1-运行-No.1-右侧菜单按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this.resource_Images.Add(Image.FromStream(fs));
                }
            });

            //上四个功能按钮（报站按钮，广播模式）
            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button(
                    "",
                    new Rectangle(715, 205 + i * 50, 80, 40),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = resource_Images[14 + i * 3], DownImage = resource_Images[15 + i * 3], DisableImage = resource_Images[16 + i * 3] }
                    );
                btn.DownEvent += btn_DownEvent;
                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
            }

            //下两个按钮（烟火信息、火灾模式）
            String[] strs = new String[] { "烟火信息", "火灾模式" };
            for (int i = 4; i < 6; i++)
            {
                Button btn = new Button(
                    strs[i - 4],
                    new Rectangle(715, 205 + i * 50, 80, 40),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = resource_Images[0], DownImage = resource_Images[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
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
            this._btns.ForEach(a => a.MouseDown(nPoint));
            return true;
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseUp(nPoint));
            return true;
        }

        void btn_DownEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    if (VC_C2_GetValue.StartStationID < VC_C2_GetValue.EndStationID)
                    {
                        if (VC_C2_GetValue.CurrentStationID > 0) VC_C2_GetValue.CurrentStationID--;
                    }
                    else
                    {
                        if (VC_C2_GetValue.CurrentStationID < VC_C2_GetValue.EndStationID)
                            VC_C2_GetValue.CurrentStationID++;
                    }
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    break;
                case 1:
                    if (VC_C2_GetValue.StartStationID < VC_C2_GetValue.EndStationID)
                    {
                        if (VC_C2_GetValue.CurrentStationID < VC_C2_GetValue.EndStationID) VC_C2_GetValue.CurrentStationID++;
                    }
                    else
                    {
                        if (VC_C2_GetValue.CurrentStationID >0)
                            VC_C2_GetValue.CurrentStationID--;
                    }
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                    break;
                case 2:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 1, 0);
                    break;
                case 3:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3], 1, 0);
                    break;
                case 4://烟火信息
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;
                case 5://火灾模式
                    break;
                default:
                    break;
            }
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
                case 0:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                    break;
                case 2:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 0, 0);
                    break;
                case 3:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3], 0, 0);
                    break;
                case 4://烟火信息
                    append_postCmd(CmdType.ChangePage, 102, 0, 0);
                    break;
                case 5://火灾模式
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
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            this._btns.ForEach(a => a.Paint(dcGs));
            if (VC_C2_GetValue.IsHandleBroadMode)
            {
                _btns[0].Enable = true;
                _btns[1].Enable = true;
                _btns[2].Enable = true;
                _btns[3].Enable = true;
            }
            else
            {
                _btns[0].Enable = false;
                _btns[1].Enable = false;
                _btns[2].Enable = false;
                _btns[3].Enable = false;
            }

            base.paint(dcGs);
        }
        #endregion
    }
}
