using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Resource.Images;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace CRH2MMI.Marshing
{
    /// <summary>
    /// 联解编组信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Marsh : CRH2BaseClass
    {
        internal Image[] Img { private set; get; }

        internal bool[] BValue { private set; get; }
        internal float[] FValue { private set; get; }

        private List<MarshItemView> m_MarshItemViewCollection;

        private MarshItemView m_CurrentItemView;

        private bool m_HasSuccessedMarshing;

        private MarshItemView CurrentItemView
        {
            set
            {
                m_CurrentItemView = value;
                m_CurrentItemView.SwitchToThis();
            }
            get { return m_CurrentItemView; }
        }

        public override void paint(Graphics g)
        {
            if (!m_HasSuccessedMarshing && BoolList[GlobalResource.Instance.GetInBoolIndex("联解编组信息 / 联挂成功完成")])
            {
                var targetViewIndex = ViewConfig.DriveState;
                if (ProjectName == "CRH2_2")
                {
                    targetViewIndex = ViewConfig.BrakeInfo;
                }
                m_HasSuccessedMarshing = true;
                append_postCmd(CmdType.ChangePage, (int)targetViewIndex, 0, 0);
            }

            RefreshValues();

            CurrentItemView.OnDraw(g);

        }

        public void RefreshValues()
        {
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                FValue[i] = FloatList[this.UIObj.InFloatList[i]];
            }
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                BValue[i] = BoolList[this.UIObj.InBoolList[i]];
            }
        }

        public override bool Init()
        {
            DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += (sender, args) =>
            {
                if (args.CourseService.CurrentCourseState == CourseState.Stoped)
                {
                    m_HasSuccessedMarshing = false;
                }
            };

            BValue = UIObj.InBoolList.Select(s => BoolList[s]).ToArray();
            FValue = UIObj.InFloatList.Select(s => FloatList[s]).ToArray();
            MarshItemView temp;
            if (GlobalInfo.CurrentCRH2Config.Type == CRH2Type.CRH380AUnion)
            {
                temp = new CRH380AUnineMarshingTR1ItemView(this);
            }
            else
            {
                temp = new MarshingTR1ItemView(this);
            }
            m_MarshItemViewCollection = new List<MarshItemView>()
            {
                temp,
                new MarshingTR2ItemView(this),
                new DemarshItemView(this),
                new MarshInfoItemView(this),
            };


            m_MarshItemViewCollection.ForEach(e =>
            {
                e.Init();
                e.MarshButtonClick += EOnMarshButtonClick;
            });

            ResetInitView();

            Img = new Image[] {ImageResource.M1, ImageResource.M2, ImageResource.M3, ImageResource.M4, ImageResource.M5};

            return true;
        }

        private void EOnMarshButtonClick(MarshItemView marshItemView, MarshButtonType marshButtonType)
        {
            switch (marshButtonType)
            {
                case MarshButtonType.TurnBack:
                    CurrentItemView = m_MarshItemViewCollection.First(f => f.CurrentPage == MarshingPage.MarshingInfo);
                    append_postCmd(CmdType.ChangePage, 20, 0, 0);
                    break;
                case MarshButtonType.TurnNext:
                    CurrentItemView = m_MarshItemViewCollection.First(f => f.CurrentPage == MarshingPage.MarshingTR1);
                    append_postCmd(CmdType.ChangePage, 26, 0, 0);
                    break;
                case MarshButtonType.TR1:
                    CurrentItemView = m_MarshItemViewCollection.First(f => f.CurrentPage == MarshingPage.MarshingTR1);
                    append_postCmd(CmdType.ChangePage, 26, 0, 0);
                    break;
                case MarshButtonType.TR2:
                    CurrentItemView = m_MarshItemViewCollection.First(f => f.CurrentPage == MarshingPage.MarshingTR2);
                    append_postCmd(CmdType.ChangePage, 26, 0, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("marshButtonType");
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                switch (nParaB)
                {
                    case 20:
                        ResetInitView();
                        break;
                }
            }

            CurrentItemView.SwitchToThis();
        }

        public override string GetInfo()
        {
            return "联解编组信息";
        }

        protected override bool OnMouseDown(Point point)
        {
            return CurrentItemView.OnMouseDown(point);
        }


        private void ResetInitView()
        {
            CurrentItemView = m_MarshItemViewCollection.First(f => f.CurrentPage == MarshingPage.MarshingInfo);
        }
    }
}