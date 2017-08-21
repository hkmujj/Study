#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：维护-No.9-亮度调节
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Urban.HeFei.TMS.Common;

namespace Urban.HeFei.TMS.Maintenance
{
    /// <summary>
    /// 功能描述：维护-No.9-亮度调节
    /// 创建人：lih
    /// 创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V809MaintenanceAdjustBrightness:baseClass
    {
        #region 私有属性
        private List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Button m_BtnReduce;
        private Brush[] m_Brushes;
        private Rectangle[] m_BrsRects;
        private Button m_BtnAdd;
        private int m_CurrentValue = 0;
        private int m_I = 0;
        private int m_MaxValue = 5;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-亮度调节";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "V809_Maintenance_AdjustBrightness";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_CurrentValue = m_MaxValue;
            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });
            m_BrsRects = new Rectangle[5];
            for (m_I = 0; m_I < m_BrsRects.Length; m_I++)
            {
                m_BrsRects[m_I] = new Rectangle(278 + 50 * m_I, 222, 38, 106);
            }

            m_Brushes = new SolidBrush[5];
            OnChangedBright();

            var btnReduceStyles = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[0], DownImage = m_ResourceImage[0] };
            m_BtnReduce = new Button("", new Rectangle(196, 218, 54, 113), (int)(ViewState.MtAbReduceBtn), btnReduceStyles, true);
            m_BtnReduce.ClickEvent += _btn_ClickEvent;

            var btnAddStyles = new ButtonStyle() { FontStyle = FontStyles.FsSong16CcB, Background = m_ResourceImage[1], DownImage = m_ResourceImage[1] };
            m_BtnAdd = new Button("", new Rectangle(546, 218, 54, 113), (int)(ViewState.MtAbAddBtn), btnAddStyles, true);
            m_BtnAdd.ClickEvent += _btn_ClickEvent;
            return true;
        }

        /// <summary>
        /// 更新亮度显示
        /// </summary>
        private void OnChangedBright()
        {
            for (m_I = 0; m_I < m_CurrentValue; m_I++)
            {
                m_Brushes[m_I] = new SolidBrush(Color.FromArgb(0, 255, 0));
            }
            for (m_I = m_CurrentValue; m_I < m_MaxValue; m_I++)
            {
                m_Brushes[m_I] = new SolidBrush(Color.FromArgb(147, 147, 147));
            }
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            if ((nPoint.X >= m_BtnReduce.Rect.X)
                   && (nPoint.X <= m_BtnReduce.Rect.X + m_BtnReduce.Rect.Width)
                   && (nPoint.Y >= m_BtnReduce.Rect.Y)
                   && (nPoint.Y <= m_BtnReduce.Rect.Y + m_BtnReduce.Rect.Height))
            {
                m_BtnReduce.MouseDown(nPoint);
            }
            if ((nPoint.X >= m_BtnAdd.Rect.X)
                   && (nPoint.X <= m_BtnAdd.Rect.X + m_BtnAdd.Rect.Width)
                   && (nPoint.Y >= m_BtnAdd.Rect.Y)
                   && (nPoint.Y <= m_BtnAdd.Rect.Y + m_BtnAdd.Rect.Height))
            {
                m_BtnAdd.MouseDown(nPoint);
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
            if ((nPoint.X >= m_BtnReduce.Rect.X)
                   && (nPoint.X <= m_BtnReduce.Rect.X + m_BtnReduce.Rect.Width)
                   && (nPoint.Y >= m_BtnReduce.Rect.Y)
                   && (nPoint.Y <= m_BtnReduce.Rect.Y + m_BtnReduce.Rect.Height))
            {
                m_BtnReduce.MouseUp(nPoint);
            }
            if ((nPoint.X >= m_BtnAdd.Rect.X)
                && (nPoint.X <= m_BtnAdd.Rect.X + m_BtnAdd.Rect.Width)
                && (nPoint.Y >= m_BtnAdd.Rect.Y)
                && (nPoint.Y <= m_BtnAdd.Rect.Y + m_BtnAdd.Rect.Height))
            {
                m_BtnAdd.MouseUp(nPoint);
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if ((int)ViewState.MtAbReduceBtn == e.Message)
            {
                if (m_CurrentValue > 0)
                {
                    m_CurrentValue--;
                }
            }
            else if ((int)ViewState.MtAbAddBtn == e.Message)
            {
                if (m_CurrentValue < m_MaxValue)
                {
                    m_CurrentValue++;
                }
            }
            OnChangedBright();
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            m_BtnAdd.Paint(dcGs);
            m_BtnReduce.Paint(dcGs);

            for (m_I = 0; m_I < m_BrsRects.Length; m_I++)
            {
                dcGs.FillRectangle(m_Brushes[m_I], m_BrsRects[m_I]);
            }
            base.paint(dcGs);
        }
        #endregion
    }
}
