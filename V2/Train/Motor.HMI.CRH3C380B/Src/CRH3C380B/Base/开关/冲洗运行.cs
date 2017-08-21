using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMIWashRun : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        private int m_Mode;

        private readonly SolidBrush[] m_RectBrushArr =
        {
            SolidBrushsItems.BlackBrush,
            SolidBrushsItems.BlackBrush,
            SolidBrushsItems.BlackBrush
        };

        private readonly SolidBrush[] m_StrBrushArr =
        {
            SolidBrushsItems.WhiteBrush,
            SolidBrushsItems.WhiteBrush,
            SolidBrushsItems.WhiteBrush
        };

        private readonly string[] m_BtnStr =
        {
            "洗车运行\n关闭",
            "洗车运行\n开始",
            "关闭车钩罩",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly string[] m_ContentStrs = {"洗车运行"};

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }
            Coordinate.RectangleFLists(ViewIDName.DMIWashRun, ref m_RectsList);

        }

        private Action<Graphics> m_Graphics;
        //2
        public override string GetInfo()
        {
            return "DMI洗车运行";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                UpdateBrush(true);
            }

        }

        public override void Paint(Graphics g)
        {
            GetValue();
            Draw(g);
            if (DMITitle.BtnContentList[0].BtnStr == string.Empty)
            {
                WashStop();
            }
            else if (DMITitle.BtnContentList[1].BtnStr == string.Empty)
            {
                WashRun();
            }
            if (m_Graphics != null)
            {
                m_Graphics(g);
            }
        }

        private void Draw(Graphics e)
        {
            PaintRectangles(e);
        }

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 190, 0, 0);
                    break;
                case 6: //洗车运行关闭
                    WashStop();
                    m_Mode = 1;
                    UpdateBrush(false);
                    break;
                case 7: //洗车运行开始
                    WashRun();
                    m_Mode = 0;
                    UpdateBrush(false);
                    break;
                //case 8://关闭车钩罩
                //    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                //    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                //    m_Mode = 2;
                //    UpdateBrush(false);
                //    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void UpdateStrBrush()
        {
            switch (m_Mode)
            {
                case 1:
                    m_RectBrushArr[0] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[0] = SolidBrushsItems.BlackBrush;
                    DMITitle.BtnContentList[0].BtnStr = string.Empty;
                    break;
                case 0:
                    m_RectBrushArr[1] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[1] = SolidBrushsItems.BlackBrush;
                    DMITitle.BtnContentList[1].BtnStr = string.Empty;
                    break;
                case 2:
                    m_RectBrushArr[2] = SolidBrushsItems.WhiteBrush;
                    m_StrBrushArr[2] = SolidBrushsItems.BlackBrush;
                    DMITitle.BtnContentList[2].BtnStr = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// 更改Brush
        /// </summary>
        /// <param name="isInit"></param>
        private void UpdateBrush(bool isInit)
        {
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
            for (int i = 0; i < 3; i++)
            {
                m_RectBrushArr[i] = SolidBrushsItems.BlackBrush;
                m_StrBrushArr[i] = SolidBrushsItems.WhiteBrush;
            }

            if (isInit)
            {
                m_Mode = 0;
            }

            UpdateStrBrush();
        }

        /// <summary>
        /// 设置画图区域和字符串的Brush
        /// </summary>
        /// <param name="theBrush">Brush</param>
        /// <returns>返回的Brush</returns>
        private static SolidBrush SetRectAndStrBrush(SolidBrush theBrush)
        {
            return theBrush == SolidBrushsItems.BlackBrush ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush;
        }

        /// <summary>
        /// 画字符串以及图形区域
        /// </summary>
        /// <param name="g">图形</param>
        private void PaintRectangles(Graphics g)
        {
            g.DrawString("开关;洗车运行", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

        }

        private void WashRun()
        {

            m_Graphics = g =>
                g.DrawString("本动车组准备由机车拖动",
                    FontsItems.FontC12B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[15],
                    FontsItems.TheAlignment(FontRelated.靠左));
            m_Graphics += g => g.DrawString("---------------------",
                FontsItems.FontC12B,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[16],
                FontsItems.TheAlignment(FontRelated.靠左));
            m_Graphics += g => g.FillRectangle(SolidBrushsItems.WhiteBrush, m_RectsList[13]);
            m_Graphics += g => g.DrawString(m_ContentStrs[0],
                FontsItems.FontC24B,
                SolidBrushsItems.BlackBrush,
                m_RectsList[13],
                FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void WashStop()
        {

            m_Graphics = g =>
                g.DrawString("",
                    FontsItems.FontC12B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[15],
                    FontsItems.TheAlignment(FontRelated.靠左));
            m_Graphics += g => g.DrawString("",
                FontsItems.FontC12B,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[16],
                FontsItems.TheAlignment(FontRelated.靠左));
            m_Graphics += g => g.DrawRectangle(PenItems.WhiltPen, Rectangle.Ceiling(m_RectsList[13]));
            m_Graphics += g => g.DrawString(m_ContentStrs[0],
                FontsItems.FontC24B,
                //DMITitle.NightMode ? SetRectAndStrBrush(m_StrBrushArr[0]) : m_StrBrushArr[0],
                //m_RectsList[13],
                SolidBrushsItems.WhiteBrush,
                m_RectsList[13],
                FontsItems.TheAlignment(FontRelated.靠左));
            var point = new Point(750, 100);
            var size = new Size(30, 350);
            var pointString = new Point(750, 80);
            m_Graphics += g => g.DrawRectangle(PenItems.WhiltPen, new Rectangle(point, size));
            m_Graphics += g => g.DrawString("100%", FontsItems.FontC12B, SolidBrushsItems.WhiteBrush, pointString);

        }

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        #endregion
    }
}