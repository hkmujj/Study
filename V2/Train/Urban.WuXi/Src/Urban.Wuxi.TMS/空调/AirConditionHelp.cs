using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Urban.Wuxi.TMS.�յ�
{
    /// <summary>
    /// �յ����ð���
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class AirConditionHelp : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "�յ����ð���";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics g)
        {
            //GetValue();
            DrawOn(g);
            base.paint(g);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void DrawOn(Graphics e)
        {
            e.DrawImage(m_Images[0], m_Rects[0]);
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// ��ʼ�����ꡢ���顢����
        /// </summary>
        private void InitData()
        {
            m_Rects = new RectangleF[20];

            m_Images = new Image[10];

            new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            m_Rects[0] = new RectangleF(0, 100, 798, 450);

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::

        /// <summary>
        /// ���ο�
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// ͼƬ��
        /// </summary>
        private Image[] m_Images;

        #endregion#
        #endregion
    }

}
