using System.Collections.Generic;
using System.Drawing;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Title;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.TrainLineNo
{
    /// <summary>
    /// 车次设定
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class TNSET : CRH2BaseClass
    {

        public static string TrainLine { set; get; }

        private TitleTextShowStrategy m_TitleTextShowStrategy;

        /// <summary>
        /// 所有的页面
        /// </summary>
        private Dictionary<TrainLinePageType, ITranLinePage> m_PageDic;

        private ITranLinePage m_SelectedPage;

        private Title.Title m_Title;

        public override bool Init()
        {

            m_Title = Title.Title.Instance;

            TrainLine = "A B C D 1 0 5 1 1 2";

            m_TitleTextShowStrategy = new TitleTextShowStrategy();

            m_PageDic = new Dictionary<TrainLinePageType, ITranLinePage>()
            {
                {
                    TrainLinePageType.Menu, new TrainLineMenuPage(this)
                    {
                        TrainLine = TrainLine,
                        ChangePage = OnChangePage,
                        TrainLineChanged = (sender, args) =>
                        {
                            var sen = (TrainLineMenuPage) sender;
                            TrainLine = sen.TrainLine;
                        }
                    }
                },
                {
                    TrainLinePageType.SetConfirmation, new TrainLineSetConfirmationPage() {ChangePage = OnChangePage}
                },
                {
                    TrainLinePageType.ChangeConfirmation,
                    new TrainLineChangedConfirmationPage() {ChangePage = OnChangePage}
                }
            };

            foreach (var page in m_PageDic.Values)
            {
                page.Init();
            }

            m_SelectedPage = m_PageDic[TrainLinePageType.Menu];

            return true;
        }

        private void OnChangePage(object sender, TrainLineChangePageEventArgs args)
        {
            if (args.ChangeTo == TrainLinePageType.Main)
            {
                append_postCmd(CmdType.ChangePage, (int)ViewConfig.InitView, 0, 0 );
                return;
            }
            m_SelectedPage = m_PageDic[args.ChangeTo];
            m_SelectedPage.Reset();
        }


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (ParaADefine.InCurent == nParaA)
            {
                ((TrainLineMenuPage)m_PageDic[TrainLinePageType.Menu]).CurrentView = (ViewConfig)nParaB;
                m_TitleTextShowStrategy.TitleText = m_SelectedPage.TitleText;
            }
            else if (ParaADefine.SwitchFromOhter == nParaA)
            {
                m_Title.TitleMenuClicking += OnTitleMenuClick;
                m_Title.TitleShowStrategy = m_TitleTextShowStrategy;
                m_SelectedPage = m_PageDic[TrainLinePageType.Menu];
                m_SelectedPage.Reset();
                m_TitleTextShowStrategy.TitleText = m_SelectedPage.TitleText;
            }
        }

        private void OnTitleMenuClick(object sender, TitleMenuClickArgs titleMenuClickArgs)
        {
            m_Title.ResetTitleShowStrategy();

            m_Title.TitleMenuClicking -= OnTitleMenuClick;
        }

        public override string GetInfo()
        {
            return "车次设定";
        }

        protected override bool OnMouseDown(Point point)
        {
            return m_SelectedPage.OnMouseDown(point);
        }

        public override void paint(Graphics g)
        {
            m_SelectedPage.OnPaint(g);
        }
    }
}