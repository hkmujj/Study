#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-07
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-主界面-No.4-列车状态
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.MainView
{
    /// <summary>
    /// 功能描述：视图1-主界面-No.4-列车状态
    /// 创建人：lih
    /// 创建时间：2015-08-07
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1MainFrameC4TrainState : baseClass
    {
        private List<Image> m_ResourceImages = new List<Image>();//图片资源
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 18, FontStyle.Regular);
        private Brush m_BlackBrush = new SolidBrush(Color.Black);
        private readonly Rectangle m_FrameRect = new Rectangle(14, 480, 662, 30);
        private readonly Rectangle m_FrameRect1 = new Rectangle(14, 448, 662, 30);
        private readonly Rectangle m_FramRect2 = new Rectangle(130, 448, 600, 30);
        private readonly Rectangle m_FramRect3 = new Rectangle(130, 480, 600, 30);
        private string m_FrameStr = "列车状态:";
        private string m_FrameStr1 = "列车信息:";
        private string m_FrameShowStr = "";
        private string[] m_StateStrs;
        private int m_I = 0;
        private Dictionary<int, string> m_TrainInfo;
        private bool m_RectFlag = false;
        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "主界面列车状态";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V1_MainFrame_C4_TrainState";
        //}

        /// <summary>
        /// 初始化函数：导入图片
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //UIObj.ParaList.ForEach(a =>
            //{
            //    using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
            //    {
            //        this._resource_Images.Add(Image.FromStream(fs));
            //    }
            //});
            m_TrainInfo = File.ReadLines(Path.Combine(AppPaths.ConfigDirectory, "列车信息.txt"), Encoding.Default)
              .Where(w => !w.StartsWith(";;;;"))
              .Select(
                  s =>
                  {
                      var d = s.Split(';');
                      return d;
                  }).Where(w => w.Length == 2).ToDictionary(s => int.Parse(s[0]), s => s[1]);
            m_StateStrs = new string[6] { "牵引", "制动", "惰行", "保持制动", "紧急制动", "快速制动" };
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
            m_RectFlag = false;
            for (m_I = 0; m_I < 6; m_I++)
            {
                if (BoolList[UIObj.InBoolList[0] + m_I])
                {
                    dcGs.DrawString(m_StateStrs[m_I], m_ChineseFont, Brushs.WhiteBrush, m_FramRect3, FontInfo.SfLc);
                    break;
                }
            }
            m_FrameShowStr = m_FrameStr;
            dcGs.DrawString(m_FrameStr, m_ChineseFont, m_BlackBrush, m_FrameRect, FontInfo.SfLc);

            foreach (var info in m_TrainInfo.Where(info => BoolList[info.Key]))
            {
                dcGs.DrawString(info.Value, m_ChineseFont, Brushs.RedBrush, m_FramRect2, FontInfo.SfLc);
                break;
            }
            dcGs.DrawString(m_FrameStr1, m_ChineseFont, m_BlackBrush, m_FrameRect1, FontInfo.SfLc);
            base.paint(dcGs);
        }
        #endregion


    }
}
