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

using System.Drawing;
using Engine.Turkmenistan.LKJ.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Util;

namespace Engine.Turkmenistan.LKJ.公共组件
{

    [GksDataType(DataType.isMMIObjectClass)]
    public class V1RunC2DigitDisplay : baseClass
    {
        #region 私有变量
        public static string Info;
        private Font fontDs = new Font("DS-Digital", 50, FontStyle.Italic);
        private SolidBrush brush = new SolidBrush(Color.FromArgb(255, 5, 0));
        private Rectangle[] recs = new Rectangle[] { new Rectangle(53, 153, 150, 50), new Rectangle(215, 153, 150, 50), new Rectangle(73, 241, 50, 50), new Rectangle(113, 241, 250, 50), new Rectangle(378, 84, 300, 50) };
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


            return true;
        }
        #endregion



        #region 界面绘制
        /// <summary>
        /// 界面绘制：按钮绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {

            

            if (VcC0BackGround.isShow)
            {   
                //实际速度
                dcGs.DrawString(FloatList[UIObj.InFloatList[0]].ToString().Split('.')[0], fontDs, brush, recs[0], FontInfo.SfRc);
                //限制速度
                dcGs.DrawString(FloatList[UIObj.InFloatList[1]].ToString().Split('.')[0], fontDs, brush, recs[1], FontInfo.SfRc);
                //类别
                dcGs.DrawString(FloatList[UIObj.InFloatList[2]].ToString(), fontDs, brush, recs[2], FontInfo.SfRc);
                //距离前方信号机
                dcGs.DrawString(FloatList[UIObj.InFloatList[3]].ToString(), fontDs, brush, recs[3], FontInfo.SfRc);
                //???
                dcGs.DrawString(Info, fontDs, brush, recs[4], FontInfo.SfRc);
            }
            base.paint(dcGs);
        }
        #endregion
     
    }
}

