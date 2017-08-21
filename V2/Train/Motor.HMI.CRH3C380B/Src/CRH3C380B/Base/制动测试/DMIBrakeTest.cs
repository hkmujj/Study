using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.�ײ㹲��;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.�ƶ�����
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBrakeTest : CRH3C380BBase
    {
        //2

        private List<RectangleF> m_RectsList;

        private bool m_TheBrakeOn;

        private readonly string[] m_BtnStr =
        {
            "",
            "ֱ���ƶ�\n����",
            "�����ƶ�\n����",
            "MRP\n��ͨ��",
            "BP\nй©",
            "����ƶ�\n����",
            "BP\n��ͨ��",
            "�ϴ�����\n���",
            "",
            "�ƶ�\n״̬"
        };

        private readonly string[] m_ContentStrs =
        {
            "",
            "2 ֱ���ƶ�����",
            "3 �����ƶ�����",
            "4 �ܷ��(MRP)��ͨ������",
            "5 �г���(BP)й©����",
            "6 ����ƶ�����",
            "7 �г���(BP)��ͨ������",
            "8 �ϴ��ƶ�������",
            "",
            "0 �ƶ�״̬"
        };

        private readonly string[] m_BrakeIndexs =
        {

            InBoolKeys.Inbͣ���ƶ�ʩ��2,
            InBoolKeys.Inbͣ���ƶ�ʩ��7,
            InBoolKeys.Inbͣ���ƶ�ʩ��10,
            InBoolKeys.Inbͣ���ƶ�ʩ��15,
        };

        private readonly string[] m_BtnStr1 = {"", "", "", "", "", "", "", "", "", "�ƶ�\n״̬"};

        public override string GetInfo()
        {
            return "DMI�ƶ�����";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_TheBrakeOn ? m_BtnStr[i] : m_BtnStr1[i];
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);
        }


        private void Draw(Graphics g)
        {
            PaintRectangles(g);
        }

        private void GetValue()
        {
            m_TheBrakeOn = m_BrakeIndexs.Any(GetInBoolValue);

            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            if (DMIButton.BtnUpList[0] == 15 || DMIButton.BtnUpList[0] == 0)
            {
                append_postCmd(CmdType.ChangePage, 21, 0, 0); //�ƶ�״̬
            }
            else if (m_TheBrakeOn)
            {
                ResponseBtnEvent();
            }
        }

        private void ResponseBtnEvent()
        {
            switch (DMIButton.BtnUpList[0])
            {
                case 7: //ֱ���ƶ�����
                    append_postCmd(CmdType.ChangePage, 252, 0, 0);
                    break;
                case 8: //�����ƶ�����
                    append_postCmd(CmdType.ChangePage, 253, 0, 0);
                    break;
                case 9: //�ܷ�ܹ�ͨ������
                    append_postCmd(CmdType.ChangePage, 254, 0, 0);
                    break;
                case 10: //�г���й©����
                    append_postCmd(CmdType.ChangePage, 255, 0, 0);
                    break;
                case 11: //����ƶ�����
                    append_postCmd(CmdType.ChangePage, 256, 0, 0);
                    break;
                case 12: //�г��ܹ�ͨ������
                    append_postCmd(CmdType.ChangePage, 257, 0, 0);
                    break;
                case 13: //�ϴ��ƶ�������
                    append_postCmd(CmdType.ChangePage, 238, 0, 0);
                    break;
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString("�ƶ�����", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.����));
            if (!m_TheBrakeOn)
            {
                g.FillRectangle(SolidBrushsItems.RedBrush1, m_RectsList[22]);
                g.DrawString("ʩ��ͣ���ƶ���", FontsItems.FontC24B,
                    SolidBrushsItems.WhiteBrush, m_RectsList[22], FontsItems.TheAlignment(FontRelated.����));
            }
            for (int i = 0; i < 10; i++)
            {
                //�м�����
                g.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.����));
            }
        }

        private void InitData()
        {

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBrakeTest, ref m_RectsList))
            {

            }
        }
    }
}