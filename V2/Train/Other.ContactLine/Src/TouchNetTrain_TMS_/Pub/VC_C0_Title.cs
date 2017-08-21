#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-第一个组件-标题
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace TouchNetTrain_TMS_
{
    /// <summary>
    /// 控件点击事件代理
    /// </summary>
    /// <param name="sender">事件发生对象</param>
    /// <param name="e">传输参数</param>
    public delegate void EventHandle_BooleanEvent(Object sender, ClickEventArgs<Boolean> e);

    /// <summary>
    /// 功能描述：公共组件-第一个组件-标题
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C0_Title : baseClass
    {
        #region 私有变量
        private int _currentViewID = 0;//当前视图ID
        private int _currentViewID_1 = 0;//当前视图第一位
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-标题栏";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        /// <summary>
        /// 获取当期视图值
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _currentViewID = nParaB;
                _currentViewID_1 = Convert.ToInt32(nParaB.ToString().ElementAt(0).ToString()) - 1;
            }
        }
        #endregion

        #region 绘制界面
        /// <summary>
        /// 界面绘制函数
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            paint_Frame(dcGs);
            paint_Title(dcGs);
            paint_Time(dcGs);
            paint_Speed(dcGs);
            paint_Gongkuang(dcGs);
            paint_Mode(dcGs);
            paint_JiWei(dcGs);
            paint_Duan(dcGs);
            paint_DanJi(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制标题
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Title(Graphics dcGs)
        {
            dcGs.FillRectangle(Global.SB_Title, new Rectangle(11, 11, 778, 30));
            dcGs.DrawRectangle(Global.Pen_Yellow_2, new Rectangle(11, 11, 778, 30));

            List<String> strList = new List<string>() { "牵引界面", "运行信息", "手动控制", "开关状态", "故障履历", "累计信息", "调试界面", "辅助功能" };
            String title = strList[_currentViewID_1];
            if (_currentViewID_1 == 2)
            {
                switch (_currentViewID)
                {
                    case 301:
                        title = "手动控制--离合齿";
                        break;
                    case 302:
                        title = "手动控制--转向架支撑";
                        break;
                    case 303:
                        title = "手动控制--保护解除";
                        break;
                }
            }
            dcGs.DrawString(
                title,
                Global.Font_Verdana_12,
                Brushes.Yellow,
                new Rectangle(11, 11, 778, 30),
                Global.SF_CC
                );
        }

        /// <summary>
        /// 背景绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.FillRectangle(Brushes.Black, new Rectangle(11, 93, 778, 442));
            dcGs.FillRectangle(Global.SB_Blue, new Rectangle(11, 46, 778, 42));
            dcGs.DrawRectangle(Global.Pen_Yellow_2, new Rectangle(11, 46, 778, 42));
            dcGs.DrawLine(Global.Pen_Yellow_2, new Point(216, 46), new Point(216, 88));
            dcGs.DrawLine(Global.Pen_Yellow_2, new Point(361, 46), new Point(361, 88));
            dcGs.DrawLine(Global.Pen_Yellow_2, new Point(454, 46), new Point(454, 88));
            dcGs.DrawLine(Global.Pen_Yellow_2, new Point(549, 46), new Point(549, 88));
            dcGs.DrawLine(Global.Pen_Yellow_2, new Point(631, 46), new Point(631, 88));
            dcGs.DrawLine(Global.Pen_Yellow_2, new Point(730, 46), new Point(730, 88));
        }

        /// <summary>
        /// 绘制时间
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Time(Graphics dcGs)
        {
            dcGs.DrawString(
                DateTime.Now.ToString("yyyy/MM/dd ") + DateTime.Now.ToLongTimeString(),
                Global.Font_Verdana_12,
                Brushes.White,
                new Rectangle(11, 46, 205, 42),
                Global.SF_CC
                );
        }

        /// <summary>
        /// 绘制速度
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Speed(Graphics dcGs)
        {
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[0]].ToString("0.0"),
                new Font("Verdana", 20),
                Brushes.White,
                new Rectangle(216, 46, 100, 42),
                Global.SF_CC
                );

            dcGs.DrawString(
                "km/h",
                Global.Font_Verdana_12,
                Brushes.White,
                new Rectangle(216, 46, 135, 42),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制工况
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Gongkuang(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                dcGs.DrawString(
                "空挡工况",
                Global.Font_Verdana_12,
                Brushes.White,
                new Rectangle(361, 46, 93, 42),
                Global.SF_CC
                );
            }
        }

        /// <summary>
        /// 绘制模式
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Mode(Graphics dcGs)
        {
            String[] strs = new String[] { "惰性", "牵引", "制动" };
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    dcGs.DrawString(
                    strs[i],
                    Global.Font_Verdana_12,
                    Brushes.White,
                    new Rectangle(454, 46, 95, 42),
                    Global.SF_CC
                    );
                }
            }
        }

        /// <summary>
        /// 绘制级位
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_JiWei(Graphics dcGs)
        {
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[1]].ToString("0.0"),
                new Font("Verdana", 20),
                Brushes.White,
                new Rectangle(549, 46, 60, 42),
                Global.SF_CC
                );

            dcGs.DrawString(
                "级",
                Global.Font_Verdana_12,
                Brushes.White,
                new Rectangle(549, 46, 75, 42),
                Global.SF_FC
                );
        }

        /// <summary>
        /// 绘制哪端控制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Duan(Graphics dcGs)
        {
            String[] strs = new String[] { "Ⅰ端控制", "Ⅱ端控制" };
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    dcGs.DrawString(
                    strs[i],
                    Global.Font_Verdana_12,
                    Brushes.White,
                    new Rectangle(631, 46, 99, 42),
                    Global.SF_CC
                    );
                }
            }
        }

        /// <summary>
        /// 绘制单机
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_DanJi(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[3]])
            {
                dcGs.DrawString(
                "单机",
                Global.Font_Verdana_12,
                Brushes.White,
                new Rectangle(730, 46, 59, 42),
                Global.SF_CC
                );
            }
        }
        #endregion
    }
}
