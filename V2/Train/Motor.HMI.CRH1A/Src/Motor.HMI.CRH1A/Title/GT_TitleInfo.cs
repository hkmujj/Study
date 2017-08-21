using System;
using System.Configuration;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.Title
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class GT_TitleInfo : CRH1BaseClass
    {
        public Rectangle Recposition = new Rectangle(0, 30, 180, 30);
        public GDIRectText[] GText = new GDIRectText[8];
        public float[] Valuef = new float[2];
        public Image[] Images = new Image[2];//事件图标
        public CRH1AButton EventButton;

        /// <summary>
        /// 门缓解标志位
        /// </summary>
        private bool m_IsDoorReleaseFlag = false;

        public override string GetInfo()
        {
            return "标题信息";
        }


        public override bool Initialize()
        {
            //3
            InitData();
            if (UIObj.ParaList.Count > 0)
            {
                Images[0] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[0]);
                Images[1] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[1]);
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ReFreshData();
            DrawOn(g);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA != 2)
            {
                return;
            }
            if (nParaB == 9 || nParaB == 5)
            {
                append_postCmd(CmdType.SetBoolValue, 5417, 1, 0);
            }
            else
            {
                append_postCmd(CmdType.SetBoolValue, 5417, 0, 0);
            }
        }

        public void GetValue()
        {
            if (UIObj.InFloatList.Count >= 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }

            m_IsDoorReleaseFlag = BoolList[UIObj.InBoolList[0]];
            if (m_IsDoorReleaseFlag)
            {
                OnPost(CmdType.ChangePage, 5, 0, 0);
            }

            if (BoolList[UIObj.InBoolList[1]])
            {
                OnPost(CmdType.ChangePage, 30, 0, 0);
            }
        }

        public void InitData()
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 4; j++)
                {
                    GText[i * 4 + j] = new GDIRectText();
                    GText[i * 4 + j].SetBkColor(119, 136, 153);
                    GText[i * 4 + j].SetTextColor(255, 255, 255);
                    GText[i * 4 + j].SetTextStyle(12, FormatStyle.Center, true, "Arial");
                    GText[i * 4 + j].SetTextRect(Recposition.X + j * (Recposition.Width + 3), Recposition.Y + i * (Recposition.Height + 3), Recposition.Width, Recposition.Height);
                    GText[i * 4 + j].SetText("");
                }
            GText[0].SetText("日期");
            GText[1].SetText("时间");
            GText[2].SetText("接触网电压");
            GText[3].SetText("列车速度");

            //初始化事件图标
            EventButton = new CRH1AButton();
            EventButton.SetButtonColor(192, 192, 192);
            EventButton.SetButtonRect(Recposition.X + 733, Recposition.Y, 57, 64);
        }
        public void ReFreshData()
        {
            //   获取系统的时间和日期;
            DateTime currentTime = CurrenTime;
            GText[4].SetText(currentTime.ToString("yyyy-MM-dd"));
            GText[5].SetText(currentTime.ToString("hh:mm:ss"));
            GText[6].SetText(Convert.ToInt32(Valuef[1]) + "仟伏");

            ////速度的正常范围是0到200之间
            //GlobalParam.Instance.TrainInfo.Speed = Valuef[0];

            GText[7].SetText(string.Format("{0:F0}公里/小时", GlobalParam.Instance.TrainInfo.AbsSpeed));

        }

        public void DrawOn(Graphics e)
        {

            for (int i = 0; i < 8; i++)
            {
                GText[i].OnDraw(e);
            }
            EventButton.OnDraw(e);

            e.DrawImage(GT_GalobalFaultManage.Instance.HasSuredActiveFault() ? Images[1] : Images[0], EventButton.OutLineRectangle);
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

        public void OnButtonDown(int x, int y)
        {
            if (EventButton.Contains(x, y))
            {
                EventButton.OnButtonDown();
            }
        }

        public void OnButtonUp(int x, int y)
        {
            if (EventButton.Contains(x, y))
            {
                OnPost(CmdType.ChangePage, 23, 0, 0);
                EventButton.OnButtonUp();
            }
        }
    }
}
