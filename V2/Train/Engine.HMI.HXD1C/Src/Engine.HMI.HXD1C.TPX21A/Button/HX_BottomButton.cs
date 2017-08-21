using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.HMI.HXD1C.TPX21A.Button
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class HX_BottomButton : baseClass
    {
        private readonly HxButton[] m_HButton = new HxButton[10];
        private readonly HxButton[] m_HelpButton = new HxButton[2];

        public static HX_BottomButton Instance { private set; get; }

        public override string GetInfo()
        {
            return "�ײ����ⰴť";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            Instance = this;

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
            for (int i = 0; i < 10; i++)
            {
                m_HButton[i] = new HxButton();
                m_HButton[i].SetButtonColor(192, 192, 192);

                if (i < 9)
                {
                    m_HButton[i].SetButtonText((i + 1).ToString());
                }
                else
                {
                    m_HButton[i].SetButtonText("0");
                }

                m_HButton[i].SetButtonRect(HxCommon.Recposition.X + 90 + i * 71, HxCommon.Recposition.Y + 605, 68, 50);
            }

            for (int i = 0; i < 2; i++)
            {
                m_HelpButton[i] = new HxButton();
                m_HelpButton[i].SetButtonColor(192, 192, 192);
                m_HelpButton[i].SetButtonRect((int)m_HButton[i + 7].m_RectPosition.X, (int)m_HButton[i].m_RectPosition.Bottom + 10, 68, 50);
            }
            m_HelpButton[0].SetButtonText("����");
            m_HelpButton[1].SetButtonText("����");

        }


        public void DrawOn(Graphics g)
        {
            for (int i = 0; i < 10; i++)
            {
                m_HButton[i].OnDraw(g);
                if (i < 2)
                {
                    m_HelpButton[i].OnDraw(g);
                }
            }
        }

        /// <summary>
        /// ��Ӧ��갴���¼�
        /// </summary>
        /// <param name="point">��갴��ʱ�������</param>
        /// <returns></returns>
        public override bool mouseDown(Point point)
        {
            for (int i = 0; i < 10; i++)
            {
                if (point.X > m_HButton[i].m_RectPosition.X && point.X <= m_HButton[i].m_RectPosition.Right &&
                    point.Y > m_HButton[i].m_RectPosition.Y && point.Y <= m_HButton[i].m_RectPosition.Bottom)
                {
                    m_HButton[i].OnButtonDown();
                    //append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[i], 1, 0);

                    return GlobalParam.Instance.NotifyButtonDownEvent((ButtonName) UIObj.InBoolList[i]);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (point.X > m_HelpButton[i].m_RectPosition.X && point.X <= m_HelpButton[i].m_RectPosition.Right &&
                    point.Y > m_HelpButton[i].m_RectPosition.Y && point.Y <= m_HelpButton[i].m_RectPosition.Bottom)
                {
                    m_HelpButton[i].OnButtonDown();
                    //append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[i + 10], 1, 0);
                    return GlobalParam.Instance.NotifyButtonDownEvent((ButtonName)UIObj.InBoolList[i + 10]);
                }
            }

            return true;
        }

        /// <summary>
        /// ��Ӧ��굯���¼�
        /// </summary>
        /// <param name="point">��굯��ʱ�������</param>
        /// <returns></returns>
        public override bool mouseUp(Point point)
        {
            for (int i = 0; i < 10; i++)
            {
                if (point.X > m_HButton[i].m_RectPosition.X && point.X <= m_HButton[i].m_RectPosition.Right &&
                    point.Y > m_HButton[i].m_RectPosition.Y && point.Y <= m_HButton[i].m_RectPosition.Bottom)
                {
                    m_HButton[i].OnButtonUp();
                    //append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[i], 0, 0);
                    return GlobalParam.Instance.NotifyButtonUpEvent((ButtonName)UIObj.InBoolList[i]);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (point.X > m_HelpButton[i].m_RectPosition.X && point.X <= m_HelpButton[i].m_RectPosition.Right &&
                    point.Y > m_HelpButton[i].m_RectPosition.Y && point.Y <= m_HelpButton[i].m_RectPosition.Bottom)
                {
                    m_HelpButton[i].OnButtonUp();
                    //append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[i + 10], 0, 0);
                    return GlobalParam.Instance.NotifyButtonUpEvent((ButtonName)UIObj.InBoolList[i + 10]);
                }
            }

            return true;
        }
    }
}
