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
using Engine.Turkmenistan.LKJ.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.Turkmenistan.LKJ.公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1RunC1RightButton : baseClass
    {
        #region 私有变量
        private readonly List<Button> m_Btns = new List<Button>();//按钮列表
        private readonly List<Image> m_ResourceImages = new List<Image>();//图片资源
        private int m_ViewIndex = -1;

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
                    m_ResourceImages.Add(Image.FromStream(fs));
                }
            });


            String[] strs = new String[] { "日期", "时间", "司机号", "区段车站", "车次", "进路号", "总重", "记长辆数", "公里标", "车位", "自检", "其他", "检测", "向前", "向后", "修改/确认", "出入库", "巡检", "其他", "复位" };

            for (int i = 0; i < 20; i++)
            {
                Button btn = new Button(
                     null,
                     new Rectangle(419 + (i % 4) * 71, 223 + (i / 4) * 63, 50, 53),
                     i,
                     new ButtonStyle() { FontStyle = FontStyles.FsSong11CcB, Background = null, DownImage = null }
                     );
                //           Button btn = new Button(
                //string.Empty,
                //new Rectangle(420+(i%4)*71, 220+(i/4)*63, 11,12),
                //i,
                //new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = resource_Images[1], DownImage = resource_Images[1] }
                //);
                btn.ClickEvent += Btn_ClickEvent;
                btn.DownEvent += Btn_DownEvent;
                m_Btns.Add(btn);
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
            m_Btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            m_Btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        void Btn_DownEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {

                case 0://日期
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    break;
                case 1://时间
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 1, 1, 0);
                    break;
                case 2://司机号
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 2, 1, 0);

                    break;
                case 3://区段车站
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 3, 1, 0);

                    break;
                case 4://车次
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4, 1, 0);

                    break;
                case 5://进路号
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 5, 1, 0);

                    break;
                case 6://总重
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 6, 1, 0);

                    break;
                case 7://计长辆数
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 7, 1, 0);

                    break;
                case 8://公里标
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 8, 1, 0);

                    break;
                case 9://车位
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 9, 1, 0);

                    break;
                case 10://自检
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 10, 1, 0);

                    break;
                case 11://其它
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 11, 1, 0);

                    break;
                case 12://检测
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 12, 1, 0);

                    break;
                case 13://向前
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 13, 1, 0);

                    break;
                case 14://向后
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 14, 1, 0);

                    break;
                case 15://修改/确认
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 15, 1, 0);

                    break;
                case 16://出入库
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 16, 1, 0);

                    break;
                case 17://巡检
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 17, 1, 0);

                    break;
                case 18://其它
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 18, 1, 0);

                    break;
                case 19://复位
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 19, 1, 0);

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
        private void Btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {

            //按钮弹起事件
            switch (e.Message)
            {

                case 0://日期
                    m_ViewIndex = 0;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    break;
                case 1://时间
                    m_ViewIndex = 1;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 1, 0, 0);
                    break;
                case 2://司机号
                    m_ViewIndex = 2;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 2, 0, 0);
                    break;
                case 3://区段车站
                    m_ViewIndex = 3;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 3, 0, 0);
                    break;
                case 4://车次
                    m_ViewIndex = 4;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4, 0, 0);

                    break;
                case 5://进路号
                    m_ViewIndex = 5;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 5, 0, 0);

                    break;
                case 6://总重
                    m_ViewIndex = 6;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 6, 0, 0);

                    break;
                case 7://计长辆数
                    m_ViewIndex = 7;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 7, 0, 0);

                    break;
                case 8://公里标
                    m_ViewIndex = 8;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 8, 0, 0);

                    break;
                case 9://车位
                    m_ViewIndex = 9;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 9, 0, 0);

                    break;
                case 10://自检
                    m_ViewIndex = 10;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 10, 0, 0);

                    break;
                case 11://其它
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 11, 0, 0);

                    break;
                case 12://检测
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 12, 0, 0);

                    break;
                case 13://向前
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 13, 0, 0);

                    break;
                case 14://向后
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 14, 0, 0);

                    break;
                case 15://修改/确认
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 15, 0, 0);

                    break;
                case 16://出入库
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 16, 0, 0);

                    break;
                case 17://巡检
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 17, 0, 0);

                    break;
                case 18://其它
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 18, 0, 0);

                    break;
                case 19://复位
                    m_ViewIndex = 19;
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 19, 0, 0);

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
            if (!VcC0BackGround.isShow)
            {
                m_ViewIndex = -1;
            }
            DateTime currentTime = DateTime.Now;

                switch (m_ViewIndex)
                {

                    case 0://日期
                        V1RunC2DigitDisplay.Info = currentTime.ToString("dd.MM.yy");
                        break;
                    case 1://时间
                        V1RunC2DigitDisplay.Info = currentTime.ToString("HH.mm.ss");
                        break;
                    case 2://司机号
                        V1RunC2DigitDisplay.Info = FloatList[UIObj.InFloatList[0]].ToString();
                        break;
                    case 3://区段车站
                        V1RunC2DigitDisplay.Info = FloatList[UIObj.InFloatList[1]].ToString();
                        break;
                    case 4://区段车站
                        V1RunC2DigitDisplay.Info = FloatList[UIObj.InFloatList[2]].ToString();
                        break;
                    case 5://区段车站
                        V1RunC2DigitDisplay.Info = FloatList[UIObj.InFloatList[3]].ToString();
                        break;
                    case 6://区段车站
                        V1RunC2DigitDisplay.Info = FloatList[UIObj.InFloatList[4]].ToString();
                        break;
                    case 7://区段车站
                        V1RunC2DigitDisplay.Info = FloatList[UIObj.InFloatList[5]].ToString();
                        break;
                    case 8://区段车站
                        V1RunC2DigitDisplay.Info = FloatList[UIObj.InFloatList[6]].ToString();
                        break;
                    case 9://区段车站
                        V1RunC2DigitDisplay.Info = FloatList[UIObj.InFloatList[7]].ToString();
                        break;
                    case 19://复位
                        V1RunC2DigitDisplay.Info = "------";
                        break;
                    default:
                        m_ViewIndex = -1;
                        V1RunC2DigitDisplay.Info = "------";
                        break;
                }

                m_Btns.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }

        #endregion
    }
}
