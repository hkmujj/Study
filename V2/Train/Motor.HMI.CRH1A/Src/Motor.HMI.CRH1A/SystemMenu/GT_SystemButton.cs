using System.Drawing;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.SystemMenu
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_SystemButton : CRH1BaseClass
    {
        public CRH1AButton GButton;
        public Rectangle Recposition = new Rectangle(454, 513, 80, 50);
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "返回系统按钮";
        }

        public override bool Initialize()
        {
            #region ;;;;;;;;;;;;;;;; 底部按钮初始化:::::::::::::::::::::::
            GButton = new CRH1AButton();
            GButton.SetButtonRect(Recposition.X, Recposition.Y, 80, 50);
            GButton.SetButtonColor(192, 192, 192);
            GButton.SetButtonText("系统");
            #endregion

            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                GButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
            };
            return true;
        }


        public override void paint(Graphics dcGs)
        {
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void DrawOn(Graphics e)
        {
            GButton.OnDraw(e);
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
        public void OnButtonDown(Point point)
        {
            //  按 钮 响 应 事 件

            if (GButton.Contains(point) && GButton.IsEnable)
            {

                GButton.OnButtonDown();

            }

        }

        public void OnButtonUp(Point point)
        {

            if (GButton.Contains(point) && GButton.IsEnable)
            {
                OnPost(CmdType.ChangePage, 4, 0, 0);


                GButton.OnButtonUp();
            }
        }

        #endregion#
    }
}