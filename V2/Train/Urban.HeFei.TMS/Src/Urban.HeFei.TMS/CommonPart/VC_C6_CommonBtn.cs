#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.6-公共按钮
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.IO;
using System.Collections.Generic;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using System.Drawing;
using System.Linq;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using HF_TMS;
using ES.Facility.Common;

namespace HF_TMS
{
    /// <summary>
    /// 功能描述：公共组件-No6-公共按钮
    /// 创建人：lih
    /// 创建时间：2015-8-7
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class VC_C6_CommonBtn : baseClass
    {
        private List<Image> _resource_Image = new List<Image>();//图片资源

        private Button _bypassBtn;//旁路按钮
        private Button _faultBtn;//故障按钮

        private ViewState _oldViewState=ViewState.MainInterface;
        private ButtonStyle[] _bypassBatnStyles;
        private ButtonStyle[] _faultBtnStyles;

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-公共按钮";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        public override string GetTypeName()
        {
            return "VC_C6_CommonBtn";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
           

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(_recPath + a, FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            _bypassBatnStyles = new ButtonStyle[3];

            _bypassBatnStyles[0] = new ButtonStyle() { FontStyle = FontStyles.FS_Song_16_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[0] };
            _bypassBatnStyles[1] = new ButtonStyle() { FontStyle = FontStyles.FS_Song_16_CC_B, Background = _resource_Image[1], DownImage = _resource_Image[1] };
            _bypassBatnStyles[2] = new ButtonStyle() { FontStyle = FontStyles.FS_Song_16_CC_B, Background = _resource_Image[2], DownImage = _resource_Image[2] };


            _faultBtnStyles = new ButtonStyle[2];



            _faultBtnStyles[0] = new ButtonStyle() { FontStyle = FontStyles.FS_Song_16_CC_B, Background = _resource_Image[3], DownImage = _resource_Image[3] };
            _faultBtnStyles[1] = new ButtonStyle() { FontStyle = FontStyles.FS_Song_16_CC_B, Background = _resource_Image[4], DownImage = _resource_Image[4] };


            _bypassBtn = new Button(
                    "",
                    new RectangleF(702, 278, 87, 42),
                   (int)ViewState.BypassInfo,
                    _bypassBatnStyles[0],
                    false
                    );
            _bypassBtn.ClickEvent += btn_ClickEvent;

            _faultBtn = new Button(
                   "",
                   new RectangleF(702, 462, 87, 42),
                  (int)ViewState.FaultInfo,
                   _faultBtnStyles[0],
                   false
                   );
            _faultBtn.ClickEvent += btn_ClickEvent;

            return true;
        }

        /// <summary>
        /// 设置读取文本标志
        /// </summary>
        /// <param name="nPara"></param>
        /// <returns></returns>
        public override bool canSetStringList(int nPara)
        {
            if (nPara == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取文本信息
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="cStr"></param>
        public override void addStringByLine(int nIndex, string cStr)
        {
            String[] split = cStr.Split(new char[] { '\t' });
        }
        #endregion

        #region 
        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            if ((nPoint.X >= this._bypassBtn.Rect.X)
                   && (nPoint.X <= this._bypassBtn.Rect.X + this._bypassBtn.Rect.Width)
                   && (nPoint.Y >= this._bypassBtn.Rect.Y)
                   && (nPoint.Y <= this._bypassBtn.Rect.Y + this._bypassBtn.Rect.Height))
            {
                _bypassBtn.MouseDown(nPoint);
            }

            if ((nPoint.X >= this._faultBtn.Rect.X)
                    && (nPoint.X <= this._faultBtn.Rect.X + this._faultBtn.Rect.Width)
                    && (nPoint.Y >= this._faultBtn.Rect.Y)
                    && (nPoint.Y <= this._faultBtn.Rect.Y + this._faultBtn.Rect.Height))
            {
                _faultBtn.MouseDown(nPoint);
            }

            return base.mouseDown(nPoint);
        }


        /// <summary>
        /// mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            if ((nPoint.X >= this._bypassBtn.Rect.X)
                    && (nPoint.X <= this._bypassBtn.Rect.X + this._bypassBtn.Rect.Width)
                    && (nPoint.Y >= this._bypassBtn.Rect.Y)
                    && (nPoint.Y <= this._bypassBtn.Rect.Y + this._bypassBtn.Rect.Height))
            {
                
                    _bypassBtn.MouseUp(nPoint);
            }

            if ((nPoint.X >= this._faultBtn.Rect.X)
                    && (nPoint.X <= this._faultBtn.Rect.X + this._faultBtn.Rect.Width)
                    && (nPoint.Y >= this._faultBtn.Rect.Y)
                    && (nPoint.Y <= this._faultBtn.Rect.Y + this._faultBtn.Rect.Height))
            {
                    _faultBtn.MouseUp(nPoint);
            }
   
            return base.mouseUp(nPoint);
        }

        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (e.Message == (int)ViewState.BypassInfo)//旁路按钮
            {
                if(CommonStatus.CurrentViewState==ViewState.BypassInfo || CommonStatus.CurrentViewState==ViewState.BypassInfo2)//返回到前一界面（按旁路按钮时的界面）
                {
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage,(int) _oldViewState, 0, 0);
                }
                else//跳转到旁路界面
                {
                    _oldViewState=CommonStatus.CurrentViewState;
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.BypassInfo, 0, 0);
                }
            }
            else if (e.Message == (int)ViewState.FaultInfo)//故障按钮
            {

            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
           // dcGs.DrawImage(_resource_Image[0], new Rectangle(702,278,87,42));
            //dcGs.DrawImage(_resource_Image[2], new Rectangle(702,462,87,42));
            if (CommonStatus.CurrentViewState == ViewState.BypassInfo || CommonStatus.CurrentViewState == ViewState.BypassInfo2)
            {
                _bypassBtn.Style = _bypassBatnStyles[2];
            }
            else
            {
                _bypassBtn.Style = _bypassBatnStyles[0];
            }
           _bypassBtn.Paint(dcGs);
           _faultBtn.Paint(dcGs);

            base.paint(dcGs);
        }

        #endregion
    }
}
