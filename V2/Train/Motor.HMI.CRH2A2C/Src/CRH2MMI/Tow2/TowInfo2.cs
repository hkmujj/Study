using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Tow2.Content;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;

namespace CRH2MMI.Tow2
{
    /// <summary>
    /// 牵引变流器信息2
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class TowInfo2 : CRH2BaseClass
    {
        private List<CRH2Button> m_CarBtns;
        private List<GDIRectText> m_CarInfos;
        private Tow2Resource m_Resource;

        private CRH2Button m_SelectedCarBtn;

        private Tow2ContentBase m_ContentView;

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                //ReselectCarBtn(m_CarBtns.First());
                m_ContentView.ReselectCarBtn();
            }
        }

        public override void paint(Graphics g)
        {
            //    m_CarInfos.ForEach(e => e.OnPaint(g));

            //    m_CarBtns.ForEach(e => e.OnDraw(g));
            m_ContentView.OnDraw(g);
        }


        protected override bool OnMouseDown(Point nPoint)
        {
            //return m_CarBtns.Any(a => a.OnMouseDown(nPoint));
            return m_ContentView.OnMouseDown(nPoint);
        }

        public override bool Init()
        {

            m_Resource = new Tow2Resource(this);

            InitUIObj(GlobalInfo.CurrentCRH2Config.TowInfo2Config.Cars.Cast<CRH2CommunicationPortModel>());

            switch (GlobalInfo.CurrentCRH2Config.Type)
            {
                case CRH2Type.CRH2A:
                    m_ContentView = new Tow2Content8Car(m_Resource);
                    break;
                case CRH2Type.CRH2B:
                    m_ContentView = new Tow2ContentCRH2B(m_Resource);
                    break;
                case CRH2Type.CRH2C:
                    m_ContentView = new Tow2ContentCRH2C(m_Resource);
                    break;
                case CRH2Type.CRH380A:
                    m_ContentView = new Tow2Content8Car(m_Resource);
                    break;
                case CRH2Type.CRH380AL:
                    m_ContentView = new Tow2Content16Car(m_Resource);
                    break;
                case CRH2Type.CRH380AReconnection:
                    m_ContentView = new Tow2Content16Car(m_Resource);
                    break;
                case CRH2Type.CRH380AUnion:
                    m_ContentView = new Tow2Content8Car(m_Resource);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            m_ContentView.Initalize();

            //InitTextInfo();

            //InitCarBtn();

            return true;
        }

        private void InitCarBtn()
        {
            bool is2C = GlobalInfo.CurrentCRH2Config.Type == CRH2Type.CRH2C;
            m_CarBtns = m_Resource.Tow2CarNamse.Select((s, i) => new CRH2Button()
            {
                OutLineRectangle = new Rectangle(200 + 71 * i + (i > 1 && !is2C ? 50 : 0), 530, 71, 43),
                BackImage = GlobalResource.Instance.BtnDownImage,
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = s,
                TextColor = Color.White,
                ButtonDownEvent = OnCarButtonDownEvent
            }).ToList();
        }

        private void OnCarButtonDownEvent(object sender, EventArgs eventArgs)
        {
            ReselectCarBtn(sender as CRH2Button);
        }

        public void ReselectCarBtn()
        {
            ReselectCarBtn(m_CarBtns.FirstOrDefault());
        }

        public virtual void ReselectCarBtn(CRH2Button btn)
        {
            if (m_SelectedCarBtn != null)
            {
                m_SelectedCarBtn.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectedCarBtn = btn;
            Debug.Assert(m_SelectedCarBtn != null, "m_SelectedCarBtn != null");
            m_SelectedCarBtn.CurrentMouseState = MouseState.MouseDown;
        }

        private void InitTextInfo()
        {
            m_CarInfos = new List<GDIRectText>();

            for (int i = 0; i < Tow2Resource.Tow2NameInfoList.Count; i += 2)
            {
                var offset = i / 2;
                var format = new StringFormat()
                {
                    FormatFlags = StringFormatFlags.DirectionRightToLeft,
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };
                var text = new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(36 + offset * 37, 114, 22, 186),
                    BkColor = Color.WhiteSmoke,
                    TextColor = Color.Black,
                    TextFormat = new StringFormat(format),
                    Text = Tow2Resource.Tow2NameInfoList[i].Name,
                };

                var text1 = new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(36 + offset * 37, 325, 22, 186),
                    BkColor = Color.WhiteSmoke,
                    TextColor = Color.Black,
                    TextFormat = new StringFormat(format),
                    Text = Tow2Resource.Tow2NameInfoList[i + 1].Name,
                };

                m_CarInfos.Add(text);
                m_CarInfos.Add(text1);
            }

            m_CarInfos.RemoveAll(r => string.IsNullOrEmpty(r.Text));

            for (var i = 0; i < m_CarInfos.Count; i++)
            {
                var idx = i;
                m_CarInfos[i].RefreshAction = o => OnRefreshAction(o, idx);
            }
        }

        private void OnRefreshAction(object o, int idx)
        {
            var txt = (GDIRectText)o;
            txt.BkColor = m_Resource.GetStateOf(m_SelectedCarBtn.Text, m_CarBtns.IndexOf(m_SelectedCarBtn), idx)
                    ? Tow2Resource.Tow2FinalNameInfoList[idx].ActiveColor
                    : CRH2Resource.DeactiveBkColor;
        }

        public override string GetInfo()
        {
            return "牵引变流器信息2";
        }
    }
}