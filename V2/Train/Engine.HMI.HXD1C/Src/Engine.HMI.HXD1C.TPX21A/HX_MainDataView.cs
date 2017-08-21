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
    class HX_MainDataView : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10];//�ײ����ְ�ť�Ƿ���
        private readonly Rectangle[] m_ImgRect = new Rectangle[6];//��ʾ�ܵ繭 ����·�� �������뿪�ص�״̬��ͼƬ��Ϣ��������
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>();//��������Ҫ�Ľ���Ԫ��
        private Rectangle[] m_DisconnectorRect = new Rectangle[2];//��ʾ����·������״����
        private readonly bool[] m_IsCurrentCollector1 = new bool[3];//�ܵ繭1״̬
        private readonly bool[] m_IsCurrentCollector2 = new bool[3];//�ܵ繭2״̬
        private readonly bool[] m_IsSeparateDisjunctor1 = new bool[2];//�������뿪��1
        private readonly bool[] m_IsSeparateDisjunctor2 = new bool[2];//�������뿪��2
        private readonly bool[] m_IsDisconnector = new bool[4];//����·��״̬
        private readonly bool[] m_IsTrainModel=new bool[4];//����ģʽ
        private readonly string[] m_StrTrainModel = new string[4] { "��������", "���ڶ���", "��������", "����ģʽ" };
        private readonly string[] m_StrVcmStatus = { "����", "�ӿ�", "�Ͽ�" };//VCM��������ʾ״̬

        private HxRectText m_OperateText;//��ʾ������ ��״̬���ı���
        private readonly HxRectText[] m_VcmText = new HxRectText[2];//��ʾVCM1 VCM2 ��״̬���ı���
        private readonly HxRectText[] m_ModemText = new HxRectText[5];//��ʾ ����ģʽ �����ڵ� ˾���ҵ��ı���
        private readonly HxRectText[] m_MVBText = new HxRectText[4];//��ʾMVB WTB ״̬���ı��� 

        private readonly bool[] m_IsVcm1 = new bool[3];
        private readonly bool[] m_IsVcm2 = new bool[3];//VCM1 VCM2 �����ӹ�ϵ
        private readonly bool[] m_IsOperators = new bool[2];//������
        private readonly bool[] m_IsDriverRoom = new bool[2];//����˾����
        private readonly bool[] m_IsMvbs = new bool[4];//MVB WTB �ߵ�״̬
        private readonly int[] m_TrainPoints = new int[3];//�����ڵ�

        public static int m_MainSegmentNum=0;//���Ϸֶϴ���������
        private bool m_CountStatus = false;
        
        public override string GetInfo()
        {
            return "��Ҫ������ͼ";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            //����ͼƬ
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Image image = Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);
            }
            nErrorObjectIndex = -1;
            return true;
        }

        public void InitData()
        {
            //��ʾ�ܵ繭 ����·�� �������뿪�ص�״̬��ͼƬ��Ϣ���������ʼ��
            for (int i = 0; i < 4;i++ )
            {
                if (i % 2 == 0)
                {
                    m_ImgRect[i] = new Rectangle(HxCommon.Recposition.X + 300, HxCommon.Recposition.Y + (i / 2) * 63+50, 100, 60);
                }
                else
                {
                    m_ImgRect[i] = new Rectangle(HxCommon.Recposition.X + 403, HxCommon.Recposition.Y + (i / 2) * 63+50, 100, 60);
                }
            }

            for (int i = 4; i < 6; i++)
            {
                if (i % 2 == 0)
                {
                    m_ImgRect[i] = new Rectangle(HxCommon.Recposition.X + 300, HxCommon.Recposition.Y + (i / 2) * 63 + 50, 100, 40);
                }
                else
                {
                    m_ImgRect[i] = new Rectangle(HxCommon.Recposition.X + 403, HxCommon.Recposition.Y + (i / 2) * 63 + 50, 100, 40);
                }
            }
            
            #region ��ʾ����״̬���ı���ĳ�ʼ��
            //������
            m_OperateText = new HxRectText();
            m_OperateText.SetBkColor(0, 0, 0);
            m_OperateText.SetDrawFrm(true);
            m_OperateText.SetLinePen(255, 255, 255, 2);
            m_OperateText.SetTextColor(255, 255, 255);
            m_OperateText.SetTextStyle(12, FormatStyle.Center, true, "����");
            m_OperateText.SetTextRect(m_ImgRect[4].X, m_ImgRect[4].Bottom + 3, 203, 33);

            //VCM
            for (int i = 0; i < 2;i++ )
            {
                m_VcmText[i] = new HxRectText();
                m_VcmText[i].SetBkColor(0, 0, 0);
                m_VcmText[i].SetDrawFrm(true);
                m_VcmText[i].SetLinePen(255, 255, 255, 2);
                m_VcmText[i].SetTextColor(255, 255, 255);
                m_VcmText[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                if (i == 0)
                {
                    m_VcmText[i].SetTextRect(m_OperateText.m_RectPosition.X, m_OperateText.m_RectPosition.Bottom + 3, 80, 35);
                }
                else
                {
                    m_VcmText[i].SetTextRect(m_OperateText.m_RectPosition.X + 123, m_OperateText.m_RectPosition.Bottom + 3,80, 35);
                }
            }

            //ģʽ��Ϣ
            for (int i = 0; i < 5; i++ )
            {
                m_ModemText[i] = new HxRectText();
                m_ModemText[i].SetBkColor(0, 0, 0);
                m_ModemText[i].SetDrawFrm(true);
                m_ModemText[i].SetLinePen(255, 255, 255, 2);
                m_ModemText[i].SetTextColor(255, 255, 255);
                m_ModemText[i].SetTextStyle(12, FormatStyle.Center, true, "����");

                if (i==0)
                {
                     m_ModemText[i].SetTextRect(m_VcmText[0].m_RectPosition.X, m_VcmText[0].m_RectPosition.Bottom + 3, 203, 34);
                }
                else if (i > 0 && i < 4)
                {
                    m_ModemText[i].SetTextRect(m_ModemText[0].m_RectPosition.X + 67 * (i-1), m_ModemText[0].m_RectPosition.Bottom + 3, 63, 34);
                }
                else
                {
                    m_ModemText[i].SetTextRect(m_ModemText[1].m_RectPosition.X , m_ModemText[1].m_RectPosition.Bottom + 3, 203, 34);
                }
            }

            //MVB WTA
            for (int i = 0; i < 4;i++ )
            {
               m_MVBText[i] = new HxRectText();
               m_MVBText[i].SetBkColor(0, 0, 0);
               m_MVBText[i].SetDrawFrm(true);
               m_MVBText[i].SetLinePen(255, 255, 255, 2);
               m_MVBText[i].SetTextColor(255, 255, 255);
               m_MVBText[i].SetTextStyle(12, FormatStyle.Center, true, "����");

               if (i < 2)
               {
                   m_MVBText[i].SetTextRect(m_ModemText[4].m_RectPosition.X + 103 * i, m_ModemText[4].m_RectPosition.Bottom + 3, 100, 34);
               }
               else
               {
                   m_MVBText[i].SetTextRect(m_MVBText[0].m_RectPosition.X + 103 * (i-2), m_MVBText[0].m_RectPosition.Bottom + 3, 100, 34);
               }
            }
            #endregion


        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void GetValue()
        {
            //���ñ���
            HxCommon.HTitle.SetText("��Ҫ����");

            HxCommon.ButtonText[0].SetText("ǣ������");
            HxCommon.ButtonText[1].SetText("�¶�");
            HxCommon.ButtonText[2].SetText("����");
            HxCommon.ButtonText[3].SetText("����ϵͳ");
            HxCommon.ButtonText[4].SetText("����״̬ ");
            HxCommon.ButtonText[5].SetText("��������");
            HxCommon.ButtonText[6].SetText(" ");
            HxCommon.ButtonText[7].SetText("��������");
            HxCommon.ButtonText[8].SetText("���ڶ���");
            HxCommon.ButtonText[9].SetText("������");

            for (int i = 0; i < 10; i++)
            {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
            }

            #region ��״̬��ϢBool ���Ķ���
            //�ܵ繭1
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector1[i] = BoolList[UIObj.InBoolList[i]];
            }

            //�ܵ繭2
            for (int i = 0; i < 3; i++)
            {
                m_IsCurrentCollector2[i] = BoolList[UIObj.InBoolList[i + 3]];
            }

            //�������뿪��1
            for (int i = 0; i < 2;i++ )
            {
                m_IsSeparateDisjunctor1[i] = BoolList[UIObj.InBoolList[i + 6]];
            }

            //�������뿪��2
            for (int i = 0; i < 2; i++)
            {
                m_IsSeparateDisjunctor2[i] = BoolList[UIObj.InBoolList[i + 8]];
            }

            //����·��
            for (int i = 0; i < 4;i++ )
            {
                m_IsDisconnector[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            //�������Ϸֶϴ���
            if ((m_CountStatus!=m_IsDisconnector[3]))
            {
                m_CountStatus = m_IsDisconnector[3];
                if (m_IsDisconnector[3])
                {
                    m_MainSegmentNum++;
                }
            }

            //�ײ����ְ�ť
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i + 14]];
            }

            //VCM1
            for (int i = 0; i < 3; i++)
            { 
                m_IsVcm1[i]=BoolList[UIObj.InBoolList[i+38]];
            }

            //VCM2
            for (int i = 0; i < 3; i++)
            {
                m_IsVcm2[i] = BoolList[UIObj.InBoolList[i + 41]];
            }

            for (int i = 0; i < 3; i++)
            {
                if (m_IsVcm1[i])
                {
                    m_VcmText[0].SetText(m_StrVcmStatus[i]);
                    break;
                }
                m_VcmText[0].SetText(" ");
            }
            for (int i = 0; i < 3; i++)
            {
                if (m_IsVcm2[i])
                {
                    m_VcmText[1].SetText(m_StrVcmStatus[i]);
                    break;    
                }
                m_VcmText[1].SetText(" ");
            }
            //if (isVcm)//VCM1Ϊ�� VCM2 Ϊ��
            //{
            //    vcmText[0].SetText("��");
            //    vcmText[1].SetText("��");
            //}
            //else //VCM1Ϊ�� VCM2 Ϊ��
            //{
            //    vcmText[0].SetText("��");
            //    vcmText[1].SetText("��");
            //}

            //������
            for (int i = 0; i < 2; i++)
            {
                m_IsOperators[i] = BoolList[UIObj.InBoolList[25 + i]];
            }

            if (m_IsOperators[0])//������1
            {
                m_OperateText.SetText("˾����1ռ��");
            }
            else if (m_IsOperators[1])//������2
            {
                m_OperateText.SetText("˾����2ռ��");
            }
            else
            {
                m_OperateText.SetText("");
            }

            if (m_IsDriverRoom[0])//˾����1���ڼ���״̬
            {
                m_ModemText[4].SetText("1");
            }
            else if (m_IsDriverRoom[1])//˾����2���ڼ���״̬
            {
                m_ModemText[4].SetText("2");
            }
            else
            {
                m_ModemText[4].SetText("��");
            }

            //˾����
            for (int i = 0; i < 2; i++)
            {
                m_IsDriverRoom[i] = BoolList[UIObj.InBoolList[27 + i]];
            }

            //MVB WTB
            for (int i = 0; i < 4; i++)
            {
                m_IsMvbs[i] = BoolList[UIObj.InBoolList[29 + i]];

                if (!m_IsMvbs[i])
                {
                    m_MVBText[i].SetText("����");
                }
                else
                {
                    m_MVBText[i].SetText("����");
                }
            }

            //����ģʽ
            for (int i = 0; i < 4; i++)
            {
                m_IsTrainModel[i] = BoolList[UIObj.InBoolList[33 + i]];
                if (m_IsTrainModel[i])
                {
                    m_ModemText[0].SetText(m_StrTrainModel[i]);
                    break;
                }
                m_ModemText[0].SetText("");
            }
            #endregion

            #region �� Float ���Ķ���
           
         
            //���Ϸֶϴ���-----��������������� ���þ����ӿڣ�
           // MainSegmentNum = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]);

            //�����ڵ�
            for (int i = 0; i < 3;i++ )
            {
                m_TrainPoints[i] = Convert.ToInt32(FloatList[UIObj.InFloatList[i+2]]);
                m_ModemText[i + 1].SetText(m_TrainPoints[i].ToString());
            }
            #endregion

        }

        public void DrawOn(Graphics g)
        {
            //�ײ�������ť����
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //ͼ����ʾ����ľ��ο����
            for (int i = 0; i < 6;i++ )
            {
                g.DrawRectangle(HxCommon.LinePen1, m_ImgRect[i]);
            }

            //����������Ϣ
            g.DrawString("�ܵ繭1", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[0].X - 90, m_ImgRect[0].Y + 20 );
            g.DrawString("�ܵ繭2", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[1].Right + 5, m_ImgRect[1].Y + 20);
            g.DrawString("�������뿪��1", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[2].X -150, m_ImgRect[2].Y + 20);
            g.DrawString("�������뿪��2", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[3].Right + 5, m_ImgRect[3].Y + 20);
            g.DrawString("����", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[4].X - 50, m_ImgRect[4].Y + 15);
            g.DrawString("���Ϸֶϴ���", HxCommon.Font16B, HxCommon.WhiteBrush, m_ImgRect[5].Right + 5 , m_ImgRect[5].Y + 15);
            g.DrawString(m_MainSegmentNum.ToString(), HxCommon.Font14B, HxCommon.WhiteBrush, m_ImgRect[5].X + 25, m_ImgRect[5].Y + 15);

            #region ��ͼƬ״̬��Ϣ����
            //�ܵ繭����1
            for (int i = 0; i < 3; i++)
            {
                if (m_IsCurrentCollector1[i])
                {
                    g.DrawImage(m_ImageList[i], m_ImgRect[0].X + 8, m_ImgRect[0].Y + 8, m_ImgRect[0].Width - 16, m_ImgRect[0].Height - 16);
                    break;
                }
                else
                {
                }
            }

            //�ܵ繭����2
            for (int i = 0; i < 3; i++)
            {
                if (m_IsCurrentCollector2[i])
                {
                    g.DrawImage(m_ImageList[i], m_ImgRect[1].X + 8, m_ImgRect[1].Y + 8, m_ImgRect[1].Width - 16, m_ImgRect[1].Height - 16);
                    break;
                }
                else
                {
                }
            }

            //�������뿪��1
            for (int i = 0; i < 2; i++)
            {
                if (m_IsSeparateDisjunctor1[i])
                {
                    g.DrawImage(m_ImageList[i + 3], m_ImgRect[2].X + 8, m_ImgRect[2].Y + 8, m_ImgRect[2].Width - 16, m_ImgRect[2].Height - 16);
                    break;
                }
            }

            //�������뿪��2
            for (int i = 0; i < 2; i++)
            {
                if (m_IsSeparateDisjunctor2[i])
                {
                    g.DrawImage(m_ImageList[i + 5], m_ImgRect[3].X + 8, m_ImgRect[3].Y + 8, m_ImgRect[3].Width - 16, m_ImgRect[3].Height - 16);
                    break;
                }
            }

            //��·��
            for (int i = 0; i < 4; i++)
            {
                if (m_IsDisconnector[i])
                {
                    g.DrawImage(m_ImageList[i + 7], m_ImgRect[4].X + 8, m_ImgRect[4].Y + 8, m_ImgRect[4].Width - 16, m_ImgRect[4].Height - 16);
                    break;
                }
            }
            #endregion

            #region ���ı���Ϣ����
            //������
            m_OperateText.OnDraw(g);

            //VCM
            for (int i = 0; i < 2;i++ )
            {
                m_VcmText[i].OnDraw(g);
            }

            g.DrawString("VCM1", HxCommon.Font16B, HxCommon.WhiteBrush, m_VcmText[0].m_RectPosition.X - 60, m_VcmText[0].m_RectPosition.Y + 10);
            g.DrawString("VCM2", HxCommon.Font16B, HxCommon.WhiteBrush, m_VcmText[1].m_RectPosition.Right + 5, m_VcmText[1].m_RectPosition.Y + 10);

            //ģʽ��Ϣ
            for (int i = 0; i < 5;i++ )
            {
                m_ModemText[i].OnDraw(g);
            }

            g.DrawString("����ģʽ", HxCommon.Font16B, HxCommon.WhiteBrush, m_ModemText[0].m_RectPosition.X - 94, m_ModemText[0].m_RectPosition.Y + 10);
            g.DrawString("�����ڵ�", HxCommon.Font16B, HxCommon.WhiteBrush, m_ModemText[1].m_RectPosition.X - 94, m_ModemText[1].m_RectPosition.Y + 10);
            g.DrawString("˾����", HxCommon.Font16B, HxCommon.WhiteBrush, m_ModemText[4].m_RectPosition.X - 75, m_ModemText[4].m_RectPosition.Y + 10);

            //MVB WTB
            for (int i = 0; i < 4;i++ )
            {
                m_MVBText[i].OnDraw(g);
            }

            g.DrawString("MVB A��״̬", HxCommon.Font16B, HxCommon.WhiteBrush, m_MVBText[0].m_RectPosition.X - 135, m_MVBText[0].m_RectPosition.Y + 10);
            g.DrawString("MVB B��״̬", HxCommon.Font16B, HxCommon.WhiteBrush, m_MVBText[1].m_RectPosition.Right + 5, m_MVBText[1].m_RectPosition.Y + 10);
            g.DrawString("WTB A��״̬", HxCommon.Font16B, HxCommon.WhiteBrush, m_MVBText[2].m_RectPosition.X - 135, m_MVBText[2].m_RectPosition.Y + 10);
            g.DrawString("WTB B��״̬", HxCommon.Font16B, HxCommon.WhiteBrush, m_MVBText[3].m_RectPosition.Right + 5, m_MVBText[3].m_RectPosition.Y + 10);

            #endregion



           
           
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
                        case 0:
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            break;
                        case 1://��ת���¶���ͼ
                           append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 2://��ת���������
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 3://��ת������ϵͳ
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 4://��ת��ǣ��״̬
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7://��ת���������� 
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 8://��ת�����ڶ��� 
                            append_postCmd(CmdType.ChangePage, 12, 0, 0);
                            break;
                        case 9://����������
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 2)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int)(btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 2)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }
    }
}
