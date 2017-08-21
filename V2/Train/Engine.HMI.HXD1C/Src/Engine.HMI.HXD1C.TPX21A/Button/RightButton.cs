using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.HMI.HXD1C.TPX21A.Button
{
    [GksDataType(DataType.isMMIObjectClass)]
    class RightButton : baseClass
    {
        private readonly HxButton[] m_HButton = new HxButton[6];


        public override string GetInfo()
        {
            return "�ұ����ⰴť";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            nErrorObjectIndex = -1;

            return true;
        }

        public override void paint(Graphics g)
        {
            DrawOn(g);
        }
         
        public void InitData()
        {
            for (int i = 0; i <6; i++)
            {
                m_HButton[i] = new HxButton();
                m_HButton[i].SetButtonColor(192, 192, 192);
                m_HButton[i].SetButtonRect(HxCommon.Recposition.X + 805, HxCommon.Recposition.Y+50+70*i, 68, 60);
            }

            m_HButton[0].SetButtonText("ȡ��");
            m_HButton[1].SetButtonText("��");
            m_HButton[2].SetButtonText("��");
            m_HButton[3].SetButtonText("��");
            m_HButton[4].SetButtonText("��");
            m_HButton[5].SetButtonText("ȷ��");
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        public void GetValue()
        {
        }

        public void DrawOn(Graphics g)
        {
            for (int i = 0; i < 6; i++)
            {
                m_HButton[i].OnDraw(g);
            }
        }

        /// <summary>
        /// ��Ӧ��갴���¼�
        /// </summary>
        /// <param name="nPoint">��갴��ʱ�������</param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            for (int i = 0; i < 6; i++)
            {
                if (nPoint.X > m_HButton[i].m_RectPosition.X && nPoint.X <= m_HButton[i].m_RectPosition.Right &&
                    nPoint.Y > m_HButton[i].m_RectPosition.Y && nPoint.Y <= m_HButton[i].m_RectPosition.Bottom)
                {
                    m_HButton[i].OnButtonDown();
                    //append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[i], 1, 0);

                    return GlobalParam.Instance.NotifyButtonDownEvent((ButtonName)UIObj.InBoolList[i]);
                }
            }

            return true;
        }

        /// <summary>
        /// ��Ӧ��굯���¼�
        /// </summary>
        /// <param name="nPoint">��굯��ʱ�������</param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            for (int i = 0; i < 6; i++)
            {
                if (nPoint.X > m_HButton[i].m_RectPosition.X && nPoint.X <= m_HButton[i].m_RectPosition.Right &&
                    nPoint.Y > m_HButton[i].m_RectPosition.Y && nPoint.Y <= m_HButton[i].m_RectPosition.Bottom)
                {
                    m_HButton[i].OnButtonUp();
                    //append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[i], 0, 0);
                    return GlobalParam.Instance.NotifyButtonUpEvent((ButtonName)UIObj.InBoolList[i]);
                }
            }

            return true;
        }
    }
}
