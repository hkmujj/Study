#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-2
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.2-机车模型绘制
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

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：公共组件-No.2-机车模型绘制
    /// 创建人：唐林
    /// 创建时间：2014-7-2
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class VC_C3_Car : baseClass
    {
        #region 私有变量
        private ViewState _currentViewState;//当前视图
        private List<Image> resource_Images = new List<Image>();//图片资源
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "车模块";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , a), FileMode.Open))
                {
                    this.resource_Images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }

        /// <summary>
        /// 获取当前视图
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                this._currentViewState = (ViewState)nParaB;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(this.resource_Images[0], new Point(0, 97 - 6));//机车图片
            this.paint_TrainName(dcGs);

            if (this._currentViewState == ViewState.车辆状态
                || this._currentViewState == ViewState.空调_模式选择
                || this._currentViewState == ViewState.空调_温度控制
                || this._currentViewState == ViewState.网络通讯)//为第三视图（车辆状态），返回
                return;

            dcGs.DrawString("TC1", new Font("宋体", 10), new SolidBrush(Color.White), new PointF(58.5f, 150.5f - 6));
            dcGs.DrawString("TC2", new Font("宋体", 10), new SolidBrush(Color.White), new PointF(722f, 150.5f - 6));
            this.paint_Driction(dcGs);
            this.paint_G(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制车头方向与占用情况
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Driction(Graphics dcGs)
        {
            //司机室占用
            for (int i = 0; i < 2; i++)//车头1、2
            {
                for (int j = 0; j < 2; j++)//两个状态
                {
                    if (BoolList[UIObj.InBoolList[0] + j + i * 2])
                        dcGs.DrawImage(this.resource_Images[2-j], new Point(85 + i * 601, 125 - 6));
                }

            }

            //车头方向
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + j + i * 2])
                        dcGs.DrawImage(this.resource_Images[3 + j], new Point(94 + i * 600, 142 - 6));
                }
            }
        }

        /// <summary>
        /// 绘制受电弓状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_G(Graphics dcGs)
        {
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    dcGs.DrawImage(this.resource_Images[5 + i], new PointF(237.5f, 100 - 6));
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    dcGs.DrawImage(this.resource_Images[5 + 6 + i], new PointF(514f, 100 - 6));
                }
            }
        }

        /// <summary>
        /// 绘制车辆名字编号
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TrainName(Graphics dcGs)
        {
            String[] strs = new String[] { "Tc1", "Mp1", "M1", "M2", "Mp2", "Tc2" };
            String[] strs_ = new String[] { "Tc1\n01021", "Mp1\n01022", "M1\n01023", "M2\n01024", "Mp2\n01025", "Tc2\n01026" };
            for (int i = 0; i < 6; i++)
            {
                if (this._currentViewState == ViewState.空调_模式选择
                    || this._currentViewState == ViewState.空调_温度控制
                    || this._currentViewState == ViewState.网络通讯)
                {
                    dcGs.DrawString(strs[i], new Font("宋体", 10), new SolidBrush(Color.White), new RectangleF(127 + 94 * i, 128, 92, 35), FontInfo.SF_CC);
                }
                else
                {
                    dcGs.DrawString(strs_[i], new Font("宋体", 10), new SolidBrush(Color.White), new RectangleF(127 + 94 * i, 128, 92, 35), FontInfo.SF_CC);
                }
            }
        }
        #endregion
    }
}
