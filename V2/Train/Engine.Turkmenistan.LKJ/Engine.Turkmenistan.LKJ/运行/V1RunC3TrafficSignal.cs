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

using System.Collections.Generic;
using System.Drawing;
using System.IO;

using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.Turkmenistan.LKJ.公共组件
{
    /// <summary>
    /// 功能描述：视图1-运行-No.1-右侧菜单按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1RunC3TrafficSignal : baseClass
    {
        #region 私有变量
        private readonly List<Image> m_ResourceImages = new List<Image>();//图片资源
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

            return true;
        }
        #endregion
       private void DrawLights(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                dcGs.DrawImage(m_ResourceImages[4], new Rectangle(340, 382, 20, 22));//白灯
            }
            if (BoolList[UIObj.InBoolList[1]])
            {
                dcGs.DrawImage(m_ResourceImages[0], new Rectangle(295, 382, 20, 22));//红灯
            }
            if (BoolList[UIObj.InBoolList[2]])
            {
                dcGs.DrawImage(m_ResourceImages[0], new Rectangle(250, 364, 20, 22));//红黄灯
                dcGs.DrawImage(m_ResourceImages[2], new Rectangle(250, 399, 20, 22));
            }
            if (BoolList[UIObj.InBoolList[3]])
            {
                dcGs.DrawImage(m_ResourceImages[2], new Rectangle(205, 364, 20, 22));//双黄灯
                dcGs.DrawImage(m_ResourceImages[2], new Rectangle(205, 399, 20, 22));
            }
            if (BoolList[UIObj.InBoolList[4]])
            {
                dcGs.DrawImage(m_ResourceImages[2], new Rectangle(159, 364, 20, 22)); ;//黄2灯
                dcGs.DrawImage(m_ResourceImages[3], new Rectangle(159, 399, 20, 22));
            }
            if (BoolList[UIObj.InBoolList[5]])
            {
                dcGs.DrawImage(m_ResourceImages[1], new Rectangle(113, 364, 20, 22));//绿黄灯
                dcGs.DrawImage(m_ResourceImages[2], new Rectangle(113, 399, 20, 22));
            }
            if (BoolList[UIObj.InBoolList[6]])
            {
                dcGs.DrawImage(m_ResourceImages[1], new RectangleF(67.5f, 382, 20, 22));//绿灯
            }

            if (BoolList[UIObj.InBoolList[7]])
            {
                dcGs.DrawImage(m_ResourceImages[5], new RectangleF(419, 186, 13, 14));//卸载工况指示灯
            }
            if (BoolList[UIObj.InBoolList[8]])
            {
                dcGs.DrawImage(m_ResourceImages[5], new RectangleF(490, 186, 13, 14));//CNYX指示灯
            }
            if (BoolList[UIObj.InBoolList[9]])
            {
                dcGs.DrawImage(m_ResourceImages[5], new RectangleF(560, 186, 13, 14));//ABEP指示灯
            }
            if (BoolList[UIObj.InBoolList[10]])
            {
                dcGs.DrawImage(m_ResourceImages[5], new RectangleF(632, 186, 13, 14));//????指示灯
            }

            if (BoolList[UIObj.InBoolList[11]])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(47, 448, 14, 15));//警惕按钮灯
            }
            if (BoolList[UIObj.InBoolList[12]])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(119, 448, 14, 15));//缓解按钮灯
            }
            if (BoolList[UIObj.InBoolList[13]])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(190, 448, 14, 15));//调车按钮灯
            }
            if (BoolList[UIObj.InBoolList[14]])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(261, 448, 14, 15));//开车按钮灯
            }
            if (BoolList[UIObj.InBoolList[15]])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(332, 448, 14, 15));//解锁按钮灯
            }

            if (BoolList[UIObj.InBoolList[16]])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(419, 218, 14, 15));//日期按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 1])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(489, 218, 14, 15));//时间按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 2])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(560, 218, 14, 15));//司机号按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 3])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(631, 218, 14, 15));//区段车站按钮灯
            }

            if (BoolList[UIObj.InBoolList[16] + 4])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(419, 281, 14, 15));//车次按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 5])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(489, 281, 14, 15));//进路号按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 6])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(560, 281, 14, 15));//总重按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 7])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(631, 281, 14, 15));//计长辆数按钮灯
            }

            if (BoolList[UIObj.InBoolList[16] + 8])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(419, 343, 14, 15));//公里标按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 9])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(489, 343, 14, 15));//车位按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 10])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(560, 343, 14, 15));//自检按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 11])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(631, 343, 14, 15));//其他1辆数按钮灯
            }

            if (BoolList[UIObj.InBoolList[16] + 12])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(419, 406, 14, 15));//检测标按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 13])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(489, 406, 14, 15));//向前按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 14])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(560, 406, 14, 15));//向后按钮灯
            }
            if (BoolList[UIObj.InBoolList[16] + 15])
            {
                dcGs.DrawImage(m_ResourceImages[6], new RectangleF(631, 406, 14, 15));//修改/确认按钮灯
            }
        
        }


        #region 界面绘制
        /// <summary>
        /// 界面绘制：按钮绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {

            if (VcC0BackGround.isShow)
            { DrawLights(dcGs); }
            base.paint(dcGs);
        }
        #endregion
    }
}

