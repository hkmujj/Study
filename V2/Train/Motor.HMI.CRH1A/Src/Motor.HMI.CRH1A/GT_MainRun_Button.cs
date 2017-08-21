using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Linq;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_MainRun_Button : CRH1BaseClass
    {
        public Rectangle Recposition = new Rectangle(2, 513, 80, 50);
        public CRH1AButton[] GButton = new CRH1AButton[2];
        public float Valuef;

        public override string GetInfo()
        {
            return "底部按钮栏";
        }

        public override bool Initialize()
        {
            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            OnPaint((dcGs));
            base.paint(dcGs);
        }

        public void GetValue()
        {
            if (UIObj.InFloatList.Count >= 1)
            {
                Valuef = FloatList[UIObj.InFloatList[0]];
            }

        }

        public void InitData()
        {
            GButton[0] = new CRH1AButton();
            GButton[0].SetButtonColor(192, 192, 192);
            GButton[0].SetButtonRect(Recposition.X + 624, Recposition.Y, Recposition.Width, Recposition.Height);
            GButton[0].SetButtonText("主菜单");

            GButton[1] = new CRH1AButton();
            GButton[1].SetButtonColor(192, 192, 192);
            GButton[1].SetButtonRect(Recposition.X + 710, Recposition.Y, Recposition.Width, Recposition.Height);
            GButton[1].SetButtonText("运行/车站");

            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var CRH1AButton in GButton)
                {
                    CRH1AButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
            };
        }

        public void OnPaint(Graphics g)
        {

            for (int i = 0; i < 2; i++)
            {
                GButton[i].OnDraw(g);
            }
        }

        protected override bool OnMouseDown(Point point)
        {

            OnButtonDown(point);

            return true;
        }
        protected override bool OnMouseUp(Point point)
        {

            OnButtonUp(point);

            return true;
        }

        public void OnButtonDown(Point nPoint)
        {
            foreach (var button in GButton.Where(w => w != null && w.Contains(nPoint) && w.IsEnable))
            {
                button.OnButtonDown();
            }
        }
        public void OnButtonUp(Point nPoint)
        {
            for (int i = 0; i < 2; i++)
            {
                if (GButton[i].Contains(nPoint) && GButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 1, 0, 0);
                    }
                    else
                    {
                        if (Valuef > 3)
                        {
                            OnPost(CmdType.ChangePage, 3, 0, 0);

                        }
                        else
                        {
                            OnPost(CmdType.ChangePage, 5, 0, 0);
                        }

                    }
                    GButton[i].OnButtonUp();
                }
            }
        }
    }
}
