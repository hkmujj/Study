#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-6
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图604-帮助-运行帮助-No.0
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NC_TMS.帮助
{
    /// <summary>
    /// 功能描述：视图604-帮助-运行帮助-No.0
    /// 创建人：唐林
    /// 创建时间：2014-7-6
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V601_Help_RunView_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "帮助视图-运行帮助视图";
        }

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            return true;
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(5, 96, 796, 454));

            dcGs.DrawImage(_resource_Image[0], new Rectangle(70, 98, 647, 450));

            base.paint(dcGs);
        }
        #endregion
    }
}
