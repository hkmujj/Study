using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;

namespace CRH2MMI.Jack
{
    internal class JackPage : CommonInnerControlBase
    {
        /// <summary>
        /// 当前面的车厢号
        /// </summary>
        public int CarNo { private set; get; }

        // ReSharper disable once InconsistentNaming
        private PageType m_SelectedPage;

        private readonly List<JackPageModel> m_JackPageModels;

        private JackPageModel m_SelectedPageModel;

        // ReSharper disable once InconsistentNaming
        protected CRH2Button m_ChangePageBtn;

        protected JackPage()
        {
            m_SelectedPage = PageType.Page1;
            m_JackPageModels = new List<JackPageModel>();
        }



        public static List<JackPage> CreatePages(JackInfo jackInfo, JackConfig jackConfig, IEnumerable<JackOfCar> jackOfCars)
        {
            var textBoxSize = new Size(jackConfig.UnitWidth, jackConfig.UnitHeight);
            var rlt = new List<JackPage>();
            // 每个车厢
            foreach (var jackOfCar in jackOfCars)
            {
                var jp = new JackPage() { CarNo = jackOfCar.CarNo };
                rlt.Add(jp);

                var groupPage = jackOfCar.Units.GroupBy(g => g.Page);

                var gpPages = groupPage as IGrouping<int, JackUnit>[] ?? groupPage.ToArray();

                if (gpPages.Count() >= 2)
                {
                    jp.m_ChangePageBtn = new CRH2Button()
                    {
                        OutLineRectangle = new Rectangle(610, 450, 150, 45),
                        BackImage = GlobalResource.Instance.BtnUpImage,
                        DownImage = GlobalResource.Instance.BtnUpImage,
                        UpImage = GlobalResource.Instance.BtnUpImage,
                        Text = jp.GetBtnText(),
                        TextColor = Color.White,
                        ButtonDownEvent = jp.OnChangePage
                    };
                }

                // 每一页
                foreach (var gpPage in gpPages)
                {
                    var jpm = new JackPageModel(jackOfCar.CarNo) { Page = gpPage.Key };
                    jpm.Init(gpPage.ToList(), jackInfo, textBoxSize);
                    jp.m_JackPageModels.Add(jpm);
                }

            }
            return rlt;
        }

        //public static List<JackPage> CreatePages(JackInfo jackInfo, JackConfig jackConfig)
        //{
        //    var textBoxSize = new Size(jackConfig.UnitWidth, jackConfig.UnitHeight);
        //    var rlt = new List<JackPage>();
        //    // 每个车厢
        //    foreach (var jackOfCar in jackConfig.JackOfCars)
        //    {
        //        var jp = new JackPage() { CarNo = jackOfCar.CarNo };
        //        rlt.Add(jp);

        //        var groupPage = jackOfCar.Units.GroupBy(g => g.Page);

        //        var gpPages = groupPage as IGrouping<int, JackUnit>[] ?? groupPage.ToArray();

        //        if (gpPages.Count() >= 2)
        //        {
        //            jp.m_ChangePageBtn = new CRH2Button()
        //                                 {
        //                                     OutLineRectangle = new Rectangle(610, 450, 150, 45),
        //                                     BackImage = GlobalResource.Instance.BtnUpImage,
        //                                     DownImage = GlobalResource.Instance.BtnUpImage,
        //                                     UpImage = GlobalResource.Instance.BtnUpImage,
        //                                     Text = jp.GetBtnText(),
        //                                     TextColor = Color.White,
        //                                     ButtonDownEvent = jp.OnChangePage
        //                                 };
        //        }

        //        // 每一页
        //        foreach (var gpPage in gpPages)
        //        {
        //            var jpm = new JackPageModel(jackOfCar.CarNo) { Page = gpPage.Key };
        //            jpm.Init(gpPage.ToList(), jackInfo, textBoxSize);
        //            jp.m_JackPageModels.Add(jpm);
        //        }

        //    }
        //    return rlt;
        //}

        public void ReselectPage1()
        {
            m_SelectedPage = PageType.Page2;
            OnChangePage(null, null);
        }


        protected virtual void OnChangePage(object sender, EventArgs e)
        {
            m_SelectedPage = m_SelectedPage == PageType.Page1 ? PageType.Page2 : PageType.Page1;
            if (m_ChangePageBtn != null)
            {
                m_ChangePageBtn.Text = GetBtnText();
            }
            m_SelectedPageModel = m_JackPageModels[(int) m_SelectedPage];
        }


        protected enum PageType
        {
            Page1,
            Page2,
        }

        protected string GetBtnText()
        {
            switch (m_SelectedPage)
            {
                case PageType.Page1 :
                    return "下一页面";
                case PageType.Page2 :
                    return "上一页面";
                default :
                    throw new ArgumentOutOfRangeException("page");
            }
        }

        public override bool OnMouseDown(Point point)
        {
            return m_ChangePageBtn != null && m_ChangePageBtn.OnMouseDown(point);
        }

        public override void OnPaint(Graphics g)
        {
            if (m_ChangePageBtn != null)
            {
                m_ChangePageBtn.OnDraw(g);
            }
            m_SelectedPageModel.OnPaint(g);
        }

    }
}
