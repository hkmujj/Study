using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Title;

namespace CRH2MMI.Fault.View
{
    abstract class FaultInfoPageBase : CommonInnerControlBase
    {
        private bool m_HasNextPage;
        private bool m_HasPreviousPage;

        /// <summary>
        /// 此页面默认btn大小 
        /// </summary>
        public static readonly Size DefaultBtnSize = new Size(120, 38);

        public static readonly Size DefaultTextSize = new Size(130, 18);

        // ReSharper disable once InconsistentNaming
        protected Font m_DefaultTextFont = new Font(CRH2Resource.Font12.FontFamily, 10, FontStyle.Bold);

        protected FaultMgr.IViewFaultGetter m_FaultGetter;

        // ReSharper disable once InconsistentNaming
        protected List<GDIRectText> m_Infos;
        // ReSharper disable once InconsistentNaming
        protected List<CRH2Button> m_ControlBtn;

        protected bool HasNextPage
        {
            set
            {
                m_HasNextPage = value;
                m_NextPageBtn.IsEnable = value;
                m_NextPageBtn.Visible = value;
            }
            get { return m_HasNextPage; }
        }

        protected bool HasPreviousPage
        {
            set
            {
                m_HasPreviousPage = value;
                m_PreviousPageBtn.IsEnable = value;
                m_PreviousPageBtn.Visible = value;
            }
            get { return m_HasPreviousPage; }
        }

        private readonly CRH2Button m_PreviousPageBtn;

        private readonly CRH2Button m_NextPageBtn;

        // ReSharper disable once InconsistentNaming
        protected List<GDIRectText> m_ConstInfos;

        protected readonly Title.Title m_Title;
        // ReSharper disable once UnassignedReadonlyField.Compiler
        protected readonly FaultInfoView m_ParentView;


        // ReSharper disable once UnassignedReadonlyField.Compiler
        protected readonly TrainView m_TrainView;

        protected FaultInfoPageBase(Title.Title title, FaultInfoView parentView, TrainView trainView)
        {
            m_Title = title;
            m_ParentView = parentView;
            m_TrainView = trainView;
            m_FaultGetter = FaultMgr.Instance.GetterFacotry.CreateGetter(FaultGetterType.ForView) as FaultMgr.IViewFaultGetter;

            m_ConstInfos = new List<GDIRectText>();
            m_Infos = new List<GDIRectText>();
            m_ControlBtn = new List<CRH2Button>();

            var location = new Point(420, 510);
            m_PreviousPageBtn = new CRH2Button()
                                {
                                    OutLineRectangle = new Rectangle(location, DefaultBtnSize),
                                    Text = "上一页面",
                                    DownImage = GlobalResource.Instance.BtnDownImage,
                                    UpImage = GlobalResource.Instance.BtnUpImage,
                                    TextColor = Color.White,
                                    ButtonDownEvent = (sender, args) => GotoPrevioursPage(),
                                    RefreshAction = o => { ( (CRH2Button) o ).Visible = m_FaultGetter.HasPreviousFault; }
                                };

            location.Offset(DefaultBtnSize.Width + 1, 0);
            m_NextPageBtn = new CRH2Button()
                        {
                            OutLineRectangle = new Rectangle(location, DefaultBtnSize),
                            Text = "下一页面",
                            DownImage = GlobalResource.Instance.BtnDownImage,
                            UpImage = GlobalResource.Instance.BtnUpImage,
                            TextColor = Color.White,
                            ButtonDownEvent = (sender, args) => GotoNextPage(),
                            RefreshAction = o => { ((CRH2Button)o).Visible = m_FaultGetter.HasNextFault; }
                        };


        }

        public override void OnDraw(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));

            m_Infos.ForEach(e => e.OnPaint(g));

            m_ControlBtn.ForEach(e => e.OnDraw(g));

            m_PreviousPageBtn.OnPaint(g);

            m_NextPageBtn.OnPaint(g);

            foreach (var car in m_TrainView.Cars)
            {
                car.CarStateProxy.UserSetState = CarState.Normal;
            }
            //m_TrainView.ResetCarState();
            var fault = m_FaultGetter.CurrentShowFaultItemInfo;
            if (fault != null)
            {
                m_TrainView.SetCarState(
                    m_FaultGetter.CurrentShowFaultItemInfo.CarNumbers.Select(
                        s => new CarStateModel() {CarNo = s - 1, State = CarState.Fault}).ToList());
                
                foreach (var carNumber in m_FaultGetter.CurrentShowFaultItemInfo.CarNumbers)
                {
                    m_TrainView.Cars[carNumber - 1].CarStateProxy.UserSetState = CarState.Fault;
                }
            }
            //m_TrainView.paint(g);
        }

        public override bool OnMouseDown(Point point)
        {
            if (m_PreviousPageBtn.OnMouseDown(point))
            {
                return true;
            }
            if (m_NextPageBtn.OnMouseDown(point))
            {
                return true;
            }
            return m_ControlBtn.Any(a => a.OnMouseDown(point));
        }

        protected virtual void GotoNextPage()
        {
            if (m_FaultGetter.HasNextFault)
            {
                m_FaultGetter.GotoNextFault();
            }
        }

        protected virtual void GotoPrevioursPage()
        {
            if (m_FaultGetter.HasPreviousFault)
            {
                m_FaultGetter.GotoPreviousFault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnPageChageOut() { }

        public virtual void OnTitleMenuClick(object sender, TitleMenuClickArgs args)
        {
            //var iters = new List<FaultItemInfo>(m_FaultGetter.IterPath);
            //iters = iters.Distinct().ToList();

            //iters.ForEach(e => m_FaultGetter.DeleteFaultItem(e));
            //m_FaultGetter.ClearIterPath();
            //m_FaultGetter.DeleteFaultItem(m_FaultGetter.CurrentShowFaultItemInfo);
            //m_TrainView.ResetCarState();

            
            
            m_Title.TitleMenuClicking -= OnTitleMenuClick;

        }
    }
}
