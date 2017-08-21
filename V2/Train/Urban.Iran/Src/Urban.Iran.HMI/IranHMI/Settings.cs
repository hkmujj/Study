using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Settings : HMIBase
    {
        public static int LightValue = 10;
        private List<FjButtonEx> m_BtnArr;
        private Bitmap m_BmpDisplayIntensity;
        private Rectangle[] m_RectArr;
        private Rectangle LightRec;
        private Rectangle StrRec;
        public Settings()
        {


        }

        private void btn_onMouseDown(FjButtonEx btnSender, Point pt)
        {
            if (btnSender.BtnIndex == (int)IranViewIndex.Login)
            {
                if (GlobleParam.Instance.WorkModel == HMIWorkModel.NoActoveDrive)
                {
                    Task.Factory.StartNew((() =>
                   {
                       StandbyView.IsJump = true;
                       Thread.Sleep(1000);
                       StandbyView.IsJump = false;
                   }));
                    ChangedPage(IranViewIndex.Standby);
                }
                else
                {
                    ChangedPage(IranViewIndex.StartLogin);
                }

            }
            else if (btnSender.BtnIndex == (int)IranViewIndex.Reset)
            {
                ChangedPage(IranViewIndex.Reset);
            }
            else if (btnSender.BtnIndex == (int)IranViewIndex.SetWheelDiameter)
            {
                ChangedPage(IranViewIndex.SetWheelDiameter);
            }
            else if (btnSender.BtnIndex == (int)IranViewIndex.SetDataTime)
            {
                ChangedPage(IranViewIndex.SetDataTime);
            }
        }

        public override string GetInfo()
        {
            return "Settings";
        }

        protected override bool Initalize()
        {
            LightValue = 10;
            m_BtnArr = new List<FjButtonEx>()
                     {
                         new FjButtonEx(33,GlobleParam.ResManagerText.GetString("Button0006"), new Rectangle(337, 136, 97, 62)),
                         new FjButtonEx(33, GlobleParam.ResManagerText.GetString("Button0007"), new Rectangle(337, 232, 97, 62)),

                         new FjButtonEx(44,GlobleParam.ResManagerText.GetString("Title0044"), new Rectangle(137, 136, 97, 62)),
                         new FjButtonEx(45,GlobleParam.ResManagerText.GetString("Title0045"), new Rectangle(137, 232, 97, 62)),
                         new FjButtonEx(46,"Set date/\n time", new Rectangle(137, 328, 97, 62)),
                         //new FJButtonEx(1, "English", new Rectangle(545, 232, 97, 62))
                     };
            StrRec = new Rectangle(714, 80, 74, 62);
            LightRec = new Rectangle(720, 198, 62, 62);
            var btn = new FjButtonEx(47, "+", new Rectangle(720, 136, 62, 62)) { BtnFont = GdiCommon.Txt18Font };
            btn.MouseDown += Btn_MouseDown;
            m_BtnArr.Add(btn);

            var btn1 = new FjButtonEx(48, "-", new Rectangle(720, 260, 62, 62)) { BtnFont = GdiCommon.Txt18Font };
            btn1.MouseDown += Btn_MouseDown;
            m_BtnArr.Add(btn1);
            m_BtnArr.ForEach(e => e.MouseDown += btn_onMouseDown);

            m_BmpDisplayIntensity = new Bitmap(RecPath + "\\frame/displayIntensity.jpg");

            m_RectArr = new[]
                      {
                          new Rectangle(720, 230, 58, 171),
                          new Rectangle(710, 187, 80, 40)
                      };

            return true;
        }

        private void Btn_MouseDown(FjButtonEx btnSender, Point pt)
        {
            if (btnSender.BtnIndex == 47 && LightValue < 100)
            {
                LightValue += 10;
            }
            else if (btnSender.BtnIndex == 48 && LightValue >= 10)
            {
                LightValue -= 10;
            }
        }

        public override void paint(Graphics g)
        {
            var vis = GlobleParam.Instance.WorkModel != HMIWorkModel.NoActoveDrive;
            for (int i = 2; i <= 4; i++)
            {
                m_BtnArr[i].Visibility = vis;
            }
            m_BtnArr.ForEach(e => e.OnDraw(g));
            g.FillRectangle(GdiCommon.DarkGreyBrush, LightRec);
            g.DrawString(LightValue.ToString(), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, LightRec, GdiCommon.CenterFormat);
            g.DrawString(GlobleParam.ResManagerText.GetString("Text0133"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, StrRec, GdiCommon.CenterFormat);
        }

        public override bool mouseDown(Point point)
        {
            foreach (var btn in m_BtnArr.Where(btn => btn.IsVisible(point)))
            {
                btn.OnMouseDown(point);
                break;
            }
            return base.mouseDown(point);
        }
    }
}