using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Urban.Wuxi.TMS.DMITitle;

namespace Urban.Wuxi.TMS.故障事件
{
    /// <summary>
    /// 故障确认页面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class FaultConfirm : TMSBaseClass
    {
        private List<Rectangle> m_Rects;
        private List<Image> m_Img;
        private readonly RectangleF m_FaultSolutionStr = new RectangleF(20, 250, 680, 300);
        private List<MsgReceiveMod.FaultMsgOrdinary> m_FaultMsg;
        private bool m_HasFault;
        private int m_LastView;
        private readonly bool[] m_MouseIsDown = new bool[2];
        /// <summary>
        /// 初始化类
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        private void InitData()
        {
            m_Rects = new List<Rectangle>();
            for (var i = 3; i < 5; i++)
            {
                m_Rects.Add(new Rectangle(730, 150 + 70 * i, 60, 52));
            }
            m_Img = new List<Image>();
            foreach (var img in UIObj.ParaList)
            {
                m_Img.Add(Image.FromFile(Path.Combine(RecPath, img)));
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                m_FaultMsg = Title.MsgInfList.CurrentMsgList.FindAll(f => !f.TheMsgFlag);
                m_LastView = (int)nParaC;
            }
        }

        public override void paint(Graphics g)
        {
            OnDraw(g);
        }

        private void OnDraw(Graphics e)
        {
            DrawContent(e);
        }

        private void DrawContent(Graphics e)
        {
            if (m_FaultMsg != null && m_FaultMsg.Count != 0)
            {
                m_HasFault = true;
                e.DrawImage(m_MouseIsDown[0] ? m_Img[1] : m_Img[0], m_Rects[0]);
                e.DrawString("确定", FormatStyle.m_Font12B,
                    FormatStyle.m_BlackBrush, m_Rects[0], FormatStyle.m_Cneter);
                e.DrawString(m_FaultMsg[0].MsgContent
               , FormatStyle.m_Font18B, FormatStyle.m_WhiteBrush, 20, 125);
                e.DrawString(m_FaultMsg[0].FaultSolutionStr,
                FormatStyle.m_Font18, FormatStyle.m_WhiteBrush, m_FaultSolutionStr);
            }
            else
            {
                m_HasFault = false;
            }
            e.DrawImage(m_MouseIsDown[1] ? m_Img[1] : m_Img[0], m_Rects[1]);
            e.DrawString("返回", FormatStyle.m_Font12B,
                    FormatStyle.m_BlackBrush, m_Rects[1], FormatStyle.m_Cneter);
            e.DrawRectangle(FormatStyle.m_WhitePen, 20, 110, 700, 100);
            e.DrawRectangle(FormatStyle.m_WhitePen, 20, 240, 700, 300);

        }

        public override bool mouseDown(Point point)
        {
            int index = m_Rects.TakeWhile(rect => !rect.Contains(point)).Count();
            switch (index)
            {
                case 0:
                case 1:
                    m_MouseIsDown[index] = true;
                    break;
                default:
                    break;
            }
            return base.mouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            var index = m_Rects.TakeWhile(rect => !rect.Contains(point)).Count();
            switch (index)
            {
                case 0:
                    m_MouseIsDown[index] = false;
                    if (m_HasFault)
                    {
                        Title.MsgInfList.MsgBeRead(m_FaultMsg[0].MsgLogicID);
                        m_FaultMsg = Title.MsgInfList.CurrentMsgList.FindAll(f => !f.TheMsgFlag);
                    }
                    break;
                case 1:
                    m_MouseIsDown[index] = false;
                    append_postCmd(CmdType.ChangePage, m_LastView, 0, 0);
                    break;
                default:
                    break;
            }
            return base.mouseUp(point);
        }
    }
}