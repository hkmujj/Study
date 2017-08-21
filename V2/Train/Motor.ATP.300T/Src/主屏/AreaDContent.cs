using System;
using System.Drawing;
using ATPComControl.MRSP;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.����;

namespace Motor.ATP._300T.����
{
    public class AreaDContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        private readonly string[] m_BrakeDistanceIndexs;
        private readonly string[] m_BrakeSpeedIndexs;

        private readonly string[] m_CaveatDistanceIndexs;
        private readonly string[] m_CaveatSpeedIndexs;

        private readonly string[] m_GradientDistanceIndexs;
        private readonly string[] m_GradientSpeedIndexs;
        public AreaDContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
            m_BrakeDistanceIndexs = new[]
            {
                InFloatKeys.�����ٶȵľ�����Ϣ1,
                InFloatKeys.�����ٶȵľ�����Ϣ2,
                InFloatKeys.�����ٶȵľ�����Ϣ3,
                InFloatKeys.�����ٶȵľ�����Ϣ4,
                InFloatKeys.�����ٶȵľ�����Ϣ5,
                InFloatKeys.�����ٶȵľ�����Ϣ6,
                InFloatKeys.�����ٶȵľ�����Ϣ7,
                InFloatKeys.�����ٶȵľ�����Ϣ8,
                InFloatKeys.�����ٶȵľ�����Ϣ9,
                InFloatKeys.�����ٶȵľ�����Ϣ10,
            };

            m_BrakeSpeedIndexs = new[]
            {
                InFloatKeys.������Ϣ1,
                InFloatKeys.������Ϣ2,
                InFloatKeys.������Ϣ3,
                InFloatKeys.������Ϣ4,
                InFloatKeys.������Ϣ5,
                InFloatKeys.������Ϣ6,
                InFloatKeys.������Ϣ7,
                InFloatKeys.������Ϣ8,
                InFloatKeys.������Ϣ9,
                InFloatKeys.������Ϣ10,
            };

            m_GradientDistanceIndexs = new[]
            {
                InFloatKeys.�¶ȵľ�����Ϣ1,
                InFloatKeys.�¶ȵľ�����Ϣ2,
                InFloatKeys.�¶ȵľ�����Ϣ3,
                InFloatKeys.�¶ȵľ�����Ϣ4,
                InFloatKeys.�¶ȵľ�����Ϣ5,
            };

            m_GradientSpeedIndexs = new[]
            {
                InFloatKeys.�¶���Ϣ1,
                InFloatKeys.�¶���Ϣ2,
                InFloatKeys.�¶���Ϣ3,
                InFloatKeys.�¶���Ϣ4,
                InFloatKeys.�¶���Ϣ5,
            };

            m_CaveatDistanceIndexs = new[]
            {
                InFloatKeys.Ԥ��ľ�����Ϣ1,
                InFloatKeys.Ԥ��ľ�����Ϣ2,
                InFloatKeys.Ԥ��ľ�����Ϣ3,
                InFloatKeys.Ԥ��ľ�����Ϣ4,
                InFloatKeys.Ԥ��ľ�����Ϣ5,
                InFloatKeys.Ԥ��ľ�����Ϣ6,
                InFloatKeys.Ԥ��ľ�����Ϣ7,
                InFloatKeys.Ԥ��ľ�����Ϣ8,
                InFloatKeys.Ԥ��ľ�����Ϣ9,
                InFloatKeys.Ԥ��ľ�����Ϣ10,
            };

