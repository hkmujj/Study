using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_TrainConfig : baseClass, IButtonEventListener
    {
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>(); //��������Ҫ�Ľ���Ԫ��
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //�ײ����ְ�ť�Ƿ���
        private readonly bool[] m_IsCurrentCollector1 = new bool[3]; //�ܵ繭1״̬
        private readonly bool[] m_IsCurrentCollector2 = new bool[3]; //�ܵ繭2״̬
        private readonly bool[] m_IsDriverRooms = new bool[2]; //˾����ռ�����
        private readonly bool[] m_IsMainSegment = new bool[4]; //����״̬
        private readonly bool[] m_IsElectricMachine = new bool[4]; //���״̬ 
        private readonly bool[] m_IsStopBrake = new bool[4]; //ͣ���ƶ�
        private readonly bool[] m_IsTrainBrake = new bool[5]; //�����ƶ�
        private readonly bool[] m_IsTrainTop1 = new bool[2]; //�������뿪��1״̬
        private readonly bool[] m_IsTrainTop2 = new bool[2]; //�������뿪��2״̬
        private int m_BCUMode;
        private readonly Image[] m_TrainStatusImages = new Image[16]; //��ʾ�����ܵ繭�ͳ������뿪�ص����״̬��ͼƬ

        private Rectangle m_TrainRect;
        private Rectangle[] m_BottomRect = new Rectangle[6];
        private readonly Rectangle[] m_ImgRect = new Rectangle[7];

        public override string GetInfo()
        {
            return "��������";
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 16)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int) (btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 16)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            //����ͼƬ
            for (int i = 0; i < 17; i++)
            {
                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);

                if (i < 16)
                {
                    m_TrainStatusImages[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i + 17]);
                }

            }
            nErrorObjectIndex = -1;

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void InitData()
        {
            //����λ�ó�ʼ��
            m_TrainRect = new Rectangle(HxCommon.Recposition.X + 130, HxCommon.Recposition.Y + 50, 520, 120);

            //ͼ����ο�
            for (int i = 0; i < 7; i++)
            {
                if (i < 4)
                {
                    if (i%2 == 0)
                    {
                        m_ImgRect[i] = new Rectangle(m_TrainRect.X + 100, m_TrainRect.Bottom + 20 + i/2*61, 120, 60);
                    }
                    else
                    {
                        m_ImgRect[i] = new Rectangle(m_TrainRect.X + 260, m_TrainRect.Bottom + 20 + i/2*61, 120, 60);
                    }
                }
                else
                {
                    m_ImgRect[i] = new Rectangle(m_ImgRect[2].X + 65, m_ImgRect[2].Bottom + (i - 4)*61, 120, 60);
                }
            }
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        public void GetValue()
        {
            //���ñ���
            HxCommon.HTitle.SetText("ǣ������");

            HxCommon.ButtonText[0].SetText("��Ҫ����");
            HxCommon.ButtonText[1].SetText("ά��");
            HxCommon.ButtonText[2].SetText("");
            HxCommon.ButtonText[3].SetText("");
            HxCommon.ButtonText[4].SetText("�г�����");
            HxCommon.ButtonText[5].SetText("��������");
            HxCommon.ButtonText[6].SetText("ǣ������");
            HxCommon.ButtonText[7].SetText("");
            HxCommon.ButtonText[8].SetText(" ");
            HxCommon.ButtonText[9].SetText("������");
            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    HxCommon.ButtonText[i].SetBkColor(255, 255, 0);
                    HxCommon.ButtonText[i].SetTextColor(0, 0, 0);
                }
                else
                {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                }
            }

            #region ��״̬��ϢBool ���Ķ���

            //�ײ����ְ�ť
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }

            //�ܵ繭1
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector1[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            //�ܵ繭2
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector2[i] = BoolList[UIObj.InBoolList[i + 13]];
            }

            //˾����
            for (int i = 0; i < 2; i++)
            {
                m_IsDriverRooms[i] = BoolList[UIObj.InBoolList[i + 16]];
            }

            //����״̬
            for (int i = 0; i < 4; i++)
            {
                m_IsMainSegment[i] = BoolList[UIObj.InBoolList[i + 18]];
            }

            //���״̬
            for (int i = 0; i < 4; i++)
            {
                m_IsElectricMachine[i] = BoolList[UIObj.InBoolList[i + 22]];
            }

            //ͣ���ƶ�
            for (int i = 0; i < 4; i++)
            {
                m_IsStopBrake[i] = BoolList[UIObj.InBoolList[i + 26]];
            }

            //�����ƶ�
            for (int i = 0; i < 4; i++)
            {
                m_IsTrainBrake[i] = BoolList[UIObj.InBoolList[i + 30]];
            }
            m_IsTrainBrake[4] = BoolList[UIObj.InBoolList[UIObj.InBoolList.Count - 1]];

            #endregion

            //BCU ģʽ ����
            m_BCUMode = Convert.ToInt32(FloatList[UIObj.InFloatList[0]]);

            //�������뿪��1
            for (int i = 0; i < 2; i++)
            {
                m_IsTrainTop1[i] = BoolList[UIObj.InBoolList[i + 34]];
            }

            //�������뿪��1
            for (int i = 0; i < 2; i++)
            {
                m_IsTrainTop2[i] = BoolList[UIObj.InBoolList[i + 36]];
            }
        }

        public void DrawOn(Graphics g)
        {

            //���Ƶ��״̬
            for (int i = 0; i < 4; i++)
            {
                if (m_IsElectricMachine[i])
                {
                    g.DrawImage(m_ImageList[i], m_ImgRect[0].X + 5, m_ImgRect[0].Y + 5, m_ImgRect[0].Width - 10,
                        m_ImgRect[0].Height - 10);
                    g.DrawImage(m_ImageList[i], m_ImgRect[1].X + 5, m_ImgRect[1].Y + 5, m_ImgRect[1].Width - 10,
                        m_ImgRect[1].Height - 10);
                    break;
                }
            }

            //ͣ���ƶ�
            for (int i = 0; i < 5; i++)
            {
                if (m_IsTrainBrake[i])
                {
                    if (i < 4)
                    {
                        g.DrawImage(m_ImageList[i + 4], m_ImgRect[2].X + 5, m_ImgRect[2].Y + 5, m_ImgRect[2].Width - 10,
                            m_ImgRect[2].Height - 10);
                        g.DrawImage(m_ImageList[i + 4], m_ImgRect[3].X + 5, m_ImgRect[3].Y + 5, m_ImgRect[3].Width - 10,
                            m_ImgRect[3].Height - 10);
                    }
                    else
                    {
                        g.DrawImage(m_ImageList[16], m_ImgRect[2].X + 5, m_ImgRect[2].Y + 5, m_ImgRect[2].Width - 10,
                            m_ImgRect[2].Height - 10);
                        g.DrawImage(m_ImageList[16], m_ImgRect[3].X + 5, m_ImgRect[3].Y + 5, m_ImgRect[3].Width - 10,
                            m_ImgRect[3].Height - 10);
                    }

                }
            }

            //ͣ���ƶ�
            for (int i = 0; i < 4; i++)
            {
                if (m_IsStopBrake[i])
                {
                    g.DrawImage(m_ImageList[i + 8], m_ImgRect[4].X + 5, m_ImgRect[4].Y + 5, m_ImgRect[2].Width - 10,
                        m_ImgRect[4].Height - 10);
                    break;
                }
            }

            //BCU ģʽ
            switch (m_BCUMode)
            {
                case 1:
                    g.DrawString("BCU", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 25, m_ImgRect[6].Y + 10);
                    g.DrawString("����Ͷ��", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 5, m_ImgRect[6].Y + 30);
                    break;
                case 2:
                    g.DrawString("BCU", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 25, m_ImgRect[6].Y + 10);
                    g.DrawString("����", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 25, m_ImgRect[6].Y + 30);
                    break;
                case 3:
                    g.DrawString("BCU", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 25, m_ImgRect[6].Y + 10);
                    g.DrawString("�����г�", HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[6].X + 5, m_ImgRect[6].Y + 30);
                    break;
                default:
                    break;

            }

            //�ײ�������ť����
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //�����ܵ繭�ͳ������뿪�����״̬
            if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0] && m_IsTrainTop1[0] && m_IsTrainTop2[0]) //0gggg
            {
                g.DrawImage(m_TrainStatusImages[0], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0] && m_IsTrainTop1[0] && m_IsTrainTop2[1]) //1gggk
            {
                g.DrawImage(m_TrainStatusImages[1], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0] && m_IsTrainTop1[1] && m_IsTrainTop2[0]) //2ggkg
            {
                g.DrawImage(m_TrainStatusImages[2], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[0] && m_IsTrainTop1[1] && m_IsTrainTop2[1]) //3ggkk
            {
                g.DrawImage(m_TrainStatusImages[3], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[1] && m_IsTrainTop1[0] && m_IsTrainTop2[0]) //4gkgg
            {
                g.DrawImage(m_TrainStatusImages[4], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[1] && m_IsTrainTop1[0] && m_IsTrainTop2[1]) //5gkgk
            {
                g.DrawImage(m_TrainStatusImages[5], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[1] && m_IsTrainTop1[1] && m_IsTrainTop2[0]) //6gkkg
            {
                g.DrawImage(m_TrainStatusImages[6], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[0] && m_IsCurrentCollector2[1] && m_IsTrainTop1[1] && m_IsTrainTop2[1]) //7gkkk
            {
                g.DrawImage(m_TrainStatusImages[7], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[0] && m_IsTrainTop1[0] && m_IsTrainTop2[0]) //8kggg
            {
                g.DrawImage(m_TrainStatusImages[8], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[0] && m_IsTrainTop1[0] && m_IsTrainTop2[1]) //9kggk
            {
                g.DrawImage(m_TrainStatusImages[9], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[0] && m_IsTrainTop1[1] && m_IsTrainTop2[0]) //10kgkg
            {
                g.DrawImage(m_TrainStatusImages[10], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[0] && m_IsTrainTop1[1] && m_IsTrainTop2[1]) //11kgkk
            {
                g.DrawImage(m_TrainStatusImages[11], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[1] && m_IsTrainTop1[0] && m_IsTrainTop2[0]) //12kkgg
            {
                g.DrawImage(m_TrainStatusImages[12], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[1] && m_IsTrainTop1[0] && m_IsTrainTop2[1]) //13kkgk
            {
                g.DrawImage(m_TrainStatusImages[13], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[1] && m_IsTrainTop1[1] && m_IsTrainTop2[0]) //14kkkg
            {
                g.DrawImage(m_TrainStatusImages[14], m_TrainRect);
            }
            else if (m_IsCurrentCollector1[1] && m_IsCurrentCollector2[1] && m_IsTrainTop1[1] && m_IsTrainTop2[1]) //15kkkk
            {
                g.DrawImage(m_TrainStatusImages[15], m_TrainRect);
            }





            for (int i = 0; i < 7; i++)
            {
                g.DrawRectangle(HxCommon.LinePen2, m_ImgRect[i]);
            }
        }

        /// <summary>
        /// ��ӦӲ����ť�����¼�
        /// </summary>
        public void ButtonDownEvent()
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_IsNumButtonDown[i])
                {
                    switch (i)
                    {
                        case 0: //����"1"����ת����Ҫ������ͼ
                            append_postCmd(CmdType.ChangePage, 2, 0, 0);
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4: //����5�л����г�������ͼ
                            append_postCmd(CmdType.ChangePage, 3, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6: //����7�л���ǣ�����ݽ���
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9: //����������
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}

   