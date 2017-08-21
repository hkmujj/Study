#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.0-下侧按钮
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图1-运行-No.3-下侧按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_Run_C3_DownButton : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private List<Button> _btn_PointOut = new List<Button>();//钱三个红色提示按钮
        private List<Button> _btn_ = new List<Button>();//后三个按钮
        private String[] _str_SpeedLimitingList;
        private Int32 _time = 0;
        private Boolean _isFlace = false;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "运行试图-状态栏";
        }

        /// <summary>
        /// 初始化函数：导入图片、创建按钮
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Images.Add(Image.FromStream(fs));
                }
            });

            _str_SpeedLimitingList = new String[] { 
            "限速\n3km/h",
            "限速\n12km/h",
            "限速\n30km/h",
            "限速\n40km/h",
            "限速\n45km/h",
            "限速\n50km/h",
            "限速\n60km/h",
            "限速\n70km/h",
            "限速\n75km/h",
            "----",
            "--"};
            
            //前三个红色按钮
            String[] _str_PO=new String[]{"方向\n列表", "列车\n逻辑", "限速\n条件"};
            for (int i = 2; i < 3; i++)
            {
                Button btn_PointOut = new Button(
                    _str_PO[i],
                    new Rectangle(306 + 81 * i, 508, 78, 42),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Images[0], DownImage = _resource_Images[2]},
                    true
                    );
                btn_PointOut.ClickEvent += btn_PointOut_ClickEvent;
                this._btn_PointOut.Add(btn_PointOut);
            }

            //后三个按钮
            String[] _str_ = new String[] { "紧急广播", "站点设置", "旁路信息" };
            for (int i = 0; i < 3; i++)
            {
                Button btn_ = new Button(
                    _str_[i],
                    new Rectangle(306 + 81 * (i+3), 508, 78, 42),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Images[1], DownImage = _resource_Images[2]}
                    );
                btn_.ClickEvent += btn__ClickEvent;
                this._btn_.Add(btn_);
            }
            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(System.Drawing.Point nPoint)
        {
            this._btn_PointOut.ForEach(a => a.MouseDown(nPoint));
            this._btn_.ForEach(a => a.MouseDown(nPoint));
            return true;
        }

        public override bool mouseUp(System.Drawing.Point nPoint)
        {
            this._btn_PointOut.ForEach(a => a.MouseUp(nPoint));
            this._btn_.ForEach(a => a.MouseUp(nPoint));
            return true;
        }

        /// <summary>
        /// 后三个按钮点击事件响应函数：切换到相应视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn__ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://紧急广播
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.紧急广播, 0, 0);
                    break;
                case 1://站点设置
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.站点, 0, 0);
                    break;
                case 2://旁路信息
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.提示信息_旁路信息, 0, 0);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 提示按钮点击事件响应函数:切换到相应视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_PointOut_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://方向列表
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.提示信息_方向列表, 0, 0);
                    break;
                case 1://列车逻辑
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.提示信息_列车逻辑, 0, 0);
                    break;
                case 2://限速条件
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.提示信息_限速条件, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            //获取到限速条件
            Boolean isFind = false;
            String text = "限速\n条件";
            //for (int i = 0; i < 12; i++)
            //{
            //    if (VC_C2_GetValue.SpeedLimitingValues[i])
            //    {
            //        isFind = true;
            //        _btn_PointOut[2].Text = this._str_SpeedLimitingList[i];
            //        break;
            //    }
            //}

            //if (isFind)
            //{
            //    _time = (_time + 1) % 10;
            //    if (_time == 9)
            //    {
            //        _btn_PointOut[2].Style.Background = null;
            //    }
            //}
            //else
            //{
            //    _time = 0;
            //    _btn_PointOut[2].Style.Background = _resource_Images[0];
            //}

            //if (VC_C2_GetValue.DirectionValues.ToList().Find(a => a))
            //    ((ButtonStyle)this._btn_PointOut[0].Style).Background = _resource_Images[0];
            //else ((ButtonStyle)this._btn_PointOut[0].Style).Background = _resource_Images[1];

            for (int i = 0; i < 9; i++)
			{
                if (VC_C2_GetValue.SpeedLimitingValues[i])
                {
                    isFind = true;
                    text = _str_SpeedLimitingList[i];
                }
			}
            //if (VC_C2_GetValue.TrainLogicValues.ToList().Find(a => a))
            //{
            //    ((ButtonStyle)this._btn_PointOut[1].Style).Background = _resource_Images[0];
            //}
            //else ((ButtonStyle)this._btn_PointOut[1].Style).Background = _resource_Images[1];

            if (isFind)
            {
                _time = (_time + 1) % 4;
                if (_time < 2)
                {
                    this._btn_PointOut[0].Text = text;
                    ((ButtonStyle)this._btn_PointOut[0].Style).Background = _resource_Images[0];
                }
                else
                {
                    ((ButtonStyle)this._btn_PointOut[0].Style).Background = null;
                }
            }
            else
            {
                this._btn_PointOut[0].Text = text;
                ((ButtonStyle)this._btn_PointOut[0].Style).Background = _resource_Images[1];
            }

            //if (VC_C2_GetValue.SpeedLimitingValues.ToList().Find(a => a))
            //    ((ButtonStyle)this._btn_PointOut[2].Style).Background = _resource_Images[0];
            //else ((ButtonStyle)this._btn_PointOut[2].Style).Background = _resource_Images[1];

            this._btn_PointOut.ForEach(a => a.Paint(dcGs));
            this._btn_.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }
        #endregion
    }
}
