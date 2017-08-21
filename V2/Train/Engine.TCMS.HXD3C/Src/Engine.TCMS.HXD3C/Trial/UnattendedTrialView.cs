using System;
using System.Collections.ObjectModel;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using Engine.TCMS.HXD3C.Models;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3C.Trial
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class UnattendedTrialView : baseClass, IButtonBtnEventResponser
    {
        private GDIRectText m_Content;

        private GDIButton m_OkButton;

        private const string NeedTrial = "试验是否开始";

        private const string Trialing = "试验正在进行";

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Content = new GDIRectText()
            {
                Text = NeedTrial,
                OutLineRectangle = new Rectangle(100, 430, 550, 100),
                OutLinePen = Common.WhitePen2,
                NeedDarwOutline =  true,
                TextFormat = Common.CenterStringFormat,
                DrawFont = Common.Txt14FontB,
                TextBrush = Common.WhiteBrush,
            };
            m_OkButton = new HXD3ButtonProxy()
            {
                OutLineRectangle = new Rectangle(660, 480, 100, 50),
                Visible =  true,
                Text = "确认",
                ButtonUpEvent = OkButtonUpEvent,
            };
            
            ButtomButtonView.Instance.AddResponser(this);

            return true;
        }

        private void OkButtonUpEvent(object sender, EventArgs eventArgs)
        {
            m_OkButton.Visible = false;
            m_Content.Text = Trialing;
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                Title.Instance.UnattendedTrialTypeLocked = true;
                Title.Instance.UnattendedTrialType = UnattendedTrialType.Unkown;
                m_Content.Text = NeedTrial;
                m_OkButton.Visible = true;
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str217);
            }
        }

        public override void paint(Graphics g)
        {
            UpdateUnattendedTrialTypeState();

            m_Content.OnDraw(g);

            m_OkButton.OnDraw(g);
        }

        private void UpdateUnattendedTrialTypeState()
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                Title.Instance.UnattendedTrialType = UnattendedTrialType.Yellow;
            }
            if (BoolList[UIObj.InBoolList[1]])
            {
                Title.Instance.UnattendedTrialType = UnattendedTrialType.YellowFlash;
            }
            if (BoolList[UIObj.InBoolList[2]])
            {
                Title.Instance.UnattendedTrialType = UnattendedTrialType.RedFlash;
            }
            if (BoolList[UIObj.InBoolList[3]])
            {
                Title.Instance.UnattendedTrialType = UnattendedTrialType.Red;
            }
        }

        public override bool mouseDown(Point point)
        {
            return m_OkButton.OnMouseDown(point);
        }

        public override bool mouseUp(Point point)
        {
            return m_OkButton.OnMouseUp(point);
        }

        public bool Response(int btnIndex)
        {
            if (GlobalParam.Instance.CurrentViewID == 217)
            {
                if (btnIndex == 8)
                {
                    this.NavigateReturn();
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                }
                return true;
            }
            return false;
        }
    }
}