using System;
using System.Drawing;
using System.Globalization;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.����;

namespace Motor.ATP._300T.����
{
    public class AreaBContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaBContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }

        /// <summary>
        /// B�����ٶ���Ϣ
        /// </summary>
        public void DrawAreaB(Graphics g,AreaBView bView = AreaBView.All)
        {
            if (bView.HasFlag(AreaBView.SpeedValue))
            {
                DrawAreaBSpeedValue(g);
            }

            if (bView.HasFlag(AreaBView.Cir))
            {
                DrawAreaBSpeedCir(g);
            }

            if (bView.HasFlag(AreaBView.ControlModel))
            {
                DrawAreaBControlModel(g);
            }
        }

        public void DrawAreaBControlModel(Graphics g)
        {
            #region ::::::::::: B7�� ����ģʽ

            /*
             * ��B7�������ַ�ʽ��ʾ�г�ģʽ��Ϣ��
             * �г�ģʽ��Ϣ����ʾ���������ʾ��
             * ����Ϊ��Բ��СΪ16�����Ƽ�������ɫΪ��ɫ��
             *--------------------------------------
             * ��� |    ģʽ           |  ��ʾ�ı�
             * -----|-------------------|-----------
             *   1  |    ��ȫ���ģʽ   |  ��ȫ
             * -----|-------------------|-----------
             *   2  |    ���ּ��ģʽ   |  ����
             * -----|-------------------|-----------
             *   3  |    Ŀ���г�ģʽ   |  Ŀ��
             * -----|-------------------|-----------
             *   4  |    ����ģʽ       |  ����
             * -----|-------------------|-----------
             *   5  |    ����ģʽ       |  ����
             * -----|-------------------|-----------
             *   6  |    ����ģʽ       |  ����
             * -----|-------------------|-----------
             *   7  |    ����ģʽ       |  ����
             * -----|-------------------|-----------
             *   8  |    �����ź�ģʽ   |  ����
             * -----|-------------------|-----------
             *   9  |    ����ģʽ       |  ����
             * -----|-------------------|-----------
             *  10  |    ð��ģʽ       |  ð��
             * -----|-------------------|-----------
             *  11  |    ð����ģʽ     |  ð��
             * -------------------------------------
             */
            if (m_ATPMainScreen.CurrentTrainMode != TrainMode.��)
            {
                g.DrawString(m_ATPMainScreen.CurrentTrainMode.ToString(), FontsItems.Font13YouB,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[27], FontsItems.TheAlignment(FontRelated.����));
            }

            #endregion
        }


        public void DrawAreaBSpeedValue(Graphics g)
        {
            #region ::::::::::: B1�� ���ַ�ʽ��ʾ���г��ٶ�

            /*
             * �ٶ�0�ȴ����Ĵ�ֱ���ϣ�-140�ȵĽǶ���ʾ0km/h��+140�ȵĽǶ���ʾ450km/h����-5�ȵĽǶ���ʾ150km/h��
             * �������ٶȿ̶ȵķ�ʽ�ֳ����Σ���һ�ΰ����ĽǶȴ�-140�ȵ�-5�ȣ���ʾ��0km/h��150km/h���ٶȣ�
             * �ڶ��ΰ����ĽǶȴ�-5�ȵ�+140�ȣ���ʾ��150km/h��450km/h���ٶ�
             * ����ΪArial��СΪ16�����Ƽ�������ɫΪ��ɫ��
             * ���̶��ߵĳ���Ϊ25�����أ��̶̿��ߵĳ���Ϊ15�����ء�
             * 
             * ��B1����������ʾ�г��ٶȣ�����ʼ�վ�����ʾ��
             * Բ�εĿ�����ٿ�����ʾ3λ���֣�ָ��ĳ��Ȳ�Ӧ����CSG�Ĺ������˿�Ȳ�Ӧ��CSG�Ĺ���
             * ����ΪArial��СΪ22�����Ƽ�������ɫΪ��ɫ
             * ���г��ٶȳ����ƶ��ٶ�ʱ��ָ��Ϊ��ɫ������Ϊ��ɫ
             * ------------------------------------------------------
             *    ����ģʽ   |         ����״̬          |   ��ɫ   
             * --------------|---------------------------|-----------
             *    |          |      Vtrain �� Vperm      |   ��ɫ
             *    |          |---------------------------|-----------
             *    |   CSM    |    Vperm < Vtrain ��Vint  |   ��ɫ
             *    |          |---------------------------|-----------
             *    |          |       Vtrain > Vint       |   ��ɫ
             *    |----------|---------------------------|-----------
             *    |          |      Vtrain �� Vperm      |   ��ɫ
             *    |          |---------------------------|-----------
             * FS |   TSM    |    Vperm < Vtrain ��Vint  |   ��ɫ
             *    |          |---------------------------|-----------
             *    |          |        Vtrain > Vint      |   ��ɫ
             *    |----------|---------------------------|-----------
             *    |  RSM     |      Vtrain �� Vrelease   |   ��ɫ
             *    |          |---------------------------|-----------
             *    |          |       Vtrain > Vrelease   |   ��ɫ
             * --------------|---------------------------|-----------
             *               |       Vtrain �� Vperm     |   ��ɫ
             *               |---------------------------|-----------
             *    ����ģʽ   |    Vperm < Vtrain ��Vint  |   ��ɫ
             *               |---------------------------|-----------
             *               |       Vtrain > Vint       |   ��ɫ
             * ------------------------------------------------------
             * Vtrain:��ǰ�ٶ�      Vperm:�����ٶ�;    Vint:��Ԥ�ٶ�;    Vrelease:�����ٶ�;     Vtarget:Ŀ���ٶ�
             * 
             */

            //����
            g.DrawImage(DialPointerImages.����_CTCS300T, m_ATPMainScreen.m_RectsList[17]);

            //ָ��
            m_ATPMainScreen.m_C3ThePointer[Math.Abs(m_ATPMainScreen.m_Vtrain) <= 150f ? 0 : 1].PaintPointerColor(g, m_ATPMainScreen.m_ThePointerImg, Math.Abs(m_ATPMainScreen.m_Vtrain));

            //���г��ٶȳ����ƶ��ٶ���ɫ��ʾ��ɫ
            g.DrawString(Convert.ToInt32(m_ATPMainScreen.m_Vtrain).ToString(CultureInfo.InvariantCulture), FontsItems.Font18DefB,
                m_ATPMainScreen.m_Vtrain > m_ATPMainScreen.m_Vint ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush,
                m_ATPMainScreen.m_RectsList[20], FontsItems.TheAlignment(FontRelated.����));

            #endregion
        }

        /// <summary>
        /// �����ٶȹ�
        /// </summary>
        /// <param name="g"></param>
        public void DrawAreaBSpeedCir(Graphics g)
        {
            #region ::::::::::: B2�� �����ٶȹ��

            /*
             * �����ٶȱ��̵���߽磬��-145�ȵ�+145����ʾ���ι�������ι���ĽǶȷ�Χ���ٶȿ̶ȵ��Դ󣬼�-145�ȵ�-140�Ⱥ�140�ȵ�145��������ʾ����������߽硣
             * ���ι����9�����ص㣬�����ٶȴ��Ŀ��Ϊ20�����ص㣨����Ĳ��ֽ����������������ӵĳߴ�ӦΪ6��20��
             * 
             * ���г�δ��������£���ʾ���Բ�ͬ��ɫ�Ĺ�����ٶȱ�ı�Ȧ����ʾĿ���ٶȺ������ٶȡ�
             * 
             * ��CSM �����г�δ��������£�Ŀ���ٶȹ���԰���ɫ��ʾ�������ٶȹ���Ի�ɫ��ʾ��
             * 
             * ��TSM �����г�δ��������£������ٶȹ���Ի�ɫ��ʾ��
             * 
             * �г��ٶȳ��������ٶȡ���δ����SBI������£���ʼ�Թ����ʽ��ʾSBI�ٶȣ��������ٶȵ�SBI֮�����Ŀ��Ϊ���������ȵ���������ʾ��ɫΪ��ɫ��
             * 
             * �г��ٶȳ���SBI�� EBIʱ���Թ����ʽ��ʾ�����ƶ��ٶȣ��������ٶȵ������ƶ��ٶ�֮�����Ŀ��Ϊ���������ȵ���������ɫΪ��ɫ��
             * ���ߣ��г��ٶȳ�����Ԥ�ٶȣ�Vint��ʱ���Թ����ʽ��ʾ��Ԥ�ٶȣ���ɫΪ��ɫ��
             * 
             * �ڲ�ͬ״̬�»��ι������ʾ��������ɫ�����ʾ
             * --------------------------------------------------------------------------
             *    ����ģʽ   |         ����״̬          |             CSG��ɫ  
             *               |                           |--------------------------------
             *               |                           |   Vperm   |  Vtarget  |   Vint 
             * --------------|---------------------------|-----------|-----------|--------
             *    |          |      Vtrain �� Vperm      |    ��ɫ   |   ����ɫ  |  ����ʾ
             *    |          |---------------------------|-----------|-----------|--------
             *    |   CSM    |    Vperm < Vtrain ��Vint  |    ��ɫ   |   ����ɫ  |   ��ɫ
             *    |          |---------------------------|-----------|-----------|--------
             *    |          |       Vtrain > Vint       |    ��ɫ   |   ����ɫ  |   ��ɫ
             *    |----------|---------------------------|-----------|-----------|--------
             *    |          |      Vtrain �� Vperm      |    ��ɫ   |   ����ɫ  |  ����ʾ
             *    |          |---------------------------|-----------|-----------|--------
             * FS |   TSM    |    Vperm < Vtrain ��Vint  |    ��ɫ   |   ����ɫ  |   ��ɫ
             *    |          |---------------------------|-----------|-----------|--------
             *    |          |        Vtrain > Vint      |    ��ɫ   |   ����ɫ  |   ��ɫ
             *    |----------|---------------------------|-----------|-----------|--------
             *    |  RSM     |      Vtrain �� Vrelease   |    ��ɫ   |   ����ʾ  |  ����ʾ
             *    |          |---------------------------|-----------|-----------|--------
             *    |          |       Vtrain > Vrelease   |    ��ɫ   |   ����ʾ  |  ����ʾ
             * --------------|---------------------------|--------------------------------
             *     ����ģʽ  |         ����״̬          |            ������
             * ----------------------------------------------------------------------------
             */

            //�������̱߽�Ĳ���
            //if (ReturnModeAndRunStatus(_theControlMode) < 40)
            //    _C3_theOutCircle[0].PaintCircle(e, 5, 0, PenItems.DarkGrayPen10);


            var theNumbOfControlMode = m_ATPMainScreen.ReturnModeAndRunStatus(m_ATPMainScreen.m_TheControlMode);

            #region :::::::::::::::: ��Ԥ�ٶȹ�� Vint

            if (theNumbOfControlMode != 11 && theNumbOfControlMode != 22 && theNumbOfControlMode < 30)
            {
                m_ATPMainScreen.m_C3TheVintCircle[0].PaintCircle(g, m_ATPMainScreen.m_Vint > 150 ? 150 : m_ATPMainScreen.m_Vint, m_ATPMainScreen.m_Vperm > 150 ? 150 : m_ATPMainScreen.m_Vperm,
                    (theNumbOfControlMode == 13 || theNumbOfControlMode == 23)
                        ? PenItems.OrangePen15
                        : PenItems.RedPen15);
                if (m_ATPMainScreen.m_Vint > 150)
                {
                    m_ATPMainScreen.m_C3TheVintCircle[1].PaintCircle(g, m_ATPMainScreen.m_Vint, m_ATPMainScreen.m_Vperm,
                        (theNumbOfControlMode == 13 || theNumbOfControlMode == 23)
                            ? PenItems.OrangePen15
                            : PenItems.RedPen15);
                }
            }

            #endregion

            #region ::::::::::::::::: �����ٶȹ�� Vperm

            if (theNumbOfControlMode < 40)
            {
                if (m_ATPMainScreen.CurrentTrainMode != TrainMode.Ŀ�� && m_ATPMainScreen.CurrentTrainMode != TrainMode.���� &&
                    m_ATPMainScreen.CurrentTrainMode != TrainMode.���� && m_ATPMainScreen.CurrentTrainMode != TrainMode.����)
                {
                    //�������̱߽�Ĳ���
                    m_ATPMainScreen.m_C3TheOutCircle[0].PaintCircle(g, 5, 0, PenItems.DarkGrayPen10);

                    m_ATPMainScreen.m_C3TheVpermCircle[0].PaintCircle(g, m_ATPMainScreen.m_Vperm > 150 ? 150 : m_ATPMainScreen.m_Vperm,
                        theNumbOfControlMode == 11
                            ? PenItems.GrayPen9
                            : (theNumbOfControlMode == 34 ? PenItems.RedPen9 : PenItems.YellowPen9));
                    if (m_ATPMainScreen.m_Vperm > 150)
                    {
                        m_ATPMainScreen.m_C3TheVpermCircle[1].PaintCircle(g, m_ATPMainScreen.m_Vperm,
                            theNumbOfControlMode == 11
                                ? PenItems.GrayPen9
                                : (theNumbOfControlMode == 34 ? PenItems.RedPen9 : PenItems.YellowPen9));
                    }
                }

                if (m_ATPMainScreen.m_Vperm >= 0)
                {
                    m_ATPMainScreen.m_C3TheHookCircle[m_ATPMainScreen.m_Vperm > 150 ? 1 : 0].PaintCircleHook(g, m_ATPMainScreen.m_Vperm, 5,
                        theNumbOfControlMode == 11
                            ? PenItems.GrayPen18
                            : (theNumbOfControlMode == 34 ? PenItems.RedPen18 : PenItems.YellowPen9));
                }
            }

            #endregion

            #region ::::::::::::::::::: Ŀ���ٶȹ�� Vtarget

            if (theNumbOfControlMode < 30)
            {
                m_ATPMainScreen.m_C3TheVtargetCircle[0].PaintCircle(g, m_ATPMainScreen.m_Vtarget > 150 ? 150 : m_ATPMainScreen.m_Vtarget, PenItems.DarkGrayPen10);
                if (m_ATPMainScreen.m_Vtarget > 150)
                {
                    m_ATPMainScreen.m_C3TheVtargetCircle[1].PaintCircle(g, m_ATPMainScreen.m_Vtarget, PenItems.DarkGrayPen10);
                }
            }

            #endregion

            #endregion

            #region ::::::::::: B3��/B4��/B5�� ����ͼ��

            /*
             * ����ͼ�갴�����ҵ�˳����ʾ��B3/4/5�����ڡ�
             * ��һ������ռ�ú󣬼����һ�����Ƿ�Ҳ��ռ�ã�
             * ������������ѱ�ռ�ã����ɵȴ��б�
             * ��Ҫ��˾��ִ��һ������ʱ���û�ɫͼ����ʾ�������л�ɫ��˸�߿򣬱߿�Ŀ��Ϊ1�����أ���˸Ƶ��Ϊ1Hz��ִ���˸ö�������ʾΪ��ɫ��
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb��������)])
            {
                g.DrawImage(PlanImages.M6, m_ATPMainScreen.m_RectsList[25]);
            }

            #endregion

            #region ::::::::::: B6�� �����ٶ�(Ԥ��)

            /*
             * ���г����п����ٶȲ���������ʱ����B6����B2����ʾ�����ٶȣ�B6�������ַ�ʽ��ʾ������ΪArial��СΪ16�����Ƽ�������ɫΪ��ɫ
             * ��ȫ���ģʽ��Чʱ������г����п����ٶȣ������ַ�ʽ�ͻ����ٶȹ����ʾ�����ٶȣ�����������ģʽ�£�ֻ�����ַ�ʽ��ʾ�����ٶȣ�
             * ������ʾ�����ٶȵ���������ɫ������ʾ��
             * --------------------------------------------------------------------
             *    ������     |         ����״̬          |   ������ʾ   |  ������ʾ   
             * --------------|---------------------------|--------------|----------
             *    |   CSM    |         ����״̬          |    ����ʾ    |   ����ʾ
             *    |----------|---------------------------|--------------|----------
             *    |          |      Vtrain �� Vperm      |     ��ɫ     |   ��ɫ
             *    |          |---------------------------|--------------|----------
             *    |   TSM    |    Vperm < Vtrain ��Vint  |     ��ɫ     |   ��ɫ
             *    |          |---------------------------|--------------|----------
             * FS |          |       Vtrain > Vint       |     ��ɫ     |   ��ɫ
             *    |----------|---------------------------|--------------|----------
             *    |          |     Vtrain �� Vrelease    |     ��ɫ     |   ��ɫ
             *    |   RSM    |---------------------------|--------------|----------
             *    |          |      Vtrain > Vrelease    |     ��ɫ     |   ��ɫ
             * --------------|---------------------------|--------------|----------
             *               |      Vtrain �� Vperm      |    ����ʾ    |   ����ʾ
             *   ����ģʽ    |---------------------------|--------------|----------
             *               |      Vtrain >  Vperm      |    ����ʾ    |   ��ɫ
             * --------------------------------------------------------------------
             */

            #endregion
        }
    }
}