            m_CaveatSpeedIndexs = new[]
            {
                InFloatKeys.Ԥ����Ϣ1,
                InFloatKeys.Ԥ����Ϣ2,
                InFloatKeys.Ԥ����Ϣ3,
                InFloatKeys.Ԥ����Ϣ4,
                InFloatKeys.Ԥ����Ϣ5,
                InFloatKeys.Ԥ����Ϣ6,
                InFloatKeys.Ԥ����Ϣ7,
                InFloatKeys.Ԥ����Ϣ8,
                InFloatKeys.Ԥ����Ϣ9,
                InFloatKeys.Ԥ����Ϣ10,
            };
        }

        /// <summary>
        /// D �� �ٶȼ�����
        /// </summary>
        /// <param name="g"></param>
        public void DrawAreaD(Graphics g)
        {
            //�ƻ���
            DrawPanRegion(g);

            #region ::::::::::: D6���������ź�

            /*
             * ���г�������CTCS-2����CTCS-3��ʱ��D6��������ʾ�����źţ��뾶Ϊ20�����ء�
             * �����źŵ���ʾ��׼������Ӧ�涨������ΪArial��СΪ14�����Ƽ�������ɫΪ��ɫ��
             */
            DrawAreaD6Signal(g);

            #endregion
        }

        /// <summary>
        /// D6 �������ź�
        /// </summary>
        /// <param name="g"></param>
        public void DrawAreaD6Signal(Graphics g)
        {
            var signal = m_ATPMainScreen.GetInFloatValue(InFloatKeys.�ź�);
            if ((signal >= 0) && (signal < 20))
            {
                if (Math.Abs(signal - 11) < float.Epsilon ||
                    Math.Abs(signal - 13) < float.Epsilon ||
                    Math.Abs(signal - 14) < float.Epsilon)
                {
                    g.DrawImage(m_ATPMainScreen.CurrentTime.Second % 2 == 0
                        ? SignalImages.��ɫ
                        : m_ATPMainScreen.UpdateSignalLamp(Convert.ToInt32(signal)), m_ATPMainScreen.m_RectsList[44]);
                }
                else
                {
                    g.DrawImage(m_ATPMainScreen.UpdateSignalLamp(Convert.ToInt32(signal)), m_ATPMainScreen.m_RectsList[44]);
                }

                g.DrawString(m_ATPMainScreen.m_SingleLampStr[Convert.ToInt32(signal)],
                    FontsItems.Font24YouB, SolidBrushsItems.BlackBrush,
                    m_ATPMainScreen.m_RectsList[44], FontsItems.TheAlignment(FontRelated.����));
            }
        }

        /// <summary>
        /// �ƻ���
        /// </summary>
        /// <param name="g"></param>
        public void DrawPanRegion(Graphics g)
        {
            if (m_ATPMainScreen.m_ShowPlanningArea)
            {
                for (var i = 0; i < 10; i++)
                {
                    m_ATPMainScreen.m_TheMrsp.InitalSpeedInfo[i].BrakingDistance =
                        m_ATPMainScreen.GetInFloatValue(m_BrakeDistanceIndexs[i]);
                    m_ATPMainScreen.m_TheMrsp.InitalSpeedInfo[i].BrakingSpeed = m_ATPMainScreen.GetProportionValue(m_ATPMainScreen.GetInFloatValue(m_BrakeSpeedIndexs[i]));

                    m_ATPMainScreen.m_TheMrsp.AllCaveatInfo[i].Distance = m_ATPMainScreen.GetInFloatValue(m_CaveatDistanceIndexs[i]);
                    m_ATPMainScreen.m_TheMrsp.AllCaveatInfo[i].CaveatType =
                      (MRSPCaveatType)(int)m_ATPMainScreen.GetInFloatValue(m_CaveatSpeedIndexs[i]);
                }
                for (var i = 0; i < 5; i++)
                {
                    m_ATPMainScreen.m_TheMrsp.AllGradientInfo[i].Distance = m_ATPMainScreen.GetInFloatValue(m_GradientDistanceIndexs[i]);
                    m_ATPMainScreen.m_TheMrsp.AllGradientInfo[i].Gradient = Convert.ToInt32(m_ATPMainScreen.GetInFloatValue(m_GradientSpeedIndexs[i]));
                }
                m_ATPMainScreen.m_TheMrsp.StartPointTSM = m_ATPMainScreen.GetInFloatValue(InFloatKeys.��ģ��λ��);
                m_ATPMainScreen.m_TheMrsp.IdOfNextStartPoint = Convert.ToInt32(m_ATPMainScreen.GetInFloatValue(InFloatKeys.��ģ�����λ��));
                m_ATPMainScreen.m_TheMrsp.Update();
                m_ATPMainScreen.m_TheMrsp.Paint(g);
            }
        }


    }
}