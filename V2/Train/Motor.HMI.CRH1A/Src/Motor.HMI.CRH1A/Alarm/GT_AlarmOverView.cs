using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Alarm
{
    /// <summary>
    /// �������ܲ˵� ����ǰ�Ļ�¼�����Ԫ���� ���������ť��ʾ�õ�Ԫ��ϸ�ֹ��� 
    /// ������Ԫ��Ű��������� �������µ�˳����
    /// </summary>  
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_AlarmOverView : CRH1BaseClass
    {
        #region ˽���ֶ�
        public GT_MenuTitle Title = new GT_MenuTitle("��������");//�˵�����
        public Rectangle Recposition = new Rectangle(0, 165, 70, 40);
        public Rectangle Rect;//�˵����Ĵ���ο�
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(119, 136, 153));//�˵�������ˢ
        public Pen Pen = new Pen(Color.FromArgb(217, 223, 229), 3);
        public CRH1AButton[] GButton = new CRH1AButton[14];

        public string[] Buttontext = new string[14] { "�� ѹ", "ǣ ��", "����", "���ƺ�ͨѶ", "��������", "�� ��", "�յ�", "ǰ ��", "��ع���", "�� ��",     
            "�� �� ��","�ᱨ","��������","��Ϣϵͳ" };//��ť�ı�������
        public int Weight = 100;
        public int Height = 50;
        //public static bool[] selected_Unit = new bool[15] { false, false, false, false, false, false, false, false, false, false, false, false, false, false,true };//��ѡ�еĵ�Ԫ��
        public static bool[] IsActive = new bool[14];//���������ӦλΪһ��ʾ�õ�Ԫ���ڻ�¼�
        // public Dictionary<int,ExceptionData> activeEvent=new Dictionary<int,ExceptionData>();
        #endregion

        #region ��̬�ֶ�
        public static int SelectedUnitView = -1;
        #endregion

        #region ���ط���
        public override string GetInfo()
        {
            return "��������";
        }

        public override bool Initialize()
        {
            //3
            InitData();

            return true;
        }
        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

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
        #endregion

        #region ˽�з���
        private void GetValue()
        {

            for (int j = 0; j < 14; j++)
            {
                if (!GT_GalobalFaultManage.Instance.HasSuredActiveFault())
                {
                    IsActive[j] = false;
                }
                else
                {
                    var allFault = GT_GalobalFaultManage.Instance.AllActiveFaults;
                    foreach (ExceptionData exData in allFault)
                    {
                        if (exData.ExUnit - 1 == j)
                        {
                            IsActive[j] = true;
                            break;
                        }
                        else
                        {
                            IsActive[j] = false;
                        }
                    }

                }
            }

            for (int i = 0; i < 14; i++)
            {
                if (IsActive[i])
                {
                    GButton[i].SetButtonColor(255, 255, 0);//�õ�Ԫ�л�¼�ʱ��ť����Ϊ��ɫ 
                }
                else
                {
                    GButton[i].SetButtonColor(192, 192, 192);//�õ�Ԫû�л�¼�ʱ��ť����Ϊ��ɫ
                }
            }
        }

        private void InitData()
        {
            Rect = new Rectangle(Recposition.X + 3, Recposition.Y, 785, 285);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i * 4 + j < 14)
                    {
                        GButton[i * 4 + j] = new CRH1AButton();
                        GButton[i * 4 + j].SetButtonRect(Rect.X + j * (Weight + 60) + 100, Rect.Y + i * (Height + 20) + 10, Weight, Height);
                        GButton[i * 4 + j].SetButtonText(Buttontext[i * 4 + j]);
                    }
                }
            }
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var button in GButton)
                {
                    button.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
            };
          
        }

        /// <summary>
        /// ���Ʒ���
        /// </summary>
        /// <param name="g"></param>
        public void DrawOn(Graphics g)
        {
            //���Ʋ˵�����
            Title.OnDraw(g);
            g.FillRectangle(Brush, Rect);
            g.DrawRectangle(Pen, Rect);
            for (int i = 0; i < 14; i++)
            {
                GButton[i].OnDraw(g);
            }
        }

        /// <summary>
        /// ��Ӧ��ť�����¼�
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void OnButtonDown(int x, int y)
        {

            for (int i = 0; i < 14; i++)
            {
                if (GButton[i].OutLineRectangle.Contains(x, y) && GButton[i].IsEnable)
                {
                    if (IsActive[i])
                    {
                        GButton[i].OnButtonDown();
                    }
                }
            }
        }

        /// <summary>
        /// ��Ӧ��ť�����¼�
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 14; i++)
            {
                if (GButton[i].OutLineRectangle.Contains(x, y) && GButton[i].IsEnable)
                {
                    if (IsActive[i])
                    {
                        SelectedUnitView = i + 1;
                        OnPost(CmdType.ChangePage, 23, 0, 0);
                        GButton[i].OnButtonUp();
                    }
                }
            }
        }
        #endregion
    }
}