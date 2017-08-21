using System.Drawing;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.Train;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.Test
{
    /// <summary>
    /// �Ʋ������� �˲˵��Ĺ����ǵ��г����ھ�ֹ״̬ʱ�Կ��Ƶƺ;������в���
    /// ���ԴӲ˵�������Ӳ˵�
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_LampTest : CRH1BaseClass
    {
        public Rectangle Recposition = new Rectangle(0, 170, 791, 280);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 152));
        public Pen BackPen = new Pen(Color.FromArgb(199, 215, 232), 3);
        public SolidBrush StrBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Font Strfont = new Font("Arial", 11);
        public CRH1AButton[] GButton = new CRH1AButton[3];

        /// <summary>
        /// ������ɱ�־
        /// </summary>
        private bool[] m_IsTestFinishFlag = new bool[3];

        /// <summary>
        /// �Ƿ��ڽ��б���ʵ�� true��ʾ����ʵ�� false ��ʾ����ʵ��
        /// </summary>
        private bool m_IsJinBaoBeginOrEndTest = false;

        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "�Ʋ���";
        }


        public override bool Initialize()
        {
            //3
            InitDate();

            return true;
        }


        public override void paint(Graphics g)
        {
            if (GlobalParam.Instance.TrainInfo.TrainState != TrainState.Stop)
            {
                OnPost(CmdType.ChangePage, (int)ViewConfig.MainView, 1, 0);
            }
            GetValue();
            RefreshButtonStatu();
            DrawOn(g);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    m_IsTestFinishFlag[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
        }
        public void InitDate()
        {
            for (int i = 0; i < 3; i++)
            {
                GButton[i] = new CRH1AButton();
                GButton[i].SetButtonRect(Recposition.X + 125 + i * 225, Recposition.Y + 130, 100, 60);
            }
            GButton[0].SetButtonText("��ȫ����");
            GButton[1].SetButtonText("�޵���");
            GButton[2].SetButtonText("����������");
        }

        public void DrawOn(Graphics e)
        {
            e.FillRectangle(BackBrush, Recposition);
            e.DrawRectangle(BackPen, Recposition);
            for (int i = 0; i < 3; i++)
            {
                //if (_isTestFinishFlag[i])
                //{
                //    G_Button[i].SetButtonColor(255, 255, 0);
                //}
                //else
                //{
                //    G_Button[i].SetButtonColor(192, 192, 192);
                //}
                GButton[i].OnDraw(e);
            }

        }

        private void RefreshButtonStatu()
        {
            //ˢ�µƲ��԰�ť״̬
            for (int index = 0; index < 3; index++)
            {
                if (m_IsTestFinishFlag[index])
                {
                    GButton[index].SetButtonColor(255, 255, 0);
                }
                else
                {
                    GButton[index].SetButtonColor(192, 192, 192);
                }
            }
        }
        #endregion#
        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }

        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point.X, point.Y);
            return true;
        }

        public void OnButtonDown(int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    if (i < 2)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 1, 0);
                    }
                    else
                    {
                        if (m_IsJinBaoBeginOrEndTest)//����ֹͣʵ��״̬
                        {
                            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 1, 0);//���Ϳ�ʼʵ���־
                            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i + 1], 0, 0);//���Ϳ�ʼʵ���־
                            m_IsJinBaoBeginOrEndTest = false;
                        }
                        else//���ڽ���ʵ��״̬
                        {
                            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 0, 0);//���ͽ���ʵ���־
                            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i + 1], 1, 0);//���ͽ���ʵ���־
                            m_IsJinBaoBeginOrEndTest = true;
                        }
                    }
                    GButton[i].OnButtonDown();
                }
            }
        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    if (i < 2)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 0, 0);
                    }
                    GButton[i].OnButtonUp();
                }
            }
        }
    }
}