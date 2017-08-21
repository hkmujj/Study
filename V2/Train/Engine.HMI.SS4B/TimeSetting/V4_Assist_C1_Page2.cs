/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-24
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：视图4-辅助-No.1-页面2
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Collections.Generic;
using System.Drawing;

namespace SS4B_TMS.TimeSetting
{
    /// <summary>
    ///     功能描述：通讯信息模块info
    ///     创建人：lih
    ///     创建时间：2014-07-24
    /// </summary>
    public class MessageModeInfo
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public MessageModeInfo(int id, string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>
        ///     读取或设置编号属性
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     读取或设置名称属性
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    ///     功能描述：视图4-辅助-No.1-页面2
    ///     创建人：lih
    ///     创建时间：2015-8-24
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V4AssistC1Page2 : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
        }

        private readonly List<Button> m_BtnsDownTabView = new List<Button>(); //按钮列表
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);

        private int m_I, m_J = 0;
        private SolidBrush m_RectBrush = new SolidBrush(Color.FromArgb(1, 116, 154));
        private Font m_DigitFont = new Font("Arial", 18);
        private Font m_ChineseFont = new Font("宋体", 14);
        private Font m_ChineseFontA = new Font("宋体", 14, FontStyle.Bold);
        private Rectangle m_PageRect = new Rectangle(710, 335, 56, 27);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.Black);
        private Rectangle[] m_FrameRects;
        private Rectangle[] m_FrameNameRects;
        private string[] m_FrameStr;
        private string m_PageStr = "页2-2";

        private int[] m_RectYs;
        private int[] m_RectXs;
        private Rectangle[] m_Rect1;
        private Rectangle[] m_Rect2;
        private Rectangle[] m_Rect3;
        private Rectangle[] m_Rect4;
        private Rectangle[] m_Rect5;

        private bool[] m_Rect2Flags;
        private bool[] m_Rect5Flags;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "辅助-页面2";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_FrameRects = new Rectangle[5];
            for (m_I = 0; m_I < m_FrameRects.Length; m_I++)
            {
                m_FrameRects[m_I] = new Rectangle(12, 262 + m_I * 50, 674, 45);
            }
            m_FrameStr = new string[5] { "辅助负载接触器", "接地隔离开关", "辅助压缩机", "蓄电池", "电池充电机" };

            m_RectYs = new int[5];
            for (m_I = 0; m_I < m_RectYs.Length; m_I++)
            {
                m_RectYs[m_I] = 266 + m_I * 50;
            }
            m_RectXs = new int[6];
            for (m_I = 0; m_I < m_RectXs.Length; m_I++)
            {
                m_RectXs[m_I] = 175 + m_I * 86;
            }

            m_Rect1 = new Rectangle[4];
            m_Rect1[0] = new Rectangle(m_RectXs[0], m_RectYs[0], 60, 36);
            m_Rect1[1] = new Rectangle(m_RectXs[2], m_RectYs[0], 60, 36);
            m_Rect1[2] = new Rectangle(m_RectXs[3], m_RectYs[0], 60, 36);
            m_Rect1[3] = new Rectangle(m_RectXs[5], m_RectYs[0], 60, 36);

            m_Rect2 = new Rectangle[2];
            m_Rect2[0] = new Rectangle(m_RectXs[1], m_RectYs[1], 60, 36);
            m_Rect2[1] = new Rectangle(m_RectXs[4], m_RectYs[1], 60, 36);
            m_Rect2Flags = new bool[2] { false, false };

            m_Rect3 = new Rectangle[2];
            m_Rect3[0] = new Rectangle(m_RectXs[1], m_RectYs[2], 60, 36);
            m_Rect3[1] = new Rectangle(m_RectXs[4], m_RectYs[2], 60, 36);

            m_Rect4 = new Rectangle[2];
            m_Rect4[0] = new Rectangle(m_RectXs[0], m_RectYs[3], 60, 36);
            m_Rect4[1] = new Rectangle(m_RectXs[5], m_RectYs[3], 60, 36);

            m_Rect5 = new Rectangle[2];
            m_Rect5[0] = new Rectangle(m_RectXs[0], m_RectYs[4], 60, 36);
            m_Rect5[1] = new Rectangle(m_RectXs[5], m_RectYs[4], 60, 36);
            m_Rect5Flags = new bool[2] { false, false };

            var bs1 = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_16_CC_B,
                Background = ImageResource.PABtnDown,
                DownImage = ImageResource.PABtnDown
            };

            var btn1 = new Button(
                "",
                new RectangleF(713, 359, 57, 34),
                3,
                bs1,
                false
                );
            btn1.ClickEvent += btn1_ClickEvent; //up
            m_BtnsDownTabView.Add(btn1);
            return true;
        }

        ///// <summary>
        ///// 设置Tc模块文本读取标志
        ///// </summary>
        ///// <param name="nPara"></param>
        ///// <returns></returns>
        //public override bool canSetStringList(int nPara)
        //{
        //    //if (nPara == 3)
        //    //{
        //    //    readTxtID = 3;
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    readTxtID = 0;
        //    //    return false;
        //    //}
        //}

        /// <summary>
        ///     读取Tc模块配置信息
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="cStr"></param>
        /// <summary>
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            //for (int i = 0; i < this._btns_Down_TabView.Count; i++)
            //{
            //    if ((nPoint.X >= this._btns_Down_TabView[i].Rect.X)
            //        && (nPoint.X <= this._btns_Down_TabView[i].Rect.X + this._btns_Down_TabView[i].Rect.Width)
            //        && (nPoint.Y >= this._btns_Down_TabView[i].Rect.Y)
            //        && (nPoint.Y <= this._btns_Down_TabView[i].Rect.Y + this._btns_Down_TabView[i].Rect.Height))
            //    {
            //        this._btns_Down_TabView[i].MouseUp(nPoint);
            //        break;
            //    }
            //}
            return base.mouseUp(nPoint);
        }

        /// <summary>
        ///     up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.ChangePage, (int)ViewState.Assist, 0, 0);
        }
    }
}