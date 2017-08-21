using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class DataViewView : baseClass
    {
        private Rectangle m_Rect;
        private readonly RectText[] m_GText = new RectText[8];


        /// <summary>
        /// ������ͼ����
        /// �洢˾���š��г����Լ�ʱ��
        /// </summary>
        public static DataView m_TheDataView;

        public override string GetInfo()
        {
            return "������ͼ";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public void InitData()
        {
            m_Rect = new Rectangle(Common2D.RectB[0].Right + 5, Common2D.rectposition.Y + 45, 280, 328);

            //
            m_TheDataView = new DataView(ShareData.m_DirverID, ShareData.m_TrainID);
            m_TheDataView.UpdateTime(this.CurrenTime());

            for (int i = 0; i < 8; i++)
            {
                m_GText[i] = new RectText();
                m_GText[i].SetBkColor(0, 0, 0);
                m_GText[i].SetTextColor(255, 255, 255);
                m_GText[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_GText[i].SetTextRect(Common2D.RectF[i].X + 2, Common2D.RectF[i].Y + 2, Common2D.RectF[i].Width - 4, Common2D.RectF[i].Height - 4);

            }
            m_GText[0].SetText("˾����");
            m_GText[1].SetText("���κ�");
            m_GText[2].SetText("");
            m_GText[3].SetImage(ImageResource.��ͷ��);
            m_GText[4].SetImage(ImageResource.��ͷ��);
            m_GText[5].SetText("");
            m_GText[6].SetText("");
            m_GText[7].SetImage(ImageResource.����);


        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                //��ʱ�����Σ���֪���ǲ���ÿ�ν����ݶ���Ҫ����
                //TheDataView.UpdateTime();
            }
        }

        public override void paint(Graphics g)
        {
            OnDraw(g);
            ButtonEvent();
        }

        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common2D.DarkBlueBrush, m_Rect);
            g.FillRectangle(Common2D.LightBlueBrush, m_Rect.X, m_Rect.Y, m_Rect.Width, 55);
            Common2D.MeunTitle.SetText("����");
            Common2D.MeunTitle.OnDraw(g);
            g.DrawString("��������", Common2D.Font14B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 20);
            g.DrawString("˾����: " + m_TheDataView.DriverID, Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 90);
            g.DrawString("���κ�: " + m_TheDataView.TrainID, Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 135);
            g.DrawString("��  ��: " + m_TheDataView.TheDatetime.Year.ToString() + "/" + this.CurrenTime().Month.ToString() + "/" + DateTime.Now.Day.ToString(),
                Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 180);
            g.DrawString("ʱ  ��: " + m_TheDataView.TheDatetime.ToLongTimeString(), Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 225);
            for (int i = 0; i < 8; i++)
            {
                m_GText[i].OnDraw(g);
            }
        }

        public void ButtonEvent()
        {
            #region ��Ӧ F1��F8��ť�¼�
            for (int i = 0; i < 8; i++)
            {
                if (ButtonStatus.m_IsRightButtonUp[i])
                {
                    switch (i)
                    {
                        case 0:
                            //����˾����
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 1:
                            //�����г���
                            append_postCmd(CmdType.ChangePage, 9, 0, 0);
                            break;
                        case 3:
                            TextInfo.Last();
                            break;
                        case 4:
                            TextInfo.Next();
                            break;
                        case 7:
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                    }
                }
            }
            #endregion
        }
    }
}