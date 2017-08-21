using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.����;

namespace Motor.ATP._300T.����
{
    public class AreaEContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaEContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }


        private readonly Image[] m_GsmImages =
        {
            ComImages.GSM_��,
            ComImages.GSM_2,
            ComImages.GSM_3,

        };

        private readonly Image[] m_RbcImages =
        {
            ComImages.δ��RBC����,
            ComImages.������RBC����,
            ComImages.�Ѿ���RBC����,
        };

        public void DrawAreaEDateTime(Graphics g)
        {
            /*
                         * �ڴ������������ʾ��
                         * ��һ����ʾ�ı���ʱ�����ڡ���
                         * �ڶ�����ʾʱ�䣬��ʾ��ʽ�ǡ�Сʱ:��:�롱����11:20:23��
                         * ��������ʾ���ڣ���ʾ��ʽ�ǡ���-��-�ա�����07-11-13��
                         * ����Ϊ��Բ��СΪ14�����Ƽ�������ɫΪ��ɫ��
                         */

            g.DrawString(
                "ʱ������\n" + m_ATPMainScreen.m_CurrentTime.ToLongTimeString() + "\n" +
                m_ATPMainScreen.m_CurrentTime.ToString("yy-MM-dd"),
                FontsItems.Font12YouR, SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[58],
                FontsItems.TheAlignment(FontRelated.����));
        }

        /// <summary>
        /// E���������Ϣ
        /// </summary>
        public void DrawAreaE(Graphics g)
        {

            #region ::::::::::: E1��������ϵͳ״̬

            /*
             * ��C3�س�ʱ����ʾC2ϵͳ��״̬��
             * ��C2����ʱ����ʾC3ϵͳ��״̬��
             * ��ʾ��״̬���������͹�������״̬��
             */

            #endregion

            #region ::::::::::: E3�����ල˾��������Ϣ��Ԥ����

            /*
             * ��ϵͳ�ල˾���Ļ����˾�����������ʩ���ƶ�ʱ��ʾͼ����ţ�����Ļ�û���κ���ʾ��
             * 
             * ����˾����������ʱ����ʾ��˸ͼ�꣨����������������˸Ƶ��Ϊ1 Hz��
             * ˾��ִ��һ���������˼ල�澯״̬�󣬴˷�����ʧ��
             * ���˾��û��������Ӧ���ල���ܽ�ʩ���ƶ�������ʾ��ɫ���š�
             * �ƶ���ͬʱ����C9����ʾһ���ƶ�ͼ�ꡣ
             * ���ƶ��ڼ䣬��˾��ִ��һ�������������˾����ҵ�ල���ܣ��˸澯ͼ�꽫������
             */

            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb˾����ȫ�豸�澯)] &&
                m_ATPMainScreen.CurrentTime.Second%2 == 0)
            {
                g.DrawImage(ComImages.˾����ȫ�豸�澯, m_ATPMainScreen.m_RectsList[12]);
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb˾����ȫ�豸�ƶ�)])
            {
                g.DrawImage(ComImages.˾����ȫ�豸�ƶ�, m_ATPMainScreen.m_RectsList[12]);
            }

            #endregion

            #region ::::::::::: E4���������ź�

            /*
             * �������豸�յ������źź���ʾ������˸�߿�Ľ����ź�ͼ�꣬��˸Ƶ��Ϊ1 Hz������������S7��
             * �����������ź�����ʱ����ͼ�겻����ʾ��
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb������Ϣ)])
            {
                g.DrawImage(ComImages.������Ϣ, m_ATPMainScreen.m_RectsList[13]);
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inbû������ͨ��)])
            {
                g.DrawImage(ComImages.û������ͨ��, m_ATPMainScreen.m_RectsList[13]);
            }

            #endregion

            #region ::::::::::: E5��������/�˿ر�ʾ

            /*
             * �ڻ������ȵ�����£���ʾ�����ء������˿����ȵ�����£���ʾ ���˿ء���
             * 
             * ͼ���СΪ54��30������Ϊ��Բ��СΪ14�����Ƽ�������ɫΪ��ɫ��
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb����)])
            {
                g.DrawString("����", FontsItems.Font14YouR,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[14],
                    FontsItems.TheAlignment(FontRelated.����));
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb�˿�)])
            {
                g.DrawString("�˿�", FontsItems.Font14YouR,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[14],
                    FontsItems.TheAlignment(FontRelated.����));
            }

            #endregion

            #region ::::::::::: E6- E10������վ����

            DrawStationName(g);

            #endregion


            #region E13�� ���Ų���Ϣ

            var openLocation = (OpenDoorLocation) m_ATPMainScreen.GetInFloatValue(InFloatKeys.���Ų�);
            switch (openLocation)
            {
                case OpenDoorLocation.Left:
                    g.DrawImage(OpenDoorImages.Open_Left, m_ATPMainScreen.m_RectsList[50]);
                    break;
                case OpenDoorLocation.Right:
                    g.DrawImage(OpenDoorImages.Open_Right, m_ATPMainScreen.m_RectsList[50]);
                    break;
            }

            #endregion


            #region ::::::::::: E16a�������κ�

            /*
             * ������ʾ�ı������κš���������ʾʵ�ʵĳ��κš�
             * ���κŵı��뷽ʽִ�С����ڹ�����·�г����α��뷽����֪ͨ��������[2005] 72�ţ���
             * ����Ϊ��Բ��СΪ14�����Ƽ�������ɫΪ��ɫ��
             * ���κ������ʾ8λ�ַ��������֡�
             */
            //if (FloatList[UIObj.OutFloatList[2]] != 0 || FloatList[UIObj.OutFloatList[3]] != 0)
            //{
            g.DrawString("���κ�\r\n\r\n\r\n", FontsItems.Font12DefB,
                SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[53], FontsItems.TheAlignment(FontRelated.����));
            g.DrawString("\r\n\r\n" + m_ATPMainScreen.OpenFunBtnCtcs300T.TrainId, FontsItems.Font12DefB,
                SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[53], FontsItems.TheAlignment(FontRelated.����));
            //}

            #endregion

            #region ::::::::::: E16b����GSM-R����״̬����RBC����״̬

            /*
             * E16b1������ʾGSM-R������״̬��
             * ����GSM-R����ʱ����ʾ����ͼ�꣬û��GSM-R����ʱ������ʾ����ͼ�ꡣ
             * ͼ���СΪ32��32���ڴ���������ʾ��
             */



            for (var i = 0; i < 3; i++)
            {
                if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GSMRIndexs[i]])
                {
                    g.DrawImage(m_GsmImages[i], m_ATPMainScreen.m_RectsList[54]);
                }
            }


            /* E16b2������ʾRBC������״̬��
            * ��ʾ��RBCδ���ӡ��������ӻ��Ѿ����ӵ�״̬��
            * ͼ���СΪ32��32���ڴ���������ʾ��
            */
            for (var i = 0; i < 3; i++)
            {
                if (m_ATPMainScreen.BoolList[m_ATPMainScreen.RbcIndexs[i]])
                {
                    g.DrawImage(m_RbcImages[i], m_ATPMainScreen.m_RectsList[55]);
                }
            }

            #endregion

            #region ::::::::::: E16c/E16d�������ż�ͼ��

            /*
             * E16c����ʾ�Ŵ��ͼ�꣬E16d����ʾ��С��ͼ�ꡣ
             * ͼ���СΪ57��40��
             */
            m_ATPMainScreen.m_TheGlasses[0] = ComImages.�Ŵ�; //�Ŵ�
            m_ATPMainScreen.m_TheGlasses[1] = ComImages.��С��; //��С
            if (m_ATPMainScreen.m_ShowPlanningArea) //��ȫ������ģʽ
            {
                switch (m_ATPMainScreen.m_RangeScaleMode)
                {
                    case 0:
                        m_ATPMainScreen.m_TheGlasses[0] = ComImages.�Ŵ���;
                        m_ATPMainScreen.m_TheGlasses[1] = ComImages.��С��;
                        break;
                    case 1:
                        m_ATPMainScreen.m_TheGlasses[0] = ComImages.�Ŵ���;
                        m_ATPMainScreen.m_TheGlasses[1] = ComImages.��С��;
                        break;
                    case 2:
                        m_ATPMainScreen.m_TheGlasses[0] = ComImages.�Ŵ�;
                        m_ATPMainScreen.m_TheGlasses[1] = ComImages.��С��;
                        break;
                }
            }

            for (var i = 0; i < 2; i++)
            {
                g.DrawImage(m_ATPMainScreen.m_TheGlasses[i], m_ATPMainScreen.m_RectsList[56 + i]);
            }

            #endregion

            #region ::::::::::: E17 �������ں�ʱ��

            DrawAreaEDateTime(g);

            #endregion

            #region ::::::::::: E19-E22�����ı���Ϣ

            /*
             * ������Χ���������ʾ4���ı����ɹ�����ʾ��Ϣ��
             * ����ʾ��Ϣ����ǰ����ʱ���ǣ�Сʱ:����:�룩��Ӧ������ʾ��Ϣ�ĳ��ȳ���1�е������
             * �ı���Ϣ��ʱ���Ⱥ�˳�������ʾ��������Ϣ��ʾ��E19������ɫ��ʾ������������ʾ��
             * �����ı����ֺ�ԭ�����ı���Ϊ��ɫ��
             * 
             * ������Ҫ˾��ȷ�ϵ���Ϣ����ʾ������˸�Ļ�ɫ�߿򣬱߿�Ŀ��Ϊ1�����أ���˸Ƶ��Ϊ1 Hz��
             * ˾������ȷ�Ϻ��ı���Ϊ��ɫ����ɫ�߿���ʧ��
             * ����������Ҫ��ѡ������ɾ���ı���
             * 
             * DMI�����������50���ı���Ϣ���ı���Ϣ����Ϊ��Բ��СΪ14�����Ƽ�����ʱ������ΪArial��СΪ10�����Ƽ�����
             */

            #endregion

            #region ::::::::::: E23���������

            /*
             * ��E23����λ����ʾ����ꡣ
             * ��ʾ��ʽΪ��Kx + y����������ĸ��K������ʾ����������+������ʾ������
             * ����ΪArail��СΪ16�����Ƽ�������ɫΪ��ɫ��x��λΪ���y��λΪ�ס�
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb�������ʾ)])
            {
                var meter = (int) m_ATPMainScreen.GetInFloatValue(InFloatKeys.�����);
                g.DrawString(string.Format("K{0} + {1}", meter/1000, meter%1000),
                    FontsItems.Font14YouB,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[41],
                    FontsItems.TheAlignment(FontRelated.����));
            }

            #endregion

            #region ::::::::::: E24-E25�����ı���Ϣ������ͷ

            /*
             * E24����ʾ���Ϲ�����ͷ��E25����ʾ���¹�����ͷ��
             * ���ı���ϢС�ڵ���4��ʱ��������ͷ����ʾ��ɫ����ʾ���ܹ�����ʾ��
             * ���ı���Ϣ����4��ʱ��������ʾ������ܹ���������ļ�ͷ��ʾ��ɫ�����ܹ�������ļ�ͷ��ʾ��ɫ��
             */

            #endregion

        }

        private void DrawStationName(Graphics g)
        {
             /*
             * ��վ�����Ժ��ַ�ʽ��ʾ�������ʾ6�����֣����ֲ���GB18030���뷽ʽ��
             * ����Ϊ��Բ��СΪ16�����Ƽ�������ɫΪ��ɫ��
             */
            var stationId =
                BitConverter.ToInt32(
                    BitConverter.GetBytes(m_ATPMainScreen.GetInFloatValue(InFloatKeys.��վ��)), 0);

            var stationName = GetStationName(stationId);
            if (!string.IsNullOrWhiteSpace(stationName))
            {
                g.DrawString(stationName,
                    FontsItems.Font16YouB, SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[47],
                    FontsItems.TheAlignment(FontRelated.����));
            }
        }

        private string GetStationName(int index)
        {
            if (m_ATPMainScreen.StationNameProviderService.StationDictionary != null && m_ATPMainScreen.StationNameProviderService.StationDictionary.ContainsKey(index))
            {
                return m_ATPMainScreen.StationNameProviderService.StationDictionary[index].Name;
            }

            if (m_ATPMainScreen.m_StationsDict.ContainsKey(index))
            {
                return m_ATPMainScreen.m_StationsDict[index];
            }

            return string.Empty;
        }
    }
}