using System;
using System.Drawing;
using System.Text;
using CommonUtil.Util;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.View;
using CommonUtil.Controls;
using CRH2MMI.Common;


namespace CRH2MMI.TrainLineNo
{
    class TrainLineMenuPage : CommonInnerControlBase, ITranLinePage
    {
        private TrainLineSetView m_TrainLineSetView;

        private SetButton m_SetButton;
        private ViewConfig m_CurrentView;

        public EventHandler<TrainLineChangePageEventArgs> ChangePage;
        public EventHandler TrainLineChanged;

        private string m_OutTrainLine;
        private string m_InTrainLine;

        private CRH2BaseClass m_SrcObj;

        public TrainLineMenuPage(CRH2BaseClass srcObj)
        {
            m_SrcObj = srcObj;
        }

        public string TrainLine
        {
            set
            {
                m_OutTrainLine = value;
                m_InTrainLine = ConvertToIn(value);

            }
            get { return m_OutTrainLine; }
        }


        public ViewConfig CurrentView
        {
            get { return m_CurrentView; }
            set
            {
                m_CurrentView = value;
                if (m_CurrentView == ViewConfig.TNChange)
                {
                    m_TrainLineSetView.Title = "请转换车次";
                }
                else if (m_CurrentView == ViewConfig.TNSet)
                {
                    m_TrainLineSetView.Title = "请设定车次";
                }
                else
                {
                    //throw new Exception();
                }
            }
        }

        private string ConvertToIn(string outTrainLine)
        {
            var sb = new StringBuilder();
            var tmp = m_OutTrainLine.Split(' ');
            foreach (var s in tmp)
            {
                sb.Append(s);
            }
            return sb.ToString();
        }

        private string ConvertToOut(string inTrainLine)
        {
            var tmp = inTrainLine.ToCharArray();
            var sb = new StringBuilder();
            foreach (var c in tmp)
            {
                sb.Append(c);
                sb.Append(' ');
            }
            return sb.ToString();
        }


        public override void Init()
        {

            m_TrainLineSetView = new TrainLineSetView(m_SrcObj);
            m_TrainLineSetView.Init();
            m_TrainLineSetView.Title = "请设定车次";
            m_TrainLineSetView.TrainLine = m_InTrainLine;

            m_SetButton = new SetButton() { SetBtnClick = OnSetBtnClick };

            CurrentView = ViewConfig.TNSet;
        }


        private void OnSetBtnClick(object sender, EventArgs eventArgs)
        {
            m_TrainLineSetView.RefreshTrainLine();

            if (!m_InTrainLine.Equals(m_TrainLineSetView.TrainLine, StringComparison.Ordinal))
            {
                m_InTrainLine = m_TrainLineSetView.TrainLine;
                m_OutTrainLine = ConvertToOut(m_InTrainLine);
                HandleUtil.OnHandle(TrainLineChanged, this, null);
            }

            if (CurrentView == ViewConfig.TNChange)
            {
                HandleUtil.OnHandle(ChangePage, this, new TrainLineChangePageEventArgs(TrainLinePageType.ChangeConfirmation));
            }
            if (CurrentView == ViewConfig.TNSet)
            {
                HandleUtil.OnHandle(ChangePage, this, new TrainLineChangePageEventArgs(TrainLinePageType.SetConfirmation));
            }
        }

        public override void OnDraw(Graphics g)
        {
            m_TrainLineSetView.OnDraw(g);

            m_SetButton.OnPaint(g);
        }

        public void Reset()
        {
            TrainLine = TNSET.TrainLine;
            m_TrainLineSetView.TrainLine = m_InTrainLine;
            m_TrainLineSetView.Reset();
        }

        public string TitleText { get { return m_TrainLineSetView.Title; } }

        public override bool OnMouseDown(Point point)
        {
            return m_TrainLineSetView.OnMouseDown(point) || m_SetButton.OnMouseDown(point);
        }
    }
}
