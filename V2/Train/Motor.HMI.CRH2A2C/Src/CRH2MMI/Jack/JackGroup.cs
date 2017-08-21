using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Jack
{
    class JackGroup : CommonInnerControlBase
    {

        private List<CRH2Button> m_CarNoBtns;

        private CRH2Button m_SelectedCarNoBtn;

        private List<JackPage> m_JackPages;

        private CRH2Button m_GroupBtn;

        public event Action<JackGroup> ChangeGroup;


        /// <summary>
        ///  选中的车厢号
        /// </summary>
        public int SelectedCarNo { get { return m_CarNoBtns.IndexOf(m_SelectedCarNoBtn); } }

        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<int, string> m_GroupIDNameDictionary = new Dictionary<int, string>()
                                                                         {
                                                                             {0, string.Empty},
                                                                             {1, "后8车厢"},
                                                                             {2, "前8车厢"}
                                                                         };

        public override void OnDraw(Graphics g)
        {
            if (m_GroupBtn != null)
            {
                m_GroupBtn.OnDraw(g);
            }

            m_CarNoBtns.ForEach(e => e.OnDraw(g));

            m_JackPages[SelectedCarNo].OnPaint(g);

        }

        public override bool OnMouseDown(Point point)
        {
            return m_CarNoBtns.Any(a => a.OnMouseDown(point)) || (m_GroupBtn != null && m_GroupBtn.OnMouseDown(point)) || m_JackPages[SelectedCarNo].OnMouseDown(point);
        }

        public static List<JackGroup> CreateGroups(JackInfo jackInfo,JackConfig jackConfig)
        {

            var rlt = new List<JackGroup>();

            foreach (var gpID in jackConfig.JackOfCars.GroupBy(g => g.GroupID))
            {
                var jg = new JackGroup();
                if (m_GroupIDNameDictionary.ContainsKey(gpID.Key) && !string.IsNullOrEmpty(m_GroupIDNameDictionary[gpID.Key]))
                {
                    jg.m_GroupBtn = new CRH2Button()
                                    {
                                        OutLineRectangle = new Rectangle(666, 530, 120, 49),
                                        Text = m_GroupIDNameDictionary[gpID.Key],
                                        TextColor = Color.White,
                                        DownImage = GlobalResource.Instance.BtnDownImage,
                                        UpImage = GlobalResource.Instance.BtnUpImage,
                                        ButtonDownEvent = (sender, args) => jg.OnChangeGroup(),
                                    };
                }

                jg.m_CarNoBtns = gpID.Select(s => new CRH2Button()
                                                  {
                                                      OutLineRectangle = new Rectangle(55 + 74 * ( s.CarNo % 8 ), 530, 74, 49),
                                                      DownImage = GlobalResource.Instance.BtnDownImage,
                                                      UpImage = GlobalResource.Instance.BtnUpImage,
                                                      ButtonDownEvent = jg.OnCarNoButtonDownEvent,
                                                      Text = string.Format("{0} 车厢", s.CarNo + 1),
                                                      TextColor = Color.White
                                                  }).ToList();

                jg.m_JackPages = JackPage.CreatePages(jackInfo, GlobalInfo.CurrentCRH2Config.JackConfig, gpID);

                rlt.Add(jg);
            }

            return rlt;
        }

        private void OnCarNoButtonDownEvent(object sender, EventArgs eventArgs)
        {
            ReselectCarNo(sender as CRH2Button);
        }

        public void ReselectCarNo()
        {
            ReselectCarNo(m_CarNoBtns.FirstOrDefault());
        }


        protected virtual void OnChangeGroup()
        {
            var handler = ChangeGroup;
            if (handler != null)
            {
                handler(this);
            }
        }

        private void ReselectCarNo(CRH2Button btn)
        {
            if (m_SelectedCarNoBtn != null)
            {
                m_SelectedCarNoBtn.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectedCarNoBtn = btn;
            m_SelectedCarNoBtn.CurrentMouseState = MouseState.MouseDown;
            m_JackPages[SelectedCarNo].ReselectPage1();
        }

    }
}
