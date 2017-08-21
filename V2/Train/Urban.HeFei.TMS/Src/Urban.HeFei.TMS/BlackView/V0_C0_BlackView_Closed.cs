#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图0-黑屏-关闭时候
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.BlackView
{
    /// <summary>
    /// 功能描述：视图0-黑屏-关闭时候
    /// 创建人：lih
    /// 创建时间：2015-8-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V0C0BlackViewClosed : baseClass
    {
        private List<Image> m_ResourceImage = new List<Image>();//图片资源

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "黑屏";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V0_C0_BlackView_Closed";
        //}

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            SetValueWhenDebug();

            return true;
        }

        private void SetValueWhenDebug()
        {
            if (DataPackage.Config.SystemConfig.StartModel == StartModel.Edit)
            {
                append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);
            }
        }

        #endregion

        #region 界面绘制

        private int m_ShowTimes = 0;
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                if (10 >= m_ShowTimes++)
                {
                    CommonStatus.IsBlackScreen = true;
                    dcGs.DrawImage(m_ResourceImage[0], 0, 0, 800, 600);
                }
                else
                {
                    m_ShowTimes = 0;
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                }
            }

            base.paint(dcGs);
        }
        #endregion
    }
}
