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
        /// 数据视图对象
        /// 存储司机号、列车号以及时间
        /// </summary>
        public static DataView m_TheDataView;

        public override string GetInfo()
        {
            return "数据视图";
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
                m_GText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_GText[i].SetTextRect(Common2D.RectF[i].X + 2, Common2D.RectF[i].Y + 2, Common2D.RectF[i].Width - 4, Common2D.RectF[i].Height - 4);

            }
            m_GText[0].SetText("司机号");
            m_GText[1].SetText("车次号");
            m_GText[2].SetText("");
            m_GText[3].SetImage(ImageResource.箭头上);
            m_GText[4].SetImage(ImageResource.箭头下);
            m_GText[5].SetText("");
            m_GText[6].SetText("");
            m_GText[7].SetImage(ImageResource.返回);


        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                //暂时先屏蔽，不知道是不是每次进数据都需要更新
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
            Common2D.MeunTitle.SetText("数据");
            Common2D.MeunTitle.OnDraw(g);
            g.DrawString("数据内容", Common2D.Font14B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 20);
            g.DrawString("司机号: " + m_TheDataView.DriverID, Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 90);
            g.DrawString("车次号: " + m_TheDataView.TrainID, Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 135);
            g.DrawString("日  期: " + m_TheDataView.TheDatetime.Year.ToString() + "/" + this.CurrenTime().Month.ToString() + "/" + DateTime.Now.Day.ToString(),
                Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 180);
            g.DrawString("时  间: " + m_TheDataView.TheDatetime.ToLongTimeString(), Common2D.Font18B, Common2D.WhiteBrush, m_Rect.X + 10, m_Rect.Y + 225);
            for (int i = 0; i < 8; i++)
            {
                m_GText[i].OnDraw(g);
            }
        }

        public void ButtonEvent()
        {
            #region 响应 F1至F8按钮事件
            for (int i = 0; i < 8; i++)
            {
                if (ButtonStatus.m_IsRightButtonUp[i])
                {
                    switch (i)
                    {
                        case 0:
                            //设置司机号
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 1:
                            //设置列车号
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