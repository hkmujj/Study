using System;
using System.Drawing;
using CommonUtil.Controls;

namespace CRH2MMI.Marshing
{
    class MarshItemView : CommonInnerControlBase
    {
        public MarshItemView(MarshingPage currentPage, Marsh parentView)
        {
            CurrentPage = currentPage;
            m_ParentView = parentView;
        }

        protected Marsh m_ParentView;

        public MarshingPage CurrentPage { private set; get; }

        protected Image[] Img { get { return m_ParentView.Img; } }

        protected bool[] BValue{get { return m_ParentView.BValue; }}

        protected float[] FValue {get { return m_ParentView.FValue; }}

        protected Title.Title Title { private set; get; }

        public event Action<MarshItemView, MarshButtonType> MarshButtonClick;

        public override void Init()
        {
            Title = CRH2MMI.Title.Title.Instance;
        }

        protected virtual void OnMarshButtonClick(MarshItemView arg1, MarshButtonType arg2)
        {
            Action<MarshItemView, MarshButtonType> handler = MarshButtonClick;
            if (handler != null)
            {
                handler(arg1, arg2);
            }
        }

        public virtual void SwitchToThis()
        {

        }
    }
}