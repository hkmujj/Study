using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
    /// �ƶ��������� �˲˵��Ĺ����ǵ��г����ھ�ֹ״̬ʱʵʩ�ƶ�����
    /// ���ԣ����Դ����˵�����ò˵�
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class GT_BrakeTest : CRH1BaseClass
    {
        #region ������Ϣ

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        private List<TestToolInfo> m_TestToolInfos = new List<TestToolInfo>();

        /// <summary>
        /// ��ʾ������ʾ��Ϣ
        /// </summary>
        private string m_ShowTestToolInfo = string.Empty;

        #endregion

        #region ˽���ֶ�

        private GT_MenuTitle m_Title = new GT_MenuTitle("�ƶ�����"); //���ò˵�����
        public Rectangle Recposition = new Rectangle(0, 30, 800, 100);
        public Rectangle[] TrainRect = new Rectangle[8]; //����λ��
        public int Weight = 90; //���Ŀ��
        public int Height = 70; //���ĸ߶�
        private Rectangle m_Rect; //ҳ������
        private CRH1AButton m_GButton;

        public Font Strfont = new Font("Arial", 9);
        public Font Strfont1 = new Font("Arial", 11);
        public SolidBrush StrBrush = new SolidBrush(Color.FromArgb(135, 135, 135));
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(192, 192, 192));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        public Pen BackPen = new Pen(Color.FromArgb(86, 103, 121), 2);
        public Pen GreenlPen = new Pen(Color.FromArgb(57, 149, 56), 2);
        public Pen RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        public Image[] Image = new Image[10];
        public bool[] Valueb = new bool[37];

        public string[] StrText = new string[]
                                   {
                                       " ", "�ƿز�������δ�ﵽ---�����ֱ�����'0'λ", "�ƿز�������δ�ﵽ---�����ѹ����", "�ƿز�������δ�ﵽ---�����ƶ�ʵʩ", "�ƿز�������δ�ﵽ---ͣ���ƶ�δʵʩ",
                                       "�ƿز�������δ�ﵽ---����һ�����ڵ��ƶ�\n�����룬�����IDU�����ƶ����ԡ�", "�ƿز�������δ�ﵽ---ͣ���ƶ����ܲ�����\nʵʩ�ƶ����ԣ����ͣ���ƶ�,����ܽ���\n����������ʵʩ�ֶ��ƶ�����",
                                       "�����ؿ����ֱ�����7��", "�����ؿ����ֱ�����0��", "�����ؿ����ֱ�����8��(�����ƶ�)", "�ƶ�����ʧ��---�ƶ�������ȷ����", "�ƶ�����ʧ��---�ƶ�������ȷʩ��", "�ƶ��������ظ�����,��δ�ɹ�---ʵʩ\n�ֹ�����",
                                       "�ƶ�����ʧ��---�г�δ���ھ�ֹ״̬", "�ƶ�����ʧ��---�ƶ�����ʱ������,����\n�����ƶ����ԡ�", "�ƶ�����ʧ��---ͣ���ƶ�δʵʩ��", "�ƶ�����ʧ��---�����ѹ����",
                                       "�ƶ�����ʧ��---��ȫ��·δ��", "�ƶ��������,������һ�������ƶ������롣", "�ƶ��������"
                                   };

        #endregion

        public override string GetInfo()
        {
            return "�ƶ�����";
        }

        public override bool Initialize()
        {
            //3
            InitData();

            //////////////////�� �� ͼ Ƭ
            if (UIObj.ParaList.Count >= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    Image[i] = System.Drawing.Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                }
            }

            LoadDescriptionFile();

            return true;
        }

        public override void paint(Graphics g)
        {
            if (GlobalParam.Instance.TrainInfo.TrainState != TrainState.Stop)
            {
                OnPost(CmdType.ChangePage, (int) ViewConfig.MainView, 1, 0);
            }
            GetValue();
            RefreshShowInfo();
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


        private void LoadDescriptionFile()
        {
            var file = Path.Combine(RecPath, "..\\Config\\˵����Ϣ.txt");
            if (File.Exists(file))
            {
                var all = File.ReadAllLines(file, Encoding.Default);
                foreach (var s in all)
                {
                    if (s.Trim() != "")
                    {
                        string[] str = s.Split(';');
                        if (str.Length == 2 && int.Parse(str[0])>=766)
                        {
                            var toolInfo = new TestToolInfo();
                            toolInfo.LogicalBit = Convert.ToInt32(str[0]);
                            toolInfo.ToolInfo = str[1].Replace('#', '\n');
                            m_TestToolInfos.Add(toolInfo);
                        }
                    }
                }
            }
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 37)
            {
                for (int i = 0; i < 37; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
        }

        public void InitData()
        {

            #region::::::::::::;;;;;;;;�� �� λ �� �� ʼ ��;;;;;;;;;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 8; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + i * ( Weight + 5 ) + 25, Recposition.Y + 140, Weight, Height);
            }

            #endregion

            #region ::::::::::::::::::::ҳ �� �� Ϣ �� �� λ ��::::::::::::::::::::::::::::::::

            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 340, 790, 80);

            #endregion

            #region ;;;;;;�� ť �� ʼ ��;;;;;;;;;;;;;;;;;;

            m_GButton = new CRH1AButton();
            m_GButton.SetButtonColor(192, 192, 192);
            m_GButton.SetButtonRect(Recposition.X + 380, Recposition.Y + 485, 90, 50);
            m_GButton.SetButtonText("��������");

            #endregion
        }

        /// <summary>
        /// ˢ����Ϣ��ʾ
        /// </summary>
        private void RefreshShowInfo()
        {
            for (int index = m_TestToolInfos.Count - 1; index >= 0; index--)
            {
                if (BoolList[m_TestToolInfos[index].LogicalBit])
                {
                    m_ShowTestToolInfo = m_TestToolInfos[index].ToolInfo;
                    break;
                }

                if (index == 0)
                {
                    m_ShowTestToolInfo = string.Empty;
                }
            }
        }


        public void DrawOn(Graphics g)
        {
            m_Title.OnDraw(g);

            //ҳ���������
            g.FillRectangle(BackBrush, m_Rect);
            g.DrawRectangle(BackPen, m_Rect);
            g.DrawImage(Image[0], m_Rect.X + 7, m_Rect.Y + 10);
            g.DrawString("(�÷�˵��)", Strfont, WhiteBrush, m_Rect.X + 40, m_Rect.Y + 8);
            for (int i = 0; i < 20; i++)
            {
                if (Valueb[17 + i])
                {
                    //  g.DrawString(str_Text[i], strfont, white_Brush, rect.X + 30, rect.Y + 30);
                }
            }

            g.FillRectangle(GreenBrush, m_Rect.X + 690, m_Rect.Y + 5, 20, 20);
            g.DrawRectangle(GreenlPen, m_Rect.X + 690, m_Rect.Y + 5, 20, 20);
            g.DrawString("ʩ��", Strfont, WhiteBrush, m_Rect.X + 720, m_Rect.Y + 7);

            g.FillRectangle(GrayBrush, m_Rect.X + 690, m_Rect.Y + 30, 20, 20);
            g.DrawRectangle(GreenlPen, m_Rect.X + 690, m_Rect.Y + 30, 20, 20);
            g.DrawString("�ͷ�", Strfont, WhiteBrush, m_Rect.X + 720, m_Rect.Y + 32);

            g.FillRectangle(GrayBrush, m_Rect.X + 690, m_Rect.Y + 55, 20, 20);
            g.DrawRectangle(RedPen, m_Rect.X + 690, m_Rect.Y + 55, 20, 20);
            g.DrawString("���жϻ����", Strfont, WhiteBrush, m_Rect.X + 710, m_Rect.Y + 57);

            //������ʾ��Ϣ
            g.DrawString(m_ShowTestToolInfo, Strfont1, WhiteBrush, m_Rect.X + 100, m_Rect.Y + 30);

            //�����г� ������
            if (!Valueb[0] && !Valueb[8]) //�ͷ�״̬
            {
                g.DrawImage(Image[1], TrainRect[0]);
            }
            else if (Valueb[0] && !Valueb[8])
            {
                g.DrawImage(Image[2], TrainRect[0]); //ʩ��
            }

            else if (Valueb[8])
            {
                g.DrawImage(Image[3], TrainRect[0]); //���жϻ����
            }
            else
            {

            }
            for (int i = 0; i < 6; i++)
            {
                if (!Valueb[i + 1] && !Valueb[i + 9])
                {
                    g.DrawImage(Image[4], TrainRect[i + 1]);
                }
                else if (Valueb[i + 1] && !Valueb[i + 9])
                {
                    g.DrawImage(Image[5], TrainRect[i + 1]);
                }
                else if (Valueb[i + 9])
                {
                    g.DrawImage(Image[6], TrainRect[i + 1]);
                }
                else
                {
                }
            }
            if (!Valueb[7] && !Valueb[15]) //�ͷ�״̬
            {
                g.DrawImage(Image[7], TrainRect[7]);
            }
            else if (Valueb[7] && !Valueb[15])
            {
                g.DrawImage(Image[8], TrainRect[7]); //ʩ��
            }

            else if (Valueb[15])
            {
                g.DrawImage(Image[9], TrainRect[7]); //���жϻ����
            }
            else
            {

            }

            for (int i = 0; i < 8; i++)
            {
                if (i == 7)
                {
                    g.DrawString("00", Strfont1, StrBrush, TrainRect[7].X + 30, TrainRect[7].Y - 15);
                }
                else
                {
                    g.DrawString("0" + ( i + 1 ).ToString(), Strfont1, StrBrush, TrainRect[i].X + 30, TrainRect[i].Y - 15);
                }

            }

            m_GButton.OnDraw(g);
        }

        /// <summary>
        /// ��ť����¼�
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void OnButtonDown(int x, int y)
        {
            if (m_GButton.Contains(x, y))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                m_GButton.OnButtonDown();
            }

        }

        public void OnButtonUp(int x, int y)
        {
            if (m_GButton.Contains(x, y))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                m_GButton.OnButtonUp();
            }
        }
    }
}
