using System.Drawing;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.����;
using Motor.ATP._300T.����.���ܼ���˵�;

namespace Motor.ATP._300T.����
{
    public class AreaCContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaCContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }

        private readonly Image[] m_C3BrakeImages = {
            BrakeImages.C3_�����ƶ�,
            BrakeImages.C3_������ƶ�,
            BrakeImages.C3_�ļ��ƶ�,
            BrakeImages.C3_һ���ƶ�,
            BrakeImages.C3_������,

        };

        private readonly Image[] m_C2BrakeImages = {
            BrakeImages.C2_�����ƶ�,
            BrakeImages.C2_������ƶ�,
            BrakeImages.C2_�ļ��ƶ�,
            BrakeImages.C2_һ���ƶ�,
            BrakeImages.C2_������,
        };

        /// <summary>
        /// C���������ʻ��Ϣ
        /// </summary>
        /// <param name="g"></param>
        public void DrawAreaC(Graphics g)
        {

            #region ::::::::::: C1������һ����ģʽ��Ϣ

            /*
             * ��C1���Դ���ɫ�����߿��ģʽ�ı�����ʾҪȷ�ϵĸ���ģʽ����˸Ƶ��Ϊ1 Hz���Ա�˾��ȷ����һ��Чģʽ��
             * ��˾������ȷ�ϣ������пس����豸���ܺ󣬸�ͼ���C1����ʧ����ΪB7����ʾ���µ���Ч����ģʽ�����ϱ���ʾ��
             * ����Ϊ��Բ��СΪ18�����Ƽ�������ɫΪ��ɫ��
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inbð����˸)])
            {
                g.DrawString(
                    "ð��",
                    FontsItems.Font18DefB, SolidBrushsItems.WhiteBrush,
                    RectangleF.Union(m_ATPMainScreen.m_RectsList[32], m_ATPMainScreen.m_RectsList[31]),
                    FontsItems.TheAlignment(FontRelated.����));
                if (m_ATPMainScreen.CurrentTime.Second%2 == 0)
                {
                    g.DrawRectangle(PenItems.YellowPen2,
                        Rectangle.Round(RectangleF.Union(m_ATPMainScreen.m_RectsList[32],
                            m_ATPMainScreen.m_RectsList[31])));
                }
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbԽ����˸)])
            {
                g.DrawString(
                    "Խ��",
                    FontsItems.Font18DefB, SolidBrushsItems.WhiteBrush,
                    RectangleF.Union(m_ATPMainScreen.m_RectsList[32], m_ATPMainScreen.m_RectsList[31]),
                    FontsItems.TheAlignment(FontRelated.����));
                if (m_ATPMainScreen.CurrentTime.Second%2 == 0)
                {
                    g.DrawRectangle(PenItems.YellowPen2,
                        Rectangle.Round(RectangleF.Union(m_ATPMainScreen.m_RectsList[32],
                            m_ATPMainScreen.m_RectsList[31])));
                }
            }

            #endregion

            #region ::::::::::: C2/3/4����Ԥ��

            /*
             */

            #endregion

            #region ::::::::::: C5/6/7����Ԥ��

            /*
             */

            #endregion

            #region ::::::::::: C8����CTCS�ȼ�

            /*
             * �����ֵķ�ʽ��ʾ�пس����豸�����еȼ�������Ϊ��Բ��СΪ14�����Ƽ�������ɫΪ��ɫ��
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbCTCS2)])
            {
                g.DrawString("CTCS 2", FontsItems.Font12YouB,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[10],
                    FontsItems.TheAlignment(FontRelated.����));
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbCTCS3)])
            {
                g.DrawString("CTCS 3", FontsItems.Font12YouB,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[10],
                    FontsItems.TheAlignment(FontRelated.����));
            }

            #endregion

            #region ::::::::::: C9�����пس����豸�ƶ�״̬

            /*
             * ��ͼ��ķ�ʽ��ʾ�пس����豸�ƶ�״̬��
             * DMI�����пس����豸���ƶ�״̬��ʾͼ�ꡣ
             * ����пس����豸���ڷ��ƶ���������״̬�����������ʾ�κ�ͼ�ꡣ
             */
            switch (m_ATPMainScreen.CurrentSignalSystem)
            {
                case SignalSystem.CTCS3:
                    for (var i = 0; i < 5; i++)
                    {
                        if (m_ATPMainScreen.BoolList[m_ATPMainScreen.m_BrakeIndexs[i]])
                        {
                            g.DrawImage(m_C3BrakeImages[i], m_ATPMainScreen.m_RectsList[11]);
                        }
                    }
                    break;
                case SignalSystem.CTCS2:
                    for (var i = 0; i < 5; i++)
                    {
                        if (m_ATPMainScreen.BoolList[m_ATPMainScreen.m_BrakeIndexs[i]])
                        {
                            g.DrawImage(m_C2BrakeImages[i], m_ATPMainScreen.m_RectsList[11]);
                        }
                    }
                    break;
            }

            #endregion
        }
    }
}