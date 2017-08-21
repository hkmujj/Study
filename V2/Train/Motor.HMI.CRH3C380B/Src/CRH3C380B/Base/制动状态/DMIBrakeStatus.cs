using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.�ײ㹲��;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.�ƶ�״̬
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBrakeStatus : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr = {"", "", "�ƶ�\n��Ч��", "�ƶ���\n״̬", "�ƶ�\n����", "ͣ��\n�ƶ�", "", "�ܷ��", "", ""};

        private readonly string[] m_ConStrArr = {"�����ƶ�", "ѩ���ƶ�", "��������"};

        private readonly string[] m_StateIndexs = {InBoolKeys.Inb1168, InBoolKeys.Inb1169, InBoolKeys.Inb1170,};

        public override string GetInfo()
        {
            return "DMI�ƶ�״̬";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2 || DMITitle.BtnContentList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
        }

        public override void Paint(Graphics g)
        {
            ResponseBtnEvent();

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintRectangles(g);
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 8:
                        append_postCmd(CmdType.ChangePage, 230, 0, 0); //�ƶ���Ч��
                        break;
                    case 9:
                        append_postCmd(CmdType.ChangePage, 240, 0, 0); //�ƶ���״̬
                        break;
                    case 10:
                        append_postCmd(CmdType.ChangePage, 250, 0, 0); //�ƶ�����
                        break;
                    case 11:
                        append_postCmd(CmdType.ChangePage, 260, 0, 0); //ͣ���ƶ�
                        break;
                    case 13:
                        append_postCmd(CmdType.ChangePage, 280, 0, 0); //�ܷ��
                        break;
                    case 20:
                        append_postCmd(CmdType.ChangePage, 2, 0, 0); //��Ϣ��
                        break;
                }
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("�ƶ�״̬", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.����));

            for (int i = 0; i < 3; i++)
            {
                if (GetInBoolValue(m_StateIndexs[i]))
                {
                    g.DrawString(m_ConStrArr[i], FontsItems.FontC12,
                        DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                        m_RectsList[19], FontsItems.TheAlignment(FontRelated.����));
                }
            }
        }

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakeStatus, ref m_RectsList))
            {

            }
        }
    }
}