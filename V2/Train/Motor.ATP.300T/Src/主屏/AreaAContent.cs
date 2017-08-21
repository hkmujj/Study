using System;
using System.Drawing;
using System.Globalization;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.����;

namespace Motor.ATP._300T.����
{
    public class AreaAContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaAContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }

        /// <summary>
        /// A������������Ϣ
        /// </summary>
        public void DrawAreaA(Graphics g)
        {
            #region ::::::::::: A1�����ƶ�Ԥ��ʱ��

            /* 
             * �ƶ�Ԥ��ʱ����A1��������������ͼ����ʾ��ͼ��Ĵ�Сȡ���ھഥ���ƶ���Ԥ��ʱ�䣨8s����
             * һ���ഥ���ƶ���Ԥ��ʱ������趨ʱ��ֵ������ͼ�꿪ʼ���ֱ�����ߴ硣
             */
            g.FillRectangle(
                m_ATPMainScreen.GetBreakWarning(Convert.ToInt32(m_ATPMainScreen.GetInFloatValue(InFloatKeys.�ƶ�Ԥ��ʱ��)))
                    .BreakWarmingBrush,
                m_ATPMainScreen.GetBreakWarning(
                    Convert.ToInt32(m_ATPMainScreen.GetInFloatValue(InFloatKeys.�ƶ�Ԥ��ʱ��))).BreakWarningRect);

            #endregion

            #region ::::::::::: A2����Ŀ�����

            /*
             * A2��ʹ�����ַ�����ʾĿ����룺��״�����ʾ�������ֱ�ʾ����
             * ��״������ߴ�Ϊ15��172����ɫ��Ϊ��ɫ��������Ϸ�Ϊ���ֱ�ʾ������λΪ�ס�
             * ��״��������Ϊ����ϵ�̶ȣ�������ϵ���ö������꣨0-100�ײ����������꣩�������ʾ��Χ��1000�ס�
             * ����ΪArial��СΪ16�����Ƽ�������ɫΪ��ɫ��
             * ��Ŀ��������1000��ʱ������Ϸ�������ʾ5�����֣���������ʾʵ��Ŀ����룬��״����ĸ߶ȱ��ֲ��䣬������ʾ�ľ���Ϊ10�ף�
             * ��Ŀ�����С��1000��ʱ����״��������̣�������ʾ�ľ���Ϊ1��
             * ���г�����Ŀ���ٶȼ�����ʱ��A2��������ʾ�����г����ڶ����ٶȼ�����ʱA2������ʾ��
             *--------------------------------------------------------
             * ������      |    ����״̬     |  Ŀ������Ƿ���ʾ
             * ------------|-----------------|------------------------
                   |CSM    |    ȫ��״̬     |      ����ʾ
             *     |-------|-----------------|------------------------
                FS |TSM    |    ȫ��״̬     |       ��ʾ
             *     |-------|-----------------|------------------------
                   |RSM    |    ȫ��״̬     |       ��ʾ
             * ------------|-----------------|------------------------
                ����ģʽ   |    ȫ��״̬     |      ����ʾ
             * -------------------------------------------------------
             */
            if ((m_ATPMainScreen.m_TheControlMode == ControlMode.TSM || m_ATPMainScreen.m_TheControlMode == ControlMode.RSM) &&
                (m_ATPMainScreen.CurrentTrainMode == TrainMode.��ȫ || m_ATPMainScreen.CurrentTrainMode == TrainMode.����))
            {


                g.DrawImage(ComImages.Ŀ�����, m_ATPMainScreen.m_RectsList[5]);

                m_ATPMainScreen.m_TargetSpeed.ForEach(e => e.CurrentValue = 0);
                for (var i = 0; i < 10; i++)
                {
                    if (m_ATPMainScreen.m_TarDistance > (i + 1) * 100)
                    {
                        m_ATPMainScreen.m_TargetSpeed[i].CurrentValue = 100;
                    }
                    else if (m_ATPMainScreen.m_TarDistance > i * 100 && m_ATPMainScreen.m_TarDistance <= (i + 1) * 100)
                    {
                        m_ATPMainScreen.m_TargetSpeed[i].CurrentValue = m_ATPMainScreen.m_TarDistance - i * 100;
                    }
                }

                m_ATPMainScreen.m_TargetSpeed.ForEach(e => e.OnDraw(g));
                var s = m_ATPMainScreen.m_TargetSpeed.FindAll(f => f.OutLineRectangle.Height != 0);
                if (s.Count != 0)
                {
                    g.FillRectangle(new SolidBrush(s[0].BackgroundColor), ATPMainScreen.GetRec(s));
                }
                g.DrawString(Convert.ToInt32(m_ATPMainScreen.m_TarDistance).ToString(CultureInfo.InvariantCulture),
                    FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[4],
                    FontsItems.TheAlignment(FontRelated.����));
            }

            #endregion

            #region ::::::::::: A3����Ԥ��

            /*
             */

            #endregion
        }
    }
